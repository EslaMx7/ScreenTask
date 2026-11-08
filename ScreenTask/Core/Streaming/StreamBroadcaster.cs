using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace ScreenTask.Core.Streaming
{
    public class StreamBroadcaster : IDisposable
    {
        private readonly ConcurrentDictionary<string, (WebSocket Socket, SemaphoreSlim Lock)> _videoClients = new();
        private readonly ConcurrentDictionary<string, (WebSocket Socket, SemaphoreSlim Lock)> _audioClients = new();
        private readonly ConcurrentDictionary<string, ViewerSession> _sessions = new();
        private readonly ImageCodecInfo _jpegEncoder;
        private EncoderParameters _encoderParams;
        private long _frameCounter;
        private long _totalBytesTransferred;
        private long _totalFramesSent;
        private long _totalFramesDropped;
        private int _currentFps;
        private int _fpsCounter;
        private long _lastFpsTimestamp;
        private byte[] _latestJpegSnapshot = Array.Empty<byte>();
        private readonly object _snapshotLock = new();
        private byte[]? _latestAudioFormatPacket;
        private readonly object _audioFormatLock = new();

        public event Action<ViewerSession>? SessionConnected;
        public event Action<string>? SessionDisconnected;
        public event Action<ViewerSession>? SessionUpdated;

        public int ConnectedClientsCount => Math.Max(_videoClients.Count, _audioClients.Count);
        public int CurrentFps => _currentFps;
        public long TotalBytesTransferred => Interlocked.Read(ref _totalBytesTransferred);
        public long TotalFramesSent => Interlocked.Read(ref _totalFramesSent);
        public long TotalFramesDropped => Interlocked.Read(ref _totalFramesDropped);

        public double FrameDropPercentage
        {
            get
            {
                long sent = TotalFramesSent;
                long dropped = TotalFramesDropped;
                long total = sent + dropped;
                return total == 0 ? 0.0 : (double)dropped / total * 100.0;
            }
        }

        public IReadOnlyCollection<ViewerSession> ConnectedSessions => _sessions.Values.ToArray();

        public double AverageFleetLatencyMs
        {
            get
            {
                var sessions = _sessions.Values.ToArray();
                if (sessions.Length == 0) return 0.0;
                double sum = 0.0;
                int count = 0;
                foreach (var s in sessions)
                {
                    if (s.RttMs > 0)
                    {
                        sum += s.RttMs;
                        count++;
                    }
                }
                return count > 0 ? sum / count : 0.0;
            }
        }

        public StreamBroadcaster(int initialQuality = 75)
        {
            _jpegEncoder = GetEncoder(ImageFormat.Jpeg) ?? throw new InvalidOperationException("JPEG encoder not found");
            _encoderParams = CreateEncoderParams(initialQuality);
            _lastFpsTimestamp = Environment.TickCount64;
        }

        public void SetQuality(int quality)
        {
            _encoderParams = CreateEncoderParams(Math.Clamp(quality, 10, 100));
        }

        private static EncoderParameters CreateEncoderParams(int quality)
        {
            var p = new EncoderParameters(1);
            p.Param[0] = new EncoderParameter(Encoder.Quality, (long)quality);
            return p;
        }

        public void AddVideoClient(string id, WebSocket socket, string remoteIp = "127.0.0.1", string? userAgent = null)
        {
            var clientLock = new SemaphoreSlim(1, 1);
            _videoClients[id] = (socket, clientLock);

            var (device, icon, browser) = UserAgentParser.Parse(userAgent);
            var session = new ViewerSession
            {
                Id = id,
                RemoteIp = remoteIp,
                UserAgent = userAgent ?? "",
                DeviceName = device,
                DeviceIcon = icon,
                BrowserName = browser,
                ConnectedAt = DateTime.UtcNow
            };

            _sessions[id] = session;
            SessionConnected?.Invoke(session);
        }

        public void RemoveVideoClient(string id)
        {
            if (_videoClients.TryRemove(id, out var entry))
            {
                entry.Lock.Dispose();
            }
            if (_sessions.TryRemove(id, out _))
            {
                SessionDisconnected?.Invoke(id);
            }
        }

        public void UpdateTelemetry(string id, double rttMs, string? viewMode)
        {
            if (_sessions.TryGetValue(id, out var session))
            {
                if (rttMs > 0) session.RttMs = rttMs;
                if (!string.IsNullOrWhiteSpace(viewMode)) session.ViewMode = viewMode;
                session.LastTelemetryTime = DateTime.UtcNow;
                SessionUpdated?.Invoke(session);
            }
        }

        public void SetClientAudioMuted(string id, bool isMuted)
        {
            if (_sessions.TryGetValue(id, out var session))
            {
                session.IsAudioMuted = isMuted;
                SessionUpdated?.Invoke(session);
            }
        }

        // Backward compatibility
        public void AddClient(string id, WebSocket socket) => AddVideoClient(id, socket);
        public void RemoveClient(string id) => RemoveVideoClient(id);

        public void AddAudioClient(string id, WebSocket socket)
        {
            var clientLock = new SemaphoreSlim(1, 1);
            _audioClients[id] = (socket, clientLock);

            byte[]? formatPacket;
            lock (_audioFormatLock)
            {
                formatPacket = _latestAudioFormatPacket;
            }

            if (formatPacket != null)
            {
                _ = SendToClientAsync(id, socket, clientLock, formatPacket, isAudio: true);
            }
        }

        public void RemoveAudioClient(string id)
        {
            if (_audioClients.TryRemove(id, out var entry))
            {
                entry.Lock.Dispose();
            }
        }

        public async Task KickAllClientsAsync(WebSocketCloseStatus closeStatus, string reason)
        {
            var kickTasks = new List<Task>();

            foreach (var kvp in _videoClients)
            {
                var socket = kvp.Value.Socket;
                if (socket.State == WebSocketState.Open)
                {
                    kickTasks.Add(Task.Run(async () =>
                    {
                        try { await socket.CloseAsync(closeStatus, reason, CancellationToken.None); } catch { }
                    }));
                }
            }

            foreach (var kvp in _audioClients)
            {
                var socket = kvp.Value.Socket;
                if (socket.State == WebSocketState.Open)
                {
                    kickTasks.Add(Task.Run(async () =>
                    {
                        try { await socket.CloseAsync(closeStatus, reason, CancellationToken.None); } catch { }
                    }));
                }
            }

            await Task.WhenAll(kickTasks);
            _videoClients.Clear();
            _audioClients.Clear();
            var sessionIds = _sessions.Keys.ToArray();
            _sessions.Clear();
            foreach (var id in sessionIds)
            {
                SessionDisconnected?.Invoke(id);
            }
        }

        public async Task KickClientAsync(string id, WebSocketCloseStatus closeStatus = WebSocketCloseStatus.NormalClosure, string reason = "Kicked by host")
        {
            if (_videoClients.TryRemove(id, out var videoEntry))
            {
                try
                {
                    if (videoEntry.Socket.State == WebSocketState.Open)
                    {
                        await videoEntry.Socket.CloseAsync(closeStatus, reason, CancellationToken.None);
                    }
                }
                catch { }
                videoEntry.Lock.Dispose();
            }

            if (_audioClients.TryRemove(id, out var audioEntry))
            {
                try
                {
                    if (audioEntry.Socket.State == WebSocketState.Open)
                    {
                        await audioEntry.Socket.CloseAsync(closeStatus, reason, CancellationToken.None);
                    }
                }
                catch { }
                audioEntry.Lock.Dispose();
            }

            if (_sessions.TryRemove(id, out _))
            {
                SessionDisconnected?.Invoke(id);
            }
        }

        public async Task<int> BroadcastAnnouncementAsync(string message)
        {
            if (string.IsNullOrWhiteSpace(message) || _videoClients.IsEmpty) return 0;

            var msgBytes = System.Text.Encoding.UTF8.GetBytes(message);
            var packet = new byte[msgBytes.Length + 1];
            packet[0] = 0x04; // Broadcast announcement packet type
            Buffer.BlockCopy(msgBytes, 0, packet, 1, msgBytes.Length);

            Interlocked.Add(ref _totalBytesTransferred, packet.Length * _videoClients.Count);

            var sendTasks = new List<Task>();
            foreach (var kvp in _videoClients)
            {
                sendTasks.Add(SendToClientAsync(kvp.Key, kvp.Value.Socket, kvp.Value.Lock, packet, isAudio: false));
            }

            await Task.WhenAll(sendTasks);
            return sendTasks.Count;
        }

        public async Task SendPongAsync(string clientId, double timestamp)
        {
            if (_videoClients.TryGetValue(clientId, out var entry))
            {
                if (entry.Socket.State != WebSocketState.Open) return;
                var pongBytes = System.Text.Encoding.UTF8.GetBytes($"{{\"type\":\"pong\",\"t\":{timestamp}}}");
                if (await entry.Lock.WaitAsync(50))
                {
                    try
                    {
                        if (entry.Socket.State == WebSocketState.Open)
                        {
                            await entry.Socket.SendAsync(new ArraySegment<byte>(pongBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                    }
                    catch { }
                    finally
                    {
                        entry.Lock.Release();
                    }
                }
            }
        }

        public byte[] GetLatestSnapshot()
        {
            lock (_snapshotLock)
            {
                return _latestJpegSnapshot;
            }
        }

        public async Task BroadcastBitmapAsync(Bitmap bitmap)
        {
            if (bitmap == null) return;

            using var stream = new PooledBufferWriterStream(64 * 1024);
            // Reserve byte 0 for video packet header (0x01)
            stream.Position = 1;
            bitmap.Save(stream, _jpegEncoder, _encoderParams);

            int totalLength = (int)stream.Length;
            int jpegLength = totalLength - 1;
            if (jpegLength <= 0) return;

            // Stamp 0x01 video frame type at byte 0
            stream.Buffer[0] = 0x01;

            // Cache snapshot for REST endpoint /api/status or /preview
            var snapshot = new byte[jpegLength];
            Buffer.BlockCopy(stream.Buffer, 1, snapshot, 0, jpegLength);
            lock (_snapshotLock)
            {
                _latestJpegSnapshot = snapshot;
            }

            Interlocked.Increment(ref _frameCounter);

            // Update FPS metrics
            Interlocked.Increment(ref _fpsCounter);
            var now = Environment.TickCount64;
            if (now - _lastFpsTimestamp >= 1000)
            {
                _currentFps = _fpsCounter;
                _fpsCounter = 0;
                _lastFpsTimestamp = now;
            }

            if (_videoClients.IsEmpty) return;

            var packetSegment = new ArraySegment<byte>(stream.Buffer, 0, totalLength);
            Interlocked.Add(ref _totalBytesTransferred, (long)totalLength * _videoClients.Count);

            // Broadcast concurrently to all connected video clients
            var sendTasks = new Task[_videoClients.Count];
            int i = 0;
            foreach (var kvp in _videoClients)
            {
                sendTasks[i++] = SendToClientAsync(kvp.Key, kvp.Value.Socket, kvp.Value.Lock, packetSegment, isAudio: false);
            }

            await Task.WhenAll(sendTasks);
        }

        public async Task BroadcastAudioChunkAsync(byte[] audioPcm)
        {
            if (audioPcm == null || audioPcm.Length == 0) return;

            // Send to dedicated audio clients; if none connected yet, fallback to video clients
            var targetClients = !_audioClients.IsEmpty ? _audioClients : _videoClients;
            if (targetClients.IsEmpty) return;

            // Construct binary packet: [0x02 (Audio), ...pcm bytes]
            var packet = new byte[audioPcm.Length + 1];
            packet[0] = 0x02; // Audio chunk type
            Buffer.BlockCopy(audioPcm, 0, packet, 1, audioPcm.Length);

            Interlocked.Add(ref _totalBytesTransferred, packet.Length * targetClients.Count);

            var sendTasks = new List<Task>(targetClients.Count);
            foreach (var kvp in targetClients)
            {
                if (_sessions.TryGetValue(kvp.Key, out var session) && session.IsAudioMuted)
                {
                    continue;
                }
                sendTasks.Add(SendToClientAsync(kvp.Key, kvp.Value.Socket, kvp.Value.Lock, packet, isAudio: true));
            }

            await Task.WhenAll(sendTasks);
        }

        public async Task BroadcastAudioFormatAsync(int sampleRate, short channels, short bitsPerSample)
        {
            // Construct metadata packet: [0x03, 4 bytes sampleRate, 2 bytes channels, 2 bytes bits]
            var packet = new byte[9];
            packet[0] = 0x03; // Audio format info
            BitConverter.GetBytes(sampleRate).CopyTo(packet, 1);
            BitConverter.GetBytes(channels).CopyTo(packet, 5);
            BitConverter.GetBytes(bitsPerSample).CopyTo(packet, 7);

            lock (_audioFormatLock)
            {
                _latestAudioFormatPacket = packet;
            }

            var count = _audioClients.Count + _videoClients.Count;
            if (count == 0) return;

            var sendTasks = new List<Task>(count);
            foreach (var kvp in _audioClients)
            {
                sendTasks.Add(SendToClientAsync(kvp.Key, kvp.Value.Socket, kvp.Value.Lock, packet, isAudio: true));
            }
            foreach (var kvp in _videoClients)
            {
                sendTasks.Add(SendToClientAsync(kvp.Key, kvp.Value.Socket, kvp.Value.Lock, packet, isAudio: false));
            }

            await Task.WhenAll(sendTasks);
        }

        private async Task SendToClientAsync(string clientId, WebSocket socket, SemaphoreSlim clientLock, ArraySegment<byte> data, bool isAudio)
        {
            if (socket.State != WebSocketState.Open)
            {
                if (isAudio) RemoveAudioClient(clientId);
                else RemoveVideoClient(clientId);
                return;
            }

            // Drop-frame protection:
            // For video: if previous video frame is still sending, drop the new video frame to maintain real-time sync.
            // For audio: audio packets are small (1KB). A small 50ms wait ensures audio packets are never dropped due to transient lock contention.
            if (isAudio)
            {
                if (!await clientLock.WaitAsync(50)) return;
            }
            else
            {
                if (!clientLock.Wait(0))
                {
                    Interlocked.Increment(ref _totalFramesDropped);
                    return;
                }
                Interlocked.Increment(ref _totalFramesSent);
            }

            try
            {
                await socket.SendAsync(data, WebSocketMessageType.Binary, true, CancellationToken.None);
            }
            catch (Exception)
            {
                if (isAudio) RemoveAudioClient(clientId);
                else RemoveVideoClient(clientId);
            }
            finally
            {
                clientLock.Release();
            }
        }

        private Task SendToClientAsync(string clientId, WebSocket socket, SemaphoreSlim clientLock, byte[] data, bool isAudio)
            => SendToClientAsync(clientId, socket, clientLock, new ArraySegment<byte>(data), isAudio);

        private static ImageCodecInfo? GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageEncoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid) return codec;
            }
            return null;
        }

        public void Dispose()
        {
            foreach (var kvp in _videoClients)
            {
                try
                {
                    if (kvp.Value.Socket.State == WebSocketState.Open) kvp.Value.Socket.Abort();
                    kvp.Value.Lock.Dispose();
                }
                catch { }
            }
            _videoClients.Clear();

            foreach (var kvp in _audioClients)
            {
                try
                {
                    if (kvp.Value.Socket.State == WebSocketState.Open) kvp.Value.Socket.Abort();
                    kvp.Value.Lock.Dispose();
                }
                catch { }
            }
            _audioClients.Clear();
            _sessions.Clear();
        }
    }
}
