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
                Assert.Equal(1000, form.ClientSize.Width);
                Assert.Equal(700, form.ClientSize.Height);
                Assert.Equal(880, form.MinimumSize.Width);
                Assert.Equal(640, form.MinimumSize.Height);

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
        public void MainForm_ConsoleNavigation_TracksSelectedTabIndex()
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

                Assert.NotNull(btnNavBroadcast);
                Assert.NotNull(btnNavClients);
                Assert.NotNull(btnNavCapture);
                Assert.NotNull(btnNavNetwork);
                Assert.NotNull(btnNavPrefs);

                // Initially Broadcast is active
                Assert.Equal(0, form.SelectedTabIndex);

                // Switch tabs
                btnNavClients.PerformClick();
                Assert.Equal(1, form.SelectedTabIndex);

                btnNavCapture.PerformClick();
                Assert.Equal(2, form.SelectedTabIndex);

                btnNavNetwork.PerformClick();
                Assert.Equal(3, form.SelectedTabIndex);

                btnNavPrefs.PerformClick();
                Assert.Equal(4, form.SelectedTabIndex);

                btnNavBroadcast.PerformClick();
                Assert.Equal(0, form.SelectedTabIndex);
            });
        }

        [Fact]
        public void ConnectedViewersForm_InitializesAndInteractsProperly()
        {
            RunInSta(() =>
            {
                using var form = new frmMain();
                form.Show();

                using var viewersForm = new ConnectedViewersForm();
                viewersForm.Show();

                // 1. Top Metric Gauges - Initially 0 active clients
                var cardMetricViewers = viewersForm.cardMetricViewers;
                var cardMetricLatency = viewersForm.cardMetricLatency;
                var cardMetricSync = viewersForm.cardMetricSync;
                var pbViewerCapacity = viewersForm.pbViewerCapacity;
                var lblMetricViewersVal = viewersForm.lblMetricViewersVal;
                var panelSessionList = viewersForm.panelSessionList;

                Assert.NotNull(cardMetricViewers);
                Assert.NotNull(cardMetricLatency);
                Assert.NotNull(cardMetricSync);
                Assert.NotNull(pbViewerCapacity);
                Assert.NotNull(lblMetricViewersVal);
                Assert.NotNull(panelSessionList);

                viewersForm.UpdateMetricViewers(0, 32);
                Assert.Contains("0 / 32 Active", lblMetricViewersVal.Text);
                Assert.Equal(0, pbViewerCapacity.Value);
                Assert.Equal(32, pbViewerCapacity.Maximum);

                var lblNoClients = viewersForm.lblNoClientsPlaceholder;
                Assert.NotNull(lblNoClients);
                Assert.True(lblNoClients.Visible);

                // 2. Announcement Dispatcher with no clients
                var txtAnnouncement = viewersForm.txtAnnouncement;
                var btnSendAnnouncement = viewersForm.btnSendAnnouncement;
                var lblAnnouncementStatus = viewersForm.lblAnnouncementStatus;

                Assert.NotNull(txtAnnouncement);
                Assert.NotNull(btnSendAnnouncement);
                Assert.NotNull(lblAnnouncementStatus);

                txtAnnouncement.Text = "Attention viewers: Stream quality optimized!";
                viewersForm.SetAnnouncementStatus("⚠️ No viewers connected to receive announcement.", clearInput: false);
                Assert.Contains("No viewers connected", lblAnnouncementStatus.Text);
                Assert.Equal("Attention viewers: Stream quality optimized!", txtAnnouncement.Text);

                // 3. Pairing Pass & QR Code
                var picPairingQr = viewersForm.picPairingQr;
                var txtPairingUrl = viewersForm.txtPairingUrl;
                var btnPairingCopy = viewersForm.btnPairingCopy;
                var btnPairingRegen = viewersForm.btnPairingRegen;
                var lblPairingPin = viewersForm.lblPairingPin;

                using var qrBmp = ScreenTask.Core.Network.QrCodeHelper.GenerateQrCodeBitmap("http://127.0.0.1:7070", 4);
                viewersForm.UpdatePairingPass(qrBmp, "http://127.0.0.1:7070", "PIN Code: 1234", "Adapter: Wi-Fi");

                Assert.NotNull(picPairingQr);
                Assert.NotNull(picPairingQr.Image);
                Assert.NotNull(txtPairingUrl);
                Assert.NotNull(btnPairingCopy);
                Assert.NotNull(btnPairingRegen);
                Assert.NotNull(lblPairingPin);
                Assert.StartsWith("http", txtPairingUrl.Text);

                // 4. Connect a simulated live client session
                using var dummyWs = new DummyWebSocket();
                form.Broadcaster.AddVideoClient(
                    "session-iphone-1",
                    dummyWs,
                    "192.168.1.182",
                    "Mozilla/5.0 (iPhone; CPU iPhone OS 17_4 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/17.4 Mobile/15E148 Safari/604.1"
                );
                var sessions = form.Broadcaster.ConnectedSessions;
                viewersForm.UpdateMetricViewers(sessions.Count, 32);
                viewersForm.UpdateSessionCards(
                    sessions,
                    (id, muted) => form.Broadcaster.SetClientAudioMuted(id, muted),
                    async (id) => await form.Broadcaster.KickClientAsync(id)
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
                var updatedSession = form.Broadcaster.ConnectedSessions.First();
                viewersForm.RefreshSessionCardData(updatedSession);
                Application.DoEvents();
                var lblBadge = FindControl<Label>(panelClientItem1, "lblClientBadge");
                Assert.NotNull(lblBadge);
                Assert.Equal("Touch Zoom 2.0x", lblBadge.Text);

                // Dispatch announcement to the 1 connected client
                txtAnnouncement.Text = "Welcome iPhone viewer!";
                viewersForm.SetAnnouncementStatus("✓ Dispatched to 1 connected viewer(s) at 12:00:00", clearInput: true);
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

                viewersForm.UpdateMetricViewers(form.Broadcaster.ConnectedSessions.Count, 32);
                viewersForm.UpdateSessionCards(
                    form.Broadcaster.ConnectedSessions,
                    (id, muted) => form.Broadcaster.SetClientAudioMuted(id, muted),
                    async (id) => await form.Broadcaster.KickClientAsync(id)
                );
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
                Assert.Equal(700, form.ClientSize.Height);

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
                Assert.Equal(700, form.ClientSize.Height);
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
        public void SourceSelectionForm_SourceCardPicker_DisplaysTabsAndCards()
        {
            RunInSta(() =>
            {
                var screens = Screen.AllScreens;
                var sources = new System.Collections.Generic.List<ScreenTask.Core.Capture.ICaptureSource>();
                for (int i = 0; i < screens.Length; i++)
                {
                    sources.Add(new ScreenTask.Core.Capture.ScreenCaptureSource(i));
                }
                using var sourceForm = new SourceSelectionForm(sources, sources.FirstOrDefault());
                sourceForm.Show();

                var tabControl = sourceForm.tabControlSources;
                var flowDisplays = sourceForm.flowDisplays;
                var flowWindows = sourceForm.flowWindows;
                var lblSelectedSource = sourceForm.lblSelectedSource;

                Assert.NotNull(tabControl);
                Assert.NotNull(flowDisplays);
                Assert.NotNull(flowWindows);
                Assert.NotNull(lblSelectedSource);
                Assert.True(lblSelectedSource.Visible);

                // Check display cards count
                Assert.True(sourceForm.DisplayCards.Count > 0, "Should have at least 1 physical display card");
                var firstDisplayCard = sourceForm.DisplayCards[0];
                Assert.NotNull(firstDisplayCard);
                Assert.True(firstDisplayCard.IsSelected);
                Assert.True(firstDisplayCard.lblSelectedBadge.Visible);
                Assert.Contains("Selected", firstDisplayCard.lblSelectedBadge.Text);
                Assert.NotNull(firstDisplayCard.IconImage);
                Assert.False(string.IsNullOrWhiteSpace(firstDisplayCard.Title));

                // Persistent selected source label check
                Assert.Contains("Selected:", lblSelectedSource.Text);
                Assert.Contains("Display 1", lblSelectedSource.Text);

                // If window cards exist, verify selecting updates selection
                if (sourceForm.WindowCards.Count > 0)
                {
                    var firstWindowCard = sourceForm.WindowCards[0];
                    firstWindowCard.PerformClick();
                    Assert.True(firstWindowCard.IsSelected);
                    Assert.False(firstDisplayCard.IsSelected);
                    Assert.Contains("Selected:", lblSelectedSource.Text);
                    Assert.Contains(firstWindowCard.Title, lblSelectedSource.Text);

                    // Selecting display card updates selection back
                    firstDisplayCard.PerformClick();
                    Assert.True(firstDisplayCard.IsSelected);
                    Assert.False(firstWindowCard.IsSelected);
                    Assert.Contains("Display 1", lblSelectedSource.Text);
                }

                // Test thumbnail update
                using var dummyThumb = new Bitmap(180, 100);
                firstDisplayCard.UpdateThumbnail(dummyThumb);
                Assert.NotNull(firstDisplayCard.picThumbnail.Image);
            });
        }

        [Fact]
        public void MainForm_MenuStrip_InitializesWithCorrectStructure()
        {
            RunInSta(() =>
            {
                using var form = new frmMain();
                form.Show();

                // MenuStrip exists and is the MainMenuStrip
                var menuStrip = FindControl<MenuStrip>(form, "menuStripMain");
                Assert.NotNull(menuStrip);
                Assert.Equal(DockStyle.Top, menuStrip.Dock);
                Assert.Same(menuStrip, form.MainMenuStrip);

                // ── 5 top-level menus ──────────────────────────────────────
                Assert.Equal(5, menuStrip.Items.Count);
                var fileMenu = menuStrip.Items["menuFile"] as ToolStripMenuItem;
                var viewMenu = menuStrip.Items["menuView"] as ToolStripMenuItem;
                var captureMenu = menuStrip.Items["menuCapture"] as ToolStripMenuItem;
                var networkMenu = menuStrip.Items["menuNetwork"] as ToolStripMenuItem;
                var helpMenu = menuStrip.Items["menuHelp"] as ToolStripMenuItem;

                Assert.NotNull(fileMenu);
                Assert.NotNull(viewMenu);
                Assert.NotNull(captureMenu);
                Assert.NotNull(networkMenu);
                Assert.NotNull(helpMenu);

                Assert.Contains("File", fileMenu!.Text);
                Assert.Contains("View", viewMenu!.Text);
                Assert.Contains("Capture", captureMenu!.Text);
                Assert.Contains("Network", networkMenu!.Text);
                Assert.Contains("Help", helpMenu!.Text);

                // ── File menu items ────────────────────────────────────────
                var toggleBroadcast = fileMenu.DropDownItems["menuFileToggleBroadcast"] as ToolStripMenuItem;
                var copyLink = fileMenu.DropDownItems["menuFileCopyLink"] as ToolStripMenuItem;
                var showQr = fileMenu.DropDownItems["menuFileShowQr"] as ToolStripMenuItem;
                var openBrowser = fileMenu.DropDownItems["menuFileOpenBrowser"] as ToolStripMenuItem;
                var miniBar = fileMenu.DropDownItems["menuFileMiniBar"] as ToolStripMenuItem;
                var exitItem = fileMenu.DropDownItems["menuFileExit"] as ToolStripMenuItem;

                Assert.NotNull(toggleBroadcast);
                Assert.NotNull(copyLink);
                Assert.NotNull(showQr);
                Assert.NotNull(openBrowser);
                Assert.NotNull(miniBar);
                Assert.NotNull(exitItem);

                // Default state: not broadcasting → Start text
                Assert.Contains("Start Broadcast", toggleBroadcast!.Text);

                // Ctrl+B shortcut on toggle broadcast
                Assert.Equal(Keys.Control | Keys.B, toggleBroadcast.ShortcutKeys);
                // Ctrl+Q for QR code
                Assert.Equal(Keys.Control | Keys.Q, showQr!.ShortcutKeys);
                // Ctrl+M for mini bar
                Assert.Equal(Keys.Control | Keys.M, miniBar!.ShortcutKeys);

                // ── View menu – 5 navigation items ────────────────────────
                Assert.Equal(5, viewMenu!.DropDownItems.Count);
                var viewBroadcast = viewMenu.DropDownItems["menuViewBroadcast"] as ToolStripMenuItem;
                var viewViewers = viewMenu.DropDownItems["menuViewViewers"] as ToolStripMenuItem;
                var viewCapture = viewMenu.DropDownItems["menuViewCapture"] as ToolStripMenuItem;
                var viewNetwork = viewMenu.DropDownItems["menuViewNetwork"] as ToolStripMenuItem;
                var viewPrefs = viewMenu.DropDownItems["menuViewPrefs"] as ToolStripMenuItem;

                Assert.NotNull(viewBroadcast);
                Assert.NotNull(viewViewers);
                Assert.NotNull(viewCapture);
                Assert.NotNull(viewNetwork);
                Assert.NotNull(viewPrefs);

                // Ctrl+1..5 shortcuts
                Assert.Equal(Keys.Control | Keys.D1, viewBroadcast!.ShortcutKeys);
                Assert.Equal(Keys.Control | Keys.D5, viewPrefs!.ShortcutKeys);

                // View items navigate to corresponding tabs
                viewCapture!.PerformClick();
                Assert.Equal(2, form.SelectedTabIndex);
                viewBroadcast.PerformClick();
                Assert.Equal(0, form.SelectedTabIndex);

                // ── Capture menu – checkable items sync with checkboxes ────
                var captureShowMouse = captureMenu!.DropDownItems["menuCaptureShowMouse"] as ToolStripMenuItem;
                var captureAudio = captureMenu.DropDownItems["menuCaptureEnableAudio"] as ToolStripMenuItem;
                var captureAudioDiag = captureMenu.DropDownItems["menuCaptureAudioDiag"] as ToolStripMenuItem;
                var captureRefresh = captureMenu.DropDownItems["menuCaptureRefreshSources"] as ToolStripMenuItem;

                Assert.NotNull(captureShowMouse);
                Assert.NotNull(captureAudio);
                Assert.NotNull(captureAudioDiag);
                Assert.NotNull(captureRefresh);
                Assert.Equal(Keys.F5, captureRefresh!.ShortcutKeys);
                Assert.True(captureShowMouse!.CheckOnClick);
                Assert.True(captureAudio!.CheckOnClick);

                // Toggle audio via checkbox → menu checkmark updates on next UpdateMenuStripState
                var cbAudio = FindControl<CheckBox>(form, "cbAudio");
                Assert.NotNull(cbAudio);
                bool initialAudioState = cbAudio!.Checked;

                // Simulate MenuActivate by calling the internal update method
                form.UpdateMenuStripState();
                Assert.Equal(initialAudioState, captureAudio!.Checked);

                cbAudio.Checked = !initialAudioState;
                form.UpdateMenuStripState();
                Assert.Equal(!initialAudioState, captureAudio.Checked);

                // Toggle mouse capture via menu item click → checkbox syncs
                var cbCaptureMouse = FindControl<CheckBox>(form, "cbCaptureMouse");
                Assert.NotNull(cbCaptureMouse);
                bool initialMouseState = cbCaptureMouse!.Checked;

                // PerformClick toggles Checked (via CheckOnClick) and fires the sync lambda
                captureShowMouse.PerformClick();
                Assert.Equal(!initialMouseState, cbCaptureMouse.Checked);

                // ── Network menu – PIN items ───────────────────────────────
                var netPin = networkMenu!.DropDownItems["menuNetworkPinProtection"] as ToolStripMenuItem;
                var netGenPin = networkMenu.DropDownItems["menuNetworkGenPin"] as ToolStripMenuItem;
                var netCopyPin = networkMenu.DropDownItems["menuNetworkCopyPin"] as ToolStripMenuItem;

                Assert.NotNull(netPin);
                Assert.NotNull(netGenPin);
                Assert.NotNull(netCopyPin);
                Assert.True(netPin!.CheckOnClick);

                // ── Help menu – 3 link items + separator + about ───────────
                var helpWebsite = helpMenu!.DropDownItems["menuHelpWebsite"] as ToolStripMenuItem;
                var helpGitHub = helpMenu.DropDownItems["menuHelpGitHub"] as ToolStripMenuItem;
                var helpCoffee = helpMenu.DropDownItems["menuHelpBuyMeCoffee"] as ToolStripMenuItem;
                var helpAbout = helpMenu.DropDownItems["menuHelpAbout"] as ToolStripMenuItem;

                Assert.NotNull(helpWebsite);
                Assert.NotNull(helpGitHub);
                Assert.NotNull(helpCoffee);
                Assert.NotNull(helpAbout);
                Assert.Equal(Keys.F1, helpAbout!.ShortcutKeys);
                Assert.Contains("About", helpAbout.Text);
            });
        }

        [Fact]
        public void SourceSelectionForm_InitializesWithTwoTabsAndPopulatesSources()
        {
            RunInSta(() =>
            {
                var screens = Screen.AllScreens;
                var sources = new System.Collections.Generic.List<ScreenTask.Core.Capture.ICaptureSource>();
                for (int i = 0; i < screens.Length; i++)
                {
                    sources.Add(new ScreenTask.Core.Capture.ScreenCaptureSource(i));
                }

                using var form = new SourceSelectionForm(sources, sources[0], true);
                Assert.NotNull(form);
                Assert.Contains("Select Video Source", form.Text);

                // Verify 2 tabs exist: Displays and Windows
                Assert.NotNull(form.tabControlSources);
                Assert.Equal(2, form.tabControlSources.TabCount);
                Assert.NotNull(form.tabDisplays);
                Assert.NotNull(form.tabWindows);
                Assert.Contains("Displays", form.tabDisplays.Text);
                Assert.Contains("Windows", form.tabWindows.Text);

                // Verify display cards populated
                Assert.True(form.DisplayCards.Count > 0);
                Assert.NotNull(form.SelectedCard);
                Assert.True(form.SelectedCard.IsSelected);
                Assert.NotNull(form.SelectedSource);
                Assert.Contains("Display 1", form.lblSelectedSource.Text);

                // Verify mouse cursor checkbox and refresh button
                Assert.NotNull(form.cbCaptureMouse);
                Assert.True(form.cbCaptureMouse.Checked);
                Assert.NotNull(form.btnRefresh);
                Assert.NotNull(form.btnSelect);
                Assert.NotNull(form.btnCancel);

                // Toggle mouse checkbox
                form.cbCaptureMouse.Checked = false;
                Assert.False(form.cbCaptureMouse.Checked);
            });
        }

        [Fact]
        public void NetworkSettingsForm_InitializesAndSavesSettings()
        {
            RunInSta(() =>
            {
                var adapters = new[] { "Wi-Fi (192.168.1.50)", "Loopback (127.0.0.1)" };
                using var form = new NetworkSettingsForm(adapters, 0, 7070, false, "1234");
                form.Show();
                Assert.NotNull(form);
                Assert.Contains("Network & PIN Settings", form.Text);

                // Verify controls populated
                Assert.NotNull(form.comboIPs);
                Assert.Equal(2, form.comboIPs.Items.Count);
                Assert.Equal(0, form.comboIPs.SelectedIndex);

                Assert.NotNull(form.numPort);
                Assert.Equal(7070, (int)form.numPort.Value);

                Assert.NotNull(form.cbPrivate);
                Assert.False(form.cbPrivate.Checked);
                Assert.False(form.txtPassword.Enabled);

                // Enable PIN
                form.cbPrivate.Checked = true;
                Assert.True(form.txtPassword.Enabled);
                Assert.True(form.btnGenPin.Enabled);
                Assert.True(form.btnCopyPin.Enabled);

                // Test Generate PIN
                form.btnGenPin.PerformClick();
                Assert.True(int.TryParse(form.Pin, out int pinVal));
                Assert.InRange(pinVal, 1000, 9999);

                // Test apply event
                bool appliedFired = false;
                form.SettingsApplied += (s, e) => appliedFired = true;
                form.btnApply.PerformClick();
                Assert.True(appliedFired);
            });
        }

        [Fact]
        public void PreferencesForm_InitializesAndUpdatesSettings()
        {
            RunInSta(() =>
            {
                using var form = new PreferencesForm(true, false, true, 1, 80, "Test log line 1\r\nTest log line 2");
                form.Show();
                Assert.NotNull(form);
                Assert.Contains("Preferences", form.Text);

                // Verify preferences populated
                Assert.True(form.cbAutoStart.Checked);
                Assert.False(form.cbMiniStart.Checked);
                Assert.True(form.cbCloseToTray.Checked);

                // Verify quality settings
                Assert.NotNull(form.comboFps);
                Assert.Equal(1, form.comboFps.SelectedIndex);
                Assert.Equal(80, form.qualitySlider.Value);
                Assert.Contains("80%", form.lblQuality.Text);

                // Verify log console
                Assert.NotNull(form.txtLog);
                Assert.Contains("Test log line 1", form.txtLog.Text);

                // Test clear log
                form.tabControl.SelectedTab = form.tabLogs;
                form.btnClearLog.PerformClick();
                Assert.Empty(form.txtLog.Text);

                // Test slider change
                bool changedFired = false;
                form.SettingsChanged += (s, e) => changedFired = true;
                form.qualitySlider.Value = 90;
                Assert.True(changedFired);
            });
        }

        [Fact]
        public void ConnectedViewersForm_InitializesProperly()
        {
            RunInSta(() =>
            {
                using var form = new ConnectedViewersForm();
                form.Show();
                Assert.NotNull(form);
                Assert.Contains("Connected Viewers", form.Text);
                Assert.NotNull(form.cardMetricViewers);
                Assert.NotNull(form.cardFleetTerminal);
                Assert.NotNull(form.cardPairingPass);
            });
        }

        [Fact]
        public void MainForm_SingleWindowConsole_HasNewActionButtons()
        {
            RunInSta(() =>
            {
                using var form = new frmMain();
                form.Show();

                var btnAudioSetup = FindControl<Button>(form, "btnAudioSetup");
                var btnNetworkSettings = FindControl<Button>(form, "btnNetworkSettings");
                var cardChipViewers = FindControl<Panel>(form, "cardChipViewers");
                var btnSwitchSource = FindControl<Button>(form, "btnSwitchSource");

                Assert.NotNull(btnAudioSetup);
                Assert.Contains("Audio Setup", btnAudioSetup.Text);

                Assert.NotNull(btnNetworkSettings);
                Assert.Contains("Network", btnNetworkSettings.Text);

                Assert.NotNull(cardChipViewers);
                Assert.Equal(Cursors.Hand, cardChipViewers.Cursor);

                Assert.NotNull(btnSwitchSource);
                Assert.Contains("Source", btnSwitchSource.Text);
            });
        }

        [Fact]
        public void AllForms_HaveFixedNonResizableBordersAndNoMaximize()
        {
            RunInSta(() =>
            {
                // 1. Main Form
                using var frmMain = new frmMain();
                Assert.Equal(FormBorderStyle.FixedSingle, frmMain.FormBorderStyle);
                Assert.False(frmMain.MaximizeBox);

                // 2. Mini Bar Form
                using var miniBar = new MiniBarForm();
                Assert.Equal(FormBorderStyle.FixedSingle, miniBar.FormBorderStyle);
                Assert.False(miniBar.MaximizeBox);

                // 3. QR Code Form
                using var qrForm = new QrCodeForm("http://127.0.0.1:7070");
                Assert.Equal(FormBorderStyle.FixedDialog, qrForm.FormBorderStyle);
                Assert.False(qrForm.MaximizeBox);

                // 4. Audio Diagnostics Form
                using var audioService = new WasapiAudioCaptureService();
                using var audioForm = new AudioDiagnosticsForm(audioService, _ => { });
                Assert.Equal(FormBorderStyle.FixedDialog, audioForm.FormBorderStyle);
                Assert.False(audioForm.MaximizeBox);

                // 5. Video Source Selection Form
                using var sourceForm = new SourceSelectionForm(new List<ScreenTask.Core.Capture.ICaptureSource>());
                Assert.Equal(FormBorderStyle.FixedDialog, sourceForm.FormBorderStyle);
                Assert.False(sourceForm.MaximizeBox);

                // 6. Network Settings Form
                using var netForm = new NetworkSettingsForm(new[] { "127.0.0.1" }, 0, 7070, false, "1234");
                Assert.Equal(FormBorderStyle.FixedDialog, netForm.FormBorderStyle);
                Assert.False(netForm.MaximizeBox);

                // 7. Connected Viewers Form
                using var viewersForm = new ConnectedViewersForm();
                Assert.Equal(FormBorderStyle.FixedDialog, viewersForm.FormBorderStyle);
                Assert.False(viewersForm.MaximizeBox);

                // 8. Preferences Form
                using var prefsForm = new PreferencesForm(true, false, true, 1, 80, "");
                Assert.Equal(FormBorderStyle.FixedDialog, prefsForm.FormBorderStyle);
                Assert.False(prefsForm.MaximizeBox);
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
