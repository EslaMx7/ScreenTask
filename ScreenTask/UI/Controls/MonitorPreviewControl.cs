using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ScreenTask.UI.Controls
{
    /// <summary>
    /// A confidence monitor control that displays a live, scaled-down preview of the captured screen
    /// or application window, complete with resolution pill and live broadcasting indicators.
    /// </summary>
    public class MonitorPreviewControl : Control
    {
        private Bitmap? _previewImage;
        private readonly object _imageLock = new();
        private bool _isLive;
        private string _sourceTitle = "Display 1 (Primary)";
        private string _resolutionText = "1920 × 1080";

        private string _audioInfoText = "Audio: Active";
        private string _fpsText = "30.0 FPS";

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string AudioInfoText
        {
            get => _audioInfoText;
            set { _audioInfoText = value; Invalidate(); }
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string FpsText
        {
            get => _fpsText;
            set { _fpsText = value; Invalidate(); }
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLive
        {
            get => _isLive;
            set { _isLive = value; Invalidate(); }
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SourceTitle
        {
            get => _sourceTitle;
            set { _sourceTitle = value; Invalidate(); }
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ResolutionText
        {
            get => _resolutionText;
            set { _resolutionText = value; Invalidate(); }
        }

        public MonitorPreviewControl()
        {
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            BackColor = Color.Black;
            Font = new Font("Segoe UI", 8.5f, FontStyle.Regular);
        }

        public void UpdatePreview(Bitmap? frame)
        {
            if (frame == null) return;

            try
            {
                // Create a low-overhead thumbnail to store for preview rendering
                int targetW = Width > 0 ? Width : 320;
                int targetH = Height > 0 ? Height : 180;
                var thumb = new Bitmap(targetW, targetH);
                using (var g = Graphics.FromImage(thumb))
                {
                    g.InterpolationMode = InterpolationMode.Bilinear;
                    g.DrawImage(frame, new Rectangle(0, 0, targetW, targetH));
                }

                lock (_imageLock)
                {
                    var old = _previewImage;
                    _previewImage = thumb;
                    old?.Dispose();
                }

                if (IsHandleCreated && !IsDisposed)
                {
                    BeginInvoke((Action)Invalidate);
                }
            }
            catch
            {
                // Ignore transient GDI thumbnail errors
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            var bounds = new Rectangle(0, 0, Width - 1, Height - 1);

            // 1. Draw Monitor Frame
            using (var bgBrush = new SolidBrush(BackColor))
            {
                g.FillRectangle(bgBrush, bounds);
            }

            // 2. Draw Preview Image (if available)
            lock (_imageLock)
            {
                if (_previewImage != null)
                {
                    g.DrawImage(_previewImage, bounds);
                }
                else
                {
                    // Fallback placeholder
                    string placeholder = _isLive ? "Sharing Screen..." : "Ready to Share";
                    TextRenderer.DrawText(g, $"🖥️ {placeholder}", Font, bounds, SystemColors.GrayText,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
            }

            // 3. Top Badges Overlay
            // Left: Source Title
            var titleRect = new Rectangle(8, 8, Math.Min(Width - 100, 240), 22);
            using (var badgeBg = new SolidBrush(Color.FromArgb(200, 20, 20, 20)))
            {
                g.FillRectangle(badgeBg, titleRect);
            }
            TextRenderer.DrawText(g, $"{_sourceTitle} • {_resolutionText}", new Font(Font, FontStyle.Bold),
                titleRect, Color.White, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            // Right: LIVE / STANDBY badge
            string statusStr = _isLive ? "● LIVE" : "● IDLE";
            Color statusColor = _isLive ? Color.Red : Color.Silver;
            var statusRect = new Rectangle(Width - 70, 8, 62, 22);
            using (var statusBg = new SolidBrush(Color.FromArgb(200, 20, 20, 20)))
            {
                g.FillRectangle(statusBg, statusRect);
            }
            TextRenderer.DrawText(g, statusStr, new Font(Font, FontStyle.Bold),
                statusRect, statusColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            // 4. Bottom Badges Overlay
            // Bottom-Left: Audio Info
            var audioRect = new Rectangle(8, Height - 28, Math.Min(Width - 120, 200), 20);
            using (var audioBg = new SolidBrush(Color.FromArgb(200, 20, 20, 20)))
            {
                g.FillRectangle(audioBg, audioRect);
            }
            TextRenderer.DrawText(g, _audioInfoText, new Font(Font.FontFamily, 7.5f),
                audioRect, Color.Silver, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            // Bottom-Right: FPS Counter
            var fpsRect = new Rectangle(Width - 85, Height - 28, 77, 20);
            using (var fpsBg = new SolidBrush(Color.FromArgb(200, 20, 20, 20)))
            {
                g.FillRectangle(fpsBg, fpsRect);
            }
            TextRenderer.DrawText(g, $"⚡ {_fpsText}", new Font(Font.FontFamily, 8f, FontStyle.Bold),
                fpsRect, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            // 5. Outer Border
            using (var borderPen = new Pen(SystemColors.ControlDark, 1f))
            {
                g.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (_imageLock)
                {
                    _previewImage?.Dispose();
                    _previewImage = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}
