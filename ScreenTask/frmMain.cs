using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
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
        private StreamBroadcaster _broadcaster;
        private ScreenCaptureEngine _captureEngine;
        private EmbeddedWebServer _webServer;
        private List<NetworkAdapterInfo> _adapters = new();
        private List<ICaptureSource> _captureSources = new();
        private List<AudioDeviceDescriptor> _audioDevices = new();
        private AppSettings _currentSettings = new();
        private readonly System.Windows.Forms.Timer _statsTimer = new();
        private readonly System.Windows.Forms.Timer _deviceChangeTimer = new() { Interval = 600 };
        private readonly System.Windows.Forms.Timer _thumbnailTimer = new() { Interval = 2500 };
        private readonly Stopwatch _uptimeStopwatch = new();
        private readonly HttpClient _httpClient = new();
        private const int WM_DEVICECHANGE = 0x0219;

        private bool _isExitingExplicitly = false;
        private bool _isLoadingSettings = false;
        private bool _isShuttingDown = false;
        private bool _isGracefulShutdownComplete = false;
        private readonly string _instanceId = Guid.NewGuid().ToString("N");
        private int _currentTabIndex = 0;
        private bool _isExpandedPreview = false;
        private bool _isUpdatingThumbnails = false;
        private bool _isRecordingMicTest = false;

        // View 1 (Clients) session tracking cards
        private readonly Dictionary<string, Panel> _sessionCardPanels = new();

        // View 2 (Capture Sources) visual cards
        private readonly List<SourceCard> _displayCards = new();
        private readonly List<SourceCard> _windowCards = new();
        private SourceCard? _selectedCard;

        public StreamBroadcaster Broadcaster => _broadcaster;
        public EmbeddedWebServer WebServer => _webServer;
        public ScreenCaptureEngine CaptureEngine => _captureEngine;
        public int SelectedTabIndex => _currentTabIndex;

        // Visual Source Picker Properties
        public IReadOnlyList<SourceCard> DisplayCards => _displayCards;
        public IReadOnlyList<SourceCard> WindowCards => _windowCards;
        public SourceCard? SelectedCard => _selectedCard;
        public ICaptureSource? SelectedSource => _selectedCard?.Source ?? (_captureSources.Count > 0 && comboScreens.SelectedIndex >= 0 && comboScreens.SelectedIndex < _captureSources.Count ? _captureSources[comboScreens.SelectedIndex] : null);

        // Status Strip properties for testing
        public StatusStrip StatusStrip => statusStripMain;
        public ToolStripStatusLabel StatusLabelServer => statusLabelServer;
        public ToolStripStatusLabel StatusLabelViewers => statusLabelViewers;
        public ToolStripStatusLabel StatusLabelFps => statusLabelFps;
        public ToolStripStatusLabel StatusLabelData => statusLabelData;
        public ToolStripStatusLabel StatusLabelUptime => statusLabelUptime;
        public ToolStripStatusLabel StatusLabelAudio => statusLabelAudio;
        public ToolStripStatusLabel StatusLabelCopyright => statusLabelCopyright;

        public bool SuppressModalDialogsForTesting { get; set; } = false;
        public AppSettings CurrentSettings => _currentSettings;
        public string? CustomSettingsFilePath { get; set; }

        public frmMain()
        {
            InitializeComponent();

            // Core 2.0 Engine Initialization
            _broadcaster = new StreamBroadcaster(80);
            _captureEngine = new ScreenCaptureEngine(_broadcaster);
            _webServer = new EmbeddedWebServer(_broadcaster, _captureEngine);

            _statsTimer.Interval = 1000;
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

            _thumbnailTimer.Tick += (s, e) =>
            {
                if (_currentTabIndex == 2 && panelViewCapture.Visible)
                {
                    TriggerThumbnailUpdate();
                }
            };

            WireEvents();
            WireMenuEvents();
            InitializeTrayContextMenu();
        }

        private void WireEvents()
        {
            this.Load += frmMain_Load;
            this.FormClosing += frmMain_FormClosing;
            this.Resize += frmMain_Resize;

            // Navigation Tabs
            btnNavBroadcast.Click += (s, e) => SelectTab(0);
            btnNavClients.Click += (s, e) => SelectTab(1);
            btnNavCapture.Click += (s, e) => SelectTab(2);
            btnNavNetwork.Click += (s, e) => SelectTab(3);
            btnNavPrefs.Click += (s, e) => SelectTab(4);

            // Shortcut Jump Buttons
            btnSwitchSource.Click += (s, e) => SelectTab(2);
            btnAudioSetup.Click += (s, e) => SelectTab(4);
            btnNetworkSettings.Click += (s, e) => SelectTab(3);

            panelContentContainer.Resize += (s, e) => UpdateLayout();

            // Broadcaster real session tracking events
            _broadcaster.SessionConnected += (session) =>
            {
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    try
                    {
                        this.BeginInvoke(() =>
                        {
                            SyncFleetMetrics();
                            UpdateSessionCards();
                        });
                    }
                    catch { }
                }
            };

            _broadcaster.SessionDisconnected += (id) =>
            {
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    try
                    {
                        this.BeginInvoke(() =>
                        {
                            SyncFleetMetrics();
                            UpdateSessionCards();
                        });
                    }
                    catch { }
                }
            };

            _broadcaster.SessionUpdated += (session) =>
            {
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    try
                    {
                        this.BeginInvoke(() =>
                        {
                            SyncFleetMetrics();
                            RefreshSessionCardData(session);
                        });
                    }
                    catch { }
                }
            };

            // Quick Mute Button in Broadcast Controls
            btnQuickMute.Click += (s, e) =>
            {
                cbAudio.Checked = !cbAudio.Checked;
            };

            // Broadcast Controls
            btnStartServer.Click += btnStartServer_Click;
            btnLaunch.Click += btnLaunch_Click;
            btnCopyUrl.Click += btnCopyUrl_Click;
            btnQr.Click += btnQr_Click;
            btnPopout.Click += (s, e) => ToggleExpandPreview();
            btnFreeze.Click += btnFreeze_Click;
            comboScreens.SelectedIndexChanged += comboScreens_SelectedIndexChanged;
            btnRefreshScreens.Click += btnRefreshScreens_Click;

            // View 1 (Connected Viewers) events
            btnSendAnnouncement.Click += async (s, e) =>
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
                    SetAnnouncementStatus("⚠️ No viewers connected to receive announcement.", clearInput: false);
                }
            };

            btnPairingCopy.Click += (s, e) =>
            {
                if (!string.IsNullOrEmpty(txtPairingUrl.Text))
                {
                    try
                    {
                        Clipboard.SetText(txtPairingUrl.Text);
                        btnPairingCopy.Text = "✓ Copied";
                        var timer = new System.Windows.Forms.Timer { Interval = 1200 };
                        timer.Tick += (s2, e2) =>
                        {
                            btnPairingCopy.Text = "📋 Copy";
                            timer.Stop();
                            timer.Dispose();
                        };
                        timer.Start();
                        Log("Direct stream URL copied to clipboard.");
                    }
                    catch { }
                }
            };

            btnPairingRegen.Click += (s, e) =>
            {
                btnGenPin.PerformClick();
                var newPinText = cbPrivate.Checked ? $"PIN Code: {txtPassword.Text}" : "PIN: Not Required";
                lblPairingPin.Text = newPinText;
            };

            // View 2 (Video Sources) events
            btnRefreshSourcesTab.Click += (s, e) =>
            {
                PopulateCaptureSources();
                PopulateSourceCards();
            };
            btnSelectSource.Click += (s, e) => SelectTab(0);
            cbCaptureMouse.CheckedChanged += cbCaptureMouse_CheckedChanged;

            // View 3 (Network) events
            comboIPs.SelectedIndexChanged += (s, e) =>
            {
                UpdateUrlPreview();
                UpdateNetworkPreviewUrl();
                SaveSettings();
            };
            btnRefreshIPs.Click += btnRefreshIPs_Click;
            numPort.ValueChanged += (s, e) =>
            {
                UpdateUrlPreview();
                UpdateNetworkPreviewUrl();
                SaveSettings();
            };
            cbPrivate.CheckedChanged += cbPrivate_CheckedChanged;
            txtPassword.TextChanged += txtPassword_TextChanged;
            btnGenPin.Click += btnGenPin_Click;
            btnCopyPin.Click += btnCopyPin_Click;

            // View 4 (Preferences & Audio) events
            cbAutoStart.CheckedChanged += (s, e) => SaveSettings();
            cbMiniStart.CheckedChanged += (s, e) => SaveSettings();
            cbCloseToTray.CheckedChanged += (s, e) => SaveSettings();
            comboFps.SelectedIndexChanged += comboFps_SelectedIndexChanged;
            qualitySlider.Scroll += qualitySlider_Scroll;

            cbAudio.CheckedChanged += cbAudio_CheckedChanged;
            comboAudioDevices.SelectedIndexChanged += comboAudioDevices_SelectedIndexChanged;
            btnRefreshAudio.Click += btnRefreshAudio_Click;
            btnTestSpeaker.Click += btnTestSpeaker_Click;
            btnTestMic.Click += btnTestMic_Click;
            btnTestAudio.Click += btnTestAudio_Click;
            btnAudioDiag.Click += btnAudioDiag_Click;

            btnClearLog.Click += (s, e) => txtLog.Clear();
            btnCopyLog.Click += btnCopyLog_Click;

            // Live Preview & VU Meter
            _captureEngine.PreviewFrameAvailable += (bmp) =>
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
                            if (pbAudioDiagMeter != null && pbAudioDiagMeter.IsHandleCreated && !pbAudioDiagMeter.IsDisposed)
                            {
                                pbAudioDiagMeter.Value = val;
                            }

                            if (val > 0)
                            {
                                int db = (int)(20 * Math.Log10(Math.Max(level, 0.001f)));
                                lblAudioMeterText.Text = $"Sound Volume: {db} dB";
                                if (lblAudioDiagMeterText != null) lblAudioDiagMeterText.Text = $"Input Volume: {db} dB";
                            }
                            else
                            {
                                lblAudioMeterText.Text = "Sound Volume: Silent";
                                if (lblAudioDiagMeterText != null) lblAudioDiagMeterText.Text = "Input Volume: Silent";
                            }
                        });
                    }
                    catch { }
                }
            };

            statusLabelCopyright.Click += statusLabelCopyright_Click;
            appNotify.Click += appNotify_Click;
            appNotify.MouseDoubleClick += (s, e) => RestoreFromTray();
        }

        private void InitializeTrayContextMenu()
        {
            trayContextMenu.Items.Clear();

            var header = new ToolStripMenuItem("ScreenTask 2.0") { Enabled = false };
            var itemToggle = new ToolStripMenuItem("▶ Start Broadcast", null, (s, e) => btnStartServer.PerformClick());
            var itemMute = new ToolStripMenuItem("🔇 Mute Audio", null, (s, e) => cbAudio.Checked = !cbAudio.Checked);
            var itemCopy = new ToolStripMenuItem("📋 Copy Stream Link", null, (s, e) => btnCopyUrl.PerformClick());
            var itemQr = new ToolStripMenuItem("📱 Show QR Code", null, (s, e) => btnQr.PerformClick());
            var itemOpen = new ToolStripMenuItem("🌐 Open in Browser", null, (s, e) => btnLaunch.PerformClick());
            var itemRestore = new ToolStripMenuItem("🗖 Open ScreenTask", null, (s, e) => RestoreFromTray());
            var itemExit = new ToolStripMenuItem("❌ Exit ScreenTask", null, (s, e) =>
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
                itemQr,
                itemOpen,
                new ToolStripSeparator(),
                itemRestore,
                itemExit
            });

            trayContextMenu.Opening += (s, e) =>
            {
                header.Text = _webServer.IsRunning ? "ScreenTask 2.0 – ● Live" : "ScreenTask 2.0 – ● Ready";
                itemToggle.Text = _webServer.IsRunning ? "⏹ Stop Broadcast" : "▶ Start Broadcast";
                itemMute.Text = cbAudio.Checked ? "🔇 Mute Audio" : "🔊 Unmute Audio";
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

        private void WireMenuEvents()
        {
            // File menu
            menuFileToggleBroadcast.Click += btnStartServer_Click;
            menuFileCopyLink.Click += btnCopyUrl_Click;
            menuFileShowQr.Click += btnQr_Click;
            menuFileOpenBrowser.Click += btnLaunch_Click;
            menuFileExit.Click += (s, e) =>
            {
                _isExitingExplicitly = true;
                Close();
            };

            // View menu
            menuViewBroadcast.Click += (s, e) => SelectTab(0);
            menuViewViewers.Click += (s, e) => SelectTab(1);
            menuViewCapture.Click += (s, e) => SelectTab(2);
            menuViewNetwork.Click += (s, e) => SelectTab(3);
            menuViewPrefs.Click += (s, e) => SelectTab(4);

            // Capture menu
            menuCaptureRefreshSources.Click += btnRefreshScreens_Click;
            menuCaptureShowMouse.Click += (s, e) =>
            {
                cbCaptureMouse.Checked = menuCaptureShowMouse.Checked;
            };
            menuCaptureEnableAudio.Click += (s, e) =>
            {
                cbAudio.Checked = menuCaptureEnableAudio.Checked;
            };
            menuCaptureAudioDiag.Click += (s, e) => SelectTab(4);

            // Network menu
            menuNetworkRefreshAdapters.Click += btnRefreshIPs_Click;
            menuNetworkPinProtection.Click += (s, e) =>
            {
                cbPrivate.Checked = menuNetworkPinProtection.Checked;
            };
            menuNetworkGenPin.Click += btnGenPin_Click;
            menuNetworkCopyPin.Click += btnCopyPin_Click;

            // Help menu
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
            bool isLive = _webServer?.IsRunning == true;

            menuFileToggleBroadcast.Text = isLive ? "⏹ Stop Broadcast" : "▶ Start Broadcast";
            menuFileCopyLink.Enabled = !string.IsNullOrEmpty(txtURL.Text);
            menuFileOpenBrowser.Enabled = txtURL.Text.StartsWith("http");

            menuCaptureShowMouse.Checked = cbCaptureMouse.Checked;
            menuCaptureEnableAudio.Checked = cbAudio.Checked;
            menuCaptureAudioDiag.Enabled = cbAudio.Checked;

            menuNetworkPinProtection.Checked = cbPrivate.Checked;
            menuNetworkGenPin.Enabled = cbPrivate.Checked;
            menuNetworkCopyPin.Enabled = cbPrivate.Checked && !string.IsNullOrEmpty(txtPassword.Text);
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
                "GitHub:   github.com/EslaMx7/ScreenTask",
                "About ScreenTask 2.0",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
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
                UpdateNetworkPreviewUrl();
                SyncPairingInfo();
                SyncFleetMetrics();
                UpdateSessionCards();
                SelectTab(0);
                UpdateLayout();
            }
            finally
            {
                _isLoadingSettings = false;
            }

            statusLabelServer.Text = "● Ready to stream";
            statusLabelViewers.Text = "👥 0 viewers";
            statusLabelFps.Text = "🎞️ 0 FPS";
            statusLabelData.Text = "📊 0.0 MB";
            statusLabelUptime.Text = "⏱️ 00:00:00";
            statusLabelAudio.Text = cbAudio.Checked ? "🔊 Audio: On" : "🔇 Audio: Off";
            statusLabelCopyright.Text = "© 2014-2026 Eslam Hamouda";

            if (_currentSettings.IsStartMinimizedEnabled)
            {
                this.WindowState = FormWindowState.Minimized;
            }

            if (_currentSettings.IsAutoStartServerEnabled)
            {
                btnStartServer_Click(this, EventArgs.Empty);
            }

            _ = CheckForUpdatesAsync();
            Log("ScreenTask 2.0 initialized. Ready.");
        }

        public void SelectTab(int index)
        {
            _currentTabIndex = index;
            UpdateNavButtons(index);

            panelViewBroadcast.Visible = (index == 0);
            panelViewClients.Visible = (index == 1);
            panelViewCapture.Visible = (index == 2);
            panelViewNetwork.Visible = (index == 3);
            panelViewPrefs.Visible = (index == 4);

            switch (index)
            {
                case 0:
                    break;
                case 1:
                    SyncPairingInfo();
                    SyncFleetMetrics();
                    UpdateSessionCards();
                    break;
                case 2:
                    PopulateSourceCards();
                    if (!_thumbnailTimer.Enabled) _thumbnailTimer.Start();
                    break;
                case 3:
                    UpdateNetworkPreviewUrl();
                    break;
                case 4:
                    UpdateQualityLabel();
                    UpdateDiagnosticInfo();
                    break;
            }

            if (index != 2 && _thumbnailTimer.Enabled)
            {
                _thumbnailTimer.Stop();
            }

            UpdateLayout();
        }

        public void OpenView(int index)
        {
            SelectTab(index);
        }

        private void UpdateNavButtons(int activeIndex)
        {
            Button[] navButtons = [btnNavBroadcast, btnNavClients, btnNavCapture, btnNavNetwork, btnNavPrefs];
            for (int i = 0; i < navButtons.Length; i++)
            {
                bool isActive = (i == activeIndex);
                navButtons[i].Font = new Font("Segoe UI", 9F, isActive ? FontStyle.Bold : FontStyle.Regular);
                navButtons[i].BackColor = isActive ? SystemColors.Window : SystemColors.ControlLight;
            }
        }

        // ═════════════════════════════════════════════════════════════════
        // View 1: Connected Viewers & Fleet Methods
        // ═════════════════════════════════════════════════════════════════

        public void UpdateMetricViewers(int activeCount, int capacity = 32)
        {
            lblMetricViewersVal.Text = $"{activeCount} / {capacity} Active";
            pbViewerCapacity.Maximum = capacity;
            pbViewerCapacity.Value = Math.Clamp(activeCount, 0, capacity);
            lblFleetHeader.Text = $"Live Client Sessions ({activeCount}):";
            statusLabelViewers.Text = $"👥 {activeCount} {(activeCount == 1 ? "viewer" : "viewers")}";
        }

        public void UpdateMetricLatency(double avgRttMs, double frameDropPercentage)
        {
            if (avgRttMs > 0)
            {
                lblMetricLatencyVal.Text = $"{avgRttMs:0.0} ms Avg";
                lblMetricLatencySub.Text = $"<50ms pipeline • {frameDropPercentage:0.0}% frame drop";
            }
            else
            {
                lblMetricLatencyVal.Text = "<1.0 ms Avg";
                lblMetricLatencySub.Text = "<50ms pipeline • 0.0% frame drop";
            }
        }

        public void UpdateMetricSync(bool isRunning, int droppedFrames = 0, double driftMs = 0.0)
        {
            if (isRunning)
            {
                lblMetricSyncVal.Text = "100% Locked";
                lblMetricSyncSub.Text = $"WASAPI + Display ({droppedFrames} drop, ±{driftMs:0.0}ms drift)";
            }
            else
            {
                lblMetricSyncVal.Text = "Standby";
                lblMetricSyncSub.Text = "Audio/Video pipeline idle";
            }
        }

        public void UpdatePairingPass(Image? qrImage, string url, string pinText, string adapterInfo)
        {
            if (qrImage != null) picPairingQr.Image = qrImage;
            txtPairingUrl.Text = url;
            lblPairingPin.Text = pinText;
            lblZeroConf.Text = adapterInfo;
        }

        public void SetAnnouncementStatus(string statusText, bool clearInput = false)
        {
            lblAnnouncementStatus.Text = statusText;
            if (clearInput) txtAnnouncement.Clear();
        }

        public void SyncFleetMetrics()
        {
            var sessions = _broadcaster.ConnectedSessions;
            UpdateMetricViewers(sessions.Count, 32);
            UpdateMetricLatency(_broadcaster.AverageFleetLatencyMs, _broadcaster.FrameDropPercentage);
            UpdateMetricSync(_captureEngine.AudioEnabled, 0, 0.0);
        }

        public void SyncPairingInfo()
        {
            var url = !string.IsNullOrEmpty(txtURL.Text) ? txtURL.Text : $"http://127.0.0.1:{numPort.Value}";
            var pinText = cbPrivate.Checked ? $"PIN Code: {txtPassword.Text}" : "PIN: Not Required";
            var adapterName = comboIPs.SelectedItem?.ToString() ?? "Wi-Fi";
            var adapterInfo = $"📡 ZeroConf / mDNS: screentask.local\nAdapter: {adapterName}";
            try
            {
                var qrImg = QrCodeHelper.GenerateQrCodeBitmap(url, 4);
                UpdatePairingPass(qrImg, url, pinText, adapterInfo);
            }
            catch { }
        }

        public void UpdateSessionCards()
        {
            UpdateSessionCards(
                _broadcaster.ConnectedSessions,
                (id, muted) => _broadcaster.SetClientAudioMuted(id, muted),
                async (id) =>
                {
                    await _broadcaster.KickClientAsync(id);
                    Log($"Disconnected viewer session: {id}");
                });
        }

        public void UpdateSessionCards(
            IReadOnlyCollection<ViewerSession> sessions,
            Action<string, bool> onMuteToggle,
            Func<string, Task> onKick)
        {
            if (InvokeRequired)
            {
                try { BeginInvoke(() => UpdateSessionCards(sessions, onMuteToggle, onKick)); } catch { }
                return;
            }

            int count = sessions.Count;
            lblNoClientsPlaceholder.Visible = (count == 0);
            UpdateMetricViewers(count, 32);

            // Remove panels for disconnected sessions
            var currentIds = new HashSet<string>(sessions.Select(s => s.Id));
            var toRemove = _sessionCardPanels.Keys.Where(k => !currentIds.Contains(k)).ToList();
            foreach (var id in toRemove)
            {
                if (_sessionCardPanels.TryGetValue(id, out var panel))
                {
                    panelSessionList.Controls.Remove(panel);
                    panel.Dispose();
                    _sessionCardPanels.Remove(id);
                }
            }

            // Create or position panels
            panelSessionList.SuspendLayout();
            int index = 0;
            foreach (var session in sessions)
            {
                if (!_sessionCardPanels.TryGetValue(session.Id, out var cardPanel))
                {
                    cardPanel = CreateSessionCard(session, index, onMuteToggle, onKick);
                    _sessionCardPanels[session.Id] = cardPanel;
                    panelSessionList.Controls.Add(cardPanel);
                }
                else
                {
                    cardPanel.Location = new Point(4, index * 78 + 4);
                    if (index == 0) cardPanel.Name = "panelClientItem1";
                }
                RefreshSessionCardData(session);
                index++;
            }
            panelSessionList.ResumeLayout(true);
        }

        private Panel CreateSessionCard(
            ViewerSession session,
            int index,
            Action<string, bool> onMuteToggle,
            Func<string, Task> onKick)
        {
            int cardWidth = Math.Max(320, panelSessionList.ClientSize.Width - 10);
            var panel = new Panel
            {
                Name = index == 0 ? "panelClientItem1" : $"panelClientItem_{session.Id}",
                Size = new Size(cardWidth, 72),
                Location = new Point(4, index * 78 + 4),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = SystemColors.Window,
                BorderStyle = BorderStyle.FixedSingle,
                Tag = session.Id
            };

            var lblTitle = new Label
            {
                Name = "lblClientTitle",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = SystemColors.ControlText,
                Location = new Point(6, 4),
                Size = new Size(160, 16),
                Text = $"{session.DeviceIcon} {session.DeviceName}"
            };

            var lblBadge = new Label
            {
                Name = "lblClientBadge",
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = SystemColors.Highlight,
                Location = new Point(cardWidth - 176, 4),
                Size = new Size(166, 16),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                TextAlign = ContentAlignment.MiddleRight,
                Text = session.ViewMode
            };

            var lblDetails = new Label
            {
                Name = "lblClientDetails",
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = SystemColors.GrayText,
                Location = new Point(6, 22),
                Size = new Size(cardWidth - 16, 15),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Text = $"{session.RemoteIp} • {session.BrowserName}"
            };

            var lblMetrics = new Label
            {
                Name = "lblClientMetrics",
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = SystemColors.ControlText,
                Location = new Point(6, 44),
                Size = new Size(185, 20),
                Text = $"RTT: {(session.RttMs > 0 ? $"{session.RttMs:0} ms" : "<1 ms")} • Up: {session.FormattedUptime}"
            };

            var btnKick = new Button
            {
                Name = index == 0 ? "btnKickClient1" : $"btnKick_{session.Id}",
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = SystemColors.ControlText,
                Location = new Point(cardWidth - 72, 42),
                Size = new Size(64, 24),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Text = "✕ Kick",
                UseVisualStyleBackColor = true
            };

            var btnMute = new Button
            {
                Name = index == 0 ? "btnMuteClient1" : $"btnMute_{session.Id}",
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = SystemColors.ControlText,
                Location = new Point(btnKick.Left - 76, 42),
                Size = new Size(70, 24),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Text = session.IsAudioMuted ? "🔇 Unmute" : "🔊 Mute",
                UseVisualStyleBackColor = true
            };

            btnMute.Click += (s, e) =>
            {
                bool newMuted = !session.IsAudioMuted;
                onMuteToggle(session.Id, newMuted);
                btnMute.Text = newMuted ? "🔇 Unmute" : "🔊 Mute";
            };

            btnKick.Click += (s, e) =>
            {
                _ = onKick(session.Id);
            };

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblBadge);
            panel.Controls.Add(lblDetails);
            panel.Controls.Add(lblMetrics);
            panel.Controls.Add(btnMute);
            panel.Controls.Add(btnKick);

            return panel;
        }

        public void RefreshSessionCardData(ViewerSession session)
        {
            if (!_sessionCardPanels.TryGetValue(session.Id, out var panel)) return;

            var lblBadge = panel.Controls["lblClientBadge"] as Label;
            var lblMetrics = panel.Controls["lblClientMetrics"] as Label;

            if (lblBadge != null) lblBadge.Text = session.ViewMode;
            if (lblMetrics != null)
            {
                lblMetrics.Text = $"RTT: {(session.RttMs > 0 ? $"{session.RttMs:0} ms" : "<1 ms")} • Up: {session.FormattedUptime}";
            }
        }

        // ═════════════════════════════════════════════════════════════════
        // View 2: Video Sources Methods
        // ═════════════════════════════════════════════════════════════════

        public void PopulateSourceCards()
        {
            foreach (var card in _displayCards.Concat(_windowCards))
            {
                card.UpdateThumbnail(null);
                card.Dispose();
            }
            _displayCards.Clear();
            _windowCards.Clear();
            _selectedCard = null;

            flowDisplays.SuspendLayout();
            flowWindows.SuspendLayout();
            flowDisplays.Controls.Clear();
            flowWindows.Controls.Clear();

            var currentSource = comboScreens.SelectedIndex >= 0 && comboScreens.SelectedIndex < _captureSources.Count
                ? _captureSources[comboScreens.SelectedIndex]
                : null;

            foreach (var src in _captureSources)
            {
                var card = new SourceCard(src);
                card.CardClicked += (s, e) => SelectSourceCard(card);
                card.DoubleClick += (s, e) =>
                {
                    SelectSourceCard(card);
                    SelectTab(0);
                };

                if (src is ScreenCaptureSource)
                {
                    _displayCards.Add(card);
                    flowDisplays.Controls.Add(card);
                }
                else
                {
                    _windowCards.Add(card);
                    flowWindows.Controls.Add(card);
                }

                if (currentSource != null && (src.Id == currentSource.Id || src.DisplayName == currentSource.DisplayName))
                {
                    card.IsSelected = true;
                    _selectedCard = card;
                }
            }

            if (_selectedCard == null && _displayCards.Count > 0)
            {
                _displayCards[0].IsSelected = true;
                _selectedCard = _displayCards[0];
            }

            if (_selectedCard != null)
            {
                lblSelectedSource.Text = $"Selected: {_selectedCard.Title}";
            }

            flowDisplays.ResumeLayout(true);
            flowWindows.ResumeLayout(true);

            TriggerThumbnailUpdate();
        }

        private void SelectSourceCard(SourceCard card)
        {
            if (_selectedCard != null)
            {
                _selectedCard.IsSelected = false;
            }
            _selectedCard = card;
            _selectedCard.IsSelected = true;
            lblSelectedSource.Text = $"Selected: {card.Title}";

            var idx = _captureSources.FindIndex(s => s.Id == card.Source?.Id || s.DisplayName == card.Source?.DisplayName);
            if (idx >= 0 && idx != comboScreens.SelectedIndex)
            {
                comboScreens.SelectedIndex = idx;
            }
        }

        private void TriggerThumbnailUpdate()
        {
            if (_isUpdatingThumbnails) return;
            _isUpdatingThumbnails = true;

            Task.Run(() =>
            {
                try
                {
                    foreach (var card in _displayCards.Concat(_windowCards).ToList())
                    {
                        if (card.IsDisposed || card.Source == null) continue;
                        try
                        {
                            using var bmp = card.Source.CaptureFrame(false);
                            if (bmp != null)
                            {
                                var thumb = new Bitmap(192, 108);
                                using (var g = Graphics.FromImage(thumb))
                                {
                                    g.InterpolationMode = InterpolationMode.Bilinear;
                                    g.DrawImage(bmp, 0, 0, 192, 108);
                                }
                                if (!card.IsDisposed && card.IsHandleCreated)
                                {
                                    try { card.BeginInvoke(() => card.UpdateThumbnail(thumb)); } catch { thumb.Dispose(); }
                                }
                                else
                                {
                                    thumb.Dispose();
                                }
                            }
                        }
                        catch { }
                    }
                }
                finally
                {
                    _isUpdatingThumbnails = false;
                }
            });
        }

        // ═════════════════════════════════════════════════════════════════
        // View 3: Network & Security Methods
        // ═════════════════════════════════════════════════════════════════

        private void UpdateNetworkPreviewUrl()
        {
            var url = !string.IsNullOrEmpty(txtURL.Text) ? txtURL.Text : $"http://127.0.0.1:{numPort.Value}";
            lblNetworkPreviewUrl.Text = url;
        }

        // ═════════════════════════════════════════════════════════════════
        // View 4: Audio Diagnostics & Setup Methods
        // ═════════════════════════════════════════════════════════════════

        private void UpdateDiagnosticInfo()
        {
            if (comboAudioDevices.SelectedIndex < 0 || comboAudioDevices.SelectedIndex >= _audioDevices.Count) return;
            var desc = _audioDevices[comboAudioDevices.SelectedIndex];
            var fmt = _captureEngine.AudioService.AudioFormat;

            if (fmt != null)
            {
                lblAudioDiagInfo.Text =
                    $"Device: {desc.Name} • {(desc.IsInput ? "Microphone" : "Loopback Speakers")} • " +
                    $"{fmt.SampleRate:N0} Hz • {fmt.Channels} Channels • {fmt.BitsPerSample}-bit • Latency: <10ms";
            }
            else
            {
                lblAudioDiagInfo.Text = $"Device: {desc.Name} • Ready for streaming";
            }
        }

        private async void btnTestMic_Click(object? sender, EventArgs e)
        {
            if (_isRecordingMicTest) return;

            btnTestMic.Enabled = false;
            btnTestMic.Text = "🎙 Speak (3s)...";
            _isRecordingMicTest = true;

            try
            {
                _captureEngine.AudioService.StartMicTest(3);

                for (int i = 3; i > 0; i--)
                {
                    btnTestMic.Text = $"🎙 Speak ({i}s)...";
                    await Task.Delay(1000);
                }

                _isRecordingMicTest = false;
                btnTestMic.Text = "▶ Playing...";
                await Task.Delay(3500);
            }
            catch (Exception ex)
            {
                Log($"Mic test error: {ex.Message}");
            }
            finally
            {
                _isRecordingMicTest = false;
                btnTestMic.Text = "🎤 Test Mic (2s)";
                btnTestMic.Enabled = true;
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

        private void btnTestAudio_Click(object? sender, EventArgs e)
        {
            try
            {
                if (comboAudioDevices.SelectedIndex >= 0 && comboAudioDevices.SelectedIndex < _audioDevices.Count)
                {
                    var desc = _audioDevices[comboAudioDevices.SelectedIndex];
                    _captureEngine.AudioService.Stop();
                    _captureEngine.AudioService.Start(desc.Device, desc.IsInput);
                    UpdateDiagnosticInfo();
                    Log($"Audio device tested: {desc.Name}");
                }
            }
            catch (Exception ex)
            {
                Log($"Device test error: {ex.Message}");
            }
        }

        private void btnAudioDiag_Click(object? sender, EventArgs e)
        {
            SelectTab(4);
        }

        // ═════════════════════════════════════════════════════════════════
        // Responsive Layout Engine
        // ═════════════════════════════════════════════════════════════════

        private void ToggleExpandPreview()
        {
            _isExpandedPreview = !_isExpandedPreview;
            btnPopout.Text = _isExpandedPreview ? "⤓ Normal View" : "⤢ Expand Preview";
            UpdateLayout();
        }

        private void UpdateLayout()
        {
            if (panelContentContainer.ClientSize.Width <= 0 || panelContentContainer.ClientSize.Height <= 0) return;
            int totalWidth = panelContentContainer.ClientSize.Width;
            int totalHeight = panelContentContainer.ClientSize.Height;

            // ── Top Header Area Layout ──
            lblStatusBadge.Location = new Point(panelHeader.ClientSize.Width - lblStatusBadge.Width - 16, 14);

            // ── Responsive Top Navigation Bar Layout ──
            int navPadding = 8;
            int navGap = 6;
            int navAvailW = panelSideNav.ClientSize.Width - (2 * navPadding);
            Button[] navButtons = [btnNavBroadcast, btnNavClients, btnNavCapture, btnNavNetwork, btnNavPrefs];
            int btnNavW = Math.Max(120, (navAvailW - (navGap * (navButtons.Length - 1))) / navButtons.Length);
            int curNavX = navPadding;
            for (int i = 0; i < navButtons.Length; i++)
            {
                navButtons[i].Location = new Point(curNavX, 3);
                int w = (i == navButtons.Length - 1) ? (panelSideNav.ClientSize.Width - navPadding - curNavX) : btnNavW;
                navButtons[i].Size = new Size(Math.Max(100, w), 30);
                curNavX += btnNavW + navGap;
            }

            // ── View 0: Broadcast Layout ──
            if (_isExpandedPreview)
            {
                groupBox3.Visible = false;
                cardUrl.Visible = false;

                cardPreview.Location = new Point(0, 0);
                cardPreview.Size = new Size(totalWidth, totalHeight);

                int expandBottomY = totalHeight - 36;
                btnSwitchSource.Width = 135;
                btnSwitchSource.Location = new Point(totalWidth - btnSwitchSource.Width - 12, expandBottomY);

                btnPopout.Width = 125;
                btnPopout.Location = new Point(btnSwitchSource.Left - btnPopout.Width - 6, expandBottomY);

                btnRefreshScreens.Width = 28;
                btnRefreshScreens.Height = 26;
                btnRefreshScreens.Location = new Point(btnPopout.Left - btnRefreshScreens.Width - 6, expandBottomY);

                comboScreens.Location = new Point(12, expandBottomY + 2);
                comboScreens.Size = new Size(Math.Max(120, btnRefreshScreens.Left - 18), 24);

                monitorPreview.Location = new Point(12, 12);
                monitorPreview.Size = new Size(totalWidth - 24, Math.Max(100, expandBottomY - 20));
            }
            else
            {
                groupBox3.Visible = true;
                cardUrl.Visible = true;

                // 1. Bottom Section (Stream URL Card)
                int urlCardHeight = 84;
                int urlCardTop = Math.Max(180, totalHeight - urlCardHeight);
                cardUrl.Location = new Point(0, urlCardTop);
                cardUrl.Size = new Size(totalWidth, urlCardHeight);

                // Row 1: Header, Wi-Fi info, PIN display, Network Settings button
                lblUrlTitle.Location = new Point(12, 10);
                lblWifiBadge.Location = new Point(lblUrlTitle.Right + 8, 12);

                btnNetworkSettings.Size = new Size(130, 24);
                btnNetworkSettings.Location = new Point(cardUrl.ClientSize.Width - btnNetworkSettings.Width - 12, 8);

                lblPinDisplay.Location = new Point(btnNetworkSettings.Left - lblPinDisplay.Width - 12, 12);
                lblWifiBadge.Visible = (lblWifiBadge.Right < lblPinDisplay.Left - 10);

                // Row 2: Link Icon, URL Box, Copy, QR, Launch Browser
                label1.Location = new Point(12, 44);

                btnLaunch.Size = new Size(115, 26);
                btnLaunch.Location = new Point(cardUrl.ClientSize.Width - btnLaunch.Width - 12, 40);

                btnQr.Size = new Size(110, 26);
                btnQr.Location = new Point(btnLaunch.Left - btnQr.Width - 6, 40);

                btnCopyUrl.Size = new Size(100, 26);
                btnCopyUrl.Location = new Point(btnQr.Left - btnCopyUrl.Width - 6, 40);

                txtURL.Location = new Point(label1.Right + 8, 42);
                txtURL.Width = Math.Max(120, btnCopyUrl.Left - txtURL.Left - 8);

                // 2. Top Section (Preview & Control Card)
                int controlCardWidth = Math.Min(300, Math.Max(265, (int)(totalWidth * 0.30)));
                int topHeight = Math.Max(200, urlCardTop - 10);

                groupBox3.Location = new Point(totalWidth - controlCardWidth, 0);
                groupBox3.Size = new Size(controlCardWidth, topHeight);

                lblBroadcastDesc.Location = new Point(12, 10);
                lblBroadcastDesc.Width = groupBox3.ClientSize.Width - 24;

                btnStartServer.Location = new Point(12, 44);
                btnStartServer.Width = groupBox3.ClientSize.Width - 24;
                btnStartServer.Height = 38;

                int halfBtnW = (groupBox3.ClientSize.Width - 24 - 6) / 2;
                btnQuickMute.Location = new Point(12, 88);
                btnQuickMute.Size = new Size(halfBtnW, 28);

                btnAudioSetup.Location = new Point(12 + halfBtnW + 6, 88);
                btnAudioSetup.Size = new Size(halfBtnW, 28);

                btnFreeze.Location = new Point(12, 122);
                btnFreeze.Width = groupBox3.ClientSize.Width - 24;
                btnFreeze.Height = 28;

                lblAudioMeterText.Location = new Point(12, 156);
                pbAudioMeter.Location = new Point(12, 172);
                pbAudioMeter.Width = groupBox3.ClientSize.Width - 24;

                lblLiveStats.Location = new Point(12, 192);
                lblLiveStats.Width = groupBox3.ClientSize.Width - 24;
                lblLiveStats.Height = Math.Max(90, groupBox3.ClientSize.Height - lblLiveStats.Top - 12);

                cardPreview.Location = new Point(0, 0);
                cardPreview.Size = new Size(groupBox3.Left - 10, topHeight);

                // Dedicated bottom bar inside cardPreview
                int bottomBarY = cardPreview.ClientSize.Height - 34;

                btnSwitchSource.Width = 135;
                btnSwitchSource.Location = new Point(cardPreview.ClientSize.Width - btnSwitchSource.Width - 12, bottomBarY);

                btnPopout.Width = 125;
                btnPopout.Location = new Point(btnSwitchSource.Left - btnPopout.Width - 6, bottomBarY);

                btnRefreshScreens.Width = 28;
                btnRefreshScreens.Height = 26;
                btnRefreshScreens.Location = new Point(btnPopout.Left - btnRefreshScreens.Width - 6, bottomBarY);

                int comboX = 12;
                int comboW = Math.Max(100, btnRefreshScreens.Left - comboX - 6);
                comboScreens.Location = new Point(comboX, bottomBarY + 2);
                comboScreens.Size = new Size(comboW, 24);

                // Strictly bound 16:9 Aspect Ratio within area above bottom bar!
                int availW = Math.Max(80, cardPreview.ClientSize.Width - 24);
                int availH = Math.Max(60, bottomBarY - 20);
                const double targetRatio = 16.0 / 9.0;
                int previewW, previewH;

                if ((double)availW / availH > targetRatio)
                {
                    previewH = availH;
                    previewW = Math.Min(availW, (int)(previewH * targetRatio));
                }
                else
                {
                    previewW = availW;
                    previewH = Math.Min(availH, (int)(previewW / targetRatio));
                }

                int previewX = Math.Max(12, (cardPreview.ClientSize.Width - previewW) / 2);
                int previewY = Math.Max(10, (bottomBarY - previewH) / 2);
                monitorPreview.Location = new Point(previewX, previewY);
                monitorPreview.Size = new Size(previewW, previewH);
            }

            // ── View 1: Connected Viewers Layout ──
            int metricGap = 10;
            int metricCount = 3;
            int metricWidth = (totalWidth - (metricGap * (metricCount - 1))) / metricCount;

            cardMetricViewers.Location = new Point(0, 0);
            cardMetricViewers.Size = new Size(metricWidth, 72);
            lblIconMetricViewers.Location = new Point(10, 8);
            lblTitleMetricViewers.Location = new Point(36, 10);
            lblMetricViewersVal.Location = new Point(10, 30);
            lblMetricViewersVal.Width = Math.Max(100, cardMetricViewers.ClientSize.Width - 20);
            pbViewerCapacity.Location = new Point(10, 52);
            pbViewerCapacity.Width = Math.Max(100, cardMetricViewers.ClientSize.Width - 20);

            cardMetricLatency.Location = new Point(metricWidth + metricGap, 0);
            cardMetricLatency.Size = new Size(metricWidth, 72);
            lblIconMetricLatency.Location = new Point(10, 8);
            lblTitleMetricLatency.Location = new Point(36, 10);
            lblMetricLatencyVal.Location = new Point(10, 30);
            lblMetricLatencyVal.Width = Math.Max(100, cardMetricLatency.ClientSize.Width - 20);
            lblMetricLatencySub.Location = new Point(10, 52);
            lblMetricLatencySub.Width = Math.Max(100, cardMetricLatency.ClientSize.Width - 20);

            int metric3X = (metricWidth + metricGap) * 2;
            cardMetricSync.Location = new Point(metric3X, 0);
            cardMetricSync.Size = new Size(totalWidth - metric3X, 72);
            lblIconMetricSync.Location = new Point(10, 8);
            lblTitleMetricSync.Location = new Point(36, 10);
            lblMetricSyncVal.Location = new Point(10, 30);
            lblMetricSyncVal.Width = Math.Max(100, cardMetricSync.ClientSize.Width - 20);
            lblMetricSyncSub.Location = new Point(10, 52);
            lblMetricSyncSub.Width = Math.Max(100, cardMetricSync.ClientSize.Width - 20);

            int rightPassWidth = Math.Min(320, Math.Max(270, (int)(totalWidth * 0.32)));
            int terminalTop = 82;
            int terminalHeight = totalHeight - terminalTop;

            cardPairingPass.Location = new Point(totalWidth - rightPassWidth, terminalTop);
            cardPairingPass.Size = new Size(rightPassWidth, terminalHeight);

            int qrSize = Math.Min(160, Math.Max(100, cardPairingPass.ClientSize.Width - 40));
            picPairingQr.Size = new Size(qrSize, qrSize);
            picPairingQr.Location = new Point((cardPairingPass.ClientSize.Width - qrSize) / 2, 16);

            lblPairingUrlTitle.Location = new Point(12, picPairingQr.Bottom + 12);

            int urlRowY = lblPairingUrlTitle.Bottom + 4;
            btnPairingCopy.Size = new Size(72, 26);
            btnPairingCopy.Location = new Point(cardPairingPass.ClientSize.Width - btnPairingCopy.Width - 12, urlRowY);
            txtPairingUrl.Location = new Point(12, urlRowY + 1);
            txtPairingUrl.Size = new Size(btnPairingCopy.Left - 20, 24);

            int pinRowY = btnPairingCopy.Bottom + 12;
            btnPairingRegen.Size = new Size(72, 26);
            btnPairingRegen.Location = new Point(cardPairingPass.ClientSize.Width - btnPairingRegen.Width - 12, pinRowY);
            lblPairingPin.Location = new Point(12, pinRowY + 3);
            lblPairingPin.Size = new Size(btnPairingRegen.Left - 20, 20);

            lblZeroConf.Location = new Point(12, btnPairingRegen.Bottom + 14);
            lblZeroConf.Width = cardPairingPass.ClientSize.Width - 24;

            cardFleetTerminal.Location = new Point(0, terminalTop);
            cardFleetTerminal.Size = new Size(cardPairingPass.Left - 10, terminalHeight);

            lblFleetHeader.Location = new Point(12, 10);
            lblFleetHeader.Width = cardFleetTerminal.ClientSize.Width - 24;

            int announceBottom = cardFleetTerminal.ClientSize.Height - 12;
            lblAnnouncementStatus.Location = new Point(12, announceBottom - 16);
            lblAnnouncementStatus.Width = cardFleetTerminal.ClientSize.Width - 24;

            int announceInputY = lblAnnouncementStatus.Top - 30;
            btnSendAnnouncement.Size = new Size(110, 26);
            btnSendAnnouncement.Location = new Point(cardFleetTerminal.ClientSize.Width - btnSendAnnouncement.Width - 12, announceInputY);

            txtAnnouncement.Location = new Point(12, announceInputY);
            txtAnnouncement.Size = new Size(btnSendAnnouncement.Left - 20, 26);

            lblAnnouncement.Location = new Point(12, announceInputY - 20);
            lblAnnouncement.Width = cardFleetTerminal.ClientSize.Width - 24;

            panelSessionList.Location = new Point(10, 34);
            panelSessionList.Size = new Size(cardFleetTerminal.ClientSize.Width - 20, lblAnnouncement.Top - 40);
            lblNoClientsPlaceholder.Location = new Point(10, Math.Max(10, (panelSessionList.ClientSize.Height - 40) / 2));
            lblNoClientsPlaceholder.Width = panelSessionList.ClientSize.Width - 20;

            int currentListWidth = Math.Max(300, panelSessionList.ClientSize.Width - 10);
            foreach (var pnl in _sessionCardPanels.Values)
            {
                pnl.Width = currentListWidth;
            }

            // ── View 2: Video Sources Layout ──
            btnRefreshSourcesTab.Location = new Point(panelCaptureHeader.ClientSize.Width - btnRefreshSourcesTab.Width - 12, 12);
            lblSelectedSource.Location = new Point(btnRefreshSourcesTab.Left - lblSelectedSource.Width - 10, 17);
            cbCaptureMouse.Location = new Point(Math.Max(380, lblSelectedSource.Left - cbCaptureMouse.Width - 16), 18);
            btnSelectSource.Location = new Point(panelCaptureFooter.ClientSize.Width - btnSelectSource.Width - 12, 6);

            // ── View 3: Network Layout ──
            int halfNetWidth = (totalWidth - 12) / 2;
            cardNetworkInterface.Location = new Point(0, 0);
            cardNetworkInterface.Size = new Size(halfNetWidth, totalHeight);

            btnRefreshIPs.Location = new Point(cardNetworkInterface.ClientSize.Width - btnRefreshIPs.Width - 16, 100);
            comboIPs.Location = new Point(16, 102);
            comboIPs.Width = Math.Max(140, btnRefreshIPs.Left - comboIPs.Left - 8);

            numPort.Location = new Point(16, 172);
            numPort.Size = new Size(140, 24);
            lblPortStatus.Location = new Point(numPort.Right + 12, 177);
            lblAdapterHint.Location = new Point(16, 220);
            lblAdapterHint.Width = cardNetworkInterface.ClientSize.Width - 32;

            cardNetworkPin.Location = new Point(halfNetWidth + 12, 0);
            cardNetworkPin.Size = new Size(totalWidth - halfNetWidth - 12, totalHeight);

            cbPrivate.Location = new Point(16, 80);
            lblPinCodeDesc.Location = new Point(16, 120);

            txtPassword.Location = new Point(16, 142);
            txtPassword.Size = new Size(140, 26);
            btnGenPin.Location = new Point(txtPassword.Right + 10, 141);
            btnGenPin.Size = new Size(125, 28);
            btnCopyPin.Location = new Point(btnGenPin.Right + 8, 141);
            btnCopyPin.Size = new Size(80, 28);

            lblNetworkPreviewUrlTitle.Location = new Point(16, 220);
            lblNetworkPreviewUrl.Location = new Point(16, 244);
            lblNetworkPreviewUrl.Width = cardNetworkPin.ClientSize.Width - 32;

            // ── View 4: Preferences & Audio Layout ──
            int leftPrefsW = Math.Min(380, Math.Max(300, (int)(totalWidth * 0.38)));
            cardGeneralPrefs.Location = new Point(0, 0);
            cardGeneralPrefs.Size = new Size(leftPrefsW, totalHeight);

            comboFps.Width = cardGeneralPrefs.ClientSize.Width - 32;
            lblQuality.Location = new Point(cardGeneralPrefs.ClientSize.Width - lblQuality.Width - 16, 285);
            qualitySlider.Width = Math.Max(120, lblQuality.Left - qualitySlider.Left - 8);

            int rightPrefsX = leftPrefsW + 12;
            int rightPrefsW = totalWidth - rightPrefsX;
            int audioH = Math.Min(260, Math.Max(240, (int)(totalHeight * 0.46)));

            cardAudioDiag.Location = new Point(rightPrefsX, 0);
            cardAudioDiag.Size = new Size(rightPrefsW, audioH);

            btnRefreshAudio.Location = new Point(cardAudioDiag.ClientSize.Width - btnRefreshAudio.Width - 16, 83);
            comboAudioDevices.Location = new Point(150, 84);
            comboAudioDevices.Width = Math.Max(120, btnRefreshAudio.Left - comboAudioDevices.Left - 8);
            pbAudioDiagMeter.Width = cardAudioDiag.ClientSize.Width - 32;

            // 2x2 Grid for Audio Test Buttons to prevent clipping on all screen sizes
            int btnRowW = (cardAudioDiag.ClientSize.Width - 32 - 10) / 2;
            int btnAudioH = 28;

            btnTestSpeaker.Location = new Point(16, 152);
            btnTestSpeaker.Size = new Size(btnRowW, btnAudioH);

            btnTestMic.Location = new Point(16 + btnRowW + 10, 152);
            btnTestMic.Size = new Size(btnRowW, btnAudioH);

            btnTestAudio.Location = new Point(16, 186);
            btnTestAudio.Size = new Size(btnRowW, btnAudioH);

            btnAudioDiag.Location = new Point(16 + btnRowW + 10, 186);
            btnAudioDiag.Size = new Size(btnRowW, btnAudioH);

            lblAudioDiagInfo.Location = new Point(16, 222);
            lblAudioDiagInfo.Width = cardAudioDiag.ClientSize.Width - 32;

            cardLogs.Location = new Point(rightPrefsX, audioH + 10);
            cardLogs.Size = new Size(rightPrefsW, totalHeight - audioH - 10);
        }

        // ═════════════════════════════════════════════════════════════════
        // Capture & Network Population Methods
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
                UpdateNetworkPreviewUrl();
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

            // 1. Physical Screens
            var screens = Screen.AllScreens;
            for (int i = 0; i < screens.Length; i++)
            {
                var src = new ScreenCaptureSource(i);
                _captureSources.Add(src);
                comboScreens.Items.Add($"🖥️ {src.DisplayName}");
            }

            // 2. Shareable Open Windows
            try
            {
                var windows = WindowEnumerator.GetShareableWindows();
                foreach (var win in windows)
                {
                    var src = new WindowCaptureSource(win.Handle, win.Title, win.ProcessName, win.Icon);
                    _captureSources.Add(src);
                    comboScreens.Items.Add($"🪟 App: {win.Title}");
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
                lblSelectedSource.Text = $"Selected: {source.DisplayName}";

                // Sync selected card
                foreach (var card in _displayCards.Concat(_windowCards))
                {
                    bool isThis = card.Source != null && (card.Source.Id == source.Id || card.Source.DisplayName == source.DisplayName);
                    card.IsSelected = isThis;
                    if (isThis) _selectedCard = card;
                }

                Log($"Capture source switched to: {source.DisplayName}");
                SaveSettings();
            }
        }

        private void btnRefreshScreens_Click(object? sender, EventArgs e)
        {
            PopulateCaptureSources();
            PopulateSourceCards();
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
        // Server & Broadcast Management
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
                    MessageBox.Show("Please select a valid network adapter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var adapter = _adapters[comboIPs.SelectedIndex];
                int port = (int)numPort.Value;
                bool requirePin = cbPrivate.Checked;
                string pin = txtPassword.Text.Trim();

                if (requirePin && string.IsNullOrEmpty(pin))
                {
                    MessageBox.Show("Please enter a PIN code or uncheck the private session option.", "PIN Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btnStartServer.Enabled = false;
                btnStartServer.Text = "⏳ Starting...";

                // Start WebServer
                _webServer.UpdateSecurity(requirePin, pin);
                await _webServer.StartAsync(adapter.IpAddress, port);

                // Start Capture Engine
                int targetFps = comboFps.SelectedIndex switch
                {
                    0 => 15,
                    1 => 30,
                    2 => 60,
                    _ => 30
                };
                _captureEngine.TargetFps = targetFps;
                _captureEngine.Start();

                // Start Audio if enabled
                if (cbAudio.Checked && comboAudioDevices.SelectedIndex >= 0 && comboAudioDevices.SelectedIndex < _audioDevices.Count)
                {
                    var audioDev = _audioDevices[comboAudioDevices.SelectedIndex];
                    _captureEngine.AudioService.Start(audioDev.Device, audioDev.IsInput);
                }

                _broadcaster.SetQuality(qualitySlider.Value);

                // Update UI state
                btnStartServer.Text = "⏹ STOP BROADCAST";
                btnStartServer.BackColor = Color.FromArgb(196, 43, 28);
                btnStartServer.Enabled = true;

                comboIPs.Enabled = false;
                numPort.Enabled = false;
                cbPrivate.Enabled = false;
                txtPassword.Enabled = false;
                btnGenPin.Enabled = false;
                btnFreeze.Enabled = true;

                lblStatusBadge.Text = "● LIVE";
                lblStatusBadge.ForeColor = Color.FromArgb(16, 124, 65);

                monitorPreview.IsLive = true;
                monitorPreview.FpsText = $"{targetFps}.0 FPS";
                monitorPreview.AudioInfoText = cbAudio.Checked ? "Audio: Active" : "Audio: Disabled";

                _uptimeStopwatch.Restart();
                _statsTimer.Start();

                UpdateUrlPreview();
                UpdateNetworkPreviewUrl();
                SyncPairingInfo();
                SyncFleetMetrics();

                // StatusStrip
                statusLabelServer.Text = $"● Live: {txtURL.Text}";
                statusLabelAudio.Text = cbAudio.Checked ? "🔊 Audio: On" : "🔇 Audio: Off";

                Log($"Server started on {txtURL.Text}");
                appNotify.ShowBalloonTip(1000, "ScreenTask 2.0", $"Broadcast live on {txtURL.Text}", ToolTipIcon.Info);
            }
            catch (Exception ex)
            {
                btnStartServer.Enabled = true;
                btnStartServer.Text = "▶ START BROADCAST";
                btnStartServer.BackColor = Color.FromArgb(16, 124, 65);
                MessageBox.Show($"Failed to start server: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log($"Error starting server: {ex.Message}");
            }
        }

        private async Task StopServerAsync()
        {
            try
            {
                btnStartServer.Enabled = false;
                btnStartServer.Text = "⏳ Stopping...";

                _statsTimer.Stop();
                _uptimeStopwatch.Stop();

                await _webServer.StopAsync();
                await _captureEngine.StopAsync();

                btnStartServer.Text = "▶ START BROADCAST";
                btnStartServer.BackColor = Color.FromArgb(16, 124, 65);
                btnStartServer.Enabled = true;

                comboIPs.Enabled = true;
                numPort.Enabled = true;
                cbPrivate.Enabled = true;
                txtPassword.Enabled = cbPrivate.Checked;
                btnGenPin.Enabled = cbPrivate.Checked;
                btnFreeze.Enabled = false;
                btnFreeze.Text = "⏸ Freeze Screen";

                lblStatusBadge.Text = "● READY";
                lblStatusBadge.ForeColor = SystemColors.GrayText;

                monitorPreview.IsLive = false;
                monitorPreview.FpsText = "0.0 FPS";
                monitorPreview.AudioInfoText = cbAudio.Checked ? "Audio: Active" : "Audio: Disabled";

                lblLiveStats.Text = "Ready to stream.\nClick Start Broadcast to begin.";
                SyncFleetMetrics();
                UpdateSessionCards();

                // StatusStrip
                statusLabelServer.Text = "● Ready to stream";
                statusLabelViewers.Text = "👥 0 viewers";
                statusLabelFps.Text = "🎞️ 0 FPS";
                statusLabelData.Text = "📊 0.0 MB";
                statusLabelUptime.Text = "⏱️ 00:00:00";
                statusLabelAudio.Text = cbAudio.Checked ? "🔊 Audio: On" : "🔇 Audio: Off";

                Log("ScreenTask server stopped.");
                appNotify.ShowBalloonTip(1000, "ScreenTask", "Server stopped.", ToolTipIcon.Info);
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

            lblLiveStats.Text =
                $"● Status: Broadcast Live\n" +
                $"👥 Viewers: {viewers}\n" +
                $"🎞️ Framerate: {fps}.0 FPS\n" +
                $"📊 Bandwidth: {mbTransferred} MB sent\n" +
                $"⏱️ Duration: {uptime}";

            statusLabelFps.Text = $"🎞️ {fps} FPS";
            statusLabelData.Text = $"📊 {mbTransferred} MB";
            statusLabelUptime.Text = $"⏱️ {uptime}";
            statusLabelViewers.Text = $"👥 {viewers} {(viewers == 1 ? "viewer" : "viewers")}";
            statusLabelAudio.Text = _captureEngine.AudioEnabled ? "🔊 Audio: On" : "🔇 Audio: Off";

            SyncFleetMetrics();
            UpdateSessionCards();
        }

        // ═════════════════════════════════════════════════════════════════
        // UI Actions & Helpers
        // ═════════════════════════════════════════════════════════════════

        private void btnCopyUrl_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtURL.Text))
            {
                Clipboard.SetText(txtURL.Text);
                btnCopyUrl.Text = "✓ Copied!";
                var timer = new System.Windows.Forms.Timer { Interval = 1200 };
                timer.Tick += (s, args) =>
                {
                    btnCopyUrl.Text = "📋 Copy Link";
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
                Log("Stream URL copied to clipboard.");
            }
        }

        private void btnLaunch_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtURL.Text) && txtURL.Text.StartsWith("http"))
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

        private void btnQr_Click(object? sender, EventArgs e)
        {
            SelectTab(1);
        }

        private void btnFreeze_Click(object? sender, EventArgs e)
        {
            _captureEngine.IsFrozen = !_captureEngine.IsFrozen;
            btnFreeze.Text = _captureEngine.IsFrozen ? "▶ Resume Screen" : "⏸ Freeze Screen";
            btnFreeze.BackColor = _captureEngine.IsFrozen ? Color.FromArgb(255, 185, 0) : SystemColors.Control;
            Log(_captureEngine.IsFrozen ? "Stream preview frozen." : "Stream preview resumed.");
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
            _captureEngine.AudioEnabled = enabled;
            menuCaptureEnableAudio.Checked = enabled;
            btnQuickMute.Text = enabled ? "🔇 Mute Audio" : "🔊 Unmute Audio";
            statusLabelAudio.Text = enabled ? "🔊 Audio: On" : "🔇 Audio: Off";
            lblAudioMeterText.Text = enabled ? "Sound Volume: Active" : "Sound Volume: Muted";

            comboAudioDevices.Enabled = enabled;
            btnTestAudio.Enabled = enabled;
            btnAudioDiag.Enabled = enabled;
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
                if (pbAudioDiagMeter != null) pbAudioDiagMeter.Value = 0;
            }

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
            menuNetworkCopyPin.Enabled = cbPrivate.Checked && !string.IsNullOrEmpty(txtPassword.Text);

            if (cbPrivate.Checked && string.IsNullOrEmpty(txtPassword.Text))
            {
                GenerateNewPin();
            }

            UpdateUrlPreview();
            SyncPairingInfo();
            SaveSettings();
        }

        private void txtPassword_TextChanged(object? sender, EventArgs e)
        {
            btnCopyPin.Enabled = cbPrivate.Checked && !string.IsNullOrEmpty(txtPassword.Text);
            menuNetworkCopyPin.Enabled = btnCopyPin.Enabled;
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
                Clipboard.SetText(txtPassword.Text);
                btnCopyPin.Text = "✓ Copied!";
                var timer = new System.Windows.Forms.Timer { Interval = 1200 };
                timer.Tick += (s, args) =>
                {
                    btnCopyPin.Text = "📋 Copy";
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
                Log("Session PIN copied to clipboard.");
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
                Clipboard.SetText(txtLog.Text);
                btnCopyLog.Text = "✓ Copied!";
                var timer = new System.Windows.Forms.Timer { Interval = 1200 };
                timer.Tick += (s, args) =>
                {
                    btnCopyLog.Text = "📋 Copy";
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
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

            lblPinDisplay.Text = cbPrivate.Checked
                ? $"PIN: {txtPassword.Text}"
                : "PIN: Not Required";
        }

        public void Log(string message)
        {
            if (txtLog.IsDisposed || !txtLog.IsHandleCreated) return;
            try
            {
                txtLog.BeginInvoke(() =>
                {
                    string entry = $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}";
                    txtLog.AppendText(entry);
                });
            }
            catch { }
        }

        public string GetSettingsFilePath()
        {
            if (!string.IsNullOrEmpty(CustomSettingsFilePath))
            {
                return CustomSettingsFilePath;
            }

            // In unit tests, isolate settings to unique per-instance path unless CustomSettingsFilePath is specified
            bool isUnitTest = AppDomain.CurrentDomain.GetAssemblies()
                .Any(a => a.GetName().Name?.StartsWith("xunit", StringComparison.OrdinalIgnoreCase) == true);
            if (isUnitTest)
            {
                return Path.Combine(Path.GetTempPath(), $"screentask_test_inst_{_instanceId}.xml");
            }

            // 1. Check %APPDATA%\ScreenTask\settings.xml (Primary persistent location)
            string appDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ScreenTask");
            string appDataPath = Path.Combine(appDataDir, "settings.xml");
            if (File.Exists(appDataPath))
            {
                return appDataPath;
            }

            // 2. Portable mode: if portable.txt or appsettings.xml exists next to exe
            string portablePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.AppSettingsFileName);
            string portableMarker = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "portable.txt");
            if (File.Exists(portableMarker) || File.Exists(portablePath))
            {
                return portablePath;
            }

            // 3. Migrate legacy current working directory appsettings.xml if present
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

            // 4. Default: ensure %APPDATA%\ScreenTask directory exists and return appDataPath
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
                    cbAudio.Checked = _currentSettings.IsAudioEnabled;
                    comboAudioDevices.Enabled = cbAudio.Checked;
                    btnRefreshAudio.Enabled = cbAudio.Checked;
                    pbAudioMeter.Enabled = cbAudio.Checked;
                    btnTestAudio.Enabled = cbAudio.Checked;
                    btnAudioDiag.Enabled = cbAudio.Checked;
                    cbAutoStart.Checked = _currentSettings.IsAutoStartServerEnabled;
                    cbMiniStart.Checked = _currentSettings.IsStartMinimizedEnabled;
                    cbCloseToTray.Checked = _currentSettings.IsCloseToTrayEnabled;
                    txtPassword.Text = _currentSettings.PinCode;
                    txtPassword.Enabled = cbPrivate.Checked;
                    lblPinDisplay.Text = cbPrivate.Checked ? $"PIN: {txtPassword.Text}" : "PIN: Not Required";
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

        private void RestoreFromTray()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
            this.Focus();
        }

        private void appNotify_Click(object? sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
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

        private async Task CheckForUpdatesAsync()
        {
            try
            {
                _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(Constants.UserAgent);
                var response = await _httpClient.GetStringAsync(Constants.GitHubApiLatestReleaseUrl);
                using var doc = JsonDocument.Parse(response);
                if (doc.RootElement.TryGetProperty("tag_name", out var tagElem))
                {
                    string latestTag = tagElem.GetString() ?? "";
                    string currentVer = "v" + Constants.AppVersion;
                    if (!string.IsNullOrEmpty(latestTag) && latestTag != currentVer && Version.TryParse(latestTag.TrimStart('v'), out var latest) && Version.TryParse(Constants.AppVersion, out var curr) && latest > curr)
                    {
                        if (this.IsHandleCreated && !this.IsDisposed)
                        {
                            this.BeginInvoke(() =>
                            {
                                if (MessageBox.Show(this,
                                    $"A newer version of ScreenTask ({latestTag}) is available!\nWould you like to open the download page?",
                                    "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    try { Process.Start(new ProcessStartInfo(Constants.GitHubUrl) { UseShellExecute = true }); } catch { }
                                }
                            });
                        }
                    }
                }
            }
            catch { }
        }

        private void frmMain_Resize(object? sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized && cbCloseToTray.Checked)
            {
                this.Hide();
            }
            UpdateLayout();
        }

        public async Task PerformGracefulShutdownAsync()
        {
            // 1. Stop all form timers
            try
            {
                _statsTimer.Stop();
                _statsTimer.Dispose();
                _deviceChangeTimer.Stop();
                _deviceChangeTimer.Dispose();
                _thumbnailTimer.Stop();
                _thumbnailTimer.Dispose();
            }
            catch { }

            // 2. Abort all connected WebSocket clients to allow Kestrel to drain immediately
            try
            {
                _broadcaster?.Dispose();
            }
            catch { }

            // 3. Stop Kestrel web server on background task with timeout guard
            if (_webServer != null && _webServer.IsRunning)
            {
                try
                {
                    var stopWebTask = Task.Run(async () => await _webServer.StopAsync());
                    await Task.WhenAny(stopWebTask, Task.Delay(2000));
                }
                catch { }
            }

            // 4. Stop capture engine and audio capture service on background thread with timeout guard
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

            // 5. Save settings before final termination
            try
            {
                SaveSettings();
            }
            catch { }

            // 6. Dispose tray notification icon
            try
            {
                appNotify?.Dispose();
            }
            catch { }
        }

        private async void frmMain_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // If graceful shutdown already finished, allow form to close
            if (_isGracefulShutdownComplete)
            {
                return;
            }

            bool isBroadcastActive = (_webServer != null && _webServer.IsRunning) ||
                                     (_captureEngine != null && _captureEngine.IsRunning);

            // Minimize to tray if enabled and user clicked [X]
            if (!_isExitingExplicitly && cbCloseToTray.Checked && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
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

            // If broadcast is running and user is exiting, ask for confirmation
            if (isBroadcastActive && !SuppressModalDialogsForTesting && e.CloseReason == CloseReason.UserClosing)
            {
                IWin32Window? dialogOwner = this.Visible ? this : null;
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

            // Begin graceful shutdown: cancel immediate close, hide UI, and execute async cleanup
            e.Cancel = true;
            _isShuttingDown = true;
            _isExitingExplicitly = true;

            try
            {
                this.Hide();
                appNotify.Visible = false;
            }
            catch { }

            await PerformGracefulShutdownAsync();

            _isGracefulShutdownComplete = true;

            try
            {
                this.Close();
            }
            catch
            {
                Application.Exit();
            }
        }
    }
}
