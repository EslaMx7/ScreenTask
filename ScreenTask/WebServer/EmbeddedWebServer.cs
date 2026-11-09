using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScreenTask.Core.Capture;
using ScreenTask.Core.Network;
using ScreenTask.Core.Streaming;

namespace ScreenTask.WebServer
{
    public class EmbeddedWebServer : IAsyncDisposable
    {
        private WebApplication? _app;
        private readonly StreamBroadcaster _broadcaster;
        private readonly ScreenCaptureEngine _captureEngine;
        private CancellationTokenSource? _cts;

        public string HostIp { get; private set; } = "127.0.0.1";
        public int Port { get; private set; } = 7070;
        public bool IsRunning { get; private set; }
        public bool RequirePin { get; set; }
        public string PinCode { get; set; } = "";

        public void UpdateSecurity(bool requirePin, string pinCode)
        {
            var oldRequirePin = RequirePin;
            var oldPin = PinCode;
            RequirePin = requirePin;
            PinCode = pinCode;

            if (requirePin && (!oldRequirePin || oldPin != pinCode))
            {
                // Disconnect existing viewers so they are prompted to enter the PIN
                _ = _broadcaster.KickAllClientsAsync(WebSocketCloseStatus.PolicyViolation, "PIN required");
            }
        }

        public string ServerUrl => $"http://{HostIp}:{Port}";

        public EmbeddedWebServer(StreamBroadcaster broadcaster, ScreenCaptureEngine captureEngine)
        {
            _broadcaster = broadcaster;
            _captureEngine = captureEngine;
        }

        public async Task StartAsync(string ip, int port)
        {
            if (IsRunning) await StopAsync();

            HostIp = ip;
            Port = port;
            _cts = new CancellationTokenSource();

            var builder = WebApplication.CreateEmptyBuilder(new WebApplicationOptions
            {
                EnvironmentName = Environments.Production
            });

            builder.WebHost.UseKestrel(options =>
            {
                options.Listen(IPAddress.Any, port);
            });

            builder.Services.AddRouting();

            _app = builder.Build();

            _app.UseWebSockets(new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromSeconds(15)
            });

            ConfigureRoutes(_app);

            await _app.StartAsync(_cts.Token);
            IsRunning = true;
        }

        public async Task StopAsync()
        {
            if (!IsRunning || _app == null) return;

            try
            {
                _cts?.Cancel();
                await _app.StopAsync(TimeSpan.FromSeconds(2));
                await _app.DisposeAsync();
            }
            catch { }
            finally
            {
                _app = null;
                _cts?.Dispose();
                _cts = null;
                IsRunning = false;
            }
        }

        private static readonly ConcurrentDictionary<string, byte[]?> _embeddedAssetCache = new(StringComparer.OrdinalIgnoreCase);

        private static byte[]? GetEmbeddedAssetBytes(string safeFileName)
        {
            return _embeddedAssetCache.GetOrAdd(safeFileName, static name =>
            {
                var assembly = typeof(EmbeddedWebServer).Assembly;
                string expectedSuffix = $".WebServer.{name}";
                string? resourceName = assembly.GetManifestResourceNames()
                    .FirstOrDefault(n => n.EndsWith(expectedSuffix, StringComparison.OrdinalIgnoreCase));

                if (resourceName == null) return null;

                using var stream = assembly.GetManifestResourceStream(resourceName);
                if (stream == null) return null;

                using var ms = new MemoryStream();
                stream.CopyTo(ms);
                return ms.ToArray();
            });
        }

        private static async Task ServeFileAsync(HttpContext context, string fileName, string contentType)
        {
            string safeFileName = Path.GetFileName(fileName);
            if (!string.IsNullOrWhiteSpace(safeFileName))
            {
                var bytes = GetEmbeddedAssetBytes(safeFileName);
                if (bytes != null)
                {
                    context.Response.ContentType = contentType;
                    context.Response.ContentLength = bytes.Length;
                    context.Response.Headers.CacheControl = "no-cache";
                    await context.Response.Body.WriteAsync(bytes);
                    return;
                }
            }

            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync($"File {safeFileName} not found");
        }

        private void ConfigureRoutes(WebApplication app)
        {
            // WebSocket stream endpoint (video frames)
            app.Map("/stream/ws", async (HttpContext context) =>
            {
                if (!context.WebSockets.IsWebSocketRequest)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }

                using var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                if (RequirePin && !string.IsNullOrEmpty(PinCode))
                {
                    var queryPin = context.Request.Query["pin"].ToString();
                    if (queryPin != PinCode)
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.PolicyViolation, "Invalid or missing PIN", CancellationToken.None);
                        return;
                    }
                }

                var clientId = Guid.NewGuid().ToString("N");
                var remoteIp = context.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";
                if (remoteIp == "::1" || remoteIp == "0.0.0.1") remoteIp = "127.0.0.1";
                var userAgent = context.Request.Headers.UserAgent.ToString();

                _broadcaster.AddVideoClient(clientId, webSocket, remoteIp, userAgent);

