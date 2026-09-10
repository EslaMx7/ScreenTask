using System;
using System.Collections.Generic;
using NAudio.CoreAudioApi;
using NAudio.Wave;

#pragma warning disable CS0618
namespace ScreenTask.Core.Audio
{
    public record AudioDeviceDescriptor(string? Id, string Name, MMDevice? Device)
    {
        public override string ToString() => Name;
    }

    public class WasapiAudioCaptureService : IDisposable
    {
        private WasapiLoopbackCapture? _capture;
        public event Action<byte[]>? AudioDataAvailable;
        public bool IsRunning { get; private set; }
        public MMDevice? CurrentDevice { get; private set; }

        public WaveFormat? AudioFormat => _capture?.WaveFormat;

        public static List<AudioDeviceDescriptor> GetRenderDevices()
        {
            var list = new List<AudioDeviceDescriptor>
            {
                new(null, "Default Playback Device", null)
            };

            try
            {
                using var enumerator = new MMDeviceEnumerator();
                var endpoints = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
                foreach (var endpoint in endpoints)
                {
                    list.Add(new AudioDeviceDescriptor(endpoint.ID, endpoint.FriendlyName, endpoint));
                }
            }
            catch { }

            return list;
        }

        public void Start(MMDevice? device = null)
        {
            if (IsRunning) return;

            try
            {
                CurrentDevice = device;
                _capture = device != null ? new WasapiLoopbackCapture(device) : new WasapiLoopbackCapture();
                _capture.DataAvailable += OnAudioDataAvailable;
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

        private void OnAudioDataAvailable(object? sender, WaveInEventArgs e)
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
