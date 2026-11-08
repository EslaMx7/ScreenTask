using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using ScreenTask.Core.Audio;
using ScreenTask.Core.Streaming;

namespace ScreenTask.Core.Capture
{
    public class ScreenCaptureEngine : IDisposable
    {
        private readonly StreamBroadcaster _broadcaster;
        private readonly WasapiAudioCaptureService _audioService;
        private CancellationTokenSource? _cts;
        private Task? _captureTask;

        public ICaptureSource CurrentSource { get; private set; }
        public bool CaptureMouse { get; set; } = true;
        public int TargetFps { get; set; } = 30;
        public bool AudioEnabled { get; set; } = false;
        public AudioDeviceDescriptor? SelectedAudioDevice { get; set; }
        public bool IsRunning { get; private set; }

        public StreamBroadcaster Broadcaster => _broadcaster;
        public WasapiAudioCaptureService AudioService => _audioService;
        public event Action<Bitmap>? PreviewFrameAvailable;
        private long _lastPreviewTicks;
        private long _lastFrozenBroadcastTicks;
        private Bitmap? _lastFrameForFreeze;
        private readonly object _freezeLock = new object();
        private volatile bool _isFrozen;

        public bool IsFrozen
        {
            get => _isFrozen;
            set
            {
                _isFrozen = value;
                if (!value)
                {
                    lock (_freezeLock)
                    {
                        _lastFrameForFreeze?.Dispose();
                        _lastFrameForFreeze = null;
                    }
                }
            }
        }

        public ScreenCaptureEngine(StreamBroadcaster broadcaster)
        {
            _broadcaster = broadcaster;
            _audioService = new WasapiAudioCaptureService();
            _audioService.AudioDataAvailable += OnAudioDataAvailable;
            CurrentSource = new ScreenCaptureSource(0);
        }

        public void SetSource(ICaptureSource source)
        {
            var old = CurrentSource;
            CurrentSource = source;
            old?.Dispose();
        }

        public void SetAudioEnabled(bool enabled)
        {
            AudioEnabled = enabled;
            if (IsRunning)
            {
                if (enabled && !_audioService.IsRunning)
                {
                    _audioService.Start(SelectedAudioDevice?.Device, SelectedAudioDevice?.IsInput ?? false);
                    if (_audioService.AudioFormat != null)
                    {
                        var fmt = _audioService.AudioFormat;
                        _ = _broadcaster.BroadcastAudioFormatAsync(fmt.SampleRate, (short)fmt.Channels, (short)fmt.BitsPerSample);
                    }
                }
                else if (!enabled && _audioService.IsRunning)
                {
                    _audioService.Stop();
                }
            }
        }

        public void SetAudioDevice(AudioDeviceDescriptor? device)
        {
            SelectedAudioDevice = device;
            if (IsRunning && AudioEnabled)
            {
                _audioService.Stop();
                _audioService.Start(device?.Device, device?.IsInput ?? false);
                if (_audioService.AudioFormat != null)
                {
                    var fmt = _audioService.AudioFormat;
                    _ = _broadcaster.BroadcastAudioFormatAsync(fmt.SampleRate, (short)fmt.Channels, (short)fmt.BitsPerSample);
                }
            }
        }

        public void SetAudioDevice(MMDevice? device)
        {
            SetAudioDevice(device != null ? new AudioDeviceDescriptor(device.ID, device.FriendlyName, false, device) : null);
        }

        public void Start()
        {
            if (IsRunning) return;

            IsRunning = true;
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            if (AudioEnabled)
            {
                _audioService.Start(SelectedAudioDevice?.Device, SelectedAudioDevice?.IsInput ?? false);
                if (_audioService.AudioFormat != null)
                {
                    var fmt = _audioService.AudioFormat;
                    _ = _broadcaster.BroadcastAudioFormatAsync(fmt.SampleRate, (short)fmt.Channels, (short)fmt.BitsPerSample);
                }
            }

            _captureTask = Task.Factory.StartNew(() => CaptureLoop(token), token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public async Task StopAsync()
        {
            if (!IsRunning) return;

            IsRunning = false;
            _isFrozen = false;
            lock (_freezeLock)
            {
                _lastFrameForFreeze?.Dispose();
                _lastFrameForFreeze = null;
            }
            _cts?.Cancel();
            _audioService.Stop();

            if (_captureTask != null)
            {
                try
                {
                    await _captureTask;
                }
                catch { }
            }

            _cts?.Dispose();
            _cts = null;
        }

        private async void OnAudioDataAvailable(byte[] pcmChunk)
        {
            if (!IsRunning || !AudioEnabled) return;
            await _broadcaster.BroadcastAudioChunkAsync(pcmChunk);
        }

        private async Task CaptureLoop(CancellationToken token)
        {
            var stopwatch = new Stopwatch();

            while (!token.IsCancellationRequested && IsRunning)
            {
                stopwatch.Restart();

                try
                {
                    Bitmap? frameToBroadcast = null;
                    bool shouldDispose = false;

                    if (_isFrozen)
                    {
                        long now = Stopwatch.GetTimestamp();
                        lock (_freezeLock)
                        {
                            if (_lastFrameForFreeze != null && Stopwatch.GetElapsedTime(_lastFrozenBroadcastTicks, now).TotalMilliseconds >= 1500)
                            {
                                _lastFrozenBroadcastTicks = now;
                                frameToBroadcast = _lastFrameForFreeze;
                                shouldDispose = false;
                            }
                        }
                    }
                    else
                    {
                        var frame = CurrentSource.CaptureFrame(CaptureMouse);
                        if (frame != null)
                        {
                            frameToBroadcast = frame;
                            shouldDispose = true;

                            lock (_freezeLock)
                            {
                                _lastFrameForFreeze?.Dispose();
                                _lastFrameForFreeze = (Bitmap)frame.Clone();
                            }
                        }
                    }

                    if (frameToBroadcast != null)
                    {
                        long now = Stopwatch.GetTimestamp();
                        if (PreviewFrameAvailable != null && Stopwatch.GetElapsedTime(_lastPreviewTicks, now).TotalMilliseconds >= 160)
                        {
                            _lastPreviewTicks = now;
                            try { PreviewFrameAvailable(frameToBroadcast); } catch { }
                        }

                        await _broadcaster.BroadcastBitmapAsync(frameToBroadcast);

                        if (shouldDispose)
                        {
                            frameToBroadcast.Dispose();
                        }
                    }
                }
                catch (Exception)
                {
                    // Catch transient desktop capture errors
                }

                int targetIntervalMs = Math.Max(10, 1000 / Math.Clamp(TargetFps, 1, 60));
                int elapsed = (int)stopwatch.ElapsedMilliseconds;
                int delay = targetIntervalMs - elapsed;

                if (delay > 0)
                {
                    try
                    {
                        await Task.Delay(delay, token);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                }
            }
        }

        public void Dispose()
        {
            _ = StopAsync();
            Task.Run(() =>
            {
                try { _audioService.Dispose(); } catch { }
            });
            CurrentSource?.Dispose();
            lock (_freezeLock)
            {
                _lastFrameForFreeze?.Dispose();
                _lastFrameForFreeze = null;
            }
        }
    }
}
