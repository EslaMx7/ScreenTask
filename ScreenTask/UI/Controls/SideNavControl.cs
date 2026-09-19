using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ScreenTask.UI.Controls
{
    public class SideNavItem
    {
        public string Text { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public object? Tag { get; set; }

        public SideNavItem() { }

        public SideNavItem(string text, string icon, object? tag = null)
        {
            Text = text;
            Icon = icon;
            Tag = tag;
        }

        public override string ToString() => $"{Icon} {Text}";
    }

    /// <summary>
    /// A native Windows Forms vertical side navigation control adhering to Windows 11 NavigationView design specifications.
    /// Features smooth rounded item plates, signature accent pill indicators, and keyboard navigation.
    /// </summary>
    public class SideNavControl : Control
    {
        private readonly List<SideNavItem> _items = new();
        private int _selectedIndex = 0;
        private int _hoveredIndex = -1;
        private int _itemHeight = 38;
        private int _itemSpacing = 4;

        public event EventHandler? SelectedIndexChanged;

        [Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<SideNavItem> Items => _items;

        [Category("Behavior")]
        [DefaultValue(0)]
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

        [Category("Appearance")]
        [DefaultValue(38)]
        public int ItemHeight
        {
            get => _itemHeight;
            set
            {
                if (value > 10 && _itemHeight != value)
                {
                    _itemHeight = value;
                    Invalidate();
                }
            }
        }

        [Category("Appearance")]
        [DefaultValue(4)]
        public int ItemSpacing
        {
            get => _itemSpacing;
            set
            {
                if (value >= 0 && _itemSpacing != value)
                {
                    _itemSpacing = value;
                    Invalidate();
                }
            }
        }

        public SideNavControl()
        {
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.Selectable, true);

            Cursor = Cursors.Hand;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            BackColor = SystemColors.Control;
        }

        public void AddItem(string text, string icon, object? tag = null)
        {
            _items.Add(new SideNavItem(text, icon, tag));
            Invalidate();
        }

        private int GetIndexAt(int y)
        {
            if (_items.Count == 0 || Height <= 0) return -1;
            int totalSlot = _itemHeight + _itemSpacing;
            int idx = y / totalSlot;
            int remainder = y % totalSlot;
            if (idx >= 0 && idx < _items.Count && remainder <= _itemHeight)
            {
                return idx;
            }
            return -1;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int idx = GetIndexAt(e.Y);
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
            Focus();
            int idx = GetIndexAt(e.Y);
            if (idx >= 0 && idx < _items.Count)
            {
                SelectedIndex = idx;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Down)
            {
                if (_selectedIndex < _items.Count - 1)
                {
                    SelectedIndex++;
                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (_selectedIndex > 0)
                {
                    SelectedIndex--;
                    e.Handled = true;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            for (int i = 0; i < _items.Count; i++)
            {
                var item = _items[i];
                int itemY = i * (_itemHeight + _itemSpacing);
                var itemRect = new Rectangle(0, itemY, Width - 1, _itemHeight);

                bool isSelected = i == _selectedIndex;
                bool isHovered = i == _hoveredIndex && !isSelected;

                // Item plate background
                if (isSelected)
                {
                    // Active plate: subtle tinted highlight
                    using var plateBrush = new SolidBrush(Color.FromArgb(28, SystemColors.Highlight));
                    using var plateBorderPen = new Pen(Color.FromArgb(50, SystemColors.Highlight), 1f);
                    g.FillRoundedRectangle(plateBrush, itemRect, 5);
                    g.DrawRoundedRectangle(plateBorderPen, itemRect, 5);

                    // Windows 11 Signature Left Accent Pill
                    int pillHeight = Math.Max(14, _itemHeight - 18);
                    int pillY = itemRect.Y + (_itemHeight - pillHeight) / 2;
                    var pillRect = new Rectangle(itemRect.X + 3, pillY, 3, pillHeight);
                    using var pillBrush = new SolidBrush(SystemColors.Highlight);
                    g.FillRoundedRectangle(pillBrush, pillRect, 1);
                }
                else if (isHovered)
                {
                    // Hover plate: gentle neutral highlight
                    using var hoverBrush = new SolidBrush(Color.FromArgb(16, SystemColors.ControlText));
                    g.FillRoundedRectangle(hoverBrush, itemRect, 5);
                }

                // Draw Icon
                if (!string.IsNullOrEmpty(item.Icon))
                {
                    var iconRect = new Rectangle(itemRect.X + 6, itemRect.Y, 22, _itemHeight);
                    using var iconFont = new Font("Segoe UI", 9.5F, FontStyle.Regular);
                    TextRenderer.DrawText(g, item.Icon, iconFont, iconRect, SystemColors.ControlText,
                        TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                }

                // Draw Text Label
                var textRect = new Rectangle(itemRect.X + 32, itemRect.Y, itemRect.Width - 34, _itemHeight);
                using var textFont = new Font(Font.FontFamily, 8.5F, isSelected ? FontStyle.Bold : FontStyle.Regular);
                TextRenderer.DrawText(g, item.Text, textFont, textRect, SystemColors.ControlText,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis);
            }
        }
    }
}
