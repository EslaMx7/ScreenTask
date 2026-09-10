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
        private readonly ConcurrentDictionary<string, (WebSocket Socket, SemaphoreSlim Lock)> _clients = new();
        private readonly ImageCodecInfo _jpegEncoder;
        private EncoderParameters _encoderParams;
        private long _frameCounter;
        private long _totalBytesTransferred;
        private int _currentFps;
        private int _fpsCounter;
        private long _lastFpsTimestamp;
        private byte[] _latestJpegSnapshot = Array.Empty<byte>();
        private readonly object _snapshotLock = new();
        private byte[]? _latestAudioFormatPacket;
        private readonly object _audioFormatLock = new();

        public int ConnectedClientsCount => _clients.Count;
        public int CurrentFps => _currentFps;
        public long TotalBytesTransferred => Interlocked.Read(ref _totalBytesTransferred);

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

        public void AddClient(string id, WebSocket socket)
        {
            var clientLock = new SemaphoreSlim(1, 1);
            _clients[id] = (socket, clientLock);

            byte[]? formatPacket;
            lock (_audioFormatLock)
            {
                formatPacket = _latestAudioFormatPacket;
            }

            if (formatPacket != null)
            {
                _ = SendToClientAsync(id, socket, clientLock, formatPacket);
            }
        }

        public async Task KickAllClientsAsync(WebSocketCloseStatus closeStatus, string reason)
        {
            var kickTasks = new List<Task>();
            foreach (var kvp in _clients)
            {
                var socket = kvp.Value.Socket;
                if (socket.State == WebSocketState.Open)
                {
                    kickTasks.Add(Task.Run(async () =>
                    {
                        try
                        {
                            await socket.CloseAsync(closeStatus, reason, CancellationToken.None);
                        }
                        catch { }
                    }));
                }
            }
            await Task.WhenAll(kickTasks);
            _clients.Clear();
        }

        public void RemoveClient(string id)
        {
            if (_clients.TryRemove(id, out var entry))
            {
                entry.Lock.Dispose();
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

            byte[] jpegBytes;
            using (var ms = new MemoryStream(64 * 1024))
            {
                bitmap.Save(ms, _jpegEncoder, _encoderParams);
                jpegBytes = ms.ToArray();
            }

            lock (_snapshotLock)
            {
                _latestJpegSnapshot = jpegBytes;
            }

            var frameIndex = Interlocked.Increment(ref _frameCounter);

            // Update FPS metrics
            Interlocked.Increment(ref _fpsCounter);
            var now = Environment.TickCount64;
            if (now - _lastFpsTimestamp >= 1000)
            {
                _currentFps = _fpsCounter;
                _fpsCounter = 0;
                _lastFpsTimestamp = now;
            }

            if (_clients.IsEmpty) return;

            // Construct binary packet: [0x01 (Video), ...jpeg bytes]
            var packet = new byte[jpegBytes.Length + 1];
            packet[0] = 0x01; // Video frame type
            Buffer.BlockCopy(jpegBytes, 0, packet, 1, jpegBytes.Length);

            Interlocked.Add(ref _totalBytesTransferred, packet.Length * _clients.Count);

            // Broadcast concurrently to all connected clients
            var sendTasks = new Task[_clients.Count];
            int i = 0;
            foreach (var kvp in _clients)
            {
                sendTasks[i++] = SendToClientAsync(kvp.Key, kvp.Value.Socket, kvp.Value.Lock, packet);
            }

            await Task.WhenAll(sendTasks);
        }

        public async Task BroadcastAudioChunkAsync(byte[] audioPcm)
        {
            if (_clients.IsEmpty || audioPcm == null || audioPcm.Length == 0) return;

            // Construct binary packet: [0x02 (Audio), ...pcm bytes]
            var packet = new byte[audioPcm.Length + 1];
            packet[0] = 0x02; // Audio chunk type
            Buffer.BlockCopy(audioPcm, 0, packet, 1, audioPcm.Length);

            Interlocked.Add(ref _totalBytesTransferred, packet.Length * _clients.Count);

            var sendTasks = new Task[_clients.Count];
            int i = 0;
            foreach (var kvp in _clients)
            {
                sendTasks[i++] = SendToClientAsync(kvp.Key, kvp.Value.Socket, kvp.Value.Lock, packet);
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

            if (_clients.IsEmpty) return;

            var sendTasks = new Task[_clients.Count];
            int i = 0;
            foreach (var kvp in _clients)
            {
                sendTasks[i++] = SendToClientAsync(kvp.Key, kvp.Value.Socket, kvp.Value.Lock, packet);
            }

            await Task.WhenAll(sendTasks);
        }

        private async Task SendToClientAsync(string clientId, WebSocket socket, SemaphoreSlim clientLock, byte[] data)
        {
            if (socket.State != WebSocketState.Open)
            {
                RemoveClient(clientId);
                return;
            }

            // Drop-frame protection: if client is already sending previous frame and locked, drop this frame for them
            if (!clientLock.Wait(0))
            {
                return;
            }

            try
            {
                await socket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Binary, true, CancellationToken.None);
            }
            catch (Exception)
            {
                RemoveClient(clientId);
            }
            finally
            {
                clientLock.Release();
            }
        }

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
            foreach (var kvp in _clients)
            {
                try
                {
                    if (kvp.Value.Socket.State == WebSocketState.Open)
                    {
                        kvp.Value.Socket.Abort();
                    }
                    kvp.Value.Lock.Dispose();
                }
                catch { }
            }
            _clients.Clear();
        }
    }
}
