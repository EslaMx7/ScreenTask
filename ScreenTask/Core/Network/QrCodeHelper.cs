using System;
using System.Drawing;
using System.IO;
using QRCoder;

namespace ScreenTask.Core.Network
{
    public static class QrCodeHelper
    {
        public static Bitmap GenerateQrCodeBitmap(string url, int pixelsPerModule = 6)
        {
            using var generator = new QRCodeGenerator();
            using var data = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.M);
            using var qrCode = new PngByteQRCode(data);
            var bytes = qrCode.GetGraphic(pixelsPerModule);

            using var ms = new MemoryStream(bytes);
            return new Bitmap(ms);
        }

        public static byte[] GenerateQrCodePngBytes(string url, int pixelsPerModule = 8)
        {
            using var generator = new QRCodeGenerator();
            using var data = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.M);
            using var qrCode = new PngByteQRCode(data);
            return qrCode.GetGraphic(pixelsPerModule);
        }
    }
}
