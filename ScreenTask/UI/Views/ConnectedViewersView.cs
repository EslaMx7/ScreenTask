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
            var panel = new Panel
            {
                Name = index == 0 ? "panelClientItem1" : $"panelClientItem_{session.Id}",
                Size = new Size(346, 72),
                Location = new Point(4, index * 78 + 4),
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
                Location = new Point(170, 4),
                Size = new Size(166, 16),
                TextAlign = ContentAlignment.MiddleRight,
                Text = session.ViewMode
            };

            var lblDetails = new Label
            {
                Name = "lblClientDetails",
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = SystemColors.GrayText,
                Location = new Point(6, 22),
                Size = new Size(330, 15),
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

            var btnMute = new Button
            {
                Name = index == 0 ? "btnMuteClient1" : $"btnMute_{session.Id}",
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = SystemColors.ControlText,
                Location = new Point(198, 42),
                Size = new Size(70, 24),
                Text = session.IsAudioMuted ? "🔇 Unmute" : "🔊 Mute",
                UseVisualStyleBackColor = true
            };

            var btnKick = new Button
            {
                Name = index == 0 ? "btnKickClient1" : $"btnKick_{session.Id}",
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = SystemColors.ControlText,
                Location = new Point(272, 42),
                Size = new Size(64, 24),
                Text = "✕ Kick",
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
