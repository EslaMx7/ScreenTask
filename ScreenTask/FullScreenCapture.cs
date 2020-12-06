using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenTask
{
    public class ScreenCapturePInvoke
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct CURSORINFO
        {
            public Int32 cbSize;
            public Int32 flags;
            public IntPtr hCursor;
            public POINTAPI ptScreenPos;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINTAPI
        {
            public int x;
            public int y;
        }

        [DllImport("user32.dll")]
        private static extern bool GetCursorInfo(out CURSORINFO pci);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw, int diFlags);

        private const Int32 CURSOR_SHOWING = 0x0001;
        private const Int32 DI_NORMAL = 0x0003;

        public static Bitmap CaptureFullScreen(bool captureMouse)
        {
            var allBounds = Screen.AllScreens.Select(s => s.Bounds).ToArray();
            Rectangle bounds = Rectangle.FromLTRB(allBounds.Min(b => b.Left), allBounds.Min(b => b.Top), allBounds.Max(b => b.Right), allBounds.Max(b => b.Bottom));

            var bitmap = CaptureScreen(bounds, captureMouse);
            return bitmap;
        }

        public static Bitmap CapturePrimaryScreen(bool captureMouse)
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            
            var bitmap = CaptureScreen(bounds, captureMouse);
            return bitmap;
        }

        public static Bitmap CaptureSelectedScreen(bool captureMouse,int screenIndex=0)
        {
            Rectangle bounds = Screen.AllScreens[screenIndex].Bounds;

            var bitmap = CaptureScreen(bounds, captureMouse);
            return bitmap;
        }

        public static Bitmap CaptureScreen(Rectangle bounds, bool captureMouse)
        {
            Bitmap result = new Bitmap(bounds.Width, bounds.Height);

            try
            {
                using (Graphics g = Graphics.FromImage(result))
                {
                    g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);

                    if (captureMouse)
                    {
                        CURSORINFO pci;
                        pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));

                        if (GetCursorInfo(out pci))
                        {
                            if (pci.flags == CURSOR_SHOWING)
                            {
                                var hdc = g.GetHdc();
                                DrawIconEx(hdc, pci.ptScreenPos.x - bounds.X, pci.ptScreenPos.y - bounds.Y, pci.hCursor, 0, 0, 0, IntPtr.Zero, DI_NORMAL);
                                g.ReleaseHdc();
                            }
                        }
                    }
                }
            }
            catch
            {
                result = null;
            }

            return result;
        }
    }
}
