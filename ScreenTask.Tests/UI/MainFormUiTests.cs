using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using ScreenTask;
using ScreenTask.Core.Audio;
using ScreenTask.UI;
using Xunit;

namespace ScreenTask.Tests.UI
{
    public class MainFormUiTests
    {
        [Fact]
        public void MainForm_InitializesWithModernDimensionsAndCards()
        {
            RunInSta(() =>
            {
                using var form = new frmMain();
                form.Show();

                Assert.NotNull(form);
                Assert.Contains("ScreenTask 2.0", form.Text);
                Assert.Equal(680, form.ClientSize.Width);
                Assert.Equal(620, form.ClientSize.Height);

                // Verify controls exist and are populated
                var comboScreens = FindControl<ComboBox>(form, "comboScreens");
                Assert.NotNull(comboScreens);

                var comboIPs = FindControl<ComboBox>(form, "comboIPs");
                Assert.NotNull(comboIPs);

                var btnStartServer = FindControl<Button>(form, "btnStartServer");
                Assert.NotNull(btnStartServer);
                Assert.Contains("START", btnStartServer.Text);

                var txtURL = FindControl<TextBox>(form, "txtURL");
                Assert.NotNull(txtURL);

                var qualitySlider = FindControl<TrackBar>(form, "qualitySlider");
                Assert.NotNull(qualitySlider);
                Assert.Equal(80, qualitySlider.Value);

                // Verify refresh buttons exist
                var btnRefreshScreens = FindControl<Button>(form, "btnRefreshScreens");
                Assert.NotNull(btnRefreshScreens);
                Assert.Contains("🔄", btnRefreshScreens.Text);

                var btnRefreshIPs = FindControl<Button>(form, "btnRefreshIPs");
                Assert.NotNull(btnRefreshIPs);
                Assert.Contains("🔄", btnRefreshIPs.Text);

                var btnRefreshAudio = FindControl<Button>(form, "btnRefreshAudio");
                Assert.NotNull(btnRefreshAudio);
                Assert.Contains("🔄", btnRefreshAudio.Text);

                // Test clicking refresh buttons
                btnRefreshScreens.PerformClick();
                btnRefreshIPs.PerformClick();
                btnRefreshAudio.PerformClick();

                // Verify comboIPs contains Loopback after refresh
                var hasLoopback = false;
                foreach (var item in comboIPs.Items)
                {
                    if (item.ToString()?.Contains("127.0.0.1") == true || item.ToString()?.Contains("Loopback") == true)
                    {
                        hasLoopback = true;
                        break;
                    }
                }
                Assert.True(hasLoopback, "comboIPs must include loopback adapter");
            });
        }

        [Fact]
        public void MainForm_ReactiveSettings_CanBeChangedWithoutErrors()
        {
            RunInSta(() =>
            {
                using var form = new frmMain();
                form.Show();

                var comboScreens = FindControl<ComboBox>(form, "comboScreens");
                var cbCaptureMouse = FindControl<CheckBox>(form, "cbCaptureMouse");
                var qualitySlider = FindControl<TrackBar>(form, "qualitySlider");
                var comboFps = FindControl<ComboBox>(form, "comboFps");

                Assert.NotNull(comboScreens);
                Assert.NotNull(cbCaptureMouse);
                Assert.NotNull(qualitySlider);
                Assert.NotNull(comboFps);

                // Test changing settings live
                if (comboScreens.Items.Count > 0)
                {
                    comboScreens.SelectedIndex = 0;
                }

                cbCaptureMouse.Checked = !cbCaptureMouse.Checked;
                qualitySlider.Value = 65;
                if (comboFps.Items.Count > 1)
                {
                    comboFps.SelectedIndex = 1;
                }
            });
        }

        [Fact]
        public void QrCodeForm_InitializesWithUrlAndValidImage()
        {
            RunInSta(() =>
            {
                var testUrl = "http://192.168.1.50:7070";
                using var qrForm = new QrCodeForm(testUrl);

                Assert.NotNull(qrForm);
                Assert.Contains("ScreenTask 2.0", qrForm.Text);

                var pic = FindControl<PictureBox>(qrForm, null);
                Assert.NotNull(pic);
                Assert.NotNull(pic.Image);
                Assert.True(pic.Image.Width > 50);
            });
        }

