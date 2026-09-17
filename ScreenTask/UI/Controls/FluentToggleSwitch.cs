using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenTask.UI.Controls
{
    /// <summary>
    /// Standard Windows native CheckBox with system visual styles enabled.
    /// Preserves property compatibility without custom owner-drawn switch painting.
    /// </summary>
    public class FluentToggleSwitch : CheckBox
    {
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color CheckedColor { get; set; } = SystemColors.Highlight;

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color UncheckedColor { get; set; } = SystemColors.ControlDark;

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ThumbColor { get; set; } = SystemColors.Window;

        public FluentToggleSwitch()
        {
            FlatStyle = FlatStyle.Standard;
            UseVisualStyleBackColor = true;
            Cursor = Cursors.Default;
        }
    }
}
