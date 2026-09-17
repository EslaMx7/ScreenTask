using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ScreenTask.UI.Controls
{
    /// <summary>
    /// A modern Windows 11 / Fluent 2 segmented pill selector control.
    /// </summary>
    public class FluentSegmentedControl : Control
    {
        private readonly List<string> _items = new();
        private int _selectedIndex = 0;
        private int _hoveredIndex = -1;

        private Color _selectedBackColor = SystemColors.Highlight;
        private Color _selectedForeColor = SystemColors.HighlightText;
        private Color _unselectedForeColor = SystemColors.ControlText;
        private Color _containerBackColor = SystemColors.Control;
        private Color _borderColor = SystemColors.ControlDark;

        public event EventHandler? SelectedIndexChanged;

        [Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<string> Items => _items;

        [Category("Behavior")]
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (value != _selectedIndex && value >= 0 && value < _items.Count)
                {
                    _selectedIndex = value;
                    Invalidate();
                    SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public FluentSegmentedControl()
        {
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            Cursor = Cursors.Hand;
            Font = new Font("Segoe UI", 8.5f, FontStyle.Regular);
            Height = 32;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int idx = GetIndexAt(e.X);
            if (idx != _hoveredIndex)
            {
                _hoveredIndex = idx;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _hoveredIndex = -1;
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            int idx = GetIndexAt(e.X);
            if (idx >= 0 && idx < _items.Count)
            {
                SelectedIndex = idx;
            }
        }

        private int GetIndexAt(int x)
        {
            if (_items.Count == 0 || Width <= 0) return -1;
            int segmentWidth = Width / _items.Count;
            return Math.Clamp(x / segmentWidth, 0, _items.Count - 1);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            var bounds = new Rectangle(0, 0, Width - 1, Height - 1);
            int outerRadius = 8;

            // Background Container
            using (var containerBrush = new SolidBrush(_containerBackColor))
            {
                g.FillRoundedRectangle(containerBrush, bounds, outerRadius);
            }

            using (var borderPen = new Pen(_borderColor, 1f))
            {
                g.DrawRoundedRectangle(borderPen, bounds, outerRadius);
            }

            if (_items.Count == 0) return;

            float segmentWidth = (float)Width / _items.Count;
            int padding = 3;

            for (int i = 0; i < _items.Count; i++)
            {
                var segRect = new RectangleF(i * segmentWidth + padding, padding, segmentWidth - (padding * 2), Height - (padding * 2));
                var roundSegRect = Rectangle.Round(segRect);

                if (i == _selectedIndex)
                {
                    // Draw active pill
                    using var activeBrush = new SolidBrush(_selectedBackColor);
                    g.FillRoundedRectangle(activeBrush, roundSegRect, 6);

                    using var textBrush = new SolidBrush(_selectedForeColor);
                    using var boldFont = new Font(Font, FontStyle.Bold);
                    TextRenderer.DrawText(g, _items[i], boldFont, roundSegRect, _selectedForeColor,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
                else
                {
                    if (i == _hoveredIndex)
                    {
                        using var hoverBrush = new SolidBrush(SystemColors.ControlLight);
                        g.FillRoundedRectangle(hoverBrush, roundSegRect, 6);
                    }

                    TextRenderer.DrawText(g, _items[i], Font, roundSegRect, _unselectedForeColor,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
            }
        }
    }
}
