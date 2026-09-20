using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScreenTask.Core.Streaming;

namespace ScreenTask.UI.Views
{
    public partial class ConnectedViewersView : UserControl
    {
        private readonly Dictionary<string, Panel> _sessionCardPanels = new();

        public event EventHandler? SendAnnouncementClicked;
        public event EventHandler? PairingCopyClicked;
        public event EventHandler? PairingRegenClicked;

        public ConnectedViewersView()
        {
            InitializeComponent();

            btnSendAnnouncement.Click += (s, e) => SendAnnouncementClicked?.Invoke(this, EventArgs.Empty);
            btnPairingCopy.Click += (s, e) => PairingCopyClicked?.Invoke(this, EventArgs.Empty);
            btnPairingRegen.Click += (s, e) => PairingRegenClicked?.Invoke(this, EventArgs.Empty);

            this.Resize += (s, e) => UpdateLayout();
        }

        private void UpdateLayout()
        {
            if (this.ClientSize.Width <= 0 || this.ClientSize.Height <= 0) return;
            int totalWidth = this.ClientSize.Width;
            int totalHeight = this.ClientSize.Height;

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
            int rightCardWidth = Math.Min(320, Math.Max(260, (int)(totalWidth * 0.32)));
            cardPairingPass.Location = new Point(Math.Max(380, totalWidth - rightCardWidth), 80);
            cardPairingPass.Width = rightCardWidth;
            cardPairingPass.Height = Math.Max(400, totalHeight - 80);

            // Respect 1:1 Square Aspect Ratio for QR Code
            int qrSize = Math.Min(180, Math.Max(120, cardPairingPass.ClientSize.Width - 40));
            picPairingQr.Size = new Size(qrSize, qrSize);
            picPairingQr.Location = new Point((cardPairingPass.ClientSize.Width - qrSize) / 2, 20);
            picPairingQr.SizeMode = PictureBoxSizeMode.Zoom;

            // Pairing controls below QR
            lblPairingUrlTitle.Location = new Point(10, picPairingQr.Bottom + 10);
            btnPairingCopy.Location = new Point(cardPairingPass.ClientSize.Width - btnPairingCopy.Width - 10, lblPairingUrlTitle.Bottom + 2);
            txtPairingUrl.Location = new Point(10, lblPairingUrlTitle.Bottom + 4);
            txtPairingUrl.Width = Math.Max(100, btnPairingCopy.Left - 16);

            btnPairingRegen.Location = new Point(cardPairingPass.ClientSize.Width - btnPairingRegen.Width - 10, txtPairingUrl.Bottom + 8);
            lblPairingPin.Location = new Point(10, txtPairingUrl.Bottom + 10);
            lblPairingPin.Width = Math.Max(100, btnPairingRegen.Left - 14);

            lblZeroConf.Location = new Point(10, btnPairingRegen.Bottom + 6);
            lblZeroConf.Width = cardPairingPass.ClientSize.Width - 20;

            int spotlightTop = lblZeroConf.Bottom + 6;
            panelSpotlight.Location = new Point(10, spotlightTop);
            panelSpotlight.Width = cardPairingPass.ClientSize.Width - 20;
            panelSpotlight.Height = Math.Max(100, cardPairingPass.ClientSize.Height - spotlightTop - 10);
            lblSpotlightTitle.Width = panelSpotlight.ClientSize.Width - 16;
            lblSpotlight1.Width = panelSpotlight.ClientSize.Width - 16;
            lblSpotlight2.Width = panelSpotlight.ClientSize.Width - 16;
            lblSpotlight3.Width = panelSpotlight.ClientSize.Width - 16;
            lblSpotlight4.Width = panelSpotlight.ClientSize.Width - 16;
            lblSpotlight5.Width = panelSpotlight.ClientSize.Width - 16;

            // Fleet Terminal Left Card
            cardFleetTerminal.Location = new Point(0, 80);
            cardFleetTerminal.Width = Math.Max(360, cardPairingPass.Left - 10);
            cardFleetTerminal.Height = Math.Max(400, totalHeight - 80);

            panelSessionList.Width = cardFleetTerminal.Width - 20;
            panelSessionList.Height = Math.Max(150, cardFleetTerminal.Height - 110);
            lblNoClientsPlaceholder.Width = panelSessionList.ClientSize.Width - 20;

            // Synchronize existing session card widths
            int currentListWidth = Math.Max(340, panelSessionList.ClientSize.Width - 10);
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

        public string AnnouncementMessage => txtAnnouncement.Text.Trim();

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
            int cardWidth = Math.Max(346, panelSessionList.ClientSize.Width - 10);
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
