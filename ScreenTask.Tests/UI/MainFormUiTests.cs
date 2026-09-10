using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using ScreenTask;
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

                Assert.NotNull(form);
                Assert.Contains("ScreenTask 2.0", form.Text);
                Assert.Equal(450, form.ClientSize.Width);
                Assert.Equal(670, form.ClientSize.Height);

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
