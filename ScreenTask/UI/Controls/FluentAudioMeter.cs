using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenTask.UI.Controls
{
    /// <summary>
    /// Standard Windows native ProgressBar for audio VU metering.
    /// Uses native OS rendering without custom owner-drawn gradients or borders.
    /// </summary>
    public class FluentAudioMeter : ProgressBar
    {
        public FluentAudioMeter()
        {
            Minimum = 0;
            Maximum = 100;
            Value = 0;
            Style = ProgressBarStyle.Continuous;
        }
    }
}
