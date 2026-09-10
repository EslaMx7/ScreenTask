using System;
using System.Drawing;
using ScreenTask.Core.Network;
using Xunit;

namespace ScreenTask.Tests.Core
{
    public class QrCodeHelperTests
    {
        [Theory]
        [InlineData("http://127.0.0.1:7070")]
        [InlineData("http://192.168.1.100:9000")]
        [InlineData("https://screentask.eslamx.com/")]
        public void GenerateQrCodeBitmap_ProducesValidBitmap(string url)
        {
            using var bmp = QrCodeHelper.GenerateQrCodeBitmap(url, 4);

            Assert.NotNull(bmp);
            Assert.True(bmp.Width > 0);
            Assert.True(bmp.Height > 0);
            Assert.Equal(bmp.Width, bmp.Height); // QR is square
        }

        [Fact]
        public void GenerateQrCodePngBytes_ProducesValidPngHeader()
        {
            var bytes = QrCodeHelper.GenerateQrCodePngBytes("http://192.168.1.100:7070", 6);

            Assert.NotNull(bytes);
            Assert.True(bytes.Length > 8);

            // Valid PNG magic bytes: 0x89, 'P', 'N', 'G', 0x0D, 0x0A, 0x1A, 0x0A
            Assert.Equal(0x89, bytes[0]);
            Assert.Equal((byte)'P', bytes[1]);
            Assert.Equal((byte)'N', bytes[2]);
            Assert.Equal((byte)'G', bytes[3]);
        }
    }
}
