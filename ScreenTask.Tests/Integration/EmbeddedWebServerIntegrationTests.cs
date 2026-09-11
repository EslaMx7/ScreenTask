using System;
using System.Diagnostics;
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
        public async Task GetViewerCss_ReturnsHttp200AndCssContentType()
        {
            var res = await _http.GetAsync($"http://127.0.0.1:{_testPort}/viewer.css");

            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.Contains("text/css", res.Content.Headers.ContentType?.ToString());

            var css = await res.Content.ReadAsStringAsync();
            Assert.Contains(".simulated-fullscreen", css);
            Assert.Contains(".floating-toolbar", css);
            Assert.Contains("100dvh", css);
        }

        [Fact]
        public async Task GetViewerJs_ReturnsHttp200AndJavaScriptContentType()
        {
            var res = await _http.GetAsync($"http://127.0.0.1:{_testPort}/viewer.js");

            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.True(
                res.Content.Headers.ContentType?.ToString().Contains("javascript") == true,
                $"Expected javascript Content-Type, got: {res.Content.Headers.ContentType}");

            var js = await res.Content.ReadAsStringAsync();
            Assert.Contains("screenCanvas", js);
            Assert.Contains("simulated-fullscreen", js);

            // Verify JavaScript has valid syntax via node if installed
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "node",
                    Arguments = "-c -",
                    RedirectStandardInput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using var proc = Process.Start(psi);
                if (proc != null)
                {
                    await proc.StandardInput.WriteAsync(js);
                    proc.StandardInput.Close();
                    var err = await proc.StandardError.ReadToEndAsync();
                    await proc.WaitForExitAsync();
                    Assert.True(proc.ExitCode == 0, $"viewer.js has syntax error: {err}");
                }
            }
            catch (System.ComponentModel.Win32Exception)
            {
                // Node.js not installed on test runner; skip node syntax check
            }
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

        [Fact]
        public async Task WebSocket_WithInvalidPin_ClosesWithPolicyViolation()
        {
            _server.RequirePin = true;
            _server.PinCode = "5678";

            using var ws = new ClientWebSocket();
            await ws.ConnectAsync(new Uri($"ws://127.0.0.1:{_testPort}/stream/ws?pin=wrong"), CancellationToken.None);

            var buffer = new byte[256];
            var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            // Expect server to close socket with PolicyViolation (1008)
            Assert.Equal(WebSocketMessageType.Close, result.MessageType);
            Assert.Equal(WebSocketCloseStatus.PolicyViolation, ws.CloseStatus);
        }

        [Fact]
        public async Task WebSocket_WithValidPin_ConnectsSuccessfully()
        {
            _server.RequirePin = true;
            _server.PinCode = "5678";

            using var ws = new ClientWebSocket();
            await ws.ConnectAsync(new Uri($"ws://127.0.0.1:{_testPort}/stream/ws?pin=5678"), CancellationToken.None);

            Assert.Equal(WebSocketState.Open, ws.State);

            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Done", CancellationToken.None);
        }

        [Fact]
        public async Task UpdateSecurity_DynamicallyKicksConnectedClientsWhenPinEnabled()
        {
            _server.RequirePin = false;

            using var ws = new ClientWebSocket();
            await ws.ConnectAsync(new Uri($"ws://127.0.0.1:{_testPort}/stream/ws"), CancellationToken.None);
            Assert.Equal(WebSocketState.Open, ws.State);

            // Dynamically enable PIN while client is connected
            _server.UpdateSecurity(true, "9999");

            var buffer = new byte[256];
            var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            // Client should be kicked with PolicyViolation
            Assert.Equal(WebSocketMessageType.Close, result.MessageType);
            Assert.Equal(WebSocketCloseStatus.PolicyViolation, ws.CloseStatus);
        }

        [Fact]
        public async Task AudioWebSocketStream_HandshakesAndReceivesFormatAndPcm()
        {
            _server.RequirePin = false;
            await _broadcaster.BroadcastAudioFormatAsync(48000, 2, 16);

            using var ws = new ClientWebSocket();
            var uri = new Uri($"ws://127.0.0.1:{_testPort}/stream/audio");

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            await ws.ConnectAsync(uri, cts.Token);
            Assert.Equal(WebSocketState.Open, ws.State);

            // AddAudioClient immediately sends the cached audio format packet (0x03)
            var formatBuffer = new byte[256];
            var formatResult = await ws.ReceiveAsync(new ArraySegment<byte>(formatBuffer), cts.Token);

            Assert.Equal(WebSocketMessageType.Binary, formatResult.MessageType);
            Assert.Equal(9, formatResult.Count);
            Assert.Equal(0x03, formatBuffer[0]); // Audio format packet
            Assert.Equal(48000, BitConverter.ToInt32(formatBuffer, 1));
            Assert.Equal(2, BitConverter.ToInt16(formatBuffer, 5));
            Assert.Equal(16, BitConverter.ToInt16(formatBuffer, 7));

            // Now broadcast a PCM audio chunk
            var dummyPcm = new byte[] { 0x01, 0x02, 0x03, 0x04 };
            await _broadcaster.BroadcastAudioChunkAsync(dummyPcm);

            var pcmBuffer = new byte[256];
            var pcmResult = await ws.ReceiveAsync(new ArraySegment<byte>(pcmBuffer), cts.Token);

            Assert.Equal(WebSocketMessageType.Binary, pcmResult.MessageType);
            Assert.Equal(1 + dummyPcm.Length, pcmResult.Count);
            Assert.Equal(0x02, pcmBuffer[0]); // Audio chunk packet
            Assert.Equal(0x01, pcmBuffer[1]);
            Assert.Equal(0x02, pcmBuffer[2]);
            Assert.Equal(0x03, pcmBuffer[3]);
            Assert.Equal(0x04, pcmBuffer[4]);

            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Done", CancellationToken.None);
        }

        [Fact]
        public async Task AudioWebSocket_WithInvalidPin_ClosesWithPolicyViolation()
        {
            _server.RequirePin = true;
            _server.PinCode = "7777";

            using var ws = new ClientWebSocket();
            await ws.ConnectAsync(new Uri($"ws://127.0.0.1:{_testPort}/stream/audio?pin=wrong"), CancellationToken.None);

            var buffer = new byte[256];
            var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            Assert.Equal(WebSocketMessageType.Close, result.MessageType);
            Assert.Equal(WebSocketCloseStatus.PolicyViolation, ws.CloseStatus);
        }

        [Fact]
        public async Task AudioWebSocket_WithValidPin_ConnectsSuccessfully()
        {
            _server.RequirePin = true;
            _server.PinCode = "7777";

            using var ws = new ClientWebSocket();
            await ws.ConnectAsync(new Uri($"ws://127.0.0.1:{_testPort}/stream/audio?pin=7777"), CancellationToken.None);

            Assert.Equal(WebSocketState.Open, ws.State);

            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Done", CancellationToken.None);
        }
    }
}
