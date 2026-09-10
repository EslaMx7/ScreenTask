using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using ScreenTask.Core.Audio;

#pragma warning disable CS0618
namespace ScreenTask.UI
{
    public class AudioDiagnosticsForm : Form
    {
        private readonly WasapiAudioCaptureService _audioService;
        private readonly Action<AudioDeviceDescriptor?> _onDeviceSelected;

        private ComboBox comboDevices = null!;
        private ProgressBar pbInputLevel = null!;
        private Label lblLevelText = null!;
        private Button btnTestMic = null!;
        private Button btnTestSpeaker = null!;
        private Label lblDiagnostics = null!;
        private Button btnApply = null!;
        private Button btnClose = null!;

        private List<AudioDeviceDescriptor> _devices = new();
        private System.Windows.Forms.Timer _meterTimer = null!;
        private WasapiCapture? _diagnosticCapture;
        private WasapiOut? _testPlayer;
        private readonly List<byte> _testMicBuffer = new();
        private bool _isRecordingMicTest;
        private float _livePeak;

        public AudioDiagnosticsForm(WasapiAudioCaptureService audioService, Action<AudioDeviceDescriptor?> onDeviceSelected)
        {
            _audioService = audioService;
            _onDeviceSelected = onDeviceSelected;

            InitializeCustomComponents();
            PopulateDevices();
            StartMonitoringSelectedDevice();
        }

        private void InitializeCustomComponents()
        {
            Text = "ScreenTask – Audio Setup & Diagnostics";
            Size = new Size(460, 540);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(15, 23, 42); // Slate 900
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9f, FontStyle.Regular);

            // Title
            var lblTitle = new Label
            {
                Text = "🎙 Audio Diagnostics (Zoom Style)",
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.FromArgb(129, 140, 248), // Indigo
                Location = new Point(20, 16),
                Size = new Size(400, 26)
            };
            Controls.Add(lblTitle);

            var lblDesc = new Label
            {
                Text = "Test your microphone and speakers to ensure viewers hear you crystal clear.",
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.FromArgb(148, 163, 184), // Slate 400
                Location = new Point(22, 44),
                Size = new Size(400, 22)
            };
            Controls.Add(lblDesc);

            // Device Picker Group
            var gbDevice = new GroupBox
            {
                Text = "Active Audio Source",
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(129, 140, 248),
                Location = new Point(20, 72),
                Size = new Size(405, 80)
            };

            var lblPick = new Label
            {
                Text = "Device:",
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.White,
                Location = new Point(12, 28),
                Size = new Size(50, 20)
            };
            gbDevice.Controls.Add(lblPick);

