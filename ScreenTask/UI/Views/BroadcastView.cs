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
