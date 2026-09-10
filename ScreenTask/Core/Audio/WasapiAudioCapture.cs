using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using NAudio.Wave;

#pragma warning disable CS0618
namespace ScreenTask.Core.Audio
{
    public record AudioDeviceDescriptor(string? Id, string Name, bool IsInput, MMDevice? Device)
    {
        public override string ToString() => Name;
    }

    public class WasapiAudioCaptureService : IDisposable
    {
        private WasapiCapture? _capture;
        private readonly List<byte> _testBuffer = new();
        private bool _isTestRecording;
        private WasapiOut? _testPlayer;

        public event Action<byte[]>? AudioDataAvailable;
        public event Action<float>? AudioLevelChanged;
        public event Action? TestRecordingFinished;

        public bool IsRunning { get; private set; }
        public MMDevice? CurrentDevice { get; private set; }
        public bool IsCurrentDeviceInput { get; private set; }
        public float CurrentPeakLevel { get; private set; }

        public WaveFormat? AudioFormat => _capture?.WaveFormat;

        public static List<AudioDeviceDescriptor> GetAudioDevices()
        {
            var list = new List<AudioDeviceDescriptor>();

            try
            {
                using var enumerator = new MMDeviceEnumerator();

                // 1. Input Devices (Microphones)
                list.Add(new(null, "🎤 Default Microphone", true, null));
                try
                {
                    var captureEndpoints = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
                    foreach (var endpoint in captureEndpoints)
                    {
                        list.Add(new(endpoint.ID, $"🎤 {endpoint.FriendlyName}", true, endpoint));
                    }
                }
                catch { }

                // 2. Output Devices (Speakers / Loopback)
                list.Add(new(null, "🔊 Default System Sound (Loopback)", false, null));
                try
                {
                    var renderEndpoints = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
                    foreach (var endpoint in renderEndpoints)
                    {
                        list.Add(new(endpoint.ID, $"🔊 {endpoint.FriendlyName}", false, endpoint));
                    }
                }
                catch { }
            }
            catch { }

            return list;
        }

        public static List<AudioDeviceDescriptor> GetRenderDevices()
        {
            return GetAudioDevices();
        }

        public void Start(MMDevice? device = null, bool isInput = false)
        {
            if (IsRunning) return;

            try
            {
                CurrentDevice = device;
                IsCurrentDeviceInput = isInput;

                if (isInput)
                {
                    // Microphone capture
                    _capture = device != null ? new WasapiCapture(device) : new WasapiCapture();
                }
                else
                {
                    // Speakers loopback capture
                    _capture = device != null ? new WasapiLoopbackCapture(device) : new WasapiLoopbackCapture();
                }

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
                CurrentPeakLevel = 0;
                AudioLevelChanged?.Invoke(0);
            }
        }

        public void StartMicTest(int durationSeconds = 3)
        {
            lock (_testBuffer)
            {
                _testBuffer.Clear();
                _isTestRecording = true;
            }

            Task.Delay(durationSeconds * 1000).ContinueWith(_ =>
            {
                lock (_testBuffer)
                {
                    _isTestRecording = false;
                }
                TestRecordingFinished?.Invoke();
                PlayTestRecording();
            });
        }

        public void PlayTestRecording()
        {
            byte[] rawData;
            lock (_testBuffer)
            {
                if (_testBuffer.Count == 0 || _capture?.WaveFormat == null) return;
                rawData = _testBuffer.ToArray();
            }

            try
            {
                var ms = new MemoryStream(rawData);
                var waveStream = new RawSourceWaveStream(ms, _capture.WaveFormat);

                _testPlayer?.Dispose();
                _testPlayer = new WasapiOut();
                _testPlayer.Init(waveStream);
                _testPlayer.Play();
            }
            catch { }
        }

        private void OnAudioDataAvailable(object? sender, WaveInEventArgs e)
        {
            if (e.BytesRecorded <= 0) return;

            var chunk = new byte[e.BytesRecorded];
            Buffer.BlockCopy(e.Buffer, 0, chunk, 0, e.BytesRecorded);

            // Calculate peak volume level
            CalculatePeakLevel(e.Buffer, e.BytesRecorded);

            // Record into test buffer if active
            lock (_testBuffer)
            {
                if (_isTestRecording)
                {
                    _testBuffer.AddRange(chunk);
                }
            }

            AudioDataAvailable?.Invoke(chunk);
        }

        private void CalculatePeakLevel(byte[] buffer, int bytesRecorded)
        {
            if (_capture?.WaveFormat == null || bytesRecorded <= 0) return;

            float peak = 0;
            var fmt = _capture.WaveFormat;

            if (fmt.BitsPerSample == 16)
            {
                int samples = bytesRecorded / 2;
                for (int i = 0; i < samples; i += 2)
                {
                    short val = BitConverter.ToInt16(buffer, i * 2);
                    float abs = Math.Abs(val / 32768f);
                    if (abs > peak) peak = abs;
                }
            }
            else if (fmt.BitsPerSample == 32)
            {
                int samples = bytesRecorded / 4;
                for (int i = 0; i < samples; i += 2)
                {
                    float val = BitConverter.ToSingle(buffer, i * 4);
                    float abs = Math.Abs(val);
                    if (abs > peak) peak = abs;
                }
            }

            CurrentPeakLevel = Math.Clamp(peak, 0f, 1f);
            AudioLevelChanged?.Invoke(CurrentPeakLevel);
        }

        public void Dispose()
        {
            Stop();
            _testPlayer?.Dispose();
            _testPlayer = null;
            _capture?.Dispose();
            _capture = null;
        }
    }
}