                var buffer = new byte[2048];
                try
                {
                    while (webSocket.State == WebSocketState.Open && !_cts!.Token.IsCancellationRequested)
                    {
                        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), _cts.Token);
                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                            break;
                        }
                        else if (result.MessageType == WebSocketMessageType.Text && result.Count > 0)
                        {
                            var jsonStr = Encoding.UTF8.GetString(buffer, 0, result.Count);
                            try
                            {
                                using var doc = JsonDocument.Parse(jsonStr);
                                var root = doc.RootElement;
                                if (root.TryGetProperty("type", out var typeProp))
                                {
                                    var type = typeProp.GetString();
                                    if (type == "ping" && root.TryGetProperty("t", out var tProp))
                                    {
                                        await _broadcaster.SendPongAsync(clientId, tProp.GetDouble());
                                    }
                                    else if (type == "telemetry")
                                    {
                                        double rtt = root.TryGetProperty("rtt", out var rProp) ? rProp.GetDouble() : 0.0;
                                        string viewMode = root.TryGetProperty("mode", out var mProp) ? (mProp.GetString() ?? "") : "";
                                        _broadcaster.UpdateTelemetry(clientId, rtt, viewMode);
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }
                catch { }
                finally
                {
                    _broadcaster.RemoveVideoClient(clientId);
                }
            });

            // Dedicated audio WebSocket endpoint
            app.Map("/stream/audio", async (HttpContext context) =>
            {
                if (!context.WebSockets.IsWebSocketRequest)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }

                using var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                if (RequirePin && !string.IsNullOrEmpty(PinCode))
                {
                    var queryPin = context.Request.Query["pin"].ToString();
                    if (queryPin != PinCode)
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.PolicyViolation, "Invalid or missing PIN", CancellationToken.None);
                        return;
                    }
                }

                var clientId = Guid.NewGuid().ToString("N");
                _broadcaster.AddAudioClient(clientId, webSocket);

                var buffer = new byte[1024];
                try
                {
                    while (webSocket.State == WebSocketState.Open && !_cts!.Token.IsCancellationRequested)
                    {
                        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), _cts.Token);
                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                            break;
                        }
                    }
                }
                catch { }
                finally
                {
                    _broadcaster.RemoveAudioClient(clientId);
                }
            });

            // Backward compatible snapshot endpoint
            app.MapGet("/ScreenTask.jpg", async (HttpContext context) =>
            {
                context.Response.ContentType = "image/jpeg";
                context.Response.Headers.CacheControl = "no-cache, no-store, must-revalidate";
                context.Response.Headers.Pragma = "no-cache";
                context.Response.Headers.Expires = "0";

                var snapshot = _broadcaster.GetLatestSnapshot();
                if (snapshot.Length > 0)
                {
                    context.Response.ContentLength = snapshot.Length;
                    await context.Response.Body.WriteAsync(snapshot);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                }
            });

            // API: Server status
            app.MapGet("/api/status", () =>
            {
                var stats = new
                {
                    appName = "ScreenTask",
                    version = "2.0.0",
                    viewers = _broadcaster.ConnectedClientsCount,
                    fps = _broadcaster.CurrentFps,
                    bytesTransferred = _broadcaster.TotalBytesTransferred,
                    audioEnabled = _captureEngine.AudioEnabled,
                    sourceName = _captureEngine.CurrentSource.DisplayName,
                    requirePin = RequirePin
                };
                return Results.Json(stats);
            });

            // API: QR Code image
            app.MapGet("/api/qrcode", (HttpContext context) =>
            {
                var bytes = QrCodeHelper.GenerateQrCodePngBytes(ServerUrl);
                return Results.File(bytes, "image/png");
            });

            // API: PIN verification
            app.MapPost("/api/auth", async (HttpContext context) =>
            {
                if (!RequirePin || string.IsNullOrEmpty(PinCode))
                {
                    return Results.Json(new { success = true });
                }

                using var reader = new StreamReader(context.Request.Body);
                var body = await reader.ReadToEndAsync();
                try
                {
                    using var doc = JsonDocument.Parse(body);
                    if (doc.RootElement.TryGetProperty("pin", out var pinProp) && pinProp.GetString() == PinCode)
                    {
                        return Results.Json(new { success = true });
                    }
                }
                catch { }

                return Results.Json(new { success = false, message = "Invalid PIN code." });
            });

            // Explicit static routes for CSS and JS
            app.MapGet("/viewer.css", (HttpContext ctx) => ServeFileAsync(ctx, "viewer.css", "text/css; charset=utf-8"));
            app.MapGet("/viewer.js", (HttpContext ctx) => ServeFileAsync(ctx, "viewer.js", "application/javascript; charset=utf-8"));
            app.MapGet("/buymeacoffee.png", (HttpContext ctx) => ServeFileAsync(ctx, "buymeacoffee.png", "image/png"));
            app.MapGet("/favicon.svg", (HttpContext ctx) => ServeFileAsync(ctx, "favicon.svg", "image/svg+xml"));

            // Home / Index
            app.MapGet("/", (HttpContext ctx) => ServeFileAsync(ctx, "index.html", "text/html; charset=utf-8"));
            app.MapGet("/index.html", (HttpContext ctx) => ServeFileAsync(ctx, "index.html", "text/html; charset=utf-8"));

            // Catch-all route handler for any other file request
            app.MapGet("/{**slug}", async (HttpContext context, string? slug) =>
            {
                var fileName = string.IsNullOrWhiteSpace(slug) ? "index.html" : slug.TrimStart('/');
                var ext = Path.GetExtension(fileName).ToLowerInvariant();
                var contentType = ext switch
                {
                    ".html" or ".htm" => "text/html; charset=utf-8",
                    ".css" => "text/css; charset=utf-8",
                    ".js" => "application/javascript; charset=utf-8",
                    ".png" => "image/png",
                    ".jpg" or ".jpeg" => "image/jpeg",
                    ".svg" => "image/svg+xml",
                    ".ico" => "image/x-icon",
                    ".json" => "application/json",
                    _ => "application/octet-stream"
                };

                await ServeFileAsync(context, fileName, contentType);
            });
        }

        public async ValueTask DisposeAsync()
        {
            await StopAsync();
        }
    }
}
