using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenTask.UI.Controls
{
    /// <summary>
    /// Standard Windows native button with system visual styles enabled.
    /// Preserves property compatibility without custom owner-drawn paint routines.
    /// </summary>
    public class FluentButton : Button
    {
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color GradientEndColor { get; set; } = Color.Empty;

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderRadius { get; set; } = 0;

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color NormalBackColor { get; set; } = SystemColors.Control;

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color HoverBackColor { get; set; } = SystemColors.Control;

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PressedBackColor { get; set; } = SystemColors.Control;

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color CustomBorderColor { get; set; } = Color.Transparent;

        public FluentButton()
        {
            FlatStyle = FlatStyle.Standard;
            UseVisualStyleBackColor = true;
            Cursor = Cursors.Default;
        }
    }
}
