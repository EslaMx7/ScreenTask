using System;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ScreenTask.Core.Capture;
using ScreenTask.Core.Streaming;
using ScreenTask.WebServer;
using Xunit;

namespace ScreenTask.Tests.Integration
{
    public class EmbeddedWebServerIntegrationTests : IAsyncLifetime
    {
        private readonly StreamBroadcaster _broadcaster;
        private readonly ScreenCaptureEngine _captureEngine;
        private readonly EmbeddedWebServer _server;
        private readonly HttpClient _http;
        private readonly int _testPort = 7892;

        public EmbeddedWebServerIntegrationTests()
        {
            _broadcaster = new StreamBroadcaster(75);
            _captureEngine = new ScreenCaptureEngine(_broadcaster);
            _server = new EmbeddedWebServer(_broadcaster, _captureEngine);
            _http = new HttpClient();
        }

        public async Task InitializeAsync()
        {
            await _server.StartAsync("127.0.0.1", _testPort);
        }

        public async Task DisposeAsync()
        {
            _http.Dispose();
            await _server.DisposeAsync();
            _captureEngine.Dispose();
            _broadcaster.Dispose();
        }

        [Fact]
        public async Task GetIndexPage_ReturnsHttp200AndValidHtml()
        {
            var res = await _http.GetAsync($"http://127.0.0.1:{_testPort}/");

            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.Contains("text/html", res.Content.Headers.ContentType?.ToString());

            var html = await res.Content.ReadAsStringAsync();
            Assert.Contains("ScreenTask", html);
            Assert.Contains("screenCanvas", html);
        }

        [Fact]
        public async Task GetStatusApi_ReturnsValidJsonMetrics()
        {
            var res = await _http.GetAsync($"http://127.0.0.1:{_testPort}/api/status");

            Assert.Equal(HttpStatusCode.OK, res.StatusCode);

            var json = await res.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            Assert.Equal("ScreenTask", root.GetProperty("appName").GetString());
            Assert.Equal("2.0.0", root.GetProperty("version").GetString());
            Assert.True(root.TryGetProperty("viewers", out _));
            Assert.True(root.TryGetProperty("fps", out _));
        }

        [Fact]
        public async Task GetQrCodeApi_ReturnsValidPngImage()
        {
            var res = await _http.GetAsync($"http://127.0.0.1:{_testPort}/api/qrcode");

            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.Equal("image/png", res.Content.Headers.ContentType?.MediaType);

            var bytes = await res.Content.ReadAsByteArrayAsync();
            Assert.True(bytes.Length > 8);
            Assert.Equal(0x89, bytes[0]);
            Assert.Equal((byte)'P', bytes[1]);
        }

        [Fact]
        public async Task WebSocketStream_HandshakesAndReceivesBroadcastedFrame()
        {
            using var ws = new ClientWebSocket();
            var uri = new Uri($"ws://127.0.0.1:{_testPort}/stream/ws");

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            await ws.ConnectAsync(uri, cts.Token);
            Assert.Equal(WebSocketState.Open, ws.State);

            // Broadcast a dummy test bitmap
            using var bmp = new Bitmap(64, 64);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Red);
            }
            await _broadcaster.BroadcastBitmapAsync(bmp);

            // Receive packet over WebSocket
            var buffer = new byte[64 * 1024];
            var receiveResult = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), cts.Token);

            Assert.True(receiveResult.Count > 1);
            Assert.Equal(WebSocketMessageType.Binary, receiveResult.MessageType);

            // Byte 0 must be 0x01 (Video frame packet type)
            Assert.Equal(0x01, buffer[0]);

            // Following bytes should be a valid JPEG (0xFF, 0xD8)
            Assert.Equal(0xFF, buffer[1]);
            Assert.Equal(0xD8, buffer[2]);

            // Close gracefully
            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Done", CancellationToken.None);
        }

        [Fact]
        public async Task PinAuthentication_RejectsInvalidAndAcceptsValidPin()
        {
            _server.RequirePin = true;
            _server.PinCode = "4321";

            // Test incorrect PIN
            var badContent = new StringContent("{\"pin\":\"9999\"}", Encoding.UTF8, "application/json");
            var badRes = await _http.PostAsync($"http://127.0.0.1:{_testPort}/api/auth", badContent);
            var badJson = await badRes.Content.ReadAsStringAsync();
            using var badDoc = JsonDocument.Parse(badJson);
            Assert.False(badDoc.RootElement.GetProperty("success").GetBoolean());

            // Test correct PIN
            var goodContent = new StringContent("{\"pin\":\"4321\"}", Encoding.UTF8, "application/json");
            var goodRes = await _http.PostAsync($"http://127.0.0.1:{_testPort}/api/auth", goodContent);
            var goodJson = await goodRes.Content.ReadAsStringAsync();
            using var goodDoc = JsonDocument.Parse(goodJson);
            Assert.True(goodDoc.RootElement.GetProperty("success").GetBoolean());
        }
    }
}
