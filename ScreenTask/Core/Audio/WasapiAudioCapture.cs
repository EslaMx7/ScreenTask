using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace ScreenTask.Core.Audio
{
    public record AudioDeviceDescriptor(string? Id, string Name, bool IsInput, MMDevice? Device)
    {
        public override string ToString() => Name;
    }

    public class WasapiAudioCaptureService : IDisposable
    {
        private WasapiRecorder? _recorder;
        private readonly List<byte> _testBuffer = new();
        private bool _isTestRecording;
        private WasapiPlayer? _testPlayer;

        public event Action<byte[]>? AudioDataAvailable;
        public event Action<float>? AudioLevelChanged;
        public event Action? TestRecordingFinished;

        public bool IsRunning { get; private set; }
        public MMDevice? CurrentDevice { get; private set; }
        public bool IsCurrentDeviceInput { get; private set; }
        public float CurrentPeakLevel { get; private set; }

        public WaveFormat? AudioFormat => _recorder?.WaveFormat;

        public static List<AudioDeviceDescriptor> GetAudioDevices()
        {
            var list = new List<AudioDeviceDescriptor>();

            try
            {
                using var enumerator = new MMDeviceEnumerator();

                // 1. Input Devices (Microphones)
                MMDevice? defaultMic = null;
                try
                {
                    defaultMic = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Multimedia);
                }
                catch { }

                var defaultMicName = defaultMic != null ? $"🎤 Default Microphone ({defaultMic.FriendlyName})" : "🎤 Default Microphone";
                list.Add(new(null, defaultMicName, true, defaultMic));

                try
                {
                    var captureEndpoints = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
                    foreach (var endpoint in captureEndpoints)
                    {
                        // Don't add duplicate if it has the same ID as default
                        if (defaultMic != null && endpoint.ID == defaultMic.ID) continue;
                        list.Add(new(endpoint.ID, $"🎤 {endpoint.FriendlyName}", true, endpoint));
                    }
                }
                catch { }

                // 2. Output Devices (Speakers / Loopback)
                MMDevice? defaultSpeaker = null;
                try
                {
                    defaultSpeaker = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                }
                catch { }

                var defaultSpeakerName = defaultSpeaker != null ? $"🔊 Default System Sound ({defaultSpeaker.FriendlyName})" : "🔊 Default System Sound (Loopback)";
                list.Add(new(null, defaultSpeakerName, false, defaultSpeaker));

                try
                {
                    var renderEndpoints = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
                    foreach (var endpoint in renderEndpoints)
                    {
                        if (defaultSpeaker != null && endpoint.ID == defaultSpeaker.ID) continue;
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

                var builder = new WasapiRecorderBuilder()
                    .WithSharedMode()
                    .WithEventSync()
                    .WithLowLatency(true)
                    .WithMmcssThreadPriority("Pro Audio");

                if (device != null)
                {
                    builder.WithDevice(device);
                }
                else
                {
                    using var enumerator = new MMDeviceEnumerator();
                    var defaultDev = isInput
                        ? enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Multimedia)
                        : enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                    builder.WithDevice(defaultDev);
                }

                if (!isInput)
                {
                    // Loopback capture for system / speaker audio
                    builder.WithLoopbackCapture();
                }

                _recorder = builder.Build();
                _recorder.DataAvailable += OnAudioDataAvailable;
                _recorder.RecordingStopped += (s, a) => IsRunning = false;
                _recorder.StartRecording();
                IsRunning = true;
            }
            catch (Exception)
            {
                IsRunning = false;
            }
        }

        public void Stop()
        {
            if (!IsRunning && _recorder == null) return;

            var oldRecorder = _recorder;
            _recorder = null;
            IsRunning = false;
            CurrentPeakLevel = 0;
            AudioLevelChanged?.Invoke(0);

            if (oldRecorder == null) return;

            try
            {
                oldRecorder.StopRecording();
            }
            catch { }

            // Clean up recorder in background thread with safety so UI thread is never blocked
            Task.Run(() =>
            {
                try
                {
                    var type = typeof(WasapiRecorder);
                    var feField = type.GetField("frameEvent", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    var waitHandle = feField?.GetValue(oldRecorder) as System.Threading.EventWaitHandle;
                    waitHandle?.Set();

                    oldRecorder.Dispose();
                }
                catch { }
            });
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
                if (_testBuffer.Count == 0 || _recorder?.WaveFormat == null) return;
                rawData = _testBuffer.ToArray();
            }

            try
            {
                var ms = new MemoryStream(rawData);
                var waveStream = new RawSourceWaveStream(ms, _recorder.WaveFormat);

                _testPlayer?.Dispose();
                _testPlayer = new WasapiPlayerBuilder().WithSharedMode().WithEventSync().Build();
                _testPlayer.Init(waveStream);
                _testPlayer.Play();
            }
            catch { }
        }

        private void OnAudioDataAvailable(ReadOnlySpan<byte> buffer, AudioClientBufferFlags flags, long devicePosition, long qpcPosition)
        {
            if (buffer.Length == 0) return;

            byte[] chunk = buffer.ToArray();

            if (flags.HasFlag(AudioClientBufferFlags.Silent))
            {
                CurrentPeakLevel = 0;
                AudioLevelChanged?.Invoke(0);
            }
            else
            {
                CalculatePeakLevel(buffer);
            }

            lock (_testBuffer)
            {
                if (_isTestRecording)
                {
                    _testBuffer.AddRange(chunk);
                }
            }

            AudioDataAvailable?.Invoke(chunk);
        }

        private void CalculatePeakLevel(ReadOnlySpan<byte> buffer)
        {
            if (_recorder?.WaveFormat == null || buffer.Length == 0) return;

            float peak = 0;
            var fmt = _recorder.WaveFormat;

            if (fmt.BitsPerSample == 16)
            {
                var span = MemoryMarshal.Cast<byte, short>(buffer);
                for (int i = 0; i < span.Length; i += 2)
                {
                    float abs = Math.Abs(span[i] / 32768f);
                    if (abs > peak) peak = abs;
                }
            }
            else if (fmt.BitsPerSample == 32)
            {
                var span = MemoryMarshal.Cast<byte, float>(buffer);
                for (int i = 0; i < span.Length; i += 2)
                {
                    float abs = Math.Abs(span[i]);
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
            _recorder?.Dispose();
            _recorder = null;
        }
    }
}
