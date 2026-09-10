using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
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
        public bool IsRunning { get; private set; }

        public StreamBroadcaster Broadcaster => _broadcaster;

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

        public void Start()
        {
            if (IsRunning) return;

            IsRunning = true;
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            if (AudioEnabled)
            {
                _audioService.Start();
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
                    using var frame = CurrentSource.CaptureFrame(CaptureMouse);
                    if (frame != null)
                    {
                        await _broadcaster.BroadcastBitmapAsync(frame);
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
            _audioService.Dispose();
            CurrentSource?.Dispose();
        }
    }
}