            comboDevices = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 8.5f),
                Location = new Point(68, 25),
                Size = new Size(322, 23)
            };
            comboDevices.SelectedIndexChanged += (s, e) =>
            {
                StartMonitoringSelectedDevice();
                UpdateDiagnosticInfo();
            };
            gbDevice.Controls.Add(comboDevices);
            Controls.Add(gbDevice);

            // Live Input Meter Group
            var gbMic = new GroupBox
            {
                Text = "Microphone & Sound Level",
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(129, 140, 248),
                Location = new Point(20, 162),
                Size = new Size(405, 120)
            };

            var lblMeter = new Label
            {
                Text = "Input Level:",
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.White,
                Location = new Point(12, 28),
                Size = new Size(75, 20)
            };
            gbMic.Controls.Add(lblMeter);

            pbInputLevel = new ProgressBar
            {
                Location = new Point(90, 26),
                Size = new Size(300, 18),
                Maximum = 100,
                Minimum = 0,
                Value = 0
            };
            gbMic.Controls.Add(pbInputLevel);

            lblLevelText = new Label
            {
                Text = "Speak into your mic: 0%",
                Font = new Font("Segoe UI", 8f),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(90, 48),
                Size = new Size(300, 18)
            };
            gbMic.Controls.Add(lblLevelText);

            btnTestMic = new Button
            {
                Text = "🎤 Test Mic (3s Loopback)",
                Location = new Point(90, 72),
                Size = new Size(180, 32),
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.FromArgb(129, 140, 248),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold)
            };
            btnTestMic.FlatAppearance.BorderColor = Color.FromArgb(71, 85, 105);
            btnTestMic.Click += btnTestMic_Click;
            gbMic.Controls.Add(btnTestMic);

            btnTestSpeaker = new Button
            {
                Text = "🔊 Test Sound",
                Location = new Point(280, 72),
                Size = new Size(110, 32),
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.5f)
            };
            btnTestSpeaker.FlatAppearance.BorderColor = Color.FromArgb(71, 85, 105);
            btnTestSpeaker.Click += btnTestSpeaker_Click;
            gbMic.Controls.Add(btnTestSpeaker);

            Controls.Add(gbMic);

            // Diagnostic Box
            var gbDiag = new GroupBox
            {
                Text = "Format & Stream Diagnostics",
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(129, 140, 248),
                Location = new Point(20, 292),
                Size = new Size(405, 130)
            };

            lblDiagnostics = new Label
            {
                Text = "Detecting audio device parameters...",
                Font = new Font("Consolas", 8.5f),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(15, 25),
                Size = new Size(375, 95)
            };
            gbDiag.Controls.Add(lblDiagnostics);
            Controls.Add(gbDiag);

            // Bottom Buttons
            btnApply = new Button
            {
                Text = "Select This Device",
                DialogResult = DialogResult.OK,
                Location = new Point(175, 440),
                Size = new Size(150, 38),
                BackColor = Color.FromArgb(79, 70, 229), // Indigo
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };
            btnApply.FlatAppearance.BorderSize = 0;
            btnApply.Click += (s, e) =>
            {
                if (comboDevices.SelectedIndex >= 0 && comboDevices.SelectedIndex < _devices.Count)
                {
                    _onDeviceSelected(_devices[comboDevices.SelectedIndex]);
                }
                Close();
            };
            Controls.Add(btnApply);

            btnClose = new Button
            {
                Text = "Close",
                DialogResult = DialogResult.Cancel,
                Location = new Point(335, 440),
                Size = new Size(90, 38),
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f)
            };
            btnClose.FlatAppearance.BorderColor = Color.FromArgb(71, 85, 105);
            btnClose.Click += (s, e) => Close();
            Controls.Add(btnClose);

            // Meter refresh timer
            _meterTimer = new System.Windows.Forms.Timer { Interval = 50 };
            _meterTimer.Tick += (s, e) =>
            {
                int val = Math.Clamp((int)(_livePeak * 100), 0, 100);
                pbInputLevel.Value = val;
                lblLevelText.Text = $"Audio Input Level: {val}% {(val > 5 ? "● ACTIVE VOICE DETECTED" : "(silent)")}";
                lblLevelText.ForeColor = val > 5 ? Color.FromArgb(34, 197, 94) : Color.FromArgb(148, 163, 184);
            };
            _meterTimer.Start();

            FormClosing += (s, e) =>
            {
                _meterTimer.Stop();
                _diagnosticCapture?.StopRecording();
                _diagnosticCapture?.Dispose();
                _diagnosticCapture = null;
                _testPlayer?.Dispose();
                _testPlayer = null;
            };
        }

        private void PopulateDevices()
        {
            comboDevices.Items.Clear();
            _devices = WasapiAudioCaptureService.GetAudioDevices();

            int selectedIndex = 0;
            for (int i = 0; i < _devices.Count; i++)
            {
                comboDevices.Items.Add(_devices[i].Name);
                if (_audioService.CurrentDevice != null && _devices[i].Id == _audioService.CurrentDevice.ID)
                {
                    selectedIndex = i;
                }
            }

            if (comboDevices.Items.Count > 0)
            {
                comboDevices.SelectedIndex = selectedIndex;
            }
        }

        private void StartMonitoringSelectedDevice()
        {
            try
            {
                _diagnosticCapture?.StopRecording();
                _diagnosticCapture?.Dispose();
                _diagnosticCapture = null;

                if (comboDevices.SelectedIndex < 0 || comboDevices.SelectedIndex >= _devices.Count) return;
                var desc = _devices[comboDevices.SelectedIndex];

                if (desc.IsInput)
                {
                    _diagnosticCapture = desc.Device != null ? new WasapiCapture(desc.Device) : new WasapiCapture();
                }
                else
                {
                    _diagnosticCapture = desc.Device != null ? new WasapiLoopbackCapture(desc.Device) : new WasapiLoopbackCapture();
                }

                _diagnosticCapture.DataAvailable += (s, e) =>
                {
                    if (e.BytesRecorded <= 0) return;

                    // Calculate peak level
                    float peak = 0;
                    var fmt = _diagnosticCapture.WaveFormat;
                    if (fmt.BitsPerSample == 16)
                    {
                        int samples = e.BytesRecorded / 2;
                        for (int i = 0; i < samples; i += 2)
                        {
                            short val = BitConverter.ToInt16(e.Buffer, i * 2);
                            float abs = Math.Abs(val / 32768f);
                            if (abs > peak) peak = abs;
                        }
                    }
                    else if (fmt.BitsPerSample == 32)
                    {
                        int samples = e.BytesRecorded / 4;
                        for (int i = 0; i < samples; i += 2)
                        {
                            float val = BitConverter.ToSingle(e.Buffer, i * 4);
                            float abs = Math.Abs(val);
                            if (abs > peak) peak = abs;
                        }
                    }
                    _livePeak = peak;

                    // If testing mic, record chunk
                    lock (_testMicBuffer)
                    {
                        if (_isRecordingMicTest)
                        {
                            var copy = new byte[e.BytesRecorded];
                            Buffer.BlockCopy(e.Buffer, 0, copy, 0, e.BytesRecorded);
                            _testMicBuffer.AddRange(copy);
                        }
                    }
                };

                _diagnosticCapture.StartRecording();
            }
            catch (Exception ex)
            {
                lblDiagnostics.Text = $"Error monitoring device: {ex.Message}";
            }
        }

        private void UpdateDiagnosticInfo()
        {
            if (comboDevices.SelectedIndex < 0 || comboDevices.SelectedIndex >= _devices.Count) return;
            var desc = _devices[comboDevices.SelectedIndex];
            var fmt = _diagnosticCapture?.WaveFormat;

            if (fmt != null)
            {
                lblDiagnostics.Text =
                    $"Device Type: {(desc.IsInput ? "Microphone (Input Voice)" : "Speakers / Loopback (Output Sound)")}\n" +
                    $"Sample Rate: {fmt.SampleRate:N0} Hz\n" +
                    $"Channels:    {fmt.Channels} ({(fmt.Channels == 1 ? "Mono" : "Stereo")})\n" +
                    $"Bit Depth:   {fmt.BitsPerSample}-bit ({(fmt.Encoding == WaveFormatEncoding.IeeeFloat ? "IEEE Float" : "PCM Integer")})\n" +
                    $"Status:      Active & Ready for Web Streaming";
            }
            else
            {
                lblDiagnostics.Text = $"Device: {desc.Name}\nStatus: Initializing...";
            }
        }

        private async void btnTestMic_Click(object? sender, EventArgs e)
        {
            if (_isRecordingMicTest) return;

            btnTestMic.Enabled = false;
            btnTestMic.BackColor = Color.FromArgb(239, 68, 68); // Red
            btnTestMic.Text = "🎙 Speak Now (3s)...";

            lock (_testMicBuffer)
            {
                _testMicBuffer.Clear();
                _isRecordingMicTest = true;
            }

            // Record 3 seconds
            for (int i = 3; i > 0; i--)
            {
                btnTestMic.Text = $"🎙 Speak Now ({i}s)...";
                await Task.Delay(1000);
            }

            lock (_testMicBuffer)
            {
                _isRecordingMicTest = false;
            }

            btnTestMic.BackColor = Color.FromArgb(34, 197, 94); // Green
            btnTestMic.Text = "▶ Playing Back...";

            // Playback
            byte[] rawData;
            lock (_testMicBuffer)
            {
                rawData = _testMicBuffer.ToArray();
            }

            if (rawData.Length > 0 && _diagnosticCapture?.WaveFormat != null)
            {
                try
                {
                    var ms = new MemoryStream(rawData);
                    var stream = new RawSourceWaveStream(ms, _diagnosticCapture.WaveFormat);
                    _testPlayer?.Dispose();
                    _testPlayer = new WasapiOut();
                    _testPlayer.Init(stream);
                    _testPlayer.Play();

                    // Wait playback duration
                    var duration = (int)(stream.TotalTime.TotalMilliseconds) + 200;
                    await Task.Delay(Math.Clamp(duration, 1000, 5000));
                }
                catch { }
            }

            btnTestMic.BackColor = Color.FromArgb(30, 41, 59);
            btnTestMic.ForeColor = Color.FromArgb(129, 140, 248);
            btnTestMic.Text = "🎤 Test Mic Again";
            btnTestMic.Enabled = true;
        }

        private void btnTestSpeaker_Click(object? sender, EventArgs e)
        {
            try
            {
                System.Media.SystemSounds.Asterisk.Play();
            }
            catch { }
        }
    }
}
