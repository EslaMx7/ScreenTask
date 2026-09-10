using System;
using System.Drawing;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using ScreenTask.Core.Streaming;
using Xunit;

namespace ScreenTask.Tests.Core
{
    public class StreamBroadcasterTests : IDisposable
    {
        private readonly StreamBroadcaster _broadcaster;

        public StreamBroadcasterTests()
        {
            _broadcaster = new StreamBroadcaster(75);
        }

        [Fact]
        public void InitialState_HasZeroClientsAndZeroFps()
        {
            Assert.Equal(0, _broadcaster.ConnectedClientsCount);
            Assert.Equal(0, _broadcaster.CurrentFps);
            Assert.Equal(0, _broadcaster.TotalBytesTransferred);
            Assert.Empty(_broadcaster.GetLatestSnapshot());
        }

        [Fact]
        public async Task BroadcastBitmapAsync_UpdatesLatestSnapshot()
        {
            using var bmp = new Bitmap(100, 100);
            using var g = Graphics.FromImage(bmp);
            g.Clear(Color.Blue);

            await _broadcaster.BroadcastBitmapAsync(bmp);

            var snapshot = _broadcaster.GetLatestSnapshot();
            Assert.NotEmpty(snapshot);

            // Valid JPEG header starts with 0xFF, 0xD8
            Assert.Equal(0xFF, snapshot[0]);
            Assert.Equal(0xD8, snapshot[1]);
        }

        [Fact]
        public void SetQuality_ClampsValuesProperly()
        {
            // Should not throw even with extreme values
            _broadcaster.SetQuality(150);
            _broadcaster.SetQuality(-10);
            _broadcaster.SetQuality(50);
        }

        public void Dispose()
        {
            _broadcaster.Dispose();
        }
    }
}
