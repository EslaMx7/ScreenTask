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
        private AppSettings _currentSettings = new();
        private readonly System.Windows.Forms.Timer _statsTimer = new();
        private readonly HttpClient _httpClient = new();

        public frmMain()
        {
            InitializeComponent();

            // Core 2.0 Engine Initialization
            _broadcaster = new StreamBroadcaster(80);
            _captureEngine = new ScreenCaptureEngine(_broadcaster);
            _webServer = new EmbeddedWebServer(_broadcaster, _captureEngine);

            _statsTimer.Interval = 1000;
            _statsTimer.Tick += OnStatsTimerTick;

            WireEvents();
        }

        private void WireEvents()
        {
            this.Load += frmMain_Load;
            this.FormClosing += frmMain_FormClosing;
            this.Resize += frmMain_Resize;

            btnStartServer.Click += btnStartServer_Click;
            btnLaunch.Click += btnLaunch_Click;
            btnCopyUrl.Click += btnCopyUrl_Click;
            btnQr.Click += btnQr_Click;

            cbPrivate.CheckedChanged += cbPrivate_CheckedChanged;
            cbCaptureMouse.CheckedChanged += cbCaptureMouse_CheckedChanged;
            cbAudio.CheckedChanged += cbAudio_CheckedChanged;
            qualitySlider.Scroll += qualitySlider_Scroll;
            comboFps.SelectedIndexChanged += comboFps_SelectedIndexChanged;

            lblMe.Click += lblMe_Click;
            appNotify.Click += appNotify_Click;
            appNotify.MouseDoubleClick += (s, e) => RestoreFromTray();
        }

        private void frmMain_Load(object? sender, EventArgs e)
        {
            PopulateNetworkAdapters();
            PopulateCaptureSources();
            LoadSettings();

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

        private void PopulateNetworkAdapters()
        {
            comboIPs.Items.Clear();
            _adapters = NetworkHelper.GetAvailableAdapters();

            int recommendedIndex = 0;
            for (int i = 0; i < _adapters.Count; i++)
            {
                comboIPs.Items.Add(_adapters[i].DisplayName);
                if (_adapters[i].IsRecommended && recommendedIndex == 0)
                {
                    recommendedIndex = i;
                }
            }

            if (comboIPs.Items.Count > 0)
            {
                comboIPs.SelectedIndex = recommendedIndex;
                UpdateUrlPreview();
            }
        }

        private void PopulateCaptureSources()
        {
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

            if (comboScreens.Items.Count > 0)
            {
                comboScreens.SelectedIndex = 0;
            }
        }

        private void UpdateUrlPreview()
        {
            if (comboIPs.SelectedIndex >= 0 && comboIPs.SelectedIndex < _adapters.Count)
            {
                var ip = _adapters[comboIPs.SelectedIndex].IpAddress;
                txtURL.Text = $"http://{ip}:{numPort.Value}";
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

                // 1. Configure Selected Capture Source
                if (comboScreens.SelectedIndex >= 0 && comboScreens.SelectedIndex < _captureSources.Count)
                {
                    _captureEngine.SetSource(_captureSources[comboScreens.SelectedIndex]);
                }
                else
                {
                    _captureEngine.SetSource(new ScreenCaptureSource(0));
                }

                // 2. Configure Stream Options
                _captureEngine.CaptureMouse = cbCaptureMouse.Checked;
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

                _broadcaster.SetQuality(qualitySlider.Value);

                // 3. Configure Security
                _webServer.RequirePin = cbPrivate.Checked;
                _webServer.PinCode = txtPassword.Text.Trim();

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
                _statsTimer.Start();

                var streamUrl = _webServer.ServerUrl;
                txtURL.Text = streamUrl;

                // Update UI state
                btnStartServer.Text = "⏹  STOP BROADCAST";
                btnStartServer.BackColor = Color.FromArgb(239, 68, 68); // Red
                lblStatusBadge.Text = "● LIVE";
                lblStatusBadge.ForeColor = Color.FromArgb(34, 197, 94); // Green

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
            }
        }

        private async Task StopServerAsync()
        {
            try
            {
                btnStartServer.Enabled = false;
                Log("Stopping streaming server...");

                _statsTimer.Stop();
                await _captureEngine.StopAsync();
                await _webServer.StopAsync();

                btnStartServer.Text = "▶  START BROADCAST";
                btnStartServer.BackColor = Color.FromArgb(99, 102, 241); // Indigo
                lblStatusBadge.Text = "● READY";
                lblStatusBadge.ForeColor = Color.FromArgb(148, 163, 184); // Muted

                lblLiveStats.Text = "⚡ Stopped • Ready to broadcast";
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
            }
        }

        private void OnStatsTimerTick(object? sender, EventArgs e)
        {
            if (!_webServer.IsRunning) return;

            int viewers = _broadcaster.ConnectedClientsCount;
            int fps = _broadcaster.CurrentFps;
            long bytes = _broadcaster.TotalBytesTransferred;
            string mbTransferred = (bytes / (1024.0 * 1024.0)).ToString("0.0");

            lblLiveStats.Text = $"🟢 LIVE: {viewers} Viewers | {fps} FPS | {mbTransferred} MB sent";
        }

        private void btnCopyUrl_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtURL.Text))
            {
                Clipboard.SetText(txtURL.Text);
                btnCopyUrl.Text = "✓";
                var timer = new System.Windows.Forms.Timer { Interval = 1200 };
                timer.Tick += (s, args) =>
                {
                    btnCopyUrl.Text = "Copy";
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
            _currentSettings.IsPrivateSession = cbPrivate.Checked;
            if (cbPrivate.Checked) txtPassword.Focus();
        }

        private void cbCaptureMouse_CheckedChanged(object? sender, EventArgs e)
        {
            _captureEngine.CaptureMouse = cbCaptureMouse.Checked;
            _currentSettings.IsShowMouseEnabled = cbCaptureMouse.Checked;
        }

        private void cbAudio_CheckedChanged(object? sender, EventArgs e)
        {
            _captureEngine.AudioEnabled = cbAudio.Checked;
            _currentSettings.IsAudioEnabled = cbAudio.Checked;
        }

        private void qualitySlider_Scroll(object? sender, EventArgs e)
        {
            _broadcaster.SetQuality(qualitySlider.Value);
            _currentSettings.ImageQuality = qualitySlider.Value;
            lblQuality.Text = $"Quality: {qualitySlider.Value}%";
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
                    cbAutoStart.Checked = _currentSettings.IsAutoStartServerEnabled;
                    cbMiniStart.Checked = _currentSettings.IsStartMinimizedEnabled;
                    txtPassword.Text = _currentSettings.PinCode;
                    numPort.Value = Math.Clamp(_currentSettings.Port, 1, 65535);
                    qualitySlider.Value = Math.Clamp(_currentSettings.ImageQuality, 20, 100);
                    lblQuality.Text = $"Quality: {qualitySlider.Value}%";

                    comboFps.SelectedIndex = _currentSettings.TargetFps switch
                    {
                        60 => 0,
                        30 => 1,
                        15 => 2,
                        5 => 3,
                        _ => 1
                    };

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
            SaveSettings();
            _statsTimer.Stop();
            _ = _webServer.StopAsync();
            _ = _captureEngine.StopAsync();
            _broadcaster.Dispose();
        }
    }
}