        [Fact]
        public void MainForm_AudioAndSecurityControls_ToggleProperly()
        {
            RunInSta(() =>
            {
                using var form = new frmMain();

                var cbAudio = FindControl<CheckBox>(form, "cbAudio");
                var comboAudioDevices = FindControl<ComboBox>(form, "comboAudioDevices");
                var btnTestAudio = FindControl<Button>(form, "btnTestAudio");
                var btnAudioDiag = FindControl<Button>(form, "btnAudioDiag");
                var pbAudioMeter = FindControl<ProgressBar>(form, "pbAudioMeter");
                var cbPrivate = FindControl<CheckBox>(form, "cbPrivate");
                var txtPassword = FindControl<TextBox>(form, "txtPassword");

                Assert.NotNull(cbAudio);
                Assert.NotNull(comboAudioDevices);
                Assert.NotNull(btnTestAudio);
                Assert.NotNull(btnAudioDiag);
                Assert.NotNull(pbAudioMeter);
                Assert.NotNull(cbPrivate);
                Assert.NotNull(txtPassword);

                // Initially if cbAudio is unchecked, devices and test are disabled
                cbAudio.Checked = false;
                Assert.False(comboAudioDevices.Enabled);
                Assert.False(btnTestAudio.Enabled);
                Assert.False(btnAudioDiag.Enabled);
                Assert.False(pbAudioMeter.Enabled);

                // Toggle cbAudio
                cbAudio.Checked = true;
                Assert.True(comboAudioDevices.Enabled);
                Assert.True(btnTestAudio.Enabled);
                Assert.True(btnAudioDiag.Enabled);
                Assert.True(pbAudioMeter.Enabled);

                // Toggle cbPrivate
                cbPrivate.Checked = false;
                Assert.False(txtPassword.Enabled);

                cbPrivate.Checked = true;
                Assert.True(txtPassword.Enabled);
                Assert.False(string.IsNullOrEmpty(txtPassword.Text)); // Auto-generated PIN
            });
        }

        [Fact]
        public void AudioDiagnosticsForm_InitializesAndDisposesWithoutHanging()
        {
            RunInSta(() =>
            {
                using var service = new WasapiAudioCaptureService();
                using var form = new AudioDiagnosticsForm(service, _ => { });
                Assert.NotNull(form);
            });
        }

        [Fact]
        public void MainForm_ModernTabs_SwitchProperly()
        {
            RunInSta(() =>
            {
                using var form = new frmMain();
                form.Show();

                var btnNavBroadcast = FindControl<Button>(form, "btnNavBroadcast");
                var btnNavCapture = FindControl<Button>(form, "btnNavCapture");
                var btnNavNetwork = FindControl<Button>(form, "btnNavNetwork");
                var btnNavPreferences = FindControl<Button>(form, "btnNavPreferences");

                var panelBroadcast = FindControl<Panel>(form, "panelTabBroadcast");
                var panelCapture = FindControl<Panel>(form, "panelTabCapture");
                var panelNetwork = FindControl<Panel>(form, "panelTabNetwork");
                var panelPreferences = FindControl<Panel>(form, "panelTabPreferences");

                Assert.NotNull(btnNavBroadcast);
                Assert.NotNull(btnNavCapture);
                Assert.NotNull(btnNavNetwork);
                Assert.NotNull(btnNavPreferences);

                Assert.NotNull(panelBroadcast);
                Assert.NotNull(panelCapture);
                Assert.NotNull(panelNetwork);
                Assert.NotNull(panelPreferences);

                // Initially Broadcast is active
                Assert.True(panelBroadcast.Visible);
                Assert.False(panelCapture.Visible);

                // Switch to Capture
                btnNavCapture.PerformClick();
                Assert.False(panelBroadcast.Visible);
                Assert.True(panelCapture.Visible);

                // Switch to Network
                btnNavNetwork.PerformClick();
                Assert.False(panelCapture.Visible);
                Assert.True(panelNetwork.Visible);

                // Switch to Preferences
                btnNavPreferences.PerformClick();
                Assert.False(panelNetwork.Visible);
                Assert.True(panelPreferences.Visible);

                // Switch back to Broadcast
                btnNavBroadcast.PerformClick();
                Assert.True(panelBroadcast.Visible);
                Assert.False(panelPreferences.Visible);
            });
        }

