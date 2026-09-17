using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ScreenTask.UI.Controls
{
    public static class GraphicsExtensions
    {
        public static GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            if (radius <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            int diameter = radius * 2;
            var size = new Size(diameter, diameter);
            var arc = new Rectangle(rect.Location, size);

            // Top Left
            path.AddArc(arc, 180, 90);

            // Top Right
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Bottom Right
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Bottom Left
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        public static void FillRoundedRectangle(this Graphics g, Brush brush, Rectangle rect, int radius)
        {
            using var path = GetRoundedRectanglePath(rect, radius);
            g.FillPath(brush, path);
        }

        public static void DrawRoundedRectangle(this Graphics g, Pen pen, Rectangle rect, int radius)
        {
            using var path = GetRoundedRectanglePath(rect, radius);
            g.DrawPath(pen, path);
        }
    }
}
