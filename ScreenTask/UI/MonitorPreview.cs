using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenTask.UI
{
    /// <summary>
    /// A native WinForms Panel confidence monitor displaying a live preview
    /// of the captured screen or application window with overlay status labels.
    /// </summary>
    public class MonitorPreview : Panel
    {
        private PictureBox picPreview = null!;
        private Label lblSourceTitle = null!;
        private Label lblLiveBadge = null!;
        private Label lblAudioInfo = null!;
        private Label lblFps = null!;
        private string _sourceTitle = "Display 1 (Primary)";
        private string _resolutionText = "1920 × 1080";
        private bool _isLive;

        public PictureBox PreviewBox => picPreview;
        public Label SourceTitleLabel => lblSourceTitle;
        public Label LiveBadgeLabel => lblLiveBadge;
        public Label AudioInfoLabel => lblAudioInfo;
        public Label FpsLabel => lblFps;

        public string AudioInfoText
        {
            get => lblAudioInfo?.Text ?? "";
            set { if (lblAudioInfo != null) lblAudioInfo.Text = value; }
        }

        public string FpsText
        {
            get => lblFps?.Text ?? "";
            set
            {
                if (lblFps != null)
                {
                    lblFps.Text = value.StartsWith("⚡") ? value : $"⚡ {value}";
                }
            }
        }

        public bool IsLive
        {
            get => _isLive;
            set
            {
                _isLive = value;
                if (lblLiveBadge != null)
                {
                    lblLiveBadge.Text = value ? "● LIVE" : "● IDLE";
                    lblLiveBadge.ForeColor = value ? Color.Red : Color.Silver;
                }
            }
        }

        public string SourceTitle
        {
            get => _sourceTitle;
            set
            {
                _sourceTitle = value;
                UpdateTitleLabel();
            }
        }

        public string ResolutionText
        {
            get => _resolutionText;
            set
            {
                _resolutionText = value;
                UpdateTitleLabel();
            }
        }

        public MonitorPreview()
        {
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.BackColor = Color.Black;
            this.Font = new Font("Segoe UI", 8.5F);
            this.Name = "monitorPreview";

            picPreview = new PictureBox
            {
                Name = "picPreview",
                BackColor = Color.Black,
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            this.Controls.Add(picPreview);

            lblSourceTitle = new Label
            {
                Name = "lblSourceTitle",
                AutoSize = true,
                BackColor = Color.FromArgb(180, 0, 0, 0),
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(8, 8),
                Padding = new Padding(4, 2, 4, 2),
                Parent = picPreview
            };

            lblLiveBadge = new Label
            {
                Name = "lblLiveBadge",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                AutoSize = true,
                BackColor = Color.FromArgb(180, 0, 0, 0),
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                ForeColor = Color.Silver,
                Location = new Point(Math.Max(10, Width - 60), 8),
                Padding = new Padding(4, 2, 4, 2),
                Parent = picPreview,
                Text = "● IDLE"
            };

            lblAudioInfo = new Label
            {
                Name = "lblAudioInfo",
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
                AutoSize = true,
                BackColor = Color.FromArgb(180, 0, 0, 0),
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = Color.WhiteSmoke,
                Location = new Point(8, Math.Max(10, Height - 24)),
                Padding = new Padding(4, 2, 4, 2),
                Parent = picPreview,
                Text = "Audio: Muted"
            };

            lblFps = new Label
            {
                Name = "lblFps",
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                AutoSize = true,
                BackColor = Color.FromArgb(180, 0, 0, 0),
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                ForeColor = Color.Gold,
                Location = new Point(Math.Max(10, Width - 70), Math.Max(10, Height - 24)),
                Padding = new Padding(4, 2, 4, 2),
                Parent = picPreview,
                Text = "⚡ 30.0 FPS"
            };

            this.Resize += (s, e) =>
            {
                if (lblLiveBadge != null) lblLiveBadge.Location = new Point(Math.Max(10, Width - lblLiveBadge.Width - 8), 8);
                if (lblAudioInfo != null) lblAudioInfo.Location = new Point(8, Math.Max(10, Height - lblAudioInfo.Height - 8));
                if (lblFps != null) lblFps.Location = new Point(Math.Max(10, Width - lblFps.Width - 8), Math.Max(10, Height - lblFps.Height - 8));
            };

            UpdateTitleLabel();
        }

        private void UpdateTitleLabel()
        {
            if (lblSourceTitle != null)
            {
                lblSourceTitle.Text = $"{_sourceTitle} • {_resolutionText}";
            }
        }

        public void UpdatePreview(Bitmap bmp)
        {
            if (IsDisposed || !IsHandleCreated) return;
            try
            {
                var cloned = (Bitmap)bmp.Clone();
                if (InvokeRequired)
                {
                    try
                    {
                        BeginInvoke(() =>
                        {
                            if (IsDisposed || picPreview.IsDisposed)
                            {
                                cloned.Dispose();
                                return;
                            }
                            var oldImg = picPreview.Image;
                            picPreview.Image = cloned;
                            oldImg?.Dispose();
                        });
                    }
                    catch
                    {
                        cloned.Dispose();
                    }
                    return;
                }
                var prev = picPreview.Image;
                picPreview.Image = cloned;
                prev?.Dispose();
            }
            catch { }
        }
    }
}
