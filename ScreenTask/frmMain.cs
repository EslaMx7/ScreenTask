using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using ScreenTask.Core.Audio;
using ScreenTask.Core.Capture;
using ScreenTask.Core.Network;
using ScreenTask.Core.Streaming;
using ScreenTask.UI;
using ScreenTask.WebServer;

namespace ScreenTask
{
    public partial class frmMain : Form
    {
        private readonly StreamBroadcaster _broadcaster;
        private readonly ScreenCaptureEngine _captureEngine;
        private readonly EmbeddedWebServer _webServer;
        private List<NetworkAdapterInfo> _adapters = new();
        private readonly List<ICaptureSource> _captureSources = new();
        private List<AudioDeviceDescriptor> _audioDevices = new();
        private AppSettings _currentSettings = new();

        private readonly System.Windows.Forms.Timer _statsTimer = new() { Interval = 1000 };
        private readonly System.Windows.Forms.Timer _deviceChangeTimer = new() { Interval = 600 };
        private readonly Stopwatch _uptimeStopwatch = new();
        private readonly HttpClient _httpClient = new() { Timeout = TimeSpan.FromSeconds(5) };
        private const int WM_DEVICECHANGE = 0x0219;

        private bool _isExitingExplicitly;
        private bool _isLoadingSettings;
        private bool _isShuttingDown;
        private bool _isGracefulShutdownComplete;
        private bool _isExpandedPreview;
        private bool _isRecordingMicTest;
        private readonly string _instanceId = Guid.NewGuid().ToString("N");

        public StreamBroadcaster Broadcaster => _broadcaster;
        public EmbeddedWebServer WebServer => _webServer;
        public ScreenCaptureEngine CaptureEngine => _captureEngine;
        public int SelectedTabIndex => tabControlMain.SelectedIndex;

        public ICaptureSource? SelectedSource =>
            comboScreens.SelectedIndex >= 0 && comboScreens.SelectedIndex < _captureSources.Count
                ? _captureSources[comboScreens.SelectedIndex]
                : null;

        // Status Strip properties
        public StatusStrip StatusStrip => statusStripMain;
        public ToolStripStatusLabel StatusLabelServer => statusLabelServer;
        public ToolStripStatusLabel StatusLabelViewers => statusLabelViewers;
        public ToolStripStatusLabel StatusLabelFps => statusLabelFps;
        public ToolStripStatusLabel StatusLabelData => statusLabelData;
        public ToolStripStatusLabel StatusLabelUptime => statusLabelUptime;
        public ToolStripStatusLabel StatusLabelAudio => statusLabelAudio;
        public ToolStripStatusLabel StatusLabelCopyright => statusLabelCopyright;

        public bool SuppressModalDialogsForTesting { get; set; }
        public AppSettings CurrentSettings => _currentSettings;
        public string? CustomSettingsFilePath { get; set; }

        public frmMain()
        {
            InitializeComponent();

            _broadcaster = new StreamBroadcaster(80);
            _captureEngine = new ScreenCaptureEngine(_broadcaster);
            _webServer = new EmbeddedWebServer(_broadcaster, _captureEngine);

            _statsTimer.Tick += OnStatsTimerTick;
            _deviceChangeTimer.Tick += (s, e) =>
            {
                _deviceChangeTimer.Stop();
                try
                {
                    PopulateAudioDevices();
                    PopulateNetworkAdapters();
                    Log("Hardware change detected: refreshed audio and network devices.");
                }
                catch { }
            };

            WireEvents();
            WireMenuEvents();
            InitializeTrayContextMenu();
        }

        private void WireEvents()
        {
            Load += frmMain_Load;
            FormClosing += frmMain_FormClosing;
            Resize += frmMain_Resize;

            tabControlMain.SelectedIndexChanged += (s, e) =>
            {
                if (tabControlMain.SelectedIndex == 1)
                {
                    SyncFleetMetrics();
                    RefreshViewerList();
                }
                else if (tabControlMain.SelectedIndex == 2)
                {
                    UpdateQualityLabel();
                    UpdateDiagnosticInfo();
                }
            };

            // Broadcaster session events
            _broadcaster.SessionConnected += session =>
            {
                if (IsHandleCreated && !IsDisposed)
                {
                    try
                    {
                        BeginInvoke(() =>
                        {
                            SyncFleetMetrics();
                            RefreshViewerList();
                        });
                    }
                    catch { }
                }
            };

            _broadcaster.SessionDisconnected += id =>
            {
                if (IsHandleCreated && !IsDisposed)
                {
                    try
                    {
                        BeginInvoke(() =>
                        {
                            SyncFleetMetrics();
                            RefreshViewerList();
                        });
                    }
                    catch { }
                }
            };

            _broadcaster.SessionUpdated += session =>
            {
                if (IsHandleCreated && !IsDisposed)
                {
                    try
                    {
                        BeginInvoke(() =>
                        {
                            SyncFleetMetrics();
                            UpdateViewerListItem(session);
                        });
                    }
                    catch { }
                }
            };

            // Broadcast Tab Controls
            btnStartServer.Click += btnStartServer_Click;
            btnFreeze.Click += btnFreeze_Click;
            btnQuickMute.Click += (s, e) => cbAudio.Checked = !cbAudio.Checked;
            btnPopout.Click += (s, e) => ToggleExpandPreview();
            btnSwitchSource.Click += (s, e) => ShowVisualSourcePicker();
            comboScreens.SelectedIndexChanged += comboScreens_SelectedIndexChanged;
            btnRefreshScreens.Click += btnRefreshScreens_Click;
            cbCaptureMouse.CheckedChanged += cbCaptureMouse_CheckedChanged;
            cbAudio.CheckedChanged += cbAudio_CheckedChanged;
            comboAudioDevices.SelectedIndexChanged += comboAudioDevices_SelectedIndexChanged;
            btnRefreshAudio.Click += btnRefreshAudio_Click;

            // Network & PIN Controls
            comboIPs.SelectedIndexChanged += (s, e) =>
            {
                UpdateUrlPreview();
                SyncPairingInfo();
                SaveSettings();
            };
            numPort.ValueChanged += (s, e) =>
            {
                UpdateUrlPreview();
                SyncPairingInfo();
                SaveSettings();
            };
            btnRefreshIPs.Click += btnRefreshIPs_Click;
            btnCopyUrl.Click += btnCopyUrl_Click;
            btnLaunch.Click += btnLaunch_Click;
            cbPrivate.CheckedChanged += cbPrivate_CheckedChanged;
            txtPassword.TextChanged += txtPassword_TextChanged;
            btnGenPin.Click += btnGenPin_Click;
            btnCopyPin.Click += btnCopyPin_Click;
            picPairingQr.Click += (s, e) => ShowQrPopupDialog();
            lblQrHint.Click += (s, e) => ShowQrPopupDialog();
            toolTipMain.SetToolTip(picPairingQr, "Click to enlarge QR code for room or projector scanning");

            // Connected Viewers Tab Controls
            lvViewers.SelectedIndexChanged += (s, e) => UpdateViewerActionButtons();
            btnMuteSelectedViewer.Click += btnMuteSelectedViewer_Click;
            btnKickSelectedViewer.Click += btnKickSelectedViewer_Click;
            btnSendAnnouncement.Click += btnSendAnnouncement_Click;
            txtAnnouncement.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    btnSendAnnouncement.PerformClick();
                }
            };

            // Settings & Logs Tab Controls
            comboFps.SelectedIndexChanged += comboFps_SelectedIndexChanged;
            qualitySlider.Scroll += qualitySlider_Scroll;
            cbAutoStart.CheckedChanged += (s, e) => SaveSettings();
            cbMiniStart.CheckedChanged += (s, e) => SaveSettings();
            cbCloseToTray.CheckedChanged += (s, e) => SaveSettings();
            btnTestSpeaker.Click += btnTestSpeaker_Click;
            btnTestMic.Click += btnTestMic_Click;
            btnFirewallRule.Click += btnFirewallRule_Click;
            btnClearLog.Click += (s, e) => txtLog.Clear();
            btnCopyLog.Click += btnCopyLog_Click;
            btnReportIssue.Click += (s, e) => ShowFeedbackDialog(FeedbackType.BugReport);

