using System;
using System.Drawing;

namespace ScreenTask.Core.Capture
{
    public interface ICaptureSource : IDisposable
    {
        string Id { get; }
        string DisplayName { get; }
        Rectangle Bounds { get; }
        bool IsValid { get; }

        Bitmap? CaptureFrame(bool captureCursor);
    }
}
