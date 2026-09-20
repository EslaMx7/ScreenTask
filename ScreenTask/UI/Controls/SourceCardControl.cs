using System;
using System.Drawing;
using System.Windows.Forms;
using ScreenTask.Core.Capture;

namespace ScreenTask.UI.Controls
{
    public partial class SourceCardControl : UserControl
    {
        private ICaptureSource? _source;
        private bool _isSelected;

        public event EventHandler? CardClicked;

        public ICaptureSource? Source => _source;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                UpdateSelectionState();
            }
        }

        public string Title
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value;
        }

        public string Subtitle
        {
            get => lblSub.Text;
            set => lblSub.Text = value;
        }

        public Image? IconImage
        {
            get => picIcon.Image;
            set => picIcon.Image = value;
        }

        public void PerformClick() => CardClicked?.Invoke(this, EventArgs.Empty);

        public SourceCardControl()
        {
            InitializeComponent();
            WireClickEvents(this);

            lblSelectedBadge.Parent = picThumbnail;
            lblSelectedBadge.Location = new Point(4, 4);

            UpdateSelectionState();
        }

        public SourceCardControl(ICaptureSource source) : this()
        {
            SetSource(source);
        }

        public void SetSource(ICaptureSource source)
        {
            _source = source;

            if (source is WindowCaptureSource winSrc)
            {
                lblTitle.Text = !string.IsNullOrWhiteSpace(winSrc.Title) ? winSrc.Title : winSrc.ProcessName;
                var bounds = winSrc.Bounds;
                string sizeStr = bounds.Width > 0 && bounds.Height > 0 ? $"{bounds.Width}x{bounds.Height}" : "";
                lblSub.Text = !string.IsNullOrWhiteSpace(sizeStr)
                    ? $"{winSrc.ProcessName} • {sizeStr}"
                    : winSrc.ProcessName;

                if (winSrc.Icon != null)
                {
                    picIcon.Image = winSrc.Icon.ToBitmap();
                }
                else
                {
                    picIcon.Image = CreateAppIconBitmap();
                }
            }
            else if (source is ScreenCaptureSource scrSrc)
            {
                var screens = Screen.AllScreens;
                int idx = scrSrc.ScreenIndex;
                string primaryStr = scrSrc.IsPrimary ? "(Primary)" : "";
                lblTitle.Text = $"Display {idx + 1} {primaryStr}".Trim();

                var bounds = scrSrc.Bounds;
                lblSub.Text = $"{bounds.Width}x{bounds.Height} Physical Display";
                picIcon.Image = CreateDisplayIconBitmap();
            }
            else
            {
                lblTitle.Text = source.DisplayName;
                lblSub.Text = "Video Capture Source";
                picIcon.Image = CreateDisplayIconBitmap();
            }
        }

        public void UpdateThumbnail(Bitmap? thumbnail)
        {
            var oldImage = picThumbnail.Image;
            picThumbnail.Image = thumbnail;
            oldImage?.Dispose();
        }

        private void UpdateSelectionState()
        {
            if (_isSelected)
            {
                pnlBorder.BackColor = SystemColors.Highlight;
                pnlBorder.Padding = new Padding(2);
                lblSelectedBadge.Visible = true;
            }
            else
            {
                pnlBorder.BackColor = SystemColors.ControlLight;
                pnlBorder.Padding = new Padding(1);
                lblSelectedBadge.Visible = false;
            }
        }

        private void WireClickEvents(Control root)
        {
            root.Click += (s, e) => CardClicked?.Invoke(this, EventArgs.Empty);

            root.MouseEnter += (s, e) =>
            {
                if (!_isSelected)
                {
                    pnlBorder.BackColor = SystemColors.HotTrack;
                }
            };

            root.MouseLeave += (s, e) =>
            {
                if (!_isSelected)
                {
                    pnlBorder.BackColor = SystemColors.ControlLight;
                }
            };

            foreach (Control child in root.Controls)
            {
                WireClickEvents(child);
            }
        }

        private static Bitmap CreateDisplayIconBitmap()
        {
            var bmp = new Bitmap(20, 20);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                using var pen = new Pen(SystemColors.ControlDarkDark, 1.5f);
                using var brush = new SolidBrush(SystemColors.ControlDark);
                // Monitor outline
                g.DrawRectangle(pen, 2, 2, 15, 11);
                // Stand
                g.DrawLine(pen, 10, 13, 10, 16);
                g.DrawLine(pen, 6, 16, 14, 16);
            }
            return bmp;
        }

        private static Bitmap CreateAppIconBitmap()
        {
            var bmp = new Bitmap(20, 20);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                using var pen = new Pen(SystemColors.ControlDarkDark, 1.5f);
                // Window outline
                g.DrawRectangle(pen, 2, 2, 15, 14);
                // Titlebar
                g.DrawLine(pen, 2, 6, 17, 6);
            }
            return bmp;
        }
    }
}
