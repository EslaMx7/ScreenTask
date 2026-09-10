using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using ScreenTask.Core.Audio;

namespace ScreenTask.UI
{
    public class AudioDiagnosticsForm : Form
    {
        private readonly WasapiAudioCaptureService _audioService;
        private readonly Action<AudioDeviceDescriptor?> _onDeviceSelected;
        private readonly bool _wasInitiallyRunning;
        private readonly MMDevice? _initialDevice;
        private readonly bool _initialIsInput;

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
        private bool _isRecordingMicTest;
        private bool _isApplying;

        public AudioDiagnosticsForm(WasapiAudioCaptureService audioService, Action<AudioDeviceDescriptor?> onDeviceSelected)
        {
            _audioService = audioService;
            _onDeviceSelected = onDeviceSelected;
            _wasInitiallyRunning = audioService.IsRunning;
            _initialDevice = audioService.CurrentDevice;
            _initialIsInput = audioService.IsCurrentDeviceInput;

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
                _isApplying = true;
                if (comboDevices.SelectedIndex >= 0 && comboDevices.SelectedIndex < _devices.Count)
                {
                    var selected = _devices[comboDevices.SelectedIndex];
                    _onDeviceSelected(selected);

                    // If stream was not broadcasting when dialog opened, stop audio capture until broadcast starts
                    if (!_wasInitiallyRunning)
                    {
                        _audioService.Stop();
                    }
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
                int val = Math.Clamp((int)(_audioService.CurrentPeakLevel * 100), 0, 100);
                pbInputLevel.Value = val;
                lblLevelText.Text = $"Audio Input Level: {val}% {(val > 5 ? "● ACTIVE VOICE DETECTED" : "(silent)")}";
                lblLevelText.ForeColor = val > 5 ? Color.FromArgb(34, 197, 94) : Color.FromArgb(148, 163, 184);
            };
            _meterTimer.Start();

            FormClosing += (s, e) =>
            {
                _meterTimer.Stop();

                // If user closed dialog without applying changes, restore original audio state
                if (!_isApplying)
                {
                    _audioService.Stop();
                    if (_wasInitiallyRunning)
                    {
                        _audioService.Start(_initialDevice, _initialIsInput);
                    }
                }
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
                if (comboDevices.SelectedIndex < 0 || comboDevices.SelectedIndex >= _devices.Count) return;
                var desc = _devices[comboDevices.SelectedIndex];

                // If audio service is already running on this exact device, keep it running
                if (_audioService.IsRunning && _audioService.CurrentDevice == desc.Device && _audioService.IsCurrentDeviceInput == desc.IsInput)
                {
                    UpdateDiagnosticInfo();
                    return;
                }

                _audioService.Stop();
                _audioService.Start(desc.Device, desc.IsInput);
                UpdateDiagnosticInfo();
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
            var fmt = _audioService.AudioFormat;

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
                lblDiagnostics.Text = $"Device: {desc.Name}\nStatus: Active";
            }
        }

        private async void btnTestMic_Click(object? sender, EventArgs e)
        {
            if (_isRecordingMicTest) return;

            btnTestMic.Enabled = false;
            btnTestMic.BackColor = Color.FromArgb(239, 68, 68); // Red
            btnTestMic.Text = "🎙 Speak Now (3s)...";
            _isRecordingMicTest = true;

            _audioService.StartMicTest(3);

            for (int i = 3; i > 0; i--)
            {
                btnTestMic.Text = $"🎙 Speak Now ({i}s)...";
                await Task.Delay(1000);
            }

            _isRecordingMicTest = false;
            btnTestMic.BackColor = Color.FromArgb(34, 197, 94); // Green
            btnTestMic.Text = "▶ Playing Back...";

            await Task.Delay(3500);

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
