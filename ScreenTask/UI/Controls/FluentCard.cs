using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenTask.UI.Controls
{
    /// <summary>
    /// Standard container panel without custom owner-drawn paint routines, rounded strokes, or borders.
    /// Preserves property compatibility with existing designer code.
    /// </summary>
    public class FluentCard : Panel
    {
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderRadius { get; set; } = 0;

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color BorderColor { get; set; } = Color.Transparent;

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color CardBackColor { get; set; } = SystemColors.Control;

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Title { get; set; } = string.Empty;

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color TitleColor { get; set; } = SystemColors.ControlText;

        public FluentCard()
        {
            BackColor = SystemColors.Control;
            BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
