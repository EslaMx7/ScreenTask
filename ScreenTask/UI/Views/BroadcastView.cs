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
            if (this.ClientSize.Width <= 0 || this.ClientSize.Height <= 0) return;
            int totalWidth = this.ClientSize.Width;
            int totalHeight = this.ClientSize.Height;

            // 1. Bottom Section (4 Stat Chips) - Pinned to bottom
            int gap = 8;
            int chipHeight = 72;
            int chipWidth = Math.Max(130, (totalWidth - (3 * gap)) / 4);
            int chipsTop = Math.Max(260, totalHeight - chipHeight - 4);

            cardChipViewers.Location = new Point(0, chipsTop);
            cardChipViewers.Size = new Size(chipWidth, chipHeight);
            lblTitleViewers.Width = Math.Max(60, chipWidth - 44);
            lblChipViewers.Width = Math.Max(60, chipWidth - 16);

            cardChipFps.Location = new Point(chipWidth + gap, chipsTop);
            cardChipFps.Size = new Size(chipWidth, chipHeight);
            lblTitleFps.Width = Math.Max(60, chipWidth - 44);
            lblChipFps.Width = Math.Max(60, chipWidth - 16);

            cardChipData.Location = new Point((chipWidth + gap) * 2, chipsTop);
            cardChipData.Size = new Size(chipWidth, chipHeight);
            lblTitleData.Width = Math.Max(60, chipWidth - 44);
            lblChipData.Width = Math.Max(60, chipWidth - 16);

            int lastChipLeft = (chipWidth + gap) * 3;
            cardChipUptime.Location = new Point(lastChipLeft, chipsTop);
            cardChipUptime.Size = new Size(totalWidth - lastChipLeft, chipHeight);
            lblTitleUptime.Width = Math.Max(60, cardChipUptime.Width - 44);
            lblChipUptime.Width = Math.Max(60, cardChipUptime.Width - 16);

            // 2. Middle Section (Stream URL Card)
            int urlCardHeight = 86;
            int urlCardTop = Math.Max(180, chipsTop - urlCardHeight - 8);
            cardUrl.Location = new Point(0, urlCardTop);
            cardUrl.Size = new Size(totalWidth, urlCardHeight);
            lblPinDisplay.Left = cardUrl.Width - lblPinDisplay.Width - 12;
            btnLaunch.Left = cardUrl.Width - btnLaunch.Width - 10;
            btnQr.Left = btnLaunch.Left - btnQr.Width - 6;
            btnCopyUrl.Left = btnQr.Left - btnCopyUrl.Width - 6;
            txtURL.Width = Math.Max(150, btnCopyUrl.Left - txtURL.Left - 8);

            // 3. Top Section (Preview & Control Card)
            int controlCardWidth = 260;
            int topHeight = Math.Max(200, urlCardTop - 10);

            groupBox3.Location = new Point(Math.Max(384, totalWidth - controlCardWidth), 0);
            groupBox3.Size = new Size(controlCardWidth, topHeight);
            lblBroadcastDesc.Width = groupBox3.ClientSize.Width - 24;
            btnStartServer.Width = groupBox3.ClientSize.Width - 24;
            btnQuickMute.Width = groupBox3.ClientSize.Width - 24;
            pbAudioMeter.Width = groupBox3.ClientSize.Width - 24;
            lblLiveStats.Width = groupBox3.ClientSize.Width - 24;
            if (groupBox3.ClientSize.Height - lblLiveStats.Top - 10 > 40)
            {
                lblLiveStats.Height = groupBox3.ClientSize.Height - lblLiveStats.Top - 10;
            }

            cardPreview.Location = new Point(0, 0);
            cardPreview.Size = new Size(Math.Max(380, groupBox3.Left - 10), topHeight);

            // Respect 16:9 Display Aspect Ratio for monitorPreview
            int availW = Math.Max(100, cardPreview.ClientSize.Width - 24);
            int availH = Math.Max(80, cardPreview.ClientSize.Height - 48);
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
            int previewY = Math.Max(8, (cardPreview.ClientSize.Height - 40 - previewH) / 2 + 4);
            monitorPreview.Location = new Point(previewX, previewY);
            monitorPreview.Size = new Size(previewW, previewH);

            lblCursorStatus.Location = new Point(14, cardPreview.ClientSize.Height - 26);
            btnSwitchSource.Location = new Point(cardPreview.ClientSize.Width - btnSwitchSource.Width - 14, cardPreview.ClientSize.Height - 32);
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
