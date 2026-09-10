using System;
using System.IO;
using NAudio.Wave;

#pragma warning disable CS0618
namespace ScreenTask.Core.Audio
{
    public class WasapiAudioCaptureService : IDisposable
    {
        private WasapiLoopbackCapture? _capture;
        public event Action<byte[]>? AudioDataAvailable;
        public bool IsRunning { get; private set; }

        public WaveFormat? AudioFormat => _capture?.WaveFormat;

        public void Start()
        {
            if (IsRunning) return;

            try
            {
                _capture = new WasapiLoopbackCapture();
                _capture.DataAvailable += OnDataAvailable;
                _capture.RecordingStopped += (s, a) => IsRunning = false;
                _capture.StartRecording();
                IsRunning = true;
            }
            catch (Exception)
            {
                IsRunning = false;
            }
        }

        public void Stop()
        {
            if (!IsRunning || _capture == null) return;

            try
            {
                _capture.StopRecording();
            }
            catch { }
            finally
            {
                IsRunning = false;
            }
        }

        private void OnDataAvailable(object? sender, WaveInEventArgs e)
        {
            if (e.BytesRecorded <= 0) return;

            // Send raw audio PCM chunk
            var chunk = new byte[e.BytesRecorded];
            Buffer.BlockCopy(e.Buffer, 0, chunk, 0, e.BytesRecorded);
            AudioDataAvailable?.Invoke(chunk);
        }

        public void Dispose()
        {
            Stop();
            _capture?.Dispose();
            _capture = null;
        }
    }
}
