using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using ScreenTask.Core.Capture;
using ScreenTask.Core.Network;
using ScreenTask.Core.Streaming;
using ScreenTask.UI;
using ScreenTask.UI.Controls;
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
        private List<ScreenTask.Core.Audio.AudioDeviceDescriptor> _audioDevices = new();
        private AppSettings _currentSettings = new();
        private readonly System.Windows.Forms.Timer _statsTimer = new();
        private readonly System.Windows.Forms.Timer _deviceChangeTimer = new() { Interval = 600 };
        private readonly Stopwatch _uptimeStopwatch = new();
        private readonly HttpClient _httpClient = new();
        private const int WM_DEVICECHANGE = 0x0219;

        private bool _isCompactMode = false;
        private bool _isExitingExplicitly = false;
        private int _currentTabIndex = 0;
        private Size _fullModeClientSize = new Size(860, 620);

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
                    Log("Hardware change detected: refreshed audio & network devices.");
                }
                catch { }
            };

            WireEvents();
            InitializeTrayContextMenu();
        }

        private void WireEvents()
        {
            this.Load += frmMain_Load;
            this.FormClosing += frmMain_FormClosing;
            this.Resize += frmMain_Resize;

            // Navigation Tabs
            btnNavBroadcast.Click += (s, e) => SelectTab(0);
            btnNavCapture.Click += (s, e) => SelectTab(1);
            btnNavNetwork.Click += (s, e) => SelectTab(2);
            btnNavPreferences.Click += (s, e) => SelectTab(3);
            btnSwitchSource.Click += (s, e) => SelectTab(1);

            // Quick Mute Button in Nav Bar
            btnQuickMute.Click += (s, e) =>
            {
                cbAudio.Checked = !cbAudio.Checked;
            };

            // Compact Mode
            btnMiniMode.Click += (s, e) => ToggleCompactMode();
            btnCompactExpand.Click += (s, e) => ToggleCompactMode();
            btnCompactStop.Click += btnStartServer_Click;
            btnCompactCopy.Click += btnCopyUrl_Click;
            panelCompactBar.DoubleClick += (s, e) => ToggleCompactMode();
            lblCompactStatus.DoubleClick += (s, e) => ToggleCompactMode();
            lblCompactUrl.DoubleClick += (s, e) => ToggleCompactMode();
            lblCompactDot.DoubleClick += (s, e) => ToggleCompactMode();

            // Broadcast Controls
            btnStartServer.Click += btnStartServer_Click;
            btnLaunch.Click += btnLaunch_Click;
            btnCopyUrl.Click += btnCopyUrl_Click;
            btnQr.Click += btnQr_Click;

            // Capture Controls
            comboScreens.SelectedIndexChanged += comboScreens_SelectedIndexChanged;
            btnRefreshScreens.Click += btnRefreshScreens_Click;
            comboIPs.SelectedIndexChanged += (s, e) => UpdateUrlPreview();
            btnRefreshIPs.Click += btnRefreshIPs_Click;
            btnRefreshAudio.Click += btnRefreshAudio_Click;
            numPort.ValueChanged += (s, e) => UpdateUrlPreview();

            // Options & Audio
            cbPrivate.CheckedChanged += cbPrivate_CheckedChanged;
            txtPassword.TextChanged += txtPassword_TextChanged;
            btnGenPin.Click += btnGenPin_Click;
            btnCopyPin.Click += btnCopyPin_Click;

            cbCaptureMouse.CheckedChanged += cbCaptureMouse_CheckedChanged;
            cbAudio.CheckedChanged += cbAudio_CheckedChanged;
            comboAudioDevices.SelectedIndexChanged += comboAudioDevices_SelectedIndexChanged;
            btnTestAudio.Click += btnTestAudio_Click;
            btnAudioDiag.Click += btnAudioDiag_Click;
            qualitySlider.Scroll += qualitySlider_Scroll;

            // Sync Segmented Framerate with comboFps
            fpsSegmentedControl.SelectedIndexChanged += (s, e) =>
            {
                if (comboFps.SelectedIndex != fpsSegmentedControl.SelectedIndex)
                {
                    comboFps.SelectedIndex = fpsSegmentedControl.SelectedIndex;
                }
            };

            comboFps.SelectedIndexChanged += (s, e) =>
            {
                if (fpsSegmentedControl.SelectedIndex != comboFps.SelectedIndex)
                {
                    fpsSegmentedControl.SelectedIndex = comboFps.SelectedIndex;
                }
                comboFps_SelectedIndexChanged(s, e);
            };

            // Logs
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
                            if (val > 0)
                            {
                                int db = (int)(20 * Math.Log10(Math.Max(level, 0.001f)));
                                lblAudioMeterText.Text = $"Sound Volume: {db} dB";
                            }
                            else
                            {
                                lblAudioMeterText.Text = "Sound Volume: Silent";
                            }
                        });
                    }
                    catch { }
                }
            };

            lblMe.Click += lblMe_Click;
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

        private void frmMain_Load(object? sender, EventArgs e)
        {
            PopulateNetworkAdapters();
            PopulateCaptureSources();
            PopulateAudioDevices();
            LoadSettings();
            SelectTab(0);

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

        private void SelectTab(int index)
        {
            _currentTabIndex = index;
            panelTabBroadcast.Visible = index == 0;
            panelTabCapture.Visible = index == 1;
            panelTabNetwork.Visible = index == 2;
            panelTabPreferences.Visible = index == 3;

            UpdateNavButton(btnNavBroadcast, index == 0);
            UpdateNavButton(btnNavCapture, index == 1);
            UpdateNavButton(btnNavNetwork, index == 2);
            UpdateNavButton(btnNavPreferences, index == 3);
        }

        private static void UpdateNavButton(Button btn, bool isSelected)
        {
            btn.Font = new Font(btn.Font.FontFamily, 9f, isSelected ? FontStyle.Bold : FontStyle.Regular);
            if (isSelected)
            {
                btn.BackColor = SystemColors.ControlLight;
                btn.UseVisualStyleBackColor = false;
            }
            else
            {
                btn.BackColor = SystemColors.Control;
                btn.UseVisualStyleBackColor = true;
            }
        }

        private void ToggleCompactMode()
        {
            _isCompactMode = !_isCompactMode;

            if (_isCompactMode)
            {
                // Save current full view dimensions before shrinking
                if (this.ClientSize.Height > 100)
                {
                    _fullModeClientSize = this.ClientSize;
                }

                // Calculate compact height scaled by current DPI
                float dpiScale = (float)this.DeviceDpi / 96f;
                int compactHeight = Math.Max(68, (int)Math.Round(68f * dpiScale));

                // Hide all main view and header controls to avoid bleed-through
                panelSideNav.Visible = false;
                panelTabBroadcast.Visible = false;
                panelTabCapture.Visible = false;
                panelTabNetwork.Visible = false;
                panelTabPreferences.Visible = false;
                lblMe.Visible = false;
                lblStatusBadge.Visible = false;
                btnMiniMode.Visible = false;

                // Configure and show compact bar filling the entire client area
                panelCompactBar.Dock = DockStyle.Fill;
                panelCompactBar.Visible = true;
                panelCompactBar.BringToFront();

                // Keep same width so window doesn't jump horizontally, only shrink height
                this.ClientSize = new Size(this.ClientSize.Width, compactHeight);
                this.TopMost = true;
            }
            else
            {
                // Hide compact bar
                panelCompactBar.Visible = false;

                // Restore full view size
                this.ClientSize = _fullModeClientSize.Height > 100 ? _fullModeClientSize : new Size(860, 620);
                this.TopMost = false;

                // Restore header & navigation controls
                lblStatusBadge.Visible = true;
                btnMiniMode.Visible = true;
                btnMiniMode.Text = "🗖 Mini Bar";

                panelSideNav.Visible = true;
                lblMe.Visible = true;

                // Restore the currently active tab panel
                SelectTab(_currentTabIndex);
            }
        }

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
                    var src = new WindowCaptureSource(win.Handle, win.Title);
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

                if (_webServer.IsRunning)
                {
                    Log($"Switched capture source to: {source.DisplayName}");
                }
            }
        }

        private void btnRefreshScreens_Click(object? sender, EventArgs e)
        {
            PopulateCaptureSources();
            Log("Refreshed capture sources and open application windows.");
        }

        private void UpdateUrlPreview()
        {
            if (comboIPs.SelectedIndex >= 0 && comboIPs.SelectedIndex < _adapters.Count)
            {
                var ip = _adapters[comboIPs.SelectedIndex].IpAddress;
                string url = $"http://{ip}:{numPort.Value}";
                txtURL.Text = url;
                lblCompactUrl.Text = url;
            }
        }

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
                Log("Starting ScreenTask 2.0 streaming engine...");
                btnStartServer.Enabled = false;
                btnCompactStop.Enabled = false;

                // 1. Configure Selected Capture Source
                if (comboScreens.SelectedIndex >= 0 && comboScreens.SelectedIndex < _captureSources.Count)
                {
                    _captureEngine.SetSource(_captureSources[comboScreens.SelectedIndex]);
                    monitorPreview.SourceTitle = _captureSources[comboScreens.SelectedIndex].DisplayName;
                }
                else
                {
                    _captureEngine.SetSource(new ScreenCaptureSource(0));
                    monitorPreview.SourceTitle = "Display 1 (Primary)";
                }

                // 2. Configure Stream Options
                _captureEngine.CaptureMouse = cbCaptureMouse.Checked;
                if (comboAudioDevices.SelectedIndex >= 0 && comboAudioDevices.SelectedIndex < _audioDevices.Count)
                {
                    _captureEngine.SetAudioDevice(_audioDevices[comboAudioDevices.SelectedIndex].Device);
                }
                _captureEngine.AudioEnabled = cbAudio.Checked;

                // Framerate
                _captureEngine.TargetFps = comboFps.SelectedIndex switch
                {
                    0 => 60,
                    1 => 30,
                    2 => 15,
                    3 => 5,
                    _ => 30
                };
                monitorPreview.FpsText = $"{_captureEngine.TargetFps}.0 FPS";

                _broadcaster.SetQuality(qualitySlider.Value);

                // 3. Configure Security
                _webServer.UpdateSecurity(cbPrivate.Checked, txtPassword.Text.Trim());

                // 4. Resolve IP & Port
                var selectedIp = "127.0.0.1";
                if (comboIPs.SelectedIndex >= 0 && comboIPs.SelectedIndex < _adapters.Count)
                {
                    selectedIp = _adapters[comboIPs.SelectedIndex].IpAddress;
                }
                int port = (int)numPort.Value;

                // 5. Start Web Server & Capture Engine
                await _webServer.StartAsync(selectedIp, port);
                _captureEngine.Start();
                _uptimeStopwatch.Restart();
                _statsTimer.Start();

                // Lock IP and Port controls while broadcasting
                comboIPs.Enabled = false;
                btnRefreshIPs.Enabled = false;
                numPort.Enabled = false;

                var streamUrl = _webServer.ServerUrl;
                txtURL.Text = streamUrl;
                lblCompactUrl.Text = streamUrl;

                // Update UI state to LIVE
                btnStartServer.Text = "⏹  STOP BROADCAST";
                btnCompactStop.Text = "Stop";

                lblStatusBadge.Text = "● Live Broadcasting";
                monitorPreview.IsLive = true;

                Log($"Stream live at: {streamUrl}");
                Log($"Broadcasting: {_captureEngine.CurrentSource.DisplayName} at {_captureEngine.TargetFps} FPS (Audio: {(cbAudio.Checked ? "On" : "Off")})");
                appNotify.ShowBalloonTip(1000, "ScreenTask 2.0 Live", $"Streaming at {streamUrl}", ToolTipIcon.Info);
            }
            catch (SocketException ex) when (ex.SocketErrorCode == SocketError.AddressAlreadyInUse)
            {
                MessageBox.Show($"Port {numPort.Value} is already in use. Please select a different port.", "Port Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Log($"Port {numPort.Value} is already in use.");
            }
            catch (Exception ex)
            {
                Log($"Error starting server: {ex.Message}");
                MessageBox.Show($"Failed to start server:\n{ex.Message}", "ScreenTask Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnStartServer.Enabled = true;
                btnCompactStop.Enabled = true;
                if (!_webServer.IsRunning)
                {
                    comboIPs.Enabled = true;
                    btnRefreshIPs.Enabled = true;
                    numPort.Enabled = true;
                }
            }
        }

        private async Task StopServerAsync()
        {
            try
            {
                btnStartServer.Enabled = false;
                btnCompactStop.Enabled = false;
                Log("Stopping streaming server...");

                _statsTimer.Stop();
                _uptimeStopwatch.Stop();
                await _captureEngine.StopAsync();
                await _webServer.StopAsync();

                // Re-enable IP and Port controls
                comboIPs.Enabled = true;
                btnRefreshIPs.Enabled = true;
                numPort.Enabled = true;

                btnStartServer.Text = "▶  START BROADCAST";
                btnCompactStop.Text = "Start";

                lblStatusBadge.Text = "● Ready to Stream";
                monitorPreview.IsLive = false;

                lblLiveStats.Text = "⚡ Stopped • Ready to broadcast";
                lblCompactStatus.Text = "● Ready to Stream";
                lblChipViewers.Text = "0 Connected";
                lblChipFps.Text = "0.0 FPS";
                lblChipUptime.Text = "00:00:00";

                Log("ScreenTask server stopped.");
                appNotify.ShowBalloonTip(1000, "ScreenTask", "Server stopped.", ToolTipIcon.Info);
            }
            catch (Exception ex)
            {
                Log($"Error stopping server: {ex.Message}");
            }
            finally
            {
                btnStartServer.Enabled = true;
                btnCompactStop.Enabled = true;
            }
        }

        private void OnStatsTimerTick(object? sender, EventArgs e)
        {
            if (!_webServer.IsRunning) return;

            int viewers = _broadcaster.ConnectedClientsCount;
            int fps = _broadcaster.CurrentFps;
            long bytes = _broadcaster.TotalBytesTransferred;
            string mbTransferred = (bytes / (1024.0 * 1024.0)).ToString("0.0");
            string uptime = _uptimeStopwatch.Elapsed.ToString(@"hh\:mm\:ss");

            lblLiveStats.Text = $"🟢 Live: {viewers} Viewers | {fps} FPS | {mbTransferred} MB sent\n⏱ Stream Time: {uptime}";
            lblCompactStatus.Text = $"● Live: {fps} FPS • {viewers} Viewers • {uptime}";
            monitorPreview.FpsText = $"{fps}.0 FPS";

            lblChipViewers.Text = $"{viewers} Connected";
            lblChipFps.Text = $"{fps}.0 FPS";
            lblChipData.Text = $"{mbTransferred} MB";
            lblChipUptime.Text = uptime;
        }

        private void btnCopyUrl_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtURL.Text))
            {
                Clipboard.SetText(txtURL.Text);
                btnCopyUrl.Text = "✓ Copied!";
                btnCompactCopy.Text = "✓ Copied!";
                var timer = new System.Windows.Forms.Timer { Interval = 1200 };
                timer.Tick += (s, args) =>
                {
                    btnCopyUrl.Text = "📋 Copy Link";
                    btnCompactCopy.Text = "📋 Copy Link";
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            }
        }

        private void btnLaunch_Click(object? sender, EventArgs e)
        {
            if (txtURL.Text.StartsWith("http"))
            {
                try
                {
                    Process.Start(new ProcessStartInfo(txtURL.Text) { UseShellExecute = true });
                }
                catch { }
            }
        }

        private void btnQr_Click(object? sender, EventArgs e)
        {
            var url = txtURL.Text.StartsWith("http") ? txtURL.Text : _webServer.ServerUrl;
            using var qrForm = new QrCodeForm(url);
            qrForm.ShowDialog(this);
        }

        private void cbPrivate_CheckedChanged(object? sender, EventArgs e)
        {
            txtPassword.Enabled = cbPrivate.Checked;
            btnGenPin.Enabled = cbPrivate.Checked;
            btnCopyPin.Enabled = cbPrivate.Checked;
            _currentSettings.IsPrivateSession = cbPrivate.Checked;

            if (cbPrivate.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    txtPassword.Text = Random.Shared.Next(1000, 9999).ToString();
                }
                txtPassword.Focus();
                txtPassword.SelectAll();
            }

            _webServer.UpdateSecurity(cbPrivate.Checked, txtPassword.Text.Trim());
            _currentSettings.PinCode = txtPassword.Text.Trim();
            lblPinDisplay.Text = cbPrivate.Checked ? $"PIN: {txtPassword.Text}" : "PIN: Not Required";

            if (_webServer.IsRunning)
            {
                Log(cbPrivate.Checked
                    ? $"Security: PIN protection enabled (PIN: {txtPassword.Text.Trim()})"
                    : "Security: PIN protection disabled (No PIN required)");
            }
        }

        private void txtPassword_TextChanged(object? sender, EventArgs e)
        {
            _currentSettings.PinCode = txtPassword.Text.Trim();
            if (cbPrivate.Checked)
            {
                _webServer.UpdateSecurity(cbPrivate.Checked, txtPassword.Text.Trim());
                lblPinDisplay.Text = $"PIN: {txtPassword.Text}";
            }
        }

        private void btnGenPin_Click(object? sender, EventArgs e)
        {
            txtPassword.Text = Random.Shared.Next(1000, 9999).ToString();
            lblPinDisplay.Text = $"PIN: {txtPassword.Text}";
            Log($"Generated new session PIN: {txtPassword.Text}");
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
                    btnCopyPin.Text = "📋 Copy PIN";
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            }
        }

        private void cbCaptureMouse_CheckedChanged(object? sender, EventArgs e)
        {
            _captureEngine.CaptureMouse = cbCaptureMouse.Checked;
            _currentSettings.IsShowMouseEnabled = cbCaptureMouse.Checked;
            lblCursorStatus.Text = cbCaptureMouse.Checked ? "Mouse Pointer: Visible" : "Mouse Pointer: Hidden";

            if (_webServer.IsRunning)
            {
                Log(cbCaptureMouse.Checked ? "Cursor capture: Visible" : "Cursor capture: Hidden");
            }
        }

        private void cbAudio_CheckedChanged(object? sender, EventArgs e)
        {
            _captureEngine.SetAudioEnabled(cbAudio.Checked);
            _currentSettings.IsAudioEnabled = cbAudio.Checked;
            comboAudioDevices.Enabled = cbAudio.Checked;
            btnRefreshAudio.Enabled = cbAudio.Checked;
            pbAudioMeter.Enabled = cbAudio.Checked;
            btnTestAudio.Enabled = cbAudio.Checked;
            btnAudioDiag.Enabled = cbAudio.Checked;

            btnQuickMute.Text = cbAudio.Checked ? "🔊 Audio On" : "🔇 Audio Off";
            monitorPreview.AudioInfoText = cbAudio.Checked ? "Audio: Active" : "Audio: Muted";

            if (!cbAudio.Checked)
            {
                pbAudioMeter.Value = 0;
                lblAudioMeterText.Text = "Sound Volume: Muted";
            }

            if (_webServer.IsRunning)
            {
                Log(cbAudio.Checked ? "Audio streaming enabled." : "Audio streaming disabled.");
            }
        }

        private void comboAudioDevices_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (comboAudioDevices.SelectedIndex >= 0 && comboAudioDevices.SelectedIndex < _audioDevices.Count)
            {
                var selected = _audioDevices[comboAudioDevices.SelectedIndex];
                _captureEngine.SetAudioDevice(selected);
                if (_webServer.IsRunning && _captureEngine.AudioEnabled)
                {
                    Log($"Audio device switched to: {selected.Name}");
                }
            }
        }

        private void btnTestAudio_Click(object? sender, EventArgs e)
        {
            try
            {
                System.Media.SystemSounds.Asterisk.Play();
                Log("Playing test sound chime.");
            }
            catch (Exception ex)
            {
                Log($"Test sound error: {ex.Message}");
            }
        }

        private void btnAudioDiag_Click(object? sender, EventArgs e)
        {
            using var diag = new ScreenTask.UI.AudioDiagnosticsForm(_captureEngine.AudioService, selected =>
            {
                if (selected != null)
                {
                    _captureEngine.SetAudioDevice(selected);
                    var idx = _audioDevices.FindIndex(d => d.Id == selected.Id && d.IsInput == selected.IsInput);
                    if (idx >= 0) comboAudioDevices.SelectedIndex = idx;
                    Log($"Audio device changed to: {selected.Name}");
                }
            });
            diag.ShowDialog(this);
        }

        private void PopulateAudioDevices()
        {
            var previousDevId = comboAudioDevices.SelectedIndex >= 0 && comboAudioDevices.SelectedIndex < _audioDevices.Count
                ? _audioDevices[comboAudioDevices.SelectedIndex].Id
                : null;

            comboAudioDevices.Items.Clear();
            _audioDevices = ScreenTask.Core.Audio.WasapiAudioCaptureService.GetAudioDevices();

            int selectedIndex = 0;
            for (int i = 0; i < _audioDevices.Count; i++)
            {
                comboAudioDevices.Items.Add(_audioDevices[i].Name);
                if (previousDevId != null && _audioDevices[i].Id == previousDevId)
                {
                    selectedIndex = i;
                }
            }

            if (comboAudioDevices.Items.Count > 0)
            {
                comboAudioDevices.SelectedIndex = selectedIndex;
            }

            comboAudioDevices.Enabled = cbAudio.Checked;
            btnRefreshAudio.Enabled = cbAudio.Checked;
            pbAudioMeter.Enabled = cbAudio.Checked;
            btnTestAudio.Enabled = cbAudio.Checked;
            btnAudioDiag.Enabled = cbAudio.Checked;
        }

        private void btnRefreshAudio_Click(object? sender, EventArgs e)
        {
            PopulateAudioDevices();
            Log("Refreshed audio input and output devices.");
        }

        private void qualitySlider_Scroll(object? sender, EventArgs e)
        {
            _broadcaster.SetQuality(qualitySlider.Value);
            _currentSettings.ImageQuality = qualitySlider.Value;
            lblQuality.Text = $"Image Quality: {qualitySlider.Value}% ({(qualitySlider.Value >= 80 ? "High Detail" : qualitySlider.Value >= 50 ? "Balanced" : "Low Bandwidth")})";
            if (_webServer.IsRunning)
            {
                Log($"Stream quality set to: {qualitySlider.Value}%");
            }
        }

        private void comboFps_SelectedIndexChanged(object? sender, EventArgs e)
        {
            _captureEngine.TargetFps = comboFps.SelectedIndex switch
            {
                0 => 60,
                1 => 30,
                2 => 15,
                3 => 5,
                _ => 30
            };
            _currentSettings.TargetFps = _captureEngine.TargetFps;
            monitorPreview.FpsText = $"{_captureEngine.TargetFps}.0 FPS";
            if (_webServer.IsRunning)
            {
                Log($"Framerate adjusted to: {_captureEngine.TargetFps} FPS");
            }
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
                    btnCopyLog.Text = "📋 Copy Log";
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            }
        }

        private void Log(string text)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action<string>(Log), text);
                return;
            }
            txtLog.AppendText($"{DateTime.Now:HH:mm:ss} : {text}\r\n");
        }

        private void lblMe_Click(object? sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(Constants.WebsiteUrl) { UseShellExecute = true });
            }
            catch { }
        }

        private void appNotify_Click(object? sender, EventArgs e)
        {
            RestoreFromTray();
        }

        private void RestoreFromTray()
        {
            if (_isCompactMode)
            {
                ToggleCompactMode();
            }
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Focus();
        }

        private void frmMain_Resize(object? sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                appNotify.Visible = true;
                appNotify.ShowBalloonTip(1000, "ScreenTask 2.0", "Running in background tray", ToolTipIcon.Info);
            }
        }

        private async Task CheckForUpdatesAsync()
        {
            try
            {
                _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(Constants.UserAgent);
                var json = await _httpClient.GetStringAsync(Constants.GitHubApiLatestReleaseUrl);
                using var doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty("tag_name", out var tagProp))
                {
                    var tag = tagProp.GetString()?.Replace("v", "") ?? "";
                    if (Version.TryParse(tag, out var latestVer))
                    {
                        var currentVer = new Version(Constants.AppVersion);
                        if (latestVer > currentVer)
                        {
                            this.Invoke(() =>
                            {
                                var res = MessageBox.Show(this,
                                    $"A new ScreenTask version is available (v{latestVer}).\nWould you like to visit the website to update?",
                                    "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (res == DialogResult.Yes)
                                {
                                    Process.Start(new ProcessStartInfo(Constants.WebsiteUrl) { UseShellExecute = true });
                                }
                            });
                        }
                    }
                }
            }
            catch { }
        }

        private void LoadSettings()
        {
            try
            {
                if (File.Exists(Constants.AppSettingsFileName))
                {
                    var serializer = new XmlSerializer(typeof(AppSettings));
                    using var file = File.OpenRead(Constants.AppSettingsFileName);
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
                    txtPassword.Text = _currentSettings.PinCode;
                    txtPassword.Enabled = cbPrivate.Checked;
                    lblPinDisplay.Text = cbPrivate.Checked ? $"PIN: {txtPassword.Text}" : "PIN: Not Required";
                    numPort.Value = Math.Clamp(_currentSettings.Port, 1, 65535);
                    qualitySlider.Value = Math.Clamp(_currentSettings.ImageQuality, 20, 100);
                    lblQuality.Text = $"Image Quality: {qualitySlider.Value}% ({(qualitySlider.Value >= 80 ? "High Detail" : qualitySlider.Value >= 50 ? "Balanced" : "Low Bandwidth")})";

                    int fpsIdx = _currentSettings.TargetFps switch
                    {
                        60 => 0,
                        30 => 1,
                        15 => 2,
                        5 => 3,
                        _ => 1
                    };
                    comboFps.SelectedIndex = fpsIdx;
                    fpsSegmentedControl.SelectedIndex = fpsIdx;

                    if (!string.IsNullOrEmpty(_currentSettings.IP))
                    {
                        var match = _adapters.FirstOrDefault(a => a.IpAddress == _currentSettings.IP);
                        if (match != null)
                        {
                            comboIPs.SelectedIndex = _adapters.IndexOf(match);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Could not load settings: {ex.Message}");
            }
        }

        private void SaveSettings()
        {
            try
            {
                _currentSettings.Port = (int)numPort.Value;
                _currentSettings.PinCode = txtPassword.Text;
                _currentSettings.IsShowMouseEnabled = cbCaptureMouse.Checked;
                _currentSettings.IsAudioEnabled = cbAudio.Checked;
                _currentSettings.IsPrivateSession = cbPrivate.Checked;
                _currentSettings.IsAutoStartServerEnabled = cbAutoStart.Checked;
                _currentSettings.IsStartMinimizedEnabled = cbMiniStart.Checked;
                _currentSettings.ImageQuality = qualitySlider.Value;
                _currentSettings.TargetFps = comboFps.SelectedIndex switch
                {
                    0 => 60,
                    1 => 30,
                    2 => 15,
                    3 => 5,
                    _ => 30
                };

                if (comboIPs.SelectedIndex >= 0 && comboIPs.SelectedIndex < _adapters.Count)
                {
                    _currentSettings.IP = _adapters[comboIPs.SelectedIndex].IpAddress;
                }

                var serializer = new XmlSerializer(typeof(AppSettings));
                using var file = new FileStream(Constants.AppSettingsFileName, FileMode.Create, FileAccess.Write);
                serializer.Serialize(file, _currentSettings);
            }
            catch { }
        }

        private void frmMain_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!_isExitingExplicitly && cbCloseToTray.Checked && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                appNotify.Visible = true;
                appNotify.ShowBalloonTip(1000, "ScreenTask 2.0", "Minimized to tray. Right-click icon to exit.", ToolTipIcon.Info);
                return;
            }

            SaveSettings();
            _deviceChangeTimer.Stop();
            _deviceChangeTimer.Dispose();
            _statsTimer.Stop();
            _ = _webServer.StopAsync();
            _ = _captureEngine.StopAsync();
            _broadcaster.Dispose();
        }
    }
}
