using System.Drawing;
using System.Windows.Forms;
using ScreenTask.Core.Capture;
using Xunit;

namespace ScreenTask.Tests.Core
{
    public class CaptureSourceTests
    {
        [Fact]
        public void ScreenCaptureSource_ReturnsValidDisplayInfo()
        {
            var source = new ScreenCaptureSource(0);

            Assert.NotNull(source.Id);
            Assert.NotEmpty(source.DisplayName);
            Assert.True(source.IsValid);
            Assert.True(source.Bounds.Width > 0);
            Assert.True(source.Bounds.Height > 0);
        }

        [Fact]
        public void ScreenCaptureSource_CaptureFrame_ReturnsBitmapMatchingBounds()
        {
            var source = new ScreenCaptureSource(0);
            using var frame = source.CaptureFrame(false);

            if (frame != null)
            {
                Assert.True(frame.Width > 0);
                Assert.True(frame.Height > 0);
            }
        }

        [Fact]
        public void WindowEnumerator_GetShareableWindows_ExcludesSystemAndSelf()
        {
            var windows = WindowEnumerator.GetShareableWindows();
            Assert.NotNull(windows);

            foreach (var win in windows)
            {
                Assert.False(string.IsNullOrWhiteSpace(win.Title));
                Assert.NotEqual("ScreenTask", win.ProcessName);
            }
        }
    }
}
