using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenTask.UI.Controls
{
    /// <summary>
    /// Standard Windows native ComboBox with system visual styles enabled.
    /// Preserves property compatibility without custom owner-drawn list painting.
    /// </summary>
    public class FluentComboBox : ComboBox
    {
        public FluentComboBox()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;
            DrawMode = DrawMode.Normal;
            FlatStyle = FlatStyle.Standard;
        }
    }
}
