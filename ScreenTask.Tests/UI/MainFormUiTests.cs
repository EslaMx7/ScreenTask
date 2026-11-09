using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScreenTask;
using ScreenTask.Core.Capture;
using ScreenTask.UI;
using Xunit;

namespace ScreenTask.Tests.UI
{
    public class MainFormUiTests
    {
        [Fact]
        public void MainForm_InitializesWithCompact3TabNativeLayout()
        {
            RunInSta(() =>
            {
                using var form = new frmMain();
                form.Show();

                Assert.NotNull(form);
                Assert.Contains("ScreenTask 2.0", form.Text);
                Assert.True(form.ClientSize.Width >= 750);
                Assert.True(form.ClientSize.Height >= 550);
                Assert.True(form.MinimumSize.Width >= 700);
                Assert.True(form.MinimumSize.Height >= 500);

                // Audio must default to muted / opt-in for presenter
                Assert.False(form.cbAudio.Checked);
                Assert.False(form.comboAudioDevices.Enabled);
                Assert.False(form.pbAudioMeter.Enabled);
                Assert.Contains("Unmute", form.btnQuickMute.Text);
                Assert.Contains("Audio: Off", form.StatusLabelAudio.Text);
                Assert.Equal("Audio: Muted", form.monitorPreview.AudioInfoText);

                var tabControl = FindControl<TabControl>(form, "tabControlMain");
                Assert.NotNull(tabControl);
                Assert.Equal(3, tabControl.TabCount);
                Assert.Equal("Broadcast", form.tabBroadcast.Text);
                Assert.Contains("Connected Viewers", form.tabViewers.Text);
                Assert.Equal("Settings & Logs", form.tabSettings.Text);

                // Verify core controls on Tab 0 (Broadcast)
                var comboScreens = FindControl<ComboBox>(form, "comboScreens");
                Assert.NotNull(comboScreens);
                Assert.True(comboScreens.Items.Count > 0);

                var comboIPs = FindControl<ComboBox>(form, "comboIPs");
                Assert.NotNull(comboIPs);
                Assert.True(comboIPs.Items.Count > 0);

                var btnStartServer = FindControl<Button>(form, "btnStartServer");
                Assert.NotNull(btnStartServer);
                Assert.Contains("START", btnStartServer.Text);

                var txtURL = FindControl<TextBox>(form, "txtURL");
                Assert.NotNull(txtURL);
                Assert.StartsWith("http://", txtURL.Text);

                var qualitySlider = FindControl<TrackBar>(form, "qualitySlider");
                Assert.NotNull(qualitySlider);
                Assert.Equal(80, qualitySlider.Value);

                // Verify refresh buttons exist and function
                var btnRefreshScreens = FindControl<Button>(form, "btnRefreshScreens");
                var btnRefreshIPs = FindControl<Button>(form, "btnRefreshIPs");
                var btnRefreshAudio = FindControl<Button>(form, "btnRefreshAudio");
                Assert.NotNull(btnRefreshScreens);
                Assert.NotNull(btnRefreshIPs);
                Assert.NotNull(btnRefreshAudio);

                btnRefreshScreens.PerformClick();
                btnRefreshIPs.PerformClick();
                btnRefreshAudio.PerformClick();

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
        public void MainForm_InlinePairingQr_InitializesWithValidImageOnBroadcastTab()
        {
            RunInSta(() =>
            {
                using var form = new frmMain();
                form.Show();

                Assert.NotNull(form.picPairingQr);
                Assert.NotNull(form.picPairingQr.Image);
                Assert.True(form.picPairingQr.Image.Width > 50);
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
                var pbAudioMeter = FindControl<ProgressBar>(form, "pbAudioMeter");
                var cbPrivate = FindControl<CheckBox>(form, "cbPrivate");
                var txtPassword = FindControl<TextBox>(form, "txtPassword");
                var btnQuickMute = FindControl<Button>(form, "btnQuickMute");

                Assert.NotNull(cbAudio);
                Assert.NotNull(comboAudioDevices);
                Assert.NotNull(pbAudioMeter);
                Assert.NotNull(cbPrivate);
                Assert.NotNull(txtPassword);
                Assert.NotNull(btnQuickMute);

                cbAudio.Checked = false;
                Assert.False(comboAudioDevices.Enabled);
                Assert.False(pbAudioMeter.Enabled);
                Assert.Contains("Unmute", btnQuickMute.Text);

                cbAudio.Checked = true;
                Assert.True(comboAudioDevices.Enabled);
                Assert.True(pbAudioMeter.Enabled);
                Assert.Contains("Mute", btnQuickMute.Text);

                btnQuickMute.PerformClick();
                Assert.False(cbAudio.Checked);
                Assert.Contains("Unmute", btnQuickMute.Text);

                cbPrivate.Checked = false;
                Assert.False(txtPassword.Enabled);

                cbPrivate.Checked = true;
                Assert.True(txtPassword.Enabled);
                Assert.False(string.IsNullOrEmpty(txtPassword.Text));
            });
        }

        [Fact]
        public void MainForm_AudioTestAndFirewallControls_ExistOnSettingsTab()
        {
            RunInSta(() =>
            {
                using var form = new frmMain();
                form.Show();
                form.SelectTab(2);

                Assert.NotNull(form.grpDiagnosticsFirewall);
                Assert.NotNull(form.btnTestSpeaker);
                Assert.NotNull(form.btnTestMic);
                Assert.NotNull(form.btnFirewallRule);
                Assert.NotNull(form.lblAudioDiagInfo);
            });
        }

        [Fact]
        public void MainForm_TabNavigation_TracksSelectedTabIndex()
        {
            RunInSta(() =>
            {
                using var form = new frmMain();
                form.Show();

                Assert.Equal(0, form.SelectedTabIndex);

                form.SelectTab(1);
                Assert.Equal(1, form.SelectedTabIndex);

                form.SelectTab(2);
                Assert.Equal(2, form.SelectedTabIndex);

                form.SelectTab(0);
                Assert.Equal(0, form.SelectedTabIndex);
            });
        }

        [Fact]
        public void MainForm_ConnectedViewersTab_ListViewAndActionsFunctionProperly()
        {
            RunInSta(() =>
            {
                using var form = new frmMain();
                form.Show();
                form.SelectTab(1);

                Assert.NotNull(form.lvViewers);
                Assert.Equal(7, form.lvViewers.Columns.Count);
                Assert.Empty(form.lvViewers.Items);
                Assert.False(form.btnMuteSelectedViewer.Enabled);
                Assert.False(form.btnKickSelectedViewer.Enabled);

                // Announcement with 0 clients
                form.txtAnnouncement.Text = "Attention viewers!";
                form.btnSendAnnouncement.PerformClick();
                Assert.Contains("No viewers connected", form.lblAnnouncementStatus.Text);

                // Connect simulated live client session
                using var dummyWs = new DummyWebSocket();
                form.Broadcaster.AddVideoClient(
                    "session-iphone-1",
                    dummyWs,
                    "192.168.1.182",
                    "Mozilla/5.0 (iPhone; CPU iPhone OS 17_4 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/17.4 Mobile/15E148 Safari/604.1"
                );
                form.RefreshViewerList();
                Application.DoEvents();

                Assert.Single(form.lvViewers.Items);
                Assert.Contains("(1)", form.tabViewers.Text);
                var row = form.lvViewers.Items[0];
                Assert.Contains("iPhone", row.SubItems[0].Text);
                Assert.Equal("192.168.1.182", row.SubItems[1].Text);
                Assert.Contains("Safari", row.SubItems[2].Text);
                Assert.Equal("Active", row.SubItems[6].Text);

                // Row is auto-selected, so action buttons are enabled
                Assert.True(form.btnMuteSelectedViewer.Enabled);
                Assert.True(form.btnKickSelectedViewer.Enabled);

                // Update telemetry
                form.Broadcaster.UpdateTelemetry("session-iphone-1", 18.2, "Touch Zoom 2.0x");
                form.RefreshViewerList();
                Assert.Equal("Touch Zoom 2.0x", form.lvViewers.Items[0].SubItems[3].Text);

                // Toggle mute on selected viewer
                form.btnMuteSelectedViewer.PerformClick();
                Assert.Equal("Muted", form.lvViewers.Items[0].SubItems[6].Text);
                Assert.Contains("Unmute", form.btnMuteSelectedViewer.Text);

                form.btnMuteSelectedViewer.PerformClick();
                Assert.Equal("Active", form.lvViewers.Items[0].SubItems[6].Text);
                Assert.Contains("Mute", form.btnMuteSelectedViewer.Text);

                // Disconnect selected viewer
                form.btnKickSelectedViewer.PerformClick();
                Application.DoEvents();

                form.RefreshViewerList();
                Assert.Empty(form.lvViewers.Items);
                Assert.Contains("(0)", form.tabViewers.Text);
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

                Assert.NotNull(form.statusLabelViewers);
                Assert.NotNull(form.statusLabelFps);
                Assert.NotNull(form.statusLabelData);
                Assert.NotNull(form.statusLabelUptime);
                Assert.Contains("viewer", form.statusLabelViewers.Text);
                Assert.Contains("FPS", form.statusLabelFps.Text);
                Assert.Contains("MB", form.statusLabelData.Text);
                Assert.Contains("00:00:00", form.statusLabelUptime.Text);
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

                var cbAudio = FindControl<CheckBox>(form, "cbAudio");
                Assert.NotNull(cbAudio);

                cbAudio.Checked = true;
                Assert.Contains("Audio: On", form.StatusLabelAudio.Text);

                cbAudio.Checked = false;
                Assert.Contains("Audio: Off", form.StatusLabelAudio.Text);
            });
        }

        [Fact]
        public void SourceSelectionForm_VisualSourcePickerDialog_DisplaysTabsAndCards()
        {
            RunInSta(() =>
            {
                var sources = new ICaptureSource[] { new ScreenCaptureSource(0) };
                using var picker = new SourceSelectionForm(sources, sources[0], true);
                picker.Show();

                Assert.NotNull(picker.tabControlSources);
                Assert.Equal(2, picker.tabControlSources.TabCount);
                Assert.NotNull(picker.flowDisplays);
                Assert.NotNull(picker.flowWindows);
                Assert.NotNull(picker.lblSelectedSource);

                Assert.True(picker.DisplayCards.Count > 0, "Should have at least 1 physical display card");
                var firstDisplayCard = picker.DisplayCards[0];
                Assert.NotNull(firstDisplayCard);
                Assert.True(firstDisplayCard.IsSelected);
                Assert.True(firstDisplayCard.lblSelectedBadge.Visible);
                Assert.Contains("Selected:", picker.lblSelectedSource.Text);

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

                var menuStrip = FindControl<MenuStrip>(form, "menuStripMain");
                Assert.NotNull(menuStrip);
                Assert.Equal(DockStyle.Top, menuStrip.Dock);
                Assert.Same(menuStrip, form.MainMenuStrip);

                Assert.Equal(6, menuStrip.Items.Count);
                var fileMenu = menuStrip.Items["menuFile"] as ToolStripMenuItem;
                var viewMenu = menuStrip.Items["menuView"] as ToolStripMenuItem;
                var captureMenu = menuStrip.Items["menuCapture"] as ToolStripMenuItem;
                var networkMenu = menuStrip.Items["menuNetwork"] as ToolStripMenuItem;
                var helpMenu = menuStrip.Items["menuHelp"] as ToolStripMenuItem;
                var feedbackQuickMenu = menuStrip.Items["menuFeedbackQuick"] as ToolStripMenuItem;

                Assert.NotNull(fileMenu);
                Assert.NotNull(viewMenu);
                Assert.NotNull(captureMenu);
                Assert.NotNull(networkMenu);
                Assert.NotNull(helpMenu);
                Assert.NotNull(feedbackQuickMenu);
                Assert.Equal(ToolStripItemAlignment.Right, feedbackQuickMenu!.Alignment);

                var reportBugItem = helpMenu!.DropDownItems["menuHelpReportBug"] as ToolStripMenuItem;
                var requestFeatureItem = helpMenu.DropDownItems["menuHelpRequestFeature"] as ToolStripMenuItem;
                var checkUpdatesItem = helpMenu.DropDownItems["menuHelpCheckUpdates"] as ToolStripMenuItem;
                Assert.NotNull(reportBugItem);
                Assert.NotNull(requestFeatureItem);
                Assert.NotNull(checkUpdatesItem);

                // Clicking status bar viewer label switches to Connected Viewers tab
                form.statusLabelViewers.PerformClick();
                Assert.Equal(1, form.SelectedTabIndex);
                form.SelectTab(0);

                var toggleBroadcast = fileMenu!.DropDownItems["menuFileToggleBroadcast"] as ToolStripMenuItem;
                var copyLink = fileMenu.DropDownItems["menuFileCopyLink"] as ToolStripMenuItem;
                var openBrowser = fileMenu.DropDownItems["menuFileOpenBrowser"] as ToolStripMenuItem;
                var exitItem = fileMenu.DropDownItems["menuFileExit"] as ToolStripMenuItem;

                Assert.NotNull(toggleBroadcast);
                Assert.NotNull(copyLink);
                Assert.NotNull(openBrowser);
                Assert.NotNull(exitItem);
                Assert.Contains("Start Broadcast", toggleBroadcast!.Text);
                Assert.Equal(Keys.Control | Keys.B, toggleBroadcast.ShortcutKeys);

                // View menu – 3 tabs
                Assert.Equal(3, viewMenu!.DropDownItems.Count);
                var viewBroadcast = viewMenu.DropDownItems["menuViewBroadcast"] as ToolStripMenuItem;
                var viewViewers = viewMenu.DropDownItems["menuViewViewers"] as ToolStripMenuItem;
                var viewSettings = viewMenu.DropDownItems["menuViewSettings"] as ToolStripMenuItem;

                Assert.NotNull(viewBroadcast);
                Assert.NotNull(viewViewers);
                Assert.NotNull(viewSettings);

                viewSettings!.PerformClick();
                Assert.Equal(2, form.SelectedTabIndex);
                viewBroadcast!.PerformClick();
                Assert.Equal(0, form.SelectedTabIndex);

                // Capture menu sync
                var captureShowMouse = captureMenu!.DropDownItems["menuCaptureShowMouse"] as ToolStripMenuItem;
                var captureAudio = captureMenu.DropDownItems["menuCaptureEnableAudio"] as ToolStripMenuItem;
                var captureRefresh = captureMenu.DropDownItems["menuCaptureRefreshSources"] as ToolStripMenuItem;

                Assert.NotNull(captureShowMouse);
                Assert.NotNull(captureAudio);
                Assert.NotNull(captureRefresh);
                Assert.Equal(Keys.F5, captureRefresh!.ShortcutKeys);

                var cbAudio = FindControl<CheckBox>(form, "cbAudio");
                Assert.NotNull(cbAudio);
                bool initialAudioState = cbAudio!.Checked;

                form.UpdateMenuStripState();
                Assert.Equal(initialAudioState, captureAudio!.Checked);

                cbAudio.Checked = !initialAudioState;
                form.UpdateMenuStripState();
                Assert.Equal(!initialAudioState, captureAudio.Checked);
            });
        }

        [Fact]
        public void MainForm_CloseToTrayAndSettings_PersistAndSurviveRestart()
        {
            RunInSta(() =>
            {
                string tempSettingsFile = Path.Combine(Path.GetTempPath(), $"screentask_test_settings_{Guid.NewGuid():N}.xml");
                try
                {
                    using (var form1 = new frmMain())
                    {
                        form1.CustomSettingsFilePath = tempSettingsFile;
                        form1.Show();

                        Assert.True(form1.cbCloseToTray.Checked, "Minimize to tray should be checked by default.");

                        form1.cbCloseToTray.Checked = false;
                        form1.numPort.Value = 9191;
                        form1.cbPrivate.Checked = true;
                        form1.txtPassword.Text = "8899";
                        form1.cbCaptureMouse.Checked = false;
                        form1.cbAutoStart.Checked = true;
                        form1.cbMiniStart.Checked = true;
                        form1.qualitySlider.Value = 60;
                        form1.comboFps.SelectedIndex = 2;

                        form1.SaveSettings();
                    }

                    Assert.True(File.Exists(tempSettingsFile), "Settings file must exist after SaveSettings.");

                    using (var form2 = new frmMain())
                    {
                        form2.CustomSettingsFilePath = tempSettingsFile;
                        form2.Show();

                        Assert.False(form2.cbCloseToTray.Checked);
                        Assert.Equal(9191, (int)form2.numPort.Value);
                        Assert.True(form2.cbPrivate.Checked);
                        Assert.Equal("8899", form2.txtPassword.Text);
                        Assert.False(form2.cbCaptureMouse.Checked);
                        Assert.True(form2.cbAutoStart.Checked);
                        Assert.True(form2.cbMiniStart.Checked);
                        Assert.Equal(60, form2.qualitySlider.Value);
                        Assert.Equal(2, form2.comboFps.SelectedIndex);
                    }
                }
                finally
                {
                    if (File.Exists(tempSettingsFile))
                    {
                        try { File.Delete(tempSettingsFile); } catch { }
                    }
                }
            });
        }

        [Fact]
        public void MainForm_ActiveBroadcast_GracefulShutdownCompletesWithoutHanging()
        {
            RunInSta(() =>
            {
                using var form = new frmMain();
                form.SuppressModalDialogsForTesting = true;
                form.Show();

                int loopbackIdx = -1;
                for (int i = 0; i < form.comboIPs.Items.Count; i++)
                {
                    if (form.comboIPs.Items[i]?.ToString()?.Contains("127.0.0.1") == true)
                    {
                        loopbackIdx = i;
                        break;
                    }
                }
                if (loopbackIdx >= 0) form.comboIPs.SelectedIndex = loopbackIdx;
                form.numPort.Value = 18765;

                form.btnStartServer.PerformClick();
                Application.DoEvents();

                Assert.True(form.WebServer.IsRunning, "Web server should be running after start broadcast.");
                Assert.True(form.CaptureEngine.IsRunning, "Capture engine should be running after start broadcast.");

                var shutdownTask = form.PerformGracefulShutdownAsync();
                bool completed = shutdownTask.Wait(TimeSpan.FromSeconds(5));

                Assert.True(completed, "Graceful shutdown must complete within timeout without hanging!");
                Assert.False(form.WebServer.IsRunning, "Web server must be stopped after shutdown.");
                Assert.False(form.CaptureEngine.IsRunning, "Capture engine must be stopped after shutdown.");
            });
        }

        [Fact]
        public void MainForm_ActiveBroadcast_FormClosingHandlesGracefulShutdown()
        {
            RunInSta(() =>
            {
                var form = new frmMain();
                form.SuppressModalDialogsForTesting = true;
                form.cbCloseToTray.Checked = false;
                form.Show();

                int loopbackIdx = -1;
                for (int i = 0; i < form.comboIPs.Items.Count; i++)
                {
                    if (form.comboIPs.Items[i]?.ToString()?.Contains("127.0.0.1") == true)
                    {
                        loopbackIdx = i;
                        break;
                    }
                }
                if (loopbackIdx >= 0) form.comboIPs.SelectedIndex = loopbackIdx;
                form.numPort.Value = 18766;

                form.btnStartServer.PerformClick();
                Application.DoEvents();

                Assert.True(form.WebServer.IsRunning);

                form.Close();

                for (int i = 0; i < 40 && !form.IsDisposed; i++)
                {
                    Application.DoEvents();
                    Thread.Sleep(50);
                }

                Assert.False(form.WebServer.IsRunning, "Web server must stop when form closes.");
                Assert.False(form.CaptureEngine.IsRunning, "Capture engine must stop when form closes.");
            });
        }

        [Fact]
        public void MainForm_ExpandPreview_TogglesConfidenceMonitorSize()
        {
            RunInSta(() =>
            {
                using var form = new frmMain();
                form.Show();

                Assert.NotNull(form.btnPopout);
                Assert.Contains("Expand Preview", form.btnPopout.Text);

                form.btnPopout.PerformClick();
                Assert.Contains("Normal View", form.btnPopout.Text);

                form.btnPopout.PerformClick();
                Assert.Contains("Expand Preview", form.btnPopout.Text);
            });
        }

        [Fact]
        public void FeedbackForm_GeneratesPredefinedTemplatesAndDiagnosticsForGitHubAndEmail()
        {
            RunInSta(() =>
            {
                using var mainForm = new frmMain();
                mainForm.Show();
                mainForm.Log("Sample diagnostic log entry");
                Application.DoEvents();

                Assert.NotNull(mainForm.btnReportIssue);
                Assert.Contains("Report Bug", mainForm.btnReportIssue.Text);

                var snapshot = mainForm.BuildFeedbackDiagnosticsSnapshot();
                Assert.Equal("2.0.0", snapshot.AppVersion);
                Assert.False(snapshot.IsAudioEnabled);
                Assert.Contains("Sample diagnostic log entry", snapshot.RecentLogLines);

                using var feedbackForm = new FeedbackForm(snapshot, FeedbackType.BugReport);
                feedbackForm.Show();

                Assert.True(feedbackForm.rbBugReport.Checked);
                Assert.False(feedbackForm.rbFeatureRequest.Checked);
                Assert.True(feedbackForm.cbIncludeDiagnostics.Checked);
                Assert.Contains("## Bug Description", feedbackForm.txtDetails.Text);
                Assert.Contains("ScreenTask Version: 2.0.0", feedbackForm.txtDiagnostics.Text);
                Assert.Contains("Sample diagnostic log entry", feedbackForm.txtDiagnostics.Text);

                feedbackForm.txtTitle.Text = "Audio stutters on second monitor";
                string ghUrl = feedbackForm.BuildGitHubIssueUrl();
                Assert.StartsWith("https://github.com/EslaMx7/ScreenTask/issues/new?", ghUrl);
                Assert.Contains("labels=bug", ghUrl);
                Assert.Contains(Uri.EscapeDataString("[Bug]: Audio stutters on second monitor"), ghUrl);

                string mailtoUri = feedbackForm.BuildMailtoUri();
                Assert.StartsWith("mailto:support@eslamx.com?subject=", mailtoUri);
                Assert.Contains(Uri.EscapeDataString("Audio stutters on second monitor"), mailtoUri);

                // Switch to Feature Request mode (unmodified template should auto-switch)
                feedbackForm.rbFeatureRequest.Checked = true;
                Assert.Contains("## Problem or Use Case", feedbackForm.txtDetails.Text);
                string featureGhUrl = feedbackForm.BuildGitHubIssueUrl();
                Assert.Contains("labels=enhancement", featureGhUrl);

                // Copy report to clipboard
                feedbackForm.btnCopyMessage.PerformClick();
                Assert.Contains("Copied full report", feedbackForm.lblStatus.Text);
            });
        }

        [Fact]
        public void FeedbackForm_CreateCrashReportDialog_PopulatesExceptionTitleAndStack()
        {
            RunInSta(() =>
            {
                var ex = new InvalidOperationException("Simulated capture pipeline crash");
                using var crashDialog = FeedbackForm.CreateCrashReportDialog(ex);
                crashDialog.Show();

                Assert.True(crashDialog.rbBugReport.Checked);
                Assert.Contains("Unhandled InvalidOperationException: Simulated capture pipeline crash", crashDialog.txtTitle.Text);
                Assert.Contains("Unhandled Exception Details:", crashDialog.txtDiagnostics.Text);
                Assert.Contains("Simulated capture pipeline crash", crashDialog.txtDiagnostics.Text);
            });
        }

        [Fact]
        public void TryParseNormalizedVersion_NormalizesShortAndLongVersionTags()
        {
            Assert.True(frmMain.TryParseNormalizedVersion("v2.0", out var v2Short));
            Assert.True(frmMain.TryParseNormalizedVersion("v2.0.0", out var v2Semantic));
            Assert.True(frmMain.TryParseNormalizedVersion("2.0.0.0", out var v2Assembly));
            Assert.Equal(v2Short, v2Semantic);
            Assert.Equal(v2Semantic, v2Assembly);
            Assert.False(v2Short > v2Semantic);

            Assert.True(frmMain.TryParseNormalizedVersion("v1.2", out var v12));
            Assert.True(v2Short > v12);

            Assert.True(frmMain.TryParseNormalizedVersion("v2.1", out var v21));
            Assert.True(v21 > v2Semantic);

            Assert.True(frmMain.TryParseNormalizedVersion("v2.0.1-rc1", out var v201));
            Assert.True(v201 > v2Semantic);
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
