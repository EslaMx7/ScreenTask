using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScreenTask.Core.Streaming;

namespace ScreenTask.UI
{
    /// <summary>
    /// Dedicated native Form frame for inspecting real-time connected viewers fleet telemetry,
    /// managing viewer audio/kick, sending broadcast alerts, and displaying connection QR code.
    /// </summary>
    public class ConnectedViewersForm : Form
    {
        private readonly Dictionary<string, Panel> _sessionCardPanels = new();

        public event EventHandler? SendAnnouncementClicked;
        public event EventHandler? PairingCopyClicked;
        public event EventHandler? PairingRegenClicked;

        // Metric Card Controls
        public Panel cardMetricViewers = null!;
        public Label lblIconMetricViewers = null!;
        public Label lblTitleMetricViewers = null!;
        public Label lblMetricViewersVal = null!;
        public ProgressBar pbViewerCapacity = null!;

        public Panel cardMetricLatency = null!;
        public Label lblIconMetricLatency = null!;
        public Label lblTitleMetricLatency = null!;
        public Label lblMetricLatencyVal = null!;
        public Label lblMetricLatencySub = null!;

        public Panel cardMetricSync = null!;
        public Label lblIconMetricSync = null!;
        public Label lblTitleMetricSync = null!;
        public Label lblMetricSyncVal = null!;
        public Label lblMetricSyncSub = null!;

        // Fleet Terminal Controls
        public Panel cardFleetTerminal = null!;
        public Label lblFleetHeader = null!;
        public Panel panelSessionList = null!;
        public Label lblNoClientsPlaceholder = null!;
        public Label lblAnnouncement = null!;
        public TextBox txtAnnouncement = null!;
        public Button btnSendAnnouncement = null!;
        public Label lblAnnouncementStatus = null!;

        // Pairing Pass Controls
        public Panel cardPairingPass = null!;
        public PictureBox picPairingQr = null!;
        public Label lblPairingUrlTitle = null!;
        public TextBox txtPairingUrl = null!;
        public Button btnPairingCopy = null!;
        public Label lblPairingPin = null!;
        public Button btnPairingRegen = null!;
        public Label lblZeroConf = null!;
        public Panel panelSpotlight = null!;
        public Label lblSpotlightTitle = null!;
        public Label lblSpotlight1 = null!;
        public Label lblSpotlight2 = null!;
        public Label lblSpotlight3 = null!;
        public Label lblSpotlight4 = null!;
        public Label lblSpotlight5 = null!;

        // Bottom Controls
        public Panel panelBottom = null!;
        public Button btnClose = null!;
        public Panel panelMainContent = null!;

        public string AnnouncementMessage => txtAnnouncement.Text.Trim();

        public ConnectedViewersForm()
        {
            InitializeCustomComponents();
            WireEvents();
        }

        private void InitializeCustomComponents()
        {
            Text = "ScreenTask 2.0 – Connected Viewers & Fleet";
            Size = new Size(820, 620);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = SystemColors.Control;
            ForeColor = SystemColors.ControlText;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            // Bottom Panel
            panelBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 46,
                Padding = new Padding(12, 6, 12, 6),
                BackColor = SystemColors.Control
            };

            btnClose = new Button
            {
                Text = "Close",
                DialogResult = DialogResult.OK,
                Size = new Size(90, 32),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(ClientSize.Width - 102, 6),
                UseVisualStyleBackColor = true
            };
            btnClose.Click += (s, e) => Close();
            panelBottom.Controls.Add(btnClose);
            Controls.Add(panelBottom);

            // Main Content Panel
            panelMainContent = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(12),
                BackColor = SystemColors.Control
            };
            Controls.Add(panelMainContent);
            panelMainContent.BringToFront();