        [Fact]
        public void MainForm_CompactMode_TogglesCorrectly()
        {
            RunInSta(() =>
            {
                using var form = new frmMain();
                form.Show();

                var btnMiniMode = FindControl<Button>(form, "btnMiniMode");
                var btnCompactExpand = FindControl<Button>(form, "btnCompactExpand");
                var btnCompactStop = FindControl<Button>(form, "btnCompactStop");
                var btnCompactCopy = FindControl<Button>(form, "btnCompactCopy");
                var panelCompactBar = FindControl<Panel>(form, "panelCompactBar");

                Assert.NotNull(btnMiniMode);
                Assert.NotNull(btnCompactExpand);
                Assert.NotNull(btnCompactStop);
                Assert.NotNull(btnCompactCopy);
                Assert.NotNull(panelCompactBar);

                Assert.False(panelCompactBar.Visible);
                Assert.Equal(620, form.ClientSize.Height);

                // Click Mini Mode to enter compact toolbar
                btnMiniMode.PerformClick();
                Assert.True(panelCompactBar.Visible);
                Assert.True(form.TopMost);
                Assert.Equal(68, form.ClientSize.Height);

                // Verify controls are visible and within bounds inside the compact bar
                Assert.True(btnCompactExpand.Visible);
                Assert.True(btnCompactStop.Visible);
                Assert.True(btnCompactCopy.Visible);
                Assert.True(btnCompactExpand.Right <= panelCompactBar.Width);
                Assert.True(btnCompactExpand.Bottom <= panelCompactBar.Height);

                // Click Expand to return to full window
                btnCompactExpand.PerformClick();
                Assert.False(panelCompactBar.Visible);
                Assert.False(form.TopMost);
                Assert.Equal(620, form.ClientSize.Height);
            });
        }

        [Fact]
        public void MainForm_ConfidenceMonitorAndTelemetry_Initialized()
        {
            RunInSta(() =>
            {
                using var form = new frmMain();
                form.Show();

                var monitor = FindControl<Control>(form, "monitorPreview");
                Assert.NotNull(monitor);

                var chipViewers = FindControl<Label>(form, "lblChipViewers");
                var chipFps = FindControl<Label>(form, "lblChipFps");
                var chipData = FindControl<Label>(form, "lblChipData");
                var chipUptime = FindControl<Label>(form, "lblChipUptime");

                Assert.NotNull(chipViewers);
                Assert.NotNull(chipFps);
                Assert.NotNull(chipData);
                Assert.NotNull(chipUptime);
                Assert.Contains("Connected", chipViewers.Text);
            });
        }

        [Fact]
        public void MainForm_PinGenerator_GeneratesValidPin()
        {
            RunInSta(() =>
            {
                using var form = new frmMain();
                form.Show();

                var cbPrivate = FindControl<CheckBox>(form, "cbPrivate");
                var txtPassword = FindControl<TextBox>(form, "txtPassword");
                var btnGenPin = FindControl<Button>(form, "btnGenPin");

                Assert.NotNull(cbPrivate);
                Assert.NotNull(txtPassword);
                Assert.NotNull(btnGenPin);

                cbPrivate.Checked = true;
                string initialPin = txtPassword.Text;

                btnGenPin.PerformClick();
                Assert.False(string.IsNullOrWhiteSpace(txtPassword.Text));
                Assert.True(int.TryParse(txtPassword.Text, out int pinNum));
                Assert.InRange(pinNum, 1000, 9999);
            });
        }

        private static void RunInSta(Action action)
        {
            Exception? ex = null;
            var thread = new Thread(() =>
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    ex = e;
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            if (ex != null)
            {
                throw new TargetInvocationException(ex);
            }
        }

        private static T? FindControl<T>(Control parent, string? name) where T : Control
        {
            if (name != null && parent.Name == name && parent is T matchedDirect)
            {
                return matchedDirect;
            }

            foreach (Control child in parent.Controls)
            {
                if ((name == null || child.Name == name) && child is T match)
                {
                    return match;
                }
                var found = FindControl<T>(child, name);
                if (found != null) return found;
            }
            return null;
        }
    }
}
