using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenTask.UI.Views
{
    public partial class BroadcastView : UserControl
    {
        public event EventHandler? StartServerClicked;
        public event EventHandler? QuickMuteClicked;
        public event EventHandler? SwitchSourceClicked;
        public event EventHandler? CopyUrlClicked;
        public event EventHandler? QrClicked;
        public event EventHandler? LaunchClicked;

        public BroadcastView()
        {
            InitializeComponent();

            btnStartServer.Click += (s, e) => StartServerClicked?.Invoke(this, EventArgs.Empty);
            btnQuickMute.Click += (s, e) => QuickMuteClicked?.Invoke(this, EventArgs.Empty);
            btnSwitchSource.Click += (s, e) => SwitchSourceClicked?.Invoke(this, EventArgs.Empty);
            btnCopyUrl.Click += (s, e) => CopyUrlClicked?.Invoke(this, EventArgs.Empty);
            btnQr.Click += (s, e) => QrClicked?.Invoke(this, EventArgs.Empty);
            btnLaunch.Click += (s, e) => LaunchClicked?.Invoke(this, EventArgs.Empty);

            this.Resize += (s, e) => UpdateLayout();
        }

        private void UpdateLayout()
        {
            if (this.ClientSize.Width <= 0) return;
            int totalWidth = this.ClientSize.Width;

            // 1. Top Section (Preview & Control Card)
            int controlCardWidth = 258;
            groupBox3.Location = new Point(Math.Max(384, totalWidth - controlCardWidth), 0);
            cardPreview.Width = Math.Max(380, groupBox3.Left - 10);
            monitorPreview.Width = cardPreview.Width - 24;
            btnSwitchSource.Left = cardPreview.Width - btnSwitchSource.Width - 14;

            // 2. Middle Section (Stream URL Card)
            cardUrl.Width = totalWidth;
            lblPinDisplay.Left = cardUrl.Width - lblPinDisplay.Width - 12;
            btnLaunch.Left = cardUrl.Width - btnLaunch.Width - 10;
            btnQr.Left = btnLaunch.Left - btnQr.Width - 6;
            btnCopyUrl.Left = btnQr.Left - btnCopyUrl.Width - 6;
            txtURL.Width = Math.Max(150, btnCopyUrl.Left - txtURL.Left - 8);

            // 3. Bottom Section (4 Stat Chips)
            int gap = 8;
            int chipWidth = Math.Max(130, (totalWidth - (3 * gap)) / 4);
            cardChipViewers.Location = new Point(0, 328);
            cardChipViewers.Width = chipWidth;

            cardChipFps.Location = new Point(chipWidth + gap, 328);
            cardChipFps.Width = chipWidth;

            cardChipData.Location = new Point((chipWidth + gap) * 2, 328);
            cardChipData.Width = chipWidth;

            cardChipUptime.Location = new Point((chipWidth + gap) * 3, 328);
            cardChipUptime.Width = totalWidth - cardChipUptime.Left;
        }

        public void SetServerRunning(bool running, string statusText)
        {
            btnStartServer.Text = running ? "⏹  STOP BROADCAST" : "▶  START BROADCAST";
            lblLiveStats.Text = statusText;
        }

        public void SetAudioMute(bool isMuted)
        {
            btnQuickMute.Text = isMuted ? "🔊 Unmute Audio" : "🔇 Mute Audio";
        }

        public void SetAudioMeter(int levelPercentage, bool enabled)
        {
            pbAudioMeter.Enabled = enabled;
            pbAudioMeter.Value = Math.Clamp(levelPercentage, 0, 100);
        }

        public void SetUrl(string url, string pinText)
        {
            txtURL.Text = url;
            lblPinDisplay.Text = pinText;
        }

        public void SetTelemetry(string viewers, string fps, string mbTransferred, string uptime)
        {
            lblChipViewers.Text = viewers;
            lblChipFps.Text = fps;
            lblChipData.Text = mbTransferred;
            lblChipUptime.Text = uptime;
        }

        public void UpdateMonitorPreview(Bitmap? snapshot, string resolution, string sourceName, string fps, string audioInfo, bool isLive, bool cursorVisible)
        {
            monitorPreview.SourceTitle = sourceName;
            monitorPreview.ResolutionText = resolution;
            monitorPreview.FpsText = fps;
            monitorPreview.AudioInfoText = audioInfo;
            monitorPreview.IsLive = isLive;
            if (snapshot != null)
            {
                monitorPreview.UpdatePreview(snapshot);
            }
            lblCursorStatus.Text = cursorVisible ? "Mouse Pointer: Visible" : "Mouse Pointer: Hidden";
        }
    }
}