            // Live Preview & VU Meter
            _captureEngine.PreviewFrameAvailable += bmp =>
            {
                monitorPreview.UpdatePreview(bmp);
            };

            _captureEngine.AudioService.AudioLevelChanged += level =>
            {
                if (pbAudioMeter.IsHandleCreated && !pbAudioMeter.IsDisposed)
                {
                    try
                    {
                        pbAudioMeter.BeginInvoke(() =>
                        {
                            int val = Math.Clamp((int)(level * 100), 0, 100);
                            pbAudioMeter.Value = val;
                            if (val > 0)
                            {
                                int db = (int)(20 * Math.Log10(Math.Max(level, 0.001f)));
                                lblAudioMeterText.Text = $"Volume: {db} dB";
                            }
                            else
                            {
                                lblAudioMeterText.Text = cbAudio.Checked ? "Volume: Silent" : "Volume: Muted";
                            }
                        });
                    }
                    catch { }
                }
            };

            statusLabelViewers.Click += (s, e) => SelectTab(1);
            statusLabelCopyright.Click += statusLabelCopyright_Click;
            appNotify.Click += appNotify_Click;
            appNotify.MouseDoubleClick += (s, e) => RestoreFromTray();
        }

        private void WireMenuEvents()
        {
            menuFileToggleBroadcast.Click += btnStartServer_Click;
            menuFileCopyLink.Click += btnCopyUrl_Click;
            menuFileOpenBrowser.Click += btnLaunch_Click;
            menuFileExit.Click += (s, e) =>
            {
                _isExitingExplicitly = true;
                Close();
            };

            menuViewBroadcast.Click += (s, e) => SelectTab(0);
            menuViewViewers.Click += (s, e) => SelectTab(1);
            menuViewSettings.Click += (s, e) => SelectTab(2);

            menuCapturePickSource.Click += (s, e) => ShowVisualSourcePicker();
            menuCaptureRefreshSources.Click += btnRefreshScreens_Click;
            menuCaptureShowMouse.Click += (s, e) => cbCaptureMouse.Checked = menuCaptureShowMouse.Checked;
            menuCaptureEnableAudio.Click += (s, e) => cbAudio.Checked = menuCaptureEnableAudio.Checked;

            menuNetworkRefreshAdapters.Click += btnRefreshIPs_Click;
            menuNetworkFirewallRule.Click += btnFirewallRule_Click;
            menuNetworkPinProtection.Click += (s, e) => cbPrivate.Checked = menuNetworkPinProtection.Checked;
            menuNetworkGenPin.Click += btnGenPin_Click;
            menuNetworkCopyPin.Click += btnCopyPin_Click;

            menuHelpReportBug.Click += (s, e) => ShowFeedbackDialog(FeedbackType.BugReport);
            menuHelpRequestFeature.Click += (s, e) => ShowFeedbackDialog(FeedbackType.FeatureRequest);
            menuFeedbackQuick.Click += (s, e) => ShowFeedbackDialog(FeedbackType.BugReport);
            menuHelpCheckUpdates.Click += async (s, e) => await CheckForUpdatesAsync(manualCheck: true);
            menuHelpWebsite.Click += (s, e) =>
            {
                try { Process.Start(new ProcessStartInfo(Constants.WebsiteUrl) { UseShellExecute = true }); } catch { }
            };
            menuHelpGitHub.Click += (s, e) =>
            {
                try { Process.Start(new ProcessStartInfo(Constants.GitHubUrl) { UseShellExecute = true }); } catch { }
            };
            menuHelpBuyMeCoffee.Click += (s, e) =>
            {
                try { Process.Start(new ProcessStartInfo(Constants.BuyMeACoffeeUrl) { UseShellExecute = true }); } catch { }
            };
            menuHelpAbout.Click += (s, e) => ShowAboutDialog();

            menuStripMain.MenuActivate += (s, e) => UpdateMenuStripState();
        }

        internal void UpdateMenuStripState()
        {
            bool isLive = _webServer.IsRunning;

            menuFileToggleBroadcast.Text = isLive ? "Stop Broadcast" : "Start Broadcast";
            menuFileCopyLink.Enabled = !string.IsNullOrEmpty(txtURL.Text);
            menuFileOpenBrowser.Enabled = txtURL.Text.StartsWith("http", StringComparison.OrdinalIgnoreCase);

            menuCaptureShowMouse.Checked = cbCaptureMouse.Checked;
            menuCaptureEnableAudio.Checked = cbAudio.Checked;

            menuNetworkPinProtection.Checked = cbPrivate.Checked;
            menuNetworkGenPin.Enabled = cbPrivate.Checked;
            menuNetworkCopyPin.Enabled = cbPrivate.Checked && !string.IsNullOrEmpty(txtPassword.Text);
        }

        private void InitializeTrayContextMenu()
        {
            trayContextMenu.Items.Clear();

            var header = new ToolStripMenuItem("ScreenTask 2.0") { Enabled = false };
            var itemToggle = new ToolStripMenuItem("Start Broadcast", null, (s, e) => btnStartServer.PerformClick());
            var itemMute = new ToolStripMenuItem("Unmute Audio", null, (s, e) => cbAudio.Checked = !cbAudio.Checked);
            var itemCopy = new ToolStripMenuItem("Copy Stream Link", null, (s, e) => btnCopyUrl.PerformClick());
            var itemOpen = new ToolStripMenuItem("Open in Browser", null, (s, e) => btnLaunch.PerformClick());
            var itemFeedback = new ToolStripMenuItem("Report Bug / Request Feature...", null, (s, e) =>
            {
                RestoreFromTray();
                ShowFeedbackDialog(FeedbackType.BugReport);
            });
            var itemRestore = new ToolStripMenuItem("Open ScreenTask", null, (s, e) => RestoreFromTray());
            var itemExit = new ToolStripMenuItem("Exit ScreenTask", null, (s, e) =>
            {
                _isExitingExplicitly = true;
                Close();
            });

            trayContextMenu.Items.AddRange(new ToolStripItem[]
            {
                header,
                new ToolStripSeparator(),
                itemToggle,
                itemMute,
                itemCopy,
                itemOpen,
                new ToolStripSeparator(),
                itemFeedback,
                itemRestore,
                itemExit
            });

            trayContextMenu.Opening += (s, e) =>
            {
                header.Text = _webServer.IsRunning ? "ScreenTask 2.0 – Live" : "ScreenTask 2.0 – Ready";
                itemToggle.Text = _webServer.IsRunning ? "Stop Broadcast" : "Start Broadcast";
                itemMute.Text = cbAudio.Checked ? "Mute Audio" : "Unmute Audio";
            };
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_DEVICECHANGE)
            {
                _deviceChangeTimer.Stop();
                _deviceChangeTimer.Start();
            }
        }

        private void frmMain_Load(object? sender, EventArgs e)
        {
            _isLoadingSettings = true;
            try
            {
                PopulateNetworkAdapters();
                PopulateCaptureSources();
                PopulateAudioDevices();
                LoadSettings();
                cbAudio_CheckedChanged(this, EventArgs.Empty);
                cbPrivate_CheckedChanged(this, EventArgs.Empty);
                UpdateUrlPreview();
                SyncPairingInfo();
                SyncFleetMetrics();
                RefreshViewerList();
                UpdateDiagnosticInfo();
            }
            finally
            {
                _isLoadingSettings = false;
            }

            statusLabelServer.Text = "● Ready to stream";
            statusLabelViewers.Text = "0 viewers";
            statusLabelFps.Text = "0 FPS";
            statusLabelData.Text = "0.0 MB";
            statusLabelUptime.Text = "00:00:00";
            statusLabelAudio.Text = cbAudio.Checked ? "Audio: On" : "Audio: Off";
            statusLabelCopyright.Text = "© 2014-2026 Eslam Hamouda";

            if (_currentSettings.IsStartMinimizedEnabled)
            {
                WindowState = FormWindowState.Minimized;
            }

            if (_currentSettings.IsAutoStartServerEnabled)
            {
                btnStartServer_Click(this, EventArgs.Empty);
            }

            if (!SuppressModalDialogsForTesting)
            {
                _ = CheckForUpdatesAsync();
            }
            Log("ScreenTask 2.0 initialized. Ready.");
        }

        public void SelectTab(int index)
        {
            if (index >= 0 && index < tabControlMain.TabPages.Count)
            {
                tabControlMain.SelectedIndex = index;
            }
        }

        public void OpenView(int index)
        {
            SelectTab(index);
        }

        private void ToggleExpandPreview()
        {
            _isExpandedPreview = !_isExpandedPreview;
            tableBroadcast.SuspendLayout();
            if (_isExpandedPreview)
            {
                tableBroadcastRight.Visible = false;
                tableBroadcast.ColumnStyles[0].Width = 100F;
                tableBroadcast.ColumnStyles[1].Width = 0F;
                btnPopout.Text = "Normal View";
            }
            else
            {
                tableBroadcastRight.Visible = true;
                tableBroadcast.ColumnStyles[0].Width = 64F;
                tableBroadcast.ColumnStyles[1].Width = 36F;
                btnPopout.Text = "Expand Preview";
            }
            tableBroadcast.ResumeLayout(true);
        }

        // ═════════════════════════════════════════════════════════════════
        // Visual Source Picker Dialog
        // ═════════════════════════════════════════════════════════════════

        public void ShowVisualSourcePicker()
        {
            PopulateCaptureSources();
            using var dialog = new SourceSelectionForm(
                _captureSources,
                SelectedSource,
                cbCaptureMouse.Checked,
                () =>
                {
                    PopulateCaptureSources();
                    return _captureSources;
                });

            dialog.SourceSelected += selected =>
            {
                int idx = _captureSources.FindIndex(s => s.Id == selected.Id || s.DisplayName == selected.DisplayName);
                if (idx >= 0)
                {
                    comboScreens.SelectedIndex = idx;
                }
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                cbCaptureMouse.Checked = dialog.cbCaptureMouse.Checked;
                if (dialog.SelectedSource != null)
                {
                    int idx = _captureSources.FindIndex(s => s.Id == dialog.SelectedSource.Id || s.DisplayName == dialog.SelectedSource.DisplayName);
                    if (idx >= 0)
                    {
                        comboScreens.SelectedIndex = idx;
                    }
                }
            }
        }

        // ═════════════════════════════════════════════════════════════════
        // Connected Viewers & Fleet Management (Tab 1)
        // ═════════════════════════════════════════════════════════════════

        public void SyncFleetMetrics()
        {
            int count = _broadcaster.ConnectedSessions.Count;
            double avgRtt = _broadcaster.AverageFleetLatencyMs;
            double dropPct = _broadcaster.FrameDropPercentage;

            tabViewers.Text = $"Connected Viewers ({count})";
            statusLabelViewers.Text = $"{count} {(count == 1 ? "viewer" : "viewers")}";
            string rttStr = avgRtt > 0 ? $"{avgRtt:0.0} ms" : "<1.0 ms";
            lblFleetSummary.Text = $"{count} active {(count == 1 ? "viewer" : "viewers")} • Average latency: {rttStr} • {dropPct:0.0}% frame drop";
        }

        public void RefreshViewerList()
        {
            RefreshViewerList(_broadcaster.ConnectedSessions);
        }

        public void RefreshViewerList(IReadOnlyCollection<ViewerSession> sessions)
        {
            if (InvokeRequired)
            {
                try { BeginInvoke(() => RefreshViewerList(sessions)); } catch { }
                return;
            }

            string? selectedId = lvViewers.SelectedItems.Count > 0
                ? lvViewers.SelectedItems[0].Tag as string
                : null;

            lvViewers.BeginUpdate();
            lvViewers.Items.Clear();

            foreach (var session in sessions)
            {
                var item = new ListViewItem($"{session.DeviceIcon} {session.DeviceName}")
                {
                    Tag = session.Id
                };
                item.SubItems.Add(session.RemoteIp);
                item.SubItems.Add(session.BrowserName);
                item.SubItems.Add(session.ViewMode);
                item.SubItems.Add(session.RttMs > 0 ? $"{session.RttMs:0} ms" : "<1 ms");
                item.SubItems.Add(session.FormattedUptime);
                item.SubItems.Add(session.IsAudioMuted ? "Muted" : "Active");

                if (selectedId != null && session.Id == selectedId)
                {
                    item.Selected = true;
                }

                lvViewers.Items.Add(item);
            }

            if (lvViewers.Items.Count > 0 && lvViewers.SelectedItems.Count == 0)
            {
                lvViewers.Items[0].Selected = true;
            }

            lvViewers.EndUpdate();
            UpdateViewerActionButtons();
            SyncFleetMetrics();
        }

        private void UpdateViewerListItem(ViewerSession session)
        {
            foreach (ListViewItem item in lvViewers.Items)
            {
                if (item.Tag as string == session.Id)
                {
                    item.SubItems[3].Text = session.ViewMode;
                    item.SubItems[4].Text = session.RttMs > 0 ? $"{session.RttMs:0} ms" : "<1 ms";
                    item.SubItems[5].Text = session.FormattedUptime;
                    item.SubItems[6].Text = session.IsAudioMuted ? "Muted" : "Active";
                    break;
                }
            }
            UpdateViewerActionButtons();
        }

        private void UpdateViewerActionButtons()
        {
            bool hasSelection = lvViewers.SelectedItems.Count > 0;
            btnMuteSelectedViewer.Enabled = hasSelection;
            btnKickSelectedViewer.Enabled = hasSelection;

            if (hasSelection && lvViewers.SelectedItems[0].Tag is string id)
            {
                var session = _broadcaster.ConnectedSessions.FirstOrDefault(s => s.Id == id);
                if (session != null)
                {
                    btnMuteSelectedViewer.Text = session.IsAudioMuted ? "Unmute Audio" : "Mute Audio";
                }
            }
            else
            {
                btnMuteSelectedViewer.Text = "Mute / Unmute";
            }
        }

        private void btnMuteSelectedViewer_Click(object? sender, EventArgs e)
        {
            if (lvViewers.SelectedItems.Count == 0 || lvViewers.SelectedItems[0].Tag is not string id) return;
            var session = _broadcaster.ConnectedSessions.FirstOrDefault(s => s.Id == id);
            if (session == null) return;

            bool newMuted = !session.IsAudioMuted;
            _broadcaster.SetClientAudioMuted(id, newMuted);
            UpdateViewerListItem(session);
        }

        private async void btnKickSelectedViewer_Click(object? sender, EventArgs e)
        {
            if (lvViewers.SelectedItems.Count == 0 || lvViewers.SelectedItems[0].Tag is not string id) return;
            await _broadcaster.KickClientAsync(id);
            Log($"Disconnected viewer session: {id}");
            RefreshViewerList();
        }

        private async void btnSendAnnouncement_Click(object? sender, EventArgs e)
        {
            var msg = txtAnnouncement.Text.Trim();
            if (string.IsNullOrEmpty(msg)) return;

            int clientCount = _broadcaster.ConnectedClientsCount;
            if (clientCount > 0)
            {
                int notified = await _broadcaster.BroadcastAnnouncementAsync(msg);
                Log($"Broadcast announcement dispatched to {notified} viewer(s): \"{msg}\"");
                SetAnnouncementStatus($"✓ Dispatched to {notified} connected viewer(s) at {DateTime.Now:HH:mm:ss}", clearInput: true);
            }
            else
            {
                SetAnnouncementStatus("No viewers connected to receive announcement.", clearInput: false);
            }
        }

        public void SetAnnouncementStatus(string statusText, bool clearInput = false)
        {
            lblAnnouncementStatus.Text = statusText;
            if (clearInput) txtAnnouncement.Clear();
        }

        public void SyncPairingInfo()
        {
            var url = !string.IsNullOrEmpty(txtURL.Text) ? txtURL.Text : $"http://127.0.0.1:{numPort.Value}";
            try
            {
                var qrImg = QrCodeHelper.GenerateQrCodeBitmap(url, 4);
                var old = picPairingQr.Image;
                picPairingQr.Image = qrImg;
                old?.Dispose();
            }
            catch { }
        }

        internal void ShowQrPopupDialog()
        {
            string url = !string.IsNullOrEmpty(txtURL.Text) ? txtURL.Text : $"http://127.0.0.1:{numPort.Value}";
            using var qrForm = new Form
            {
                Text = "Scan QR Code to Join Stream – ScreenTask 2.0",
                ClientSize = new Size(460, 520),
                MinimumSize = new Size(400, 460),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ShowIcon = false,
                Font = new Font("Segoe UI", 9F)
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(20)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            using var qrBitmap = QrCodeHelper.GenerateQrCodeBitmap(url, 10);
            var picLargeQr = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = qrBitmap
            };

            var lblLargeUrl = new Label
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Consolas", 13F, FontStyle.Bold),
                Margin = new Padding(0, 14, 0, 4),
                Text = url
            };

            string pinText = cbPrivate.Checked && !string.IsNullOrEmpty(txtPassword.Text)
                ? $"🔒 Session PIN: {txtPassword.Text}"
                : "Point your phone or tablet camera at the QR code above";

            var lblSubInfo = new Label
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10F, cbPrivate.Checked ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = cbPrivate.Checked ? Color.FromArgb(20, 90, 170) : SystemColors.GrayText,
                Margin = new Padding(0, 2, 0, 14),
                Text = pinText
            };

            var btnCloseQr = new Button
            {
                Text = "Close",
                Size = new Size(110, 34),
                Anchor = AnchorStyles.None,
                DialogResult = DialogResult.OK,
                UseVisualStyleBackColor = true
            };

            layout.Controls.Add(picLargeQr, 0, 0);
            layout.Controls.Add(lblLargeUrl, 0, 1);
            layout.Controls.Add(lblSubInfo, 0, 2);
            layout.Controls.Add(btnCloseQr, 0, 3);
            qrForm.Controls.Add(layout);
            qrForm.AcceptButton = btnCloseQr;
            qrForm.CancelButton = btnCloseQr;

            qrForm.ShowDialog(this);
        }

        // ═════════════════════════════════════════════════════════════════
        // Audio Diagnostics & Firewall Helper (Tab 2)
        // ═════════════════════════════════════════════════════════════════

        private void UpdateDiagnosticInfo()
        {
            if (comboAudioDevices.SelectedIndex < 0 || comboAudioDevices.SelectedIndex >= _audioDevices.Count) return;
            var desc = _audioDevices[comboAudioDevices.SelectedIndex];
            var fmt = _captureEngine.AudioService.AudioFormat;

            if (fmt != null)
            {
                lblAudioDiagInfo.Text =
                    $"Active: {desc.Name}\n" +
                    $"{(desc.IsInput ? "Microphone" : "Loopback Speakers")} • {fmt.SampleRate:N0} Hz • {fmt.Channels} ch • {fmt.BitsPerSample}-bit";
            }
            else
            {
                lblAudioDiagInfo.Text = $"Selected: {desc.Name} ({(desc.IsInput ? "Microphone" : "System Loopback")})";
            }
        }

        private void btnTestSpeaker_Click(object? sender, EventArgs e)
        {
            try
            {
                System.Media.SystemSounds.Asterisk.Play();
                Log("Played speaker test chime.");
            }
            catch (Exception ex)
            {
                Log($"Speaker test error: {ex.Message}");
            }
        }

        private async void btnTestMic_Click(object? sender, EventArgs e)
        {
            if (_isRecordingMicTest) return;

            btnTestMic.Enabled = false;
            _isRecordingMicTest = true;

            try
            {
                _captureEngine.AudioService.StartMicTest(3);
                for (int i = 3; i > 0; i--)
                {
                    btnTestMic.Text = $"Speak ({i}s)...";
                    await Task.Delay(1000);
                }

                _isRecordingMicTest = false;
                btnTestMic.Text = "Playing...";
                await Task.Delay(3200);
            }
            catch (Exception ex)
            {
                Log($"Mic test error: {ex.Message}");
            }
            finally
            {
                _isRecordingMicTest = false;
                btnTestMic.Text = "Test Mic (3s)";
                btnTestMic.Enabled = true;
            }
        }

        private async void btnFirewallRule_Click(object? sender, EventArgs e)
        {
            int port = (int)numPort.Value;
            btnFirewallRule.Enabled = false;
            try
            {
                bool exists = await FirewallHelper.RuleExistsAsync(port);
                if (exists)
                {
                    Log($"Windows Firewall rule for port {port} is already configured.");
                    if (!SuppressModalDialogsForTesting)
                    {
                        MessageBox.Show(this,
                            $"Windows Defender Firewall already allows inbound connections on TCP port {port}.",
                            "Firewall Rule Active",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    return;
                }

                bool added = await FirewallHelper.TryAddRuleAsync(port);
                if (added)
                {
                    Log($"Added Windows Firewall inbound rule for TCP port {port}.");
                    if (!SuppressModalDialogsForTesting)
                    {
                        MessageBox.Show(this,
                            $"Successfully added Windows Defender Firewall rule for TCP port {port}.",
                            "Firewall Rule Added",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
                else
                {
                    Log($"Firewall rule addition for port {port} was canceled or failed.");
                }
            }
            catch (Exception ex)
            {
                Log($"Firewall helper error: {ex.Message}");
            }
            finally
            {
                btnFirewallRule.Enabled = true;
            }
        }

        // ═════════════════════════════════════════════════════════════════
        // Capture & Network Device Population
        // ═════════════════════════════════════════════════════════════════

        private void PopulateNetworkAdapters()
        {
            var previousIp = comboIPs.SelectedIndex >= 0 && comboIPs.SelectedIndex < _adapters.Count
                ? _adapters[comboIPs.SelectedIndex].IpAddress
                : null;

            comboIPs.Items.Clear();
            _adapters = NetworkHelper.GetAvailableAdapters();

            int selectedIndex = -1;
            for (int i = 0; i < _adapters.Count; i++)
            {
                comboIPs.Items.Add(_adapters[i].DisplayName);
                if (previousIp != null && _adapters[i].IpAddress == previousIp)
                {
                    selectedIndex = i;
                }
                else if (selectedIndex == -1 && _adapters[i].IsRecommended)
                {
                    selectedIndex = i;
                }
            }

            if (comboIPs.Items.Count > 0)
            {
                comboIPs.SelectedIndex = selectedIndex >= 0 ? selectedIndex : 0;
                UpdateUrlPreview();
                SyncPairingInfo();
            }
        }

        private void btnRefreshIPs_Click(object? sender, EventArgs e)
        {
            PopulateNetworkAdapters();
            Log("Refreshed network adapters.");
        }

        private void PopulateCaptureSources()
        {
            var previousSelection = comboScreens.SelectedIndex >= 0 && comboScreens.SelectedIndex < _captureSources.Count
                ? _captureSources[comboScreens.SelectedIndex].DisplayName
                : null;

            comboScreens.Items.Clear();
            _captureSources.Clear();

            var screens = Screen.AllScreens;
            for (int i = 0; i < screens.Length; i++)
            {
                var src = new ScreenCaptureSource(i);
                _captureSources.Add(src);
                comboScreens.Items.Add($"Display: {src.DisplayName}");
            }

            try
            {
                var windows = WindowEnumerator.GetShareableWindows();
                foreach (var win in windows)
                {
                    var src = new WindowCaptureSource(win.Handle, win.Title, win.ProcessName, win.Icon);
                    _captureSources.Add(src);
                    comboScreens.Items.Add($"Window: {win.Title}");
                }
            }
            catch { }

            int newIndex = 0;
            if (previousSelection != null)
            {
                int found = _captureSources.FindIndex(s => s.DisplayName == previousSelection);
                if (found >= 0) newIndex = found;
            }

            if (comboScreens.Items.Count > 0)
            {
                comboScreens.SelectedIndex = newIndex;
            }
        }

        private void comboScreens_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (comboScreens.SelectedIndex >= 0 && comboScreens.SelectedIndex < _captureSources.Count)
            {
                var source = _captureSources[comboScreens.SelectedIndex];
                _captureEngine.SetSource(source);
                monitorPreview.SourceTitle = source.DisplayName;
                monitorPreview.ResolutionText = $"{source.Bounds.Width} × {source.Bounds.Height}";

                // Update preview snapshot immediately even when idle
                try
                {
                    using var snap = source.CaptureFrame(cbCaptureMouse.Checked);
                    if (snap != null)
                    {
                        monitorPreview.UpdatePreview(snap);
                    }
                }
                catch { }

                Log($"Capture source switched to: {source.DisplayName}");
                SaveSettings();
            }
        }

        private void btnRefreshScreens_Click(object? sender, EventArgs e)
        {
            PopulateCaptureSources();
            Log("Refreshed capture sources.");
        }

        private void PopulateAudioDevices()
        {
            var previousId = comboAudioDevices.SelectedIndex >= 0 && comboAudioDevices.SelectedIndex < _audioDevices.Count
                ? _audioDevices[comboAudioDevices.SelectedIndex].Id
                : null;

            comboAudioDevices.Items.Clear();
            _audioDevices = WasapiAudioCaptureService.GetAudioDevices();

            int selectedIndex = 0;
            for (int i = 0; i < _audioDevices.Count; i++)
            {
                comboAudioDevices.Items.Add(_audioDevices[i].Name);
                if (previousId != null && _audioDevices[i].Id == previousId)
                {
                    selectedIndex = i;
                }
            }

            if (comboAudioDevices.Items.Count > 0)
            {
                comboAudioDevices.SelectedIndex = selectedIndex;
            }
        }

        private void btnRefreshAudio_Click(object? sender, EventArgs e)
        {
            PopulateAudioDevices();
            UpdateDiagnosticInfo();
            Log("Refreshed audio devices.");
        }

        private void comboAudioDevices_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (comboAudioDevices.SelectedIndex >= 0 && comboAudioDevices.SelectedIndex < _audioDevices.Count)
            {
                var device = _audioDevices[comboAudioDevices.SelectedIndex];
                _captureEngine.SelectedAudioDevice = device;
                _captureEngine.AudioService.Stop();
                if (cbAudio.Checked)
                {
                    _captureEngine.AudioService.Start(device.Device, device.IsInput);
                }
                UpdateDiagnosticInfo();
                Log($"Audio capture device switched to: {device.Name}");
                SaveSettings();
            }
        }

        // ═════════════════════════════════════════════════════════════════
        // Server & Broadcast Lifecycle
        // ═════════════════════════════════════════════════════════════════

        private async void btnStartServer_Click(object? sender, EventArgs e)
        {
            if (_webServer.IsRunning)
            {
                await StopServerAsync();
            }
            else
            {
                await StartServerAsync();
            }
        }

        private async Task StartServerAsync()
        {
            try
            {
                if (comboIPs.SelectedIndex < 0 || comboIPs.SelectedIndex >= _adapters.Count)
                {
                    if (!SuppressModalDialogsForTesting)
                    {
                        MessageBox.Show(this, "Please select a valid network adapter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }

                var adapter = _adapters[comboIPs.SelectedIndex];
                int port = (int)numPort.Value;
                bool requirePin = cbPrivate.Checked;
                string pin = txtPassword.Text.Trim();

                if (requirePin && string.IsNullOrEmpty(pin))
                {
                    if (!SuppressModalDialogsForTesting)
                    {
                        MessageBox.Show(this, "Please enter a PIN code or uncheck Require PIN Code.", "PIN Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return;
                }

                btnStartServer.Enabled = false;
                btnStartServer.Text = "Starting...";

                _webServer.UpdateSecurity(requirePin, pin);
                await _webServer.StartAsync(adapter.IpAddress, port);

                int targetFps = comboFps.SelectedIndex switch
                {
                    0 => 15,
                    1 => 30,
                    2 => 60,
                    _ => 30
                };
                _captureEngine.TargetFps = targetFps;
                _captureEngine.Start();

                if (cbAudio.Checked && comboAudioDevices.SelectedIndex >= 0 && comboAudioDevices.SelectedIndex < _audioDevices.Count)
                {
                    var audioDev = _audioDevices[comboAudioDevices.SelectedIndex];
                    _captureEngine.AudioService.Start(audioDev.Device, audioDev.IsInput);
                }

                _broadcaster.SetQuality(qualitySlider.Value);

                btnStartServer.Text = "⏹  STOP BROADCAST";
                btnStartServer.BackColor = Color.FromArgb(196, 43, 28);
                btnStartServer.Enabled = true;

                comboIPs.Enabled = false;
                numPort.Enabled = false;
                btnFreeze.Enabled = true;

                monitorPreview.IsLive = true;
                monitorPreview.FpsText = $"{targetFps}.0 FPS";
                monitorPreview.AudioInfoText = cbAudio.Checked ? "Audio: Active" : "Audio: Muted";

                _uptimeStopwatch.Restart();
                _statsTimer.Start();

                UpdateUrlPreview();
                SyncPairingInfo();
                SyncFleetMetrics();

                statusLabelServer.Text = $"● Live: {txtURL.Text}";
                statusLabelAudio.Text = cbAudio.Checked ? "Audio: On" : "Audio: Off";

                Log($"Broadcast started on {txtURL.Text}");
                appNotify.ShowBalloonTip(1000, "ScreenTask 2.0", $"Broadcast live on {txtURL.Text}", ToolTipIcon.Info);
            }
            catch (Exception ex)
            {
                btnStartServer.Enabled = true;
                btnStartServer.Text = "▶  START BROADCAST";
                btnStartServer.BackColor = Color.FromArgb(16, 124, 65);
                if (!SuppressModalDialogsForTesting)
                {
                    MessageBox.Show(this, $"Failed to start server: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Log($"Error starting server: {ex.Message}");
            }
        }

        private async Task StopServerAsync()
        {
            try
            {
                btnStartServer.Enabled = false;
                btnStartServer.Text = "Stopping...";

                _statsTimer.Stop();
                _uptimeStopwatch.Stop();

                await _webServer.StopAsync();
                await _captureEngine.StopAsync();

                btnStartServer.Text = "▶  START BROADCAST";
                btnStartServer.BackColor = Color.FromArgb(16, 124, 65);
                btnStartServer.Enabled = true;

                comboIPs.Enabled = true;
                numPort.Enabled = true;
                btnFreeze.Enabled = false;
                btnFreeze.Text = "Freeze Screen";

                monitorPreview.IsLive = false;
                monitorPreview.FpsText = "0.0 FPS";
                monitorPreview.AudioInfoText = cbAudio.Checked ? "Audio: Active" : "Audio: Muted";

                SyncFleetMetrics();
                RefreshViewerList();

                statusLabelServer.Text = "● Ready to stream";
                statusLabelViewers.Text = "0 viewers";
                statusLabelFps.Text = "0 FPS";
                statusLabelData.Text = "0.0 MB";
                statusLabelUptime.Text = "00:00:00";
                statusLabelAudio.Text = cbAudio.Checked ? "Audio: On" : "Audio: Off";

                Log("Broadcast stopped.");
                appNotify.ShowBalloonTip(1000, "ScreenTask 2.0", "Broadcast stopped.", ToolTipIcon.Info);
            }
            catch (Exception ex)
            {
                Log($"Error stopping server: {ex.Message}");
            }
        }

        private void OnStatsTimerTick(object? sender, EventArgs e)
        {
            int viewers = _broadcaster.ConnectedClientsCount;
            int fps = _broadcaster.CurrentFps;
            long bytes = _broadcaster.TotalBytesTransferred;
            string mbTransferred = (bytes / (1024.0 * 1024.0)).ToString("0.0");
            string uptime = _uptimeStopwatch.Elapsed.ToString(@"hh\:mm\:ss");

            monitorPreview.FpsText = $"{fps}.0 FPS";
            statusLabelFps.Text = $"{fps} FPS";
            statusLabelData.Text = $"{mbTransferred} MB";
            statusLabelUptime.Text = uptime;
            statusLabelViewers.Text = $"{viewers} {(viewers == 1 ? "viewer" : "viewers")}";
            statusLabelAudio.Text = _captureEngine.AudioEnabled ? "Audio: On" : "Audio: Off";

            SyncFleetMetrics();
            if (tabControlMain.SelectedIndex == 1)
            {
                RefreshViewerList();
            }
        }

        // ═════════════════════════════════════════════════════════════════
        // UI Control Handlers
        // ═════════════════════════════════════════════════════════════════

        private void btnCopyUrl_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtURL.Text))
            {
                try
                {
                    Clipboard.SetText(txtURL.Text);
                    btnCopyUrl.Text = "Copied";
                    var timer = new System.Windows.Forms.Timer { Interval = 1200 };
                    timer.Tick += (s, args) =>
                    {
                        btnCopyUrl.Text = "Copy";
                        timer.Stop();
                        timer.Dispose();
                    };
                    timer.Start();
                    Log("Stream URL copied to clipboard.");
                }
                catch { }
            }
        }

        private void btnLaunch_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtURL.Text) && txtURL.Text.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    Process.Start(new ProcessStartInfo(txtURL.Text) { UseShellExecute = true });
                    Log("Opened stream URL in default web browser.");
                }
                catch (Exception ex)
                {
                    Log($"Failed to open browser: {ex.Message}");
                }
            }
        }

        private void btnFreeze_Click(object? sender, EventArgs e)
        {
            _captureEngine.IsFrozen = !_captureEngine.IsFrozen;
            btnFreeze.Text = _captureEngine.IsFrozen ? "Resume Screen" : "Freeze Screen";
            Log(_captureEngine.IsFrozen ? "Stream frame frozen." : "Stream frame resumed.");
        }

        private void cbCaptureMouse_CheckedChanged(object? sender, EventArgs e)
        {
            _captureEngine.CaptureMouse = cbCaptureMouse.Checked;
            menuCaptureShowMouse.Checked = cbCaptureMouse.Checked;
            SaveSettings();
        }

        private void cbAudio_CheckedChanged(object? sender, EventArgs e)
        {
            bool enabled = cbAudio.Checked;
            _captureEngine.SetAudioEnabled(enabled);
            menuCaptureEnableAudio.Checked = enabled;
            btnQuickMute.Text = enabled ? "Mute Audio" : "Unmute Audio";
            statusLabelAudio.Text = enabled ? "Audio: On" : "Audio: Off";
            lblAudioMeterText.Text = enabled ? "Volume: Silent" : "Volume: Muted";
            monitorPreview.AudioInfoText = enabled ? "Audio: Active" : "Audio: Muted";

            comboAudioDevices.Enabled = enabled;
            btnRefreshAudio.Enabled = enabled;
            pbAudioMeter.Enabled = enabled;

            if (enabled)
            {
                if (comboAudioDevices.SelectedIndex >= 0 && comboAudioDevices.SelectedIndex < _audioDevices.Count)
                {
                    var dev = _audioDevices[comboAudioDevices.SelectedIndex];
                    _captureEngine.AudioService.Start(dev.Device, dev.IsInput);
                }
            }
            else
            {
                _captureEngine.AudioService.Stop();
                pbAudioMeter.Value = 0;
            }

            UpdateDiagnosticInfo();
            SaveSettings();
        }

        public void GenerateNewPin()
        {
            var rng = new Random();
            txtPassword.Text = rng.Next(1000, 9999).ToString();
            Log($"Generated new session PIN: {txtPassword.Text}");
        }

        private void cbPrivate_CheckedChanged(object? sender, EventArgs e)
        {
            txtPassword.Enabled = cbPrivate.Checked;
            btnGenPin.Enabled = cbPrivate.Checked;
            btnCopyPin.Enabled = cbPrivate.Checked && !string.IsNullOrEmpty(txtPassword.Text);
            menuNetworkPinProtection.Checked = cbPrivate.Checked;
            menuNetworkGenPin.Enabled = cbPrivate.Checked;
            menuNetworkCopyPin.Enabled = btnCopyPin.Enabled;

            if (cbPrivate.Checked && string.IsNullOrEmpty(txtPassword.Text))
            {
                GenerateNewPin();
            }

            if (_webServer.IsRunning)
            {
                _webServer.UpdateSecurity(cbPrivate.Checked, txtPassword.Text.Trim());
            }

            UpdateUrlPreview();
            SyncPairingInfo();
            SaveSettings();
        }

        private void txtPassword_TextChanged(object? sender, EventArgs e)
        {
            btnCopyPin.Enabled = cbPrivate.Checked && !string.IsNullOrEmpty(txtPassword.Text);
            menuNetworkCopyPin.Enabled = btnCopyPin.Enabled;

            if (_webServer.IsRunning && cbPrivate.Checked)
            {
                _webServer.UpdateSecurity(true, txtPassword.Text.Trim());
            }

            UpdateUrlPreview();
            SyncPairingInfo();
            SaveSettings();
        }

        private void btnGenPin_Click(object? sender, EventArgs e)
        {
            GenerateNewPin();
        }

        private void btnCopyPin_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                try
                {
                    Clipboard.SetText(txtPassword.Text);
                    btnCopyPin.Text = "Copied";
                    var timer = new System.Windows.Forms.Timer { Interval = 1200 };
                    timer.Tick += (s, args) =>
                    {
                        btnCopyPin.Text = "Copy";
                        timer.Stop();
                        timer.Dispose();
                    };
                    timer.Start();
                    Log("Session PIN copied to clipboard.");
                }
                catch { }
            }
        }

        private void qualitySlider_Scroll(object? sender, EventArgs e)
        {
            UpdateQualityLabel();
            _broadcaster.SetQuality(qualitySlider.Value);
            SaveSettings();
        }

        private void UpdateQualityLabel()
        {
            lblQuality.Text = $"{qualitySlider.Value}%";
        }

        private void comboFps_SelectedIndexChanged(object? sender, EventArgs e)
        {
            int targetFps = comboFps.SelectedIndex switch
            {
                0 => 15,
                1 => 30,
                2 => 60,
                _ => 30
            };
            _captureEngine.TargetFps = targetFps;
            SaveSettings();
        }

        private void btnCopyLog_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLog.Text))
            {
                try
                {
                    Clipboard.SetText(txtLog.Text);
                    btnCopyLog.Text = "Copied";
                    var timer = new System.Windows.Forms.Timer { Interval = 1200 };
                    timer.Tick += (s, args) =>
                    {
                        btnCopyLog.Text = "Copy Log";
                        timer.Stop();
                        timer.Dispose();
                    };
                    timer.Start();
                }
                catch { }
            }
        }

        private void UpdateUrlPreview()
        {
            if (comboIPs.SelectedIndex >= 0 && comboIPs.SelectedIndex < _adapters.Count)
            {
                var ip = _adapters[comboIPs.SelectedIndex].IpAddress;
                int port = (int)numPort.Value;
                txtURL.Text = $"http://{ip}:{port}";
            }
            else
            {
                txtURL.Text = $"http://127.0.0.1:{numPort.Value}";
            }
        }

        public void Log(string message)
        {
            if (IsDisposed || txtLog.IsDisposed) return;
            try
            {
                string entry = $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}";
                if (InvokeRequired)
                {
                    if (IsHandleCreated)
                    {
                        BeginInvoke(() =>
                        {
                            if (!txtLog.IsDisposed) txtLog.AppendText(entry);
                        });
                    }
                }
                else
                {
                    txtLog.AppendText(entry);
                }
            }
            catch { }
        }

        internal FeedbackDiagnosticsSnapshot BuildFeedbackDiagnosticsSnapshot()
        {
            string sourceType = "Screen";
            if (comboScreens.SelectedIndex >= 0 && comboScreens.SelectedIndex < _captureSources.Count)
            {
                sourceType = _captureSources[comboScreens.SelectedIndex] is WindowCaptureSource
                    ? "Application Window"
                    : "Display Monitor";
            }

            string audioType = "None";
            if (comboAudioDevices.SelectedIndex >= 0 && comboAudioDevices.SelectedIndex < _audioDevices.Count)
            {
                audioType = _audioDevices[comboAudioDevices.SelectedIndex].IsInput
                    ? "Microphone Input"
                    : "System Output Loopback";
            }

            int fps = comboFps.SelectedIndex switch
            {
                0 => 15,
                1 => 30,
                2 => 60,
                _ => 30
            };

            string[] allLines = txtLog.Text.Split(["\r\n", "\n"], StringSplitOptions.RemoveEmptyEntries);
            string recentLogs = string.Join(Environment.NewLine, allLines.TakeLast(20));

            return new FeedbackDiagnosticsSnapshot
            {
                AppVersion = Constants.AppVersion,
                CaptureSourceType = sourceType,
                TargetFps = fps,
                ImageQuality = qualitySlider.Value,
                IsAudioEnabled = cbAudio.Checked,
                AudioDeviceType = audioType,
                IsServerRunning = _webServer.IsRunning,
                IsPinProtected = cbPrivate.Checked,
                ActiveViewers = _broadcaster.ConnectedClientsCount,
                RecentLogLines = recentLogs
            };
        }

        internal void ShowFeedbackDialog(FeedbackType initialType = FeedbackType.BugReport)
        {
            using var dialog = new FeedbackForm(BuildFeedbackDiagnosticsSnapshot(), initialType);
            dialog.ShowDialog(this);
        }

        private void ShowAboutDialog()
        {
            MessageBox.Show(this,
                $"ScreenTask 2.0  (v{Constants.AppVersion})\n\n" +
                "Instant, zero-configuration local screen sharing\n" +
                "over Wi-Fi, Ethernet, or mobile hotspot.\n\n" +
                "Author:   Eslam Hamouda\n" +
                "License:  GNU General Public License v3.0\n" +
                "Website:  screentask.eslamx.com\n" +
                $"Support:  {Constants.SupportEmail}\n" +
                "GitHub:   github.com/EslaMx7/ScreenTask",
                "About ScreenTask 2.0",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ═════════════════════════════════════════════════════════════════
        // Settings Persistence
        // ═════════════════════════════════════════════════════════════════

        public string GetSettingsFilePath()
        {
            if (!string.IsNullOrEmpty(CustomSettingsFilePath))
            {
                return CustomSettingsFilePath;
            }

            bool isUnitTest = AppDomain.CurrentDomain.GetAssemblies()
                .Any(a => a.GetName().Name?.StartsWith("xunit", StringComparison.OrdinalIgnoreCase) == true);
            if (isUnitTest)
            {
                return Path.Combine(Path.GetTempPath(), $"screentask_test_inst_{_instanceId}.xml");
            }

            string appDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ScreenTask");
            string appDataPath = Path.Combine(appDataDir, "settings.xml");
            if (File.Exists(appDataPath))
            {
                return appDataPath;
            }

            string portablePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.AppSettingsFileName);
            string portableMarker = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "portable.txt");
            if (File.Exists(portableMarker) || File.Exists(portablePath))
            {
                return portablePath;
            }

            string cwdPath = Path.GetFullPath(Constants.AppSettingsFileName);
            if (File.Exists(cwdPath))
            {
                try
                {
                    Directory.CreateDirectory(appDataDir);
                    File.Copy(cwdPath, appDataPath, overwrite: true);
                    return appDataPath;
                }
                catch
                {
                    return cwdPath;
                }
            }

            try
            {
                Directory.CreateDirectory(appDataDir);
                return appDataPath;
            }
            catch
            {
                return portablePath;
            }
        }

        public void LoadSettings()
        {
            bool previousLoadingState = _isLoadingSettings;
            _isLoadingSettings = true;
            try
            {
                string filePath = GetSettingsFilePath();
                if (File.Exists(filePath))
                {
                    var serializer = new XmlSerializer(typeof(AppSettings));
                    using var file = File.OpenRead(filePath);
                    _currentSettings = (AppSettings)serializer.Deserialize(file)!;

                    cbPrivate.Checked = _currentSettings.IsPrivateSession;
                    cbCaptureMouse.Checked = _currentSettings.IsShowMouseEnabled;
                    // Presenter must explicitly opt-in to audio streaming each session
                    _currentSettings.IsAudioEnabled = false;
                    cbAudio.Checked = false;
                    comboAudioDevices.Enabled = false;
                    btnRefreshAudio.Enabled = false;
                    pbAudioMeter.Enabled = false;
                    cbAutoStart.Checked = _currentSettings.IsAutoStartServerEnabled;
                    cbMiniStart.Checked = _currentSettings.IsStartMinimizedEnabled;
                    cbCloseToTray.Checked = _currentSettings.IsCloseToTrayEnabled;
                    txtPassword.Text = _currentSettings.PinCode;
                    txtPassword.Enabled = cbPrivate.Checked;
                    numPort.Value = Math.Clamp(_currentSettings.Port, 1, 65535);
                    qualitySlider.Value = Math.Clamp(_currentSettings.ImageQuality, 20, 100);
                    UpdateQualityLabel();

                    int fpsIdx = _currentSettings.TargetFps switch
                    {
                        15 => 0,
                        30 => 1,
                        60 => 2,
                        _ => 1
                    };
                    comboFps.SelectedIndex = fpsIdx;

                    if (!string.IsNullOrEmpty(_currentSettings.IP))
                    {
                        var match = _adapters.FirstOrDefault(a => a.IpAddress == _currentSettings.IP);
                        if (match != null)
                        {
                            comboIPs.SelectedIndex = _adapters.IndexOf(match);
                        }
                    }

                    if (_currentSettings.SelectedScreenIndex >= 0 && _currentSettings.SelectedScreenIndex < comboScreens.Items.Count)
                    {
                        comboScreens.SelectedIndex = _currentSettings.SelectedScreenIndex;
                    }

                    if (!string.IsNullOrEmpty(_currentSettings.SelectedAudioDeviceId))
                    {
                        int devIdx = _audioDevices.FindIndex(d => d.Id == _currentSettings.SelectedAudioDeviceId);
                        if (devIdx >= 0 && devIdx < comboAudioDevices.Items.Count)
                        {
                            comboAudioDevices.SelectedIndex = devIdx;
                        }
                    }
                }
                else
                {
                    cbAudio.Checked = false;
                    comboAudioDevices.Enabled = false;
                    btnRefreshAudio.Enabled = false;
                    pbAudioMeter.Enabled = false;
                    comboFps.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                Log($"Could not load settings: {ex.Message}");
            }
            finally
            {
                _isLoadingSettings = previousLoadingState;
            }
        }

        public void SaveSettings()
        {
            if (_isLoadingSettings) return;

            try
            {
                _currentSettings.Port = (int)numPort.Value;
                _currentSettings.PinCode = txtPassword.Text;
                _currentSettings.IsShowMouseEnabled = cbCaptureMouse.Checked;
                _currentSettings.IsAudioEnabled = cbAudio.Checked;
                _currentSettings.IsPrivateSession = cbPrivate.Checked;
                _currentSettings.IsAutoStartServerEnabled = cbAutoStart.Checked;
                _currentSettings.IsStartMinimizedEnabled = cbMiniStart.Checked;
                _currentSettings.IsCloseToTrayEnabled = cbCloseToTray.Checked;
                _currentSettings.ImageQuality = qualitySlider.Value;
                _currentSettings.TargetFps = comboFps.SelectedIndex switch
                {
                    0 => 15,
                    1 => 30,
                    2 => 60,
                    _ => 30
                };

                if (comboIPs.SelectedIndex >= 0 && comboIPs.SelectedIndex < _adapters.Count)
                {
                    _currentSettings.IP = _adapters[comboIPs.SelectedIndex].IpAddress;
                }

                if (comboScreens.SelectedIndex >= 0 && comboScreens.SelectedIndex < _captureSources.Count)
                {
                    _currentSettings.SelectedScreenIndex = comboScreens.SelectedIndex;
                }

                if (comboAudioDevices.SelectedIndex >= 0 && comboAudioDevices.SelectedIndex < _audioDevices.Count)
                {
                    _currentSettings.SelectedAudioDeviceId = _audioDevices[comboAudioDevices.SelectedIndex].Id ?? string.Empty;
                }

                string filePath = GetSettingsFilePath();
                string? dir = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                var serializer = new XmlSerializer(typeof(AppSettings));
                string tempFile = filePath + $".tmp_{Guid.NewGuid():N}";
                using (var file = new FileStream(tempFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    serializer.Serialize(file, _currentSettings);
                }
                File.Move(tempFile, filePath, overwrite: true);
            }
            catch
            {
                try
                {
                    string filePath = GetSettingsFilePath();
                    var serializer = new XmlSerializer(typeof(AppSettings));
                    using var file = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
                    serializer.Serialize(file, _currentSettings);
                }
                catch (Exception ex)
                {
                    Log($"Could not save settings: {ex.Message}");
                }
            }
        }

        // ═════════════════════════════════════════════════════════════════
        // Tray, Updates & Graceful Shutdown
        // ═════════════════════════════════════════════════════════════════

        private void RestoreFromTray()
        {
            Show();
            WindowState = FormWindowState.Normal;
            BringToFront();
            Focus();
        }

        private void appNotify_Click(object? sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                RestoreFromTray();
            }
        }

        private void statusLabelCopyright_Click(object? sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(Constants.WebsiteUrl) { UseShellExecute = true });
            }
            catch { }
        }

        private async Task CheckForUpdatesAsync(bool manualCheck = false)
        {
            try
            {
                if (_httpClient.DefaultRequestHeaders.UserAgent.Count == 0)
                {
                    _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(Constants.UserAgent);
                }

                var response = await _httpClient.GetStringAsync(Constants.GitHubApiLatestReleaseUrl);
                using var doc = JsonDocument.Parse(response);
                if (doc.RootElement.TryGetProperty("tag_name", out var tagElem))
                {
                    string latestTag = tagElem.GetString() ?? "";
                    string releaseUrl = Constants.GitHubReleasesUrl;
                    if (doc.RootElement.TryGetProperty("html_url", out var urlElem) &&
                        !string.IsNullOrWhiteSpace(urlElem.GetString()))
                    {
                        releaseUrl = urlElem.GetString()!;
                    }

                    if (!string.IsNullOrEmpty(latestTag) &&
                        TryParseNormalizedVersion(latestTag, out var latest) &&
                        TryParseNormalizedVersion(Constants.AppVersion, out var curr) &&
                        latest > curr)
                    {
                        if (!SuppressModalDialogsForTesting && IsHandleCreated && !IsDisposed)
                        {
                            BeginInvoke(() =>
                            {
                                if (MessageBox.Show(this,
                                    $"A newer version of ScreenTask ({latestTag}) is available!\nWould you like to open the download page?",
                                    "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    try { Process.Start(new ProcessStartInfo(releaseUrl) { UseShellExecute = true }); } catch { }
                                }
                            });
                        }
                    }
                    else if (manualCheck && IsHandleCreated && !IsDisposed)
                    {
                        BeginInvoke(() =>
                        {
                            MessageBox.Show(this,
                                $"You are running the latest version of ScreenTask (v{Constants.AppVersion}).",
                                "Up to Date",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        });
                    }
                }
            }
            catch
            {
                if (manualCheck && IsHandleCreated && !IsDisposed)
                {
                    BeginInvoke(() =>
                    {
                        MessageBox.Show(this,
                            "Could not check for updates right now. Please verify your internet connection or visit github.com/EslaMx7/ScreenTask/releases.",
                            "Update Check",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    });
                }
            }
        }

        internal static bool TryParseNormalizedVersion(string? versionText, out Version normalized)
        {
            normalized = new Version(0, 0, 0, 0);
            if (string.IsNullOrWhiteSpace(versionText))
            {
                return false;
            }

            string cleaned = versionText.Trim().TrimStart('v', 'V');
            int suffixIdx = cleaned.IndexOfAny(['-', '+']);
            if (suffixIdx > 0)
            {
                cleaned = cleaned[..suffixIdx];
            }

            if (!Version.TryParse(cleaned, out var parsed))
            {
                return false;
            }

            normalized = new Version(
                parsed.Major,
                parsed.Minor,
                parsed.Build < 0 ? 0 : parsed.Build,
                parsed.Revision < 0 ? 0 : parsed.Revision);
            return true;
        }

        private void frmMain_Resize(object? sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized && cbCloseToTray.Checked)
            {
                Hide();
            }
        }

        public async Task PerformGracefulShutdownAsync()
        {
            try
            {
                _statsTimer.Stop();
                _statsTimer.Dispose();
                _deviceChangeTimer.Stop();
                _deviceChangeTimer.Dispose();
            }
            catch { }

            try
            {
                _broadcaster?.Dispose();
            }
            catch { }

            if (_webServer != null && _webServer.IsRunning)
            {
                try
                {
                    var stopWebTask = Task.Run(async () => await _webServer.StopAsync());
                    await Task.WhenAny(stopWebTask, Task.Delay(2000));
                }
                catch { }
            }

            if (_captureEngine != null)
            {
                try
                {
                    var stopCaptureTask = Task.Run(async () =>
                    {
                        try { await _captureEngine.StopAsync(); } catch { }
                        try { _captureEngine.Dispose(); } catch { }
                    });
                    await Task.WhenAny(stopCaptureTask, Task.Delay(2000));
                }
                catch { }
            }

            try
            {
                SaveSettings();
            }
            catch { }

            try
            {
                appNotify?.Dispose();
            }
            catch { }
        }

        private async void frmMain_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (_isGracefulShutdownComplete)
            {
                return;
            }

            bool isBroadcastActive = (_webServer != null && _webServer.IsRunning) ||
                                     (_captureEngine != null && _captureEngine.IsRunning);

            if (!_isExitingExplicitly && cbCloseToTray.Checked && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
                Hide();
                string balloonMsg = isBroadcastActive
                    ? "Broadcast continues streaming in system tray. Double-click icon to restore."
                    : "Minimized to system tray. Double-click icon to restore.";
                appNotify.ShowBalloonTip(1000, "ScreenTask 2.0", balloonMsg, ToolTipIcon.Info);
                return;
            }

            if (_isShuttingDown)
            {
                e.Cancel = true;
                return;
            }

            if (isBroadcastActive && !SuppressModalDialogsForTesting && e.CloseReason == CloseReason.UserClosing)
            {
                IWin32Window? dialogOwner = Visible ? this : null;
                var dialogResult = MessageBox.Show(
                    dialogOwner,
                    "ScreenTask is currently broadcasting live.\n\nDo you want to stop the broadcast and exit?",
                    "Active Broadcast – Exit Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (dialogResult != DialogResult.Yes)
                {
                    e.Cancel = true;
                    _isExitingExplicitly = false;
                    return;
                }
            }

            e.Cancel = true;
            _isShuttingDown = true;
            _isExitingExplicitly = true;

            try
            {
                Hide();
                appNotify.Visible = false;
            }
            catch { }

            await PerformGracefulShutdownAsync();

            _isGracefulShutdownComplete = true;

            try
            {
                Close();
            }
            catch
            {
                Application.Exit();
            }
        }
    }
}