            // 1. Metric Viewers
            cardMetricViewers = new Panel
            {
                BackColor = SystemColors.Window,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(240, 72)
            };
            lblIconMetricViewers = new Label
            {
                Font = new Font("Segoe UI", 12F),
                Location = new Point(8, 8),
                Size = new Size(24, 22),
                Text = "👥"
            };
            lblTitleMetricViewers = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = SystemColors.GrayText,
                Location = new Point(34, 10),
                Text = "LAN Active Viewers"
            };
            lblMetricViewersVal = new Label
            {
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = SystemColors.ControlText,
                Location = new Point(8, 30),
                Size = new Size(220, 20),
                Text = "0 / 32 Active"
            };
            pbViewerCapacity = new ProgressBar
            {
                Location = new Point(8, 52),
                Size = new Size(220, 8),
                Maximum = 32,
                Value = 0
            };
            cardMetricViewers.Controls.Add(lblIconMetricViewers);
            cardMetricViewers.Controls.Add(lblTitleMetricViewers);
            cardMetricViewers.Controls.Add(lblMetricViewersVal);
            cardMetricViewers.Controls.Add(pbViewerCapacity);
            panelMainContent.Controls.Add(cardMetricViewers);

            // 2. Metric Latency
            cardMetricLatency = new Panel
            {
                BackColor = SystemColors.Window,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(240, 72)
            };
            lblIconMetricLatency = new Label
            {
                Font = new Font("Segoe UI", 12F),
                Location = new Point(8, 8),
                Size = new Size(24, 22),
                Text = "⚡"
            };
            lblTitleMetricLatency = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = SystemColors.GrayText,
                Location = new Point(34, 10),
                Text = "Fleet Average Latency"
            };
            lblMetricLatencyVal = new Label
            {
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = SystemColors.ControlText,
                Location = new Point(8, 30),
                Size = new Size(220, 20),
                Text = "0.0 ms Avg"
            };
            lblMetricLatencySub = new Label
            {
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = SystemColors.GrayText,
                Location = new Point(8, 52),
                Size = new Size(220, 16),
                Text = "<50ms pipeline • 0.0% drop"
            };
            cardMetricLatency.Controls.Add(lblIconMetricLatency);
            cardMetricLatency.Controls.Add(lblTitleMetricLatency);
            cardMetricLatency.Controls.Add(lblMetricLatencyVal);
            cardMetricLatency.Controls.Add(lblMetricLatencySub);
            panelMainContent.Controls.Add(cardMetricLatency);

            // 3. Metric Sync
            cardMetricSync = new Panel
            {
                BackColor = SystemColors.Window,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(280, 72)
            };
            lblIconMetricSync = new Label
            {
                Font = new Font("Segoe UI", 12F),
                Location = new Point(8, 8),
                Size = new Size(24, 22),
                Text = "🎵"
            };
            lblTitleMetricSync = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = SystemColors.GrayText,
                Location = new Point(34, 10),
                Text = "Audio/Video Lip Sync"
            };
            lblMetricSyncVal = new Label
            {
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = SystemColors.ControlText,
                Location = new Point(8, 30),
                Size = new Size(260, 20),
                Text = "100% Locked"
            };
            lblMetricSyncSub = new Label
            {
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = SystemColors.GrayText,
                Location = new Point(8, 52),
                Size = new Size(260, 16),
                Text = "WASAPI + Display (0 drop, ±0.0ms)"
            };
            cardMetricSync.Controls.Add(lblIconMetricSync);
            cardMetricSync.Controls.Add(lblTitleMetricSync);
            cardMetricSync.Controls.Add(lblMetricSyncVal);
            cardMetricSync.Controls.Add(lblMetricSyncSub);
            panelMainContent.Controls.Add(cardMetricSync);

            // 4. Fleet Terminal (Left)
            cardFleetTerminal = new Panel
            {
                BackColor = SystemColors.Window,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(12, 88),
                Size = new Size(490, 430)
            };

            lblFleetHeader = new Label
            {
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = SystemColors.ControlText,
                Location = new Point(10, 8),
                Size = new Size(400, 20),
                Text = "Live Client Sessions (0 Connected):"
            };
            cardFleetTerminal.Controls.Add(lblFleetHeader);

            panelSessionList = new Panel
            {
                AutoScroll = true,
                BackColor = SystemColors.ControlLightLight,
                Location = new Point(10, 32),
                Size = new Size(468, 300)
            };
            lblNoClientsPlaceholder = new Label
            {
                Name = "lblNoClientsPlaceholder",
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = SystemColors.GrayText,
                Location = new Point(10, 120),
                Size = new Size(448, 40),
                Text = "No viewers currently connected. Share the link or scan the QR pass on the right to start watching.",
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelSessionList.Controls.Add(lblNoClientsPlaceholder);
            cardFleetTerminal.Controls.Add(panelSessionList);

            lblAnnouncement = new Label
            {
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = SystemColors.ControlText,
                Location = new Point(10, 338),
                Size = new Size(250, 16),
                Text = "📢 Dispatch Broadcast Alert / Toast:"
            };
            cardFleetTerminal.Controls.Add(lblAnnouncement);

            txtAnnouncement = new TextBox
            {
                Font = new Font("Segoe UI", 8.5F),
                Location = new Point(10, 356),
                Size = new Size(360, 23),
                PlaceholderText = "Type announcement for all connected viewers..."
            };
            cardFleetTerminal.Controls.Add(txtAnnouncement);

            btnSendAnnouncement = new Button
            {
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                Location = new Point(376, 355),
                Size = new Size(102, 25),
                Text = "Send Toast",
                UseVisualStyleBackColor = true
            };
            cardFleetTerminal.Controls.Add(btnSendAnnouncement);

            lblAnnouncementStatus = new Label
            {
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = SystemColors.GrayText,
                Location = new Point(10, 384),
                Size = new Size(468, 16),
                Text = "Alerts appear as live animated banners on all client screens."
            };
            cardFleetTerminal.Controls.Add(lblAnnouncementStatus);
            panelMainContent.Controls.Add(cardFleetTerminal);

            // 5. Pairing Pass (Right)
            cardPairingPass = new Panel
            {
                BackColor = SystemColors.Window,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(510, 88),
                Size = new Size(280, 430)
            };

            picPairingQr = new PictureBox
            {
                BackColor = Color.White,
                Location = new Point(60, 16),
                Size = new Size(160, 160),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            cardPairingPass.Controls.Add(picPairingQr);

            lblPairingUrlTitle = new Label
            {
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                Location = new Point(10, 184),
                Size = new Size(260, 16),
                Text = "Direct Web Viewer URL:"
            };
            cardPairingPass.Controls.Add(lblPairingUrlTitle);

            txtPairingUrl = new TextBox
            {
                Font = new Font("Consolas", 8.5F),
                Location = new Point(10, 202),
                Size = new Size(185, 21),
                ReadOnly = true
            };
            cardPairingPass.Controls.Add(txtPairingUrl);

            btnPairingCopy = new Button
            {
                Font = new Font("Segoe UI", 7.5F),
                Location = new Point(200, 201),
                Size = new Size(70, 23),
                Text = "Copy",
                UseVisualStyleBackColor = true
            };
            cardPairingPass.Controls.Add(btnPairingCopy);

            lblPairingPin = new Label
            {
                Font = new Font("Consolas", 9F, FontStyle.Bold),
                ForeColor = SystemColors.Highlight,
                Location = new Point(10, 230),
                Size = new Size(185, 20),
                Text = "PIN: Not Required"
            };
            cardPairingPass.Controls.Add(lblPairingPin);

            btnPairingRegen = new Button
            {
                Font = new Font("Segoe UI", 7.5F),
                Location = new Point(200, 229),
                Size = new Size(70, 23),
                Text = "🎲 New",
                UseVisualStyleBackColor = true
            };
            cardPairingPass.Controls.Add(btnPairingRegen);

            lblZeroConf = new Label
            {
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = SystemColors.GrayText,
                Location = new Point(10, 256),
                Size = new Size(260, 30),
                Text = "📡 ZeroConf: screentask.local\nAdapter: Wi-Fi"
            };
            cardPairingPass.Controls.Add(lblZeroConf);

            panelSpotlight = new Panel
            {
                BackColor = SystemColors.Control,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(10, 292),
                Size = new Size(258, 126),
                Padding = new Padding(8)
            };
            lblSpotlightTitle = new Label
            {
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                ForeColor = SystemColors.ControlText,
                Location = new Point(6, 6),
                Size = new Size(244, 16),
                Text = "Fleet Sharing Features:"
            };
            lblSpotlight1 = new Label
            {
                Font = new Font("Segoe UI", 7F),
                ForeColor = SystemColors.ControlDarkDark,
                Location = new Point(6, 24),
                Size = new Size(244, 14),
                Text = "• Zero-configuration LAN discovery"
            };
            lblSpotlight2 = new Label
            {
                Font = new Font("Segoe UI", 7F),
                ForeColor = SystemColors.ControlDarkDark,
                Location = new Point(6, 40),
                Size = new Size(244, 14),
                Text = "• Sub-50ms WebSocket streaming"
            };
            lblSpotlight3 = new Label
            {
                Font = new Font("Segoe UI", 7F),
                ForeColor = SystemColors.ControlDarkDark,
                Location = new Point(6, 56),
                Size = new Size(244, 14),
                Text = "• Automatic frame drop protection"
            };
            lblSpotlight4 = new Label
            {
                Font = new Font("Segoe UI", 7F),
                ForeColor = SystemColors.ControlDarkDark,
                Location = new Point(6, 72),
                Size = new Size(244, 14),
                Text = "• Individual viewer audio mute control"
            };
            lblSpotlight5 = new Label
            {
                Font = new Font("Segoe UI", 7F),
                ForeColor = SystemColors.ControlDarkDark,
                Location = new Point(6, 88),
                Size = new Size(244, 14),
                Text = "• Live on-screen announcement toasts"
            };
            panelSpotlight.Controls.Add(lblSpotlightTitle);
            panelSpotlight.Controls.Add(lblSpotlight1);
            panelSpotlight.Controls.Add(lblSpotlight2);
            panelSpotlight.Controls.Add(lblSpotlight3);
            panelSpotlight.Controls.Add(lblSpotlight4);
            panelSpotlight.Controls.Add(lblSpotlight5);
            cardPairingPass.Controls.Add(panelSpotlight);

            panelMainContent.Controls.Add(cardPairingPass);

            panelMainContent.Resize += (s, e) => UpdateLayout();
            UpdateLayout();
        }

        private void WireEvents()
        {
            btnSendAnnouncement.Click += (s, e) => SendAnnouncementClicked?.Invoke(this, EventArgs.Empty);
            btnPairingCopy.Click += (s, e) => PairingCopyClicked?.Invoke(this, EventArgs.Empty);
            btnPairingRegen.Click += (s, e) => PairingRegenClicked?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateLayout()
        {
            if (panelMainContent.ClientSize.Width <= 0 || panelMainContent.ClientSize.Height <= 0) return;
            int totalWidth = panelMainContent.ClientSize.Width;
            int totalHeight = panelMainContent.ClientSize.Height;

            // 1. Top 3 Metric Cards
            int gap = 8;
            int metricWidth = Math.Max(150, (totalWidth - (2 * gap)) / 3);
            cardMetricViewers.Location = new Point(0, 0);
            cardMetricViewers.Width = metricWidth;
            lblMetricViewersVal.Width = Math.Max(100, cardMetricViewers.ClientSize.Width - 16);
            pbViewerCapacity.Width = Math.Max(100, cardMetricViewers.ClientSize.Width - 16);

            cardMetricLatency.Location = new Point(metricWidth + gap, 0);
            cardMetricLatency.Width = metricWidth;
            lblMetricLatencyVal.Width = Math.Max(100, cardMetricLatency.ClientSize.Width - 16);
            lblMetricLatencySub.Width = Math.Max(100, cardMetricLatency.ClientSize.Width - 16);

            cardMetricSync.Location = new Point((metricWidth + gap) * 2, 0);
            cardMetricSync.Width = totalWidth - cardMetricSync.Left;
            lblMetricSyncVal.Width = Math.Max(100, cardMetricSync.ClientSize.Width - 16);
            lblMetricSyncSub.Width = Math.Max(100, cardMetricSync.ClientSize.Width - 16);

            // 2. Bottom 2 Panels: Fleet Terminal (left) and Pairing Pass (right)
            int rightCardWidth = Math.Min(300, Math.Max(260, (int)(totalWidth * 0.35)));
            cardPairingPass.Location = new Point(Math.Max(380, totalWidth - rightCardWidth), 80);
            cardPairingPass.Width = rightCardWidth;
            cardPairingPass.Height = Math.Max(380, totalHeight - 80);

            // Respect 1:1 Square Aspect Ratio for QR Code
            int qrSize = Math.Min(160, Math.Max(100, cardPairingPass.ClientSize.Width - 40));
            picPairingQr.Size = new Size(qrSize, qrSize);
            picPairingQr.Location = new Point((cardPairingPass.ClientSize.Width - qrSize) / 2, 16);

            // Pairing controls below QR
            lblPairingUrlTitle.Location = new Point(10, picPairingQr.Bottom + 10);
            btnPairingCopy.Location = new Point(cardPairingPass.ClientSize.Width - btnPairingCopy.Width - 10, lblPairingUrlTitle.Bottom + 2);
            txtPairingUrl.Location = new Point(10, lblPairingUrlTitle.Bottom + 4);
            txtPairingUrl.Width = Math.Max(80, btnPairingCopy.Left - 16);

            btnPairingRegen.Location = new Point(cardPairingPass.ClientSize.Width - btnPairingRegen.Width - 10, txtPairingUrl.Bottom + 8);
            lblPairingPin.Location = new Point(10, txtPairingUrl.Bottom + 10);
            lblPairingPin.Width = Math.Max(80, btnPairingRegen.Left - 14);

            lblZeroConf.Location = new Point(10, btnPairingRegen.Bottom + 6);
            lblZeroConf.Width = cardPairingPass.ClientSize.Width - 20;

            int spotlightTop = lblZeroConf.Bottom + 6;
            panelSpotlight.Location = new Point(10, spotlightTop);
            panelSpotlight.Width = cardPairingPass.ClientSize.Width - 20;
            panelSpotlight.Height = Math.Max(90, cardPairingPass.ClientSize.Height - spotlightTop - 10);

            // Fleet Terminal Left Card
            cardFleetTerminal.Location = new Point(0, 80);
            cardFleetTerminal.Width = Math.Max(340, cardPairingPass.Left - 10);
            cardFleetTerminal.Height = Math.Max(380, totalHeight - 80);

            panelSessionList.Width = cardFleetTerminal.Width - 20;
            panelSessionList.Height = Math.Max(150, cardFleetTerminal.Height - 110);
            lblNoClientsPlaceholder.Width = panelSessionList.ClientSize.Width - 20;

            int currentListWidth = Math.Max(320, panelSessionList.ClientSize.Width - 10);
            foreach (var pnl in _sessionCardPanels.Values)
            {
                pnl.Width = currentListWidth;
            }

            lblAnnouncement.Top = cardFleetTerminal.Height - 88;
            txtAnnouncement.Top = cardFleetTerminal.Height - 68;
            btnSendAnnouncement.Top = cardFleetTerminal.Height - 70;
            lblAnnouncementStatus.Top = cardFleetTerminal.Height - 40;
            lblAnnouncementStatus.Width = cardFleetTerminal.Width - 20;

            btnSendAnnouncement.Left = cardFleetTerminal.Width - btnSendAnnouncement.Width - 10;
            txtAnnouncement.Width = Math.Max(100, btnSendAnnouncement.Left - txtAnnouncement.Left - 8);
        }

        public void UpdateMetricViewers(int activeCount, int capacity = 32)
        {
            lblMetricViewersVal.Text = $"{activeCount} / {capacity} Active";
            pbViewerCapacity.Maximum = capacity;
            pbViewerCapacity.Value = Math.Clamp(activeCount, 0, capacity);
            lblFleetHeader.Text = $"Live Client Sessions ({activeCount}):";
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
            lblNoClientsPlaceholder.Visible = count == 0;

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
            if (_sessionCardPanels.TryGetValue(session.Id, out var panel))
            {
                if (panel.InvokeRequired)
                {
                    try { panel.BeginInvoke(() => RefreshSessionCardData(session)); } catch { }
                    return;
                }

                foreach (Control ctrl in panel.Controls)
                {
                    if (ctrl.Name == "lblClientBadge")
                    {
                        ctrl.Text = session.ViewMode;
                    }
                    else if (ctrl.Name == "lblClientMetrics")
                    {
                        ctrl.Text = $"RTT: {(session.RttMs > 0 ? $"{session.RttMs:0} ms" : "<1 ms")} • Up: {session.FormattedUptime}";
                    }
                    else if (ctrl.Name.StartsWith("btnMute"))
                    {
                        ctrl.Text = session.IsAudioMuted ? "🔇 Unmute" : "🔊 Mute";
                    }
                }
            }
        }
    }
}
