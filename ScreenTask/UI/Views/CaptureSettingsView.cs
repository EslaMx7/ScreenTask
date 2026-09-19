using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ScreenTask.UI.Views
{
    public partial class CaptureSettingsView : UserControl
    {
        public event EventHandler? RefreshScreensClicked;
        public event EventHandler? ScreenSelectionChanged;
        public event EventHandler? CaptureMouseChanged;
        public event EventHandler? AudioToggled;
        public event EventHandler? AudioDeviceChanged;
        public event EventHandler? RefreshAudioClicked;
        public event EventHandler? TestAudioClicked;
        public event EventHandler? AudioDiagClicked;
        public event EventHandler? FpsChanged;
        public event EventHandler? QualityChanged;

        public CaptureSettingsView()
        {
            InitializeComponent();

            btnRefreshScreens.Click += (s, e) => RefreshScreensClicked?.Invoke(this, EventArgs.Empty);
            comboScreens.SelectedIndexChanged += (s, e) => ScreenSelectionChanged?.Invoke(this, EventArgs.Empty);
            cbCaptureMouse.CheckedChanged += (s, e) => CaptureMouseChanged?.Invoke(this, EventArgs.Empty);

            cbAudio.CheckedChanged += (s, e) =>
            {
                bool audioOn = cbAudio.Checked;
                comboAudioDevices.Enabled = audioOn;
                btnRefreshAudio.Enabled = audioOn;
                btnTestAudio.Enabled = audioOn;
                btnAudioDiag.Enabled = audioOn;
                AudioToggled?.Invoke(this, EventArgs.Empty);
            };

            comboAudioDevices.SelectedIndexChanged += (s, e) => AudioDeviceChanged?.Invoke(this, EventArgs.Empty);
            btnRefreshAudio.Click += (s, e) => RefreshAudioClicked?.Invoke(this, EventArgs.Empty);
            btnTestAudio.Click += (s, e) => TestAudioClicked?.Invoke(this, EventArgs.Empty);
            btnAudioDiag.Click += (s, e) => AudioDiagClicked?.Invoke(this, EventArgs.Empty);

            comboFps.SelectedIndexChanged += (s, e) => FpsChanged?.Invoke(this, EventArgs.Empty);
            qualitySlider.ValueChanged += (s, e) =>
            {
                lblQuality.Text = $"Image Quality: {qualitySlider.Value}% ({(qualitySlider.Value >= 80 ? "High Detail" : qualitySlider.Value >= 50 ? "Balanced" : "Low Bandwidth")})";
                QualityChanged?.Invoke(this, EventArgs.Empty);
            };
        }

        public void SetScreens(IEnumerable<string> items, int selectedIndex)
        {
            comboScreens.Items.Clear();
            foreach (var item in items) comboScreens.Items.Add(item);
            if (selectedIndex >= 0 && selectedIndex < comboScreens.Items.Count)
            {
                comboScreens.SelectedIndex = selectedIndex;
            }
        }

        public void SetAudioDevices(IEnumerable<string> items, int selectedIndex)
        {
            comboAudioDevices.Items.Clear();
            foreach (var item in items) comboAudioDevices.Items.Add(item);
            if (selectedIndex >= 0 && selectedIndex < comboAudioDevices.Items.Count)
            {
                comboAudioDevices.SelectedIndex = selectedIndex;
            }
        }

        public void SetAudioEnabled(bool enabled)
        {
            cbAudio.Checked = enabled;
            comboAudioDevices.Enabled = enabled;
            btnRefreshAudio.Enabled = enabled;
            btnTestAudio.Enabled = enabled;
            btnAudioDiag.Enabled = enabled;
        }

        public void SetQuality(int quality)
        {
            qualitySlider.Value = Math.Clamp(quality, qualitySlider.Minimum, qualitySlider.Maximum);
            lblQuality.Text = $"Image Quality: {qualitySlider.Value}% ({(qualitySlider.Value >= 80 ? "High Detail" : qualitySlider.Value >= 50 ? "Balanced" : "Low Bandwidth")})";
        }
    }
}
