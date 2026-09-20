using System;
using System.Drawing;
using System.Net.WebSockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScreenTask;
using ScreenTask.Core.Audio;
using ScreenTask.UI;
using ScreenTask.UI.Controls;
using ScreenTask.UI.Views;
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
                Assert.Equal(860, form.ClientSize.Width);
                Assert.Equal(620, form.ClientSize.Height);

                var panelSideNav = FindControl<Panel>(form, "panelSideNav");
                Assert.NotNull(panelSideNav);

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
                form.Show();

                var cbAudio = FindControl<CheckBox>(form, "cbAudio");
                var comboAudioDevices = FindControl<ComboBox>(form, "comboAudioDevices");
                var btnTestAudio = FindControl<Button>(form, "btnTestAudio");
                var btnAudioDiag = FindControl<Button>(form, "btnAudioDiag");
                var pbAudioMeter = FindControl<ProgressBar>(form, "pbAudioMeter");
                var cbPrivate = FindControl<CheckBox>(form, "cbPrivate");
                var txtPassword = FindControl<TextBox>(form, "txtPassword");
                var btnQuickMute = FindControl<Button>(form, "btnQuickMute");

                Assert.NotNull(cbAudio);
                Assert.NotNull(comboAudioDevices);
                Assert.NotNull(btnTestAudio);
                Assert.NotNull(btnAudioDiag);
                Assert.NotNull(pbAudioMeter);
                Assert.NotNull(cbPrivate);
                Assert.NotNull(txtPassword);
                Assert.NotNull(btnQuickMute);

                // Initially if cbAudio is unchecked, devices and test are disabled
                cbAudio.Checked = false;
                Assert.False(comboAudioDevices.Enabled);
                Assert.False(btnTestAudio.Enabled);
                Assert.False(btnAudioDiag.Enabled);
                Assert.False(pbAudioMeter.Enabled);
                Assert.Contains("Unmute", btnQuickMute.Text);

                // Toggle cbAudio
                cbAudio.Checked = true;
                Assert.True(comboAudioDevices.Enabled);
                Assert.True(btnTestAudio.Enabled);
                Assert.True(btnAudioDiag.Enabled);
                Assert.True(pbAudioMeter.Enabled);
                Assert.Contains("Mute", btnQuickMute.Text);

                // Clicking btnQuickMute toggles audio
                btnQuickMute.PerformClick();
                Assert.False(cbAudio.Checked);
                Assert.Contains("Unmute", btnQuickMute.Text);

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
                var btnNavClients = FindControl<Button>(form, "btnNavClients");
                var btnNavCapture = FindControl<Button>(form, "btnNavCapture");
                var btnNavNetwork = FindControl<Button>(form, "btnNavNetwork");
                var btnNavPrefs = FindControl<Button>(form, "btnNavPrefs");
                var panelBroadcast = FindControl<Control>(form, "viewBroadcast");
                var panelClients = FindControl<Control>(form, "viewClients");
                var panelCapture = FindControl<Control>(form, "viewCapture");
                var panelNetwork = FindControl<Control>(form, "viewNetwork");
                var panelPreferences = FindControl<Control>(form, "viewPreferences");

                Assert.NotNull(btnNavBroadcast);
                Assert.NotNull(btnNavClients);
                Assert.NotNull(btnNavCapture);
                Assert.NotNull(btnNavNetwork);
                Assert.NotNull(btnNavPrefs);
                Assert.NotNull(panelBroadcast);
                Assert.NotNull(panelClients);
                Assert.NotNull(panelCapture);
                Assert.NotNull(panelNetwork);
                Assert.NotNull(panelPreferences);

                // Initially Broadcast is active
                Assert.True(panelBroadcast.Visible);
                Assert.False(panelClients.Visible);
                Assert.Equal(0, form.SelectedTabIndex);

                // Switch to Connected Viewers & QR
                btnNavClients.PerformClick();
                Assert.False(panelBroadcast.Visible);
                Assert.True(panelClients.Visible);
                Assert.Equal(1, form.SelectedTabIndex);

                // Switch to Capture
                btnNavCapture.PerformClick();
                Assert.False(panelClients.Visible);
                Assert.True(panelCapture.Visible);
                Assert.Equal(2, form.SelectedTabIndex);

                // Switch to Network
                btnNavNetwork.PerformClick();
                Assert.False(panelCapture.Visible);
                Assert.True(panelNetwork.Visible);
                Assert.Equal(3, form.SelectedTabIndex);

                // Switch to Preferences
                btnNavPrefs.PerformClick();
                Assert.False(panelNetwork.Visible);
                Assert.True(panelPreferences.Visible);
                Assert.Equal(4, form.SelectedTabIndex);

                // Switch back to Broadcast
                btnNavBroadcast.PerformClick();
                Assert.True(panelBroadcast.Visible);
                Assert.False(panelPreferences.Visible);
                Assert.Equal(0, form.SelectedTabIndex);
            });
        }

        [Fact]
        public void MainForm_ConnectedViewersAndPairingTab_InitializesAndInteractsProperly()
        {
            RunInSta(() =>
            {
                using var form = new frmMain();
                form.Show();

                var btnNavClients = FindControl<Button>(form, "btnNavClients");
                Assert.NotNull(btnNavClients);

                // Switch to Connected Viewers & QR tab
                btnNavClients.PerformClick();

                var panelClients = FindControl<Control>(form, "viewClients");
                Assert.NotNull(panelClients);
                Assert.True(panelClients.Visible);

                // 1. Top Metric Gauges - Initially 0 active clients
                var cardMetricViewers = FindControl<Control>(form, "cardMetricViewers");
                var cardMetricLatency = FindControl<Control>(form, "cardMetricLatency");
                var cardMetricSync = FindControl<Control>(form, "cardMetricSync");
                var pbViewerCapacity = FindControl<ProgressBar>(form, "pbViewerCapacity");
                var lblMetricViewersVal = FindControl<Label>(form, "lblMetricViewersVal");
                var panelSessionList = FindControl<Panel>(form, "panelSessionList");

                Assert.NotNull(cardMetricViewers);
                Assert.NotNull(cardMetricLatency);
                Assert.NotNull(cardMetricSync);
                Assert.NotNull(pbViewerCapacity);
                Assert.NotNull(lblMetricViewersVal);
                Assert.NotNull(panelSessionList);

                Assert.Contains("0 / 32 Active", lblMetricViewersVal.Text);
                Assert.Equal(0, pbViewerCapacity.Value);
                Assert.Equal(32, pbViewerCapacity.Maximum);

                var lblNoClients = FindControl<Label>(panelSessionList, "lblNoClientsPlaceholder");
                Assert.NotNull(lblNoClients);
                Assert.True(lblNoClients.Visible);

                // 2. Announcement Dispatcher with no clients
                var txtAnnouncement = FindControl<TextBox>(form, "txtAnnouncement");
                var btnSendAnnouncement = FindControl<Button>(form, "btnSendAnnouncement");
                var lblAnnouncementStatus = FindControl<Label>(form, "lblAnnouncementStatus");

                Assert.NotNull(txtAnnouncement);
                Assert.NotNull(btnSendAnnouncement);
                Assert.NotNull(lblAnnouncementStatus);

                txtAnnouncement.Text = "Attention viewers: Stream quality optimized!";
                btnSendAnnouncement.PerformClick();
                Assert.Contains("No viewers connected", lblAnnouncementStatus.Text);
                Assert.Equal("Attention viewers: Stream quality optimized!", txtAnnouncement.Text);

                // 3. Pairing Pass & QR Code
                var picPairingQr = FindControl<PictureBox>(form, "picPairingQr");
                var txtPairingUrl = FindControl<TextBox>(form, "txtPairingUrl");
                var btnPairingCopy = FindControl<Button>(form, "btnPairingCopy");
                var btnPairingRegen = FindControl<Button>(form, "btnPairingRegen");
                var lblPairingPin = FindControl<Label>(form, "lblPairingPin");

                Assert.NotNull(picPairingQr);
                Assert.NotNull(picPairingQr.Image);
                Assert.NotNull(txtPairingUrl);
                Assert.NotNull(btnPairingCopy);
                Assert.NotNull(btnPairingRegen);
                Assert.NotNull(lblPairingPin);
                Assert.StartsWith("http", txtPairingUrl.Text);

                // Regenerate PIN test
                btnPairingRegen.PerformClick();
                Assert.Contains("PIN", lblPairingPin.Text);

                // 4. Connect a simulated live client session
                using var dummyWs = new DummyWebSocket();
                form.Broadcaster.AddVideoClient(
                    "session-iphone-1",
                    dummyWs,
                    "192.168.1.182",
                    "Mozilla/5.0 (iPhone; CPU iPhone OS 17_4 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/17.4 Mobile/15E148 Safari/604.1"
                );
                Application.DoEvents();

                // Capacity gauge and placeholder update dynamically
                Assert.Contains("1 / 32 Active", lblMetricViewersVal.Text);
                Assert.Equal(1, pbViewerCapacity.Value);
                Assert.False(lblNoClients.Visible);

                // Dynamic card is generated
                var panelClientItem1 = FindControl<Panel>(panelSessionList, "panelClientItem1");
                Assert.NotNull(panelClientItem1);

                var lblTitle = FindControl<Label>(panelClientItem1, "lblClientTitle");
                var lblDetails = FindControl<Label>(panelClientItem1, "lblClientDetails");
                Assert.NotNull(lblTitle);
                Assert.NotNull(lblDetails);
                Assert.Contains("iPhone", lblTitle.Text);
                Assert.Contains("192.168.1.182", lblDetails.Text);
                Assert.Contains("Safari", lblDetails.Text);

                // Telemetry update reflection
                form.Broadcaster.UpdateTelemetry("session-iphone-1", 18.2, "Touch Zoom 2.0x");
                Application.DoEvents();
                var lblBadge = FindControl<Label>(panelClientItem1, "lblClientBadge");
                Assert.NotNull(lblBadge);
                Assert.Equal("Touch Zoom 2.0x", lblBadge.Text);

                // Dispatch announcement to the 1 connected client
                txtAnnouncement.Text = "Welcome iPhone viewer!";
                btnSendAnnouncement.PerformClick();
                Assert.Contains("Dispatched to 1 connected viewer", lblAnnouncementStatus.Text);
                Assert.Equal(string.Empty, txtAnnouncement.Text);

                // Audio Mute toggle
                var btnMuteClient1 = FindControl<Button>(panelClientItem1, "btnMuteClient1");
                Assert.NotNull(btnMuteClient1);
                Assert.Contains("Mute", btnMuteClient1.Text);

                btnMuteClient1.PerformClick();
                Assert.Contains("Unmute", btnMuteClient1.Text);

                btnMuteClient1.PerformClick();
                Assert.Contains("Mute", btnMuteClient1.Text);

                // Kick client
                var btnKickClient1 = FindControl<Button>(panelClientItem1, "btnKickClient1");
                Assert.NotNull(btnKickClient1);
                btnKickClient1.PerformClick();
                Application.DoEvents();

                // Session card removed and gauges return to 0
                panelClientItem1 = FindControl<Panel>(panelSessionList, "panelClientItem1");
                Assert.Null(panelClientItem1);
                Assert.Contains("0 / 32 Active", lblMetricViewersVal.Text);
                Assert.Equal(0, pbViewerCapacity.Value);
                Assert.True(lblNoClients.Visible);
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
                Assert.NotNull(btnMiniMode);
                Assert.NotNull(form.MiniBarForm);
                Assert.False(form.MiniBarForm.Visible);
                Assert.True(form.Visible);
                Assert.Equal(620, form.ClientSize.Height);

                // Click Mini Mode to enter compact toolbar form
                btnMiniMode.PerformClick();
                Assert.True(form.MiniBarForm.Visible);
                Assert.True(form.MiniBarForm.TopMost);
                Assert.Equal(68, form.MiniBarForm.ClientSize.Height);

                var btnCompactExpand = FindControl<Button>(form.MiniBarForm, "btnCompactExpand");
                var btnCompactStop = FindControl<Button>(form.MiniBarForm, "btnCompactStop");
                var btnCompactCopy = FindControl<Button>(form.MiniBarForm, "btnCompactCopy");
                Assert.NotNull(btnCompactExpand);
                Assert.NotNull(btnCompactStop);
                Assert.NotNull(btnCompactCopy);

                // Verify controls are visible and within bounds inside the compact bar
                Assert.True(btnCompactExpand.Visible);
                Assert.True(btnCompactStop.Visible);
                Assert.True(btnCompactCopy.Visible);
                Assert.True(btnCompactExpand.Right <= form.MiniBarForm.ClientSize.Width);
                Assert.True(btnCompactExpand.Bottom <= form.MiniBarForm.ClientSize.Height);

                // Click Expand to return to full window
                btnCompactExpand.PerformClick();
                Assert.False(form.MiniBarForm.Visible);
                Assert.True(form.Visible);
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

        [Fact]
        public void MainForm_StatusStrip_DisplaysStatsAndShortCopyright()
        {
            RunInSta(() =>
            {
                using var form = new frmMain();
                form.Show();

                var statusStrip = form.StatusStrip;
                Assert.NotNull(statusStrip);
                Assert.Equal(DockStyle.Bottom, statusStrip.Dock);
                Assert.Equal(7, statusStrip.Items.Count);

                Assert.NotNull(form.StatusLabelServer);
                Assert.True(form.StatusLabelServer.Spring);
                Assert.Contains("Ready to stream", form.StatusLabelServer.Text);

                Assert.NotNull(form.StatusLabelViewers);
                Assert.Contains("0 viewers", form.StatusLabelViewers.Text);

                Assert.NotNull(form.StatusLabelFps);
                Assert.Contains("0 FPS", form.StatusLabelFps.Text);

                Assert.NotNull(form.StatusLabelData);
                Assert.Contains("0.0 MB", form.StatusLabelData.Text);

                Assert.NotNull(form.StatusLabelUptime);
                Assert.Contains("00:00:00", form.StatusLabelUptime.Text);

                Assert.NotNull(form.StatusLabelAudio);
                Assert.Contains("Audio:", form.StatusLabelAudio.Text);

                Assert.NotNull(form.StatusLabelCopyright);
                Assert.Contains("© 2014-2026 Eslam Hamouda", form.StatusLabelCopyright.Text);
                Assert.True(form.StatusLabelCopyright.IsLink);
                Assert.Contains("screentask.eslamx.com", form.StatusLabelCopyright.ToolTipText);

                // Test reactive audio status change
                var cbAudio = FindControl<CheckBox>(form, "cbAudio");
                Assert.NotNull(cbAudio);

                cbAudio.Checked = true;
                Assert.Contains("Audio: On", form.StatusLabelAudio.Text);

                cbAudio.Checked = false;
                Assert.Contains("Audio: Off", form.StatusLabelAudio.Text);
            });
        }

        [Fact]
        public void MainForm_CaptureSettingsView_SourceCardPicker_DisplaysTabsAndCards()
        {
            RunInSta(() =>
            {
                using var form = new frmMain();
                form.Show();
                form.SelectTab(2);

                var captureView = form.CaptureSettingsView;
                Assert.NotNull(captureView);

                var btnTabDisplays = FindControl<Button>(form, "btnTabDisplays");
                var btnTabWindows = FindControl<Button>(form, "btnTabWindows");
                var flowSourceCards = FindControl<FlowLayoutPanel>(form, "flowSourceCards");
                var comboScreens = FindControl<ComboBox>(form, "comboScreens");

                Assert.NotNull(btnTabDisplays);
                Assert.NotNull(btnTabWindows);
                Assert.NotNull(flowSourceCards);
                Assert.NotNull(comboScreens);

                Assert.Contains("Displays", btnTabDisplays.Text);
                Assert.Contains("Windows", btnTabWindows.Text);

                // Check display cards count
                Assert.True(captureView.DisplayCards.Count > 0, "Should have at least 1 physical display card");
                var firstDisplayCard = captureView.DisplayCards[0];
                Assert.NotNull(firstDisplayCard);
                Assert.True(firstDisplayCard.IsSelected);
                Assert.True(firstDisplayCard.lblSelectedBadge.Visible);
                Assert.Contains("Selected", firstDisplayCard.lblSelectedBadge.Text);
                Assert.NotNull(firstDisplayCard.IconImage);
                Assert.False(string.IsNullOrWhiteSpace(firstDisplayCard.Title));

                // Switch to Windows tab
                btnTabWindows.PerformClick();
                Assert.Equal(CaptureSettingsView.SourceFilterTab.Windows, captureView.ActiveTab);

                // Switch back to Displays tab
                btnTabDisplays.PerformClick();
                Assert.Equal(CaptureSettingsView.SourceFilterTab.Displays, captureView.ActiveTab);

                // Test thumbnail update
                using var dummyThumb = new Bitmap(180, 100);
                firstDisplayCard.UpdateThumbnail(dummyThumb);
                Assert.NotNull(firstDisplayCard.picThumbnail.Image);
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

        private class DummyWebSocket : WebSocket
        {
            private WebSocketState _state = WebSocketState.Open;
            public override WebSocketCloseStatus? CloseStatus => WebSocketCloseStatus.NormalClosure;
            public override string? CloseStatusDescription => "Normal";
            public override WebSocketState State => _state;
            public override string? SubProtocol => null;
            public override void Abort() { _state = WebSocketState.Aborted; }
            public override Task CloseAsync(WebSocketCloseStatus closeStatus, string? statusDescription, CancellationToken cancellationToken)
            {
                _state = WebSocketState.Closed;
                return Task.CompletedTask;
            }
            public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string? statusDescription, CancellationToken cancellationToken)
            {
                _state = WebSocketState.Closed;
                return Task.CompletedTask;
            }
            public override void Dispose() { }
            public override Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
            {
                return Task.FromResult(new WebSocketReceiveResult(0, WebSocketMessageType.Close, true));
            }
            public override Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
            {
                return Task.CompletedTask;
            }
        }
    }
}
