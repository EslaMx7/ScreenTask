using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenTask.UI.Controls
{
    /// <summary>
    /// A standard Windows Forms UserControl confidence monitor displaying a live preview
    /// of the captured screen or application window using native controls (PictureBox and Labels).
    /// </summary>
    public partial class MonitorPreviewControl : UserControl
    {
        private string _sourceTitle = "Display 1 (Primary)";
        private string _resolutionText = "1920 × 1080";
        private bool _isLive;

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string AudioInfoText
        {
            get => lblAudioInfo.Text;
            set => lblAudioInfo.Text = value;
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string FpsText
        {
            get => lblFps.Text;
            set => lblFps.Text = value.StartsWith("⚡") ? value : $"⚡ {value}";
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLive
        {
            get => _isLive;
            set
            {
                _isLive = value;
                lblLiveBadge.Text = value ? "● LIVE" : "● IDLE";
                lblLiveBadge.ForeColor = value ? Color.Red : Color.Silver;
            }
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SourceTitle
        {
            get => _sourceTitle;
            set
            {
                _sourceTitle = value;
                UpdateTitleLabel();
            }
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ResolutionText
        {
            get => _resolutionText;
            set
            {
                _resolutionText = value;
                UpdateTitleLabel();
            }
        }

        public MonitorPreviewControl()
        {
            InitializeComponent();
            lblSourceTitle.Parent = picPreview;
            lblLiveBadge.Parent = picPreview;
            lblAudioInfo.Parent = picPreview;
            lblFps.Parent = picPreview;
            UpdateTitleLabel();
        }

        private void UpdateTitleLabel()
        {
            lblSourceTitle.Text = $"{_sourceTitle} • {_resolutionText}";
        }

        public void UpdatePreview(Bitmap bmp)
        {
            if (IsDisposed || !IsHandleCreated) return;

            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(() => UpdatePreview(bmp));
                    return;
                }

                var oldImg = picPreview.Image;
                picPreview.Image = (Bitmap)bmp.Clone();
                oldImg?.Dispose();
            }
            catch
            {
                // Ignore transient GDI thumbnail errors during teardown
            }
        }
    }
}
