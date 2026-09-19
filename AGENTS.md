# Project Overview

ScreenTask 2.0 is a free, open-source, portable Windows desktop application built with modern
.NET (targeting .NET 9.0 Windows desktop with C# 13) that provides instant, zero-configuration
screen and audio sharing over local Wi-Fi, Ethernet LAN, or mobile hotspots without requiring
internet access. It features low-latency (<50ms) dual-channel binary WebSocket streaming, NAudio 3
WASAPI system audio capture, monitor and individual application window sharing, in-memory zero-disk
compression, PIN session protection, QR code pairing, and a responsive HTML5/CSS3/ES6 web client
supporting iOS Safari, Android Chrome, and desktop browsers.

## Repository Structure

* `.github/` - Community health and project funding configuration files.
* `ScreenTask/` - Main Windows Forms desktop host application and embedded Kestrel web server.
  * `Core/` - Core domain engines for audio, screen capture, networking, and streaming.
    * `Audio/` - NAudio 3 WASAPI audio capture service for loopback and microphone devices.
    * `Capture/` - Capture abstraction, Win32 GDI interop, and capture timer engine.
    * `Network/` - Network adapter enumeration, firewall rule helpers, and QR code generation.
    * `Streaming/` - In-memory JPEG encoding, frame-dropping protection, WebSocket broadcaster, and `ViewerSession` fleet telemetry.
  * `UI/` - Windows Forms views, controls, and dialogs.
    * `Views/` - Decoupled `UserControl` views (`BroadcastView`, `ConnectedViewersView`, `CaptureSettingsView`, `NetworkSettingsView`, `PreferencesView`).
    * `Controls/` - Custom composite UserControls (`MonitorPreviewControl`).
    * `MiniBarForm.cs` - Dedicated floating compact toolbar form.
    * Dialogs (`QrCodeForm`, `AudioDiagnosticsForm`).
  * `WebServer/` - Embedded ASP.NET Core server, REST endpoints, and static web viewer assets.
* `ScreenTask.Tests/` - Automated xUnit test suite containing unit, integration, and UI tests.
  * `Core/` - Unit tests for settings, capture clipping, adapters, QR codes, and audio.
  * `Integration/` - Integration tests for embedded Kestrel server, WebSockets, and static assets.
  * `UI/` - STA-threaded Windows Forms UI validation tests for forms, views, controls, and events.
* `ScreenTask.sln` - Visual Studio solution file uniting the application and test projects.

## Build & Development Commands

### Install & Restore
```powershell
dotnet restore
```

### Build
```powershell
# Debug build
dotnet build

# Release build (strict zero warnings/errors)
dotnet build -c Release
```

### Test
```powershell
# Run all unit, integration, and UI tests
dotnet test

# Run tests without rebuilding (useful when ScreenTask.exe is running)
dotnet test ScreenTask.Tests/ScreenTask.Tests.csproj --no-build

# Run a specific test class or method
dotnet test --filter "FullyQualifiedName~EmbeddedWebServerIntegrationTests"
dotnet test --filter "FullyQualifiedName~MainFormUiTests"
```

### Lint & Format
```powershell
# Verify formatting without modifying files
dotnet format --verify-no-changes

# Auto-format C# source files
dotnet format
```

### Static Syntax Check (Web Assets)
```powershell
# Validate JavaScript syntax for the web client viewer
node -c ScreenTask/WebServer/viewer.js
```

### Run
```powershell
# Launch the desktop application
dotnet run --project ScreenTask/ScreenTask.csproj
```

### Publish & Deploy
```powershell
# Publish standalone single-file executable for Windows x64
dotnet publish ScreenTask/ScreenTask.csproj -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
```

## Code Style & Conventions

* **Language Version**: C# 13 running on `.NET 9.0-windows10.0.19041.0`.
* **Nullable Reference Types**: Enabled globally (`<Nullable>enable</Nullable>`). No nullable
  suppression (`!`) without documented justification.
* **Naming Conventions**:
  * PascalCase for types, methods, public properties, and events (`ScreenCaptureEngine`).
  * `I` prefix for interfaces (`ICaptureSource`).
  * `_camelCase` with leading underscore for private fields (`_broadcaster`, `_captureEngine`).
  * `camelCase` for local variables and parameters.
  * WinForms event handlers use `controlName_EventName` (`btnStartServer_Click`).
* **Web Viewer Code**:
  * Pure ES6+ JavaScript wrapped in IIFE (`viewer.js`), standard CSS3 (`viewer.css`).
  * Zero npm/webpack build step required; assets are served directly from disk.
  * Always verify syntax with `node -c ScreenTask/WebServer/viewer.js` after edits.
* **Commit Message Format**: Follow Conventional Commits:
  * Format: `<type>(<scope>): <short description>`
  * Types: `feat`, `fix`, `refactor`, `test`, `docs`, `style`, `chore`
  * Scopes: `core`, `audio`, `capture`, `network`, `web`, `ui`, `tests`
  * Example: `feat(audio): migrate to NAudio 3 WasapiRecorder and add dedicated audio channel`

## Architecture Notes

```mermaid
flowchart TD
    subgraph Host["Host PC (ScreenTask.exe)"]
        UI["frmMain (WinForms Host UI)"] -->|Hosts| VIEWS["UserControl Views (Broadcast, Viewers, Capture, Network, Prefs)"]
        UI <-->|Toggle| MINI["MiniBarForm (Floating Compact Bar)"]
        VIEWS -->|Configure| ENGINE["ScreenCaptureEngine"]
        VIEWS -->|Configure| AUDIO["WasapiAudioCaptureService"]
        ENGINE -->|Frames| BROADCASTER["StreamBroadcaster"]
        AUDIO -->|PCM Chunks| BROADCASTER
        BROADCASTER <-->|Fleet Telemetry| SESS["ConcurrentDictionary<string, ViewerSession>"]
        BROADCASTER -->|Dual WS Channels| KESTREL["EmbeddedWebServer (Kestrel)"]
    end

    subgraph Browser["Web Client Viewer (Phone / Tablet / PC)"]
        KESTREL -->|/stream/ws| WS_VIDEO["wsVideo (Canvas 2D)"]
        KESTREL -->|/stream/audio| WS_AUDIO["wsAudio (Web Audio API)"]
        KESTREL -->|Static Assets| STATIC["index.html + viewer.css + viewer.js"]
        WS_VIDEO -->|Ping Heartbeat + Zoom/Mode Telemetry| KESTREL
        KESTREL -->|0x04 Broadcast Announcements| TOAST["Toast Alerts"]
    end
```

### Component Breakdown & Data Flow
1. **UserControl Views & MiniBarForm**: UI is broken down into 5 independent WinForms `UserControl`
   views (`BroadcastView`, `ConnectedViewersView`, `CaptureSettingsView`, `NetworkSettingsView`,
   `PreferencesView`) plus a standalone `MiniBarForm` floating toolbar. This guarantees complete Visual
   Studio Designer support with zero panel occlusion or designer Z-order fighting.
2. **ScreenCaptureEngine**: Driven by an asynchronous high-resolution timer loop. Captures frames
   from `ICaptureSource` (`ScreenCaptureSource` or `WindowCaptureSource`) using GDI32 BitBlt or
   PrintWindow, conditionally draws the hardware mouse cursor, and pushes bitmaps to broadcaster.
3. **WasapiAudioCaptureService**: Captures system output loopback or microphone audio via modern
   NAudio 3 `WasapiRecorderBuilder`. Converts multi-channel audio to normalized PCM, calculates
   real-time VU levels for UI metering, and dispatches buffers to `StreamBroadcaster`.
4. **StreamBroadcaster**: Encodes incoming bitmaps into memory-buffered JPEGs using
   `System.Drawing.Imaging`. Implements drop-frame protection for slow network clients and pushes
   binary packets over `/stream/ws` (video) and `/stream/audio` (audio). Manages real-time viewer
   sessions (`ViewerSession`), measuring fleet RTT ping latency, handling individual audio muting,
   and dispatching broadcast announcements.
5. **EmbeddedWebServer**: In-process ASP.NET Core Kestrel instance. Hosts static viewer assets,
   serves REST endpoints (`/api/status`, `/api/auth`, `/api/qrcode`), and handles WebSocket
   connections with optional PIN session authentication.
6. **Web Viewer**: Pure HTML5/CSS3/JS client (`viewer.js`). Renders video onto `<canvas>`, streams audio
   through the Web Audio API with a jitter buffer, supports double-tap zoom, pinch-to-zoom (up to 5x),
   simulated iOS fullscreen, ping/pong heartbeats, and on-screen broadcast alert toasts.

## Testing Strategy

1. **Unit Tests** (`ScreenTask.Tests/Core/`):
   * Validate standalone algorithms: `AppSettings` XML serialization, `CaptureSource` coordinate
     bounding, `NetworkHelper` adapter and loopback filtering, `QrCodeHelper` PNG generation,
     and `StreamBroadcaster` client tracking.
2. **Integration Tests** (`ScreenTask.Tests/Integration/`):
   * Spins up real embedded Kestrel web servers on local loopback test ports.
   * Tests HTTP GET for `/`, `/viewer.css`, `/viewer.js`, `/api/status`, `/api/qrcode`.
   * Verifies WebSocket connections, binary frame delivery, and audio PCM packets.
   * Tests PIN authorization enforcement, invalid attempts, and dynamic client disconnection.
   * Automatically validates `viewer.js` syntax via `node -c -` through standard input.
3. **UI Tests** (`ScreenTask.Tests/UI/`):
   * Runs forms (`frmMain`, `QrCodeForm`, `AudioDiagnosticsForm`) inside STA threads.
   * Validates control layouts, refresh buttons, and deadlock-free disposal.
4. **Local Execution**:
   * Run `dotnet test` from repository root.
5. **CI Execution**:
   > TODO: Add GitHub Actions workflow (`.github/workflows/ci.yml`) targeting `windows-latest`.

## Security & Compliance

* **Secrets & Credentials**:
  * No secrets, tokens, or external API keys are required or stored in the codebase.
  * PIN authentication uses in-memory comparison over `/api/auth` without database persistence.
* **Network Exposure**:
  * Server binds strictly to the user-selected local network adapter or loopback (`127.0.0.1`).
  * Unauthenticated WebSocket attempts are terminated with `PolicyViolation` (1008) when PIN
    protection is enabled.
* **Native Interop & Memory**:
  * Win32 API calls (`gdi32.dll`, `user32.dll`, `dwmapi.dll`) use safe P/Invoke declarations.
  * All device contexts and graphics handles (`HDC`, `HBITMAP`) are released in `finally` blocks.
  * Frame buffers use in-memory streams; zero media files are written to physical disk.
* **Licensing**:
  * Licensed under the GNU General Public License v3.0 (GPLv3).
  * Any new dependencies or contributions must remain GPLv3 compatible.

## Agent Guardrails

* **Restricted Files (Do Not Modify Without User Request)**:
  * Solution file `ScreenTask.sln` structure unless explicitly adding a project.
  * Visual Studio metadata (`.vs/`, `*.user`, `*.suo`).
  * Binary icons (`ScreenTask/file_icon.ico`, `ScreenTask/screenicon.ico`).
  * Copyright headers and authorship notes in `README.md` and source files.
* **Audio Library APIs**:
  * Do NOT use deprecated NAudio classes (`WasapiCapture`, `WasapiLoopbackCapture`).
  * Always use modern NAudio 3 APIs: `WasapiRecorderBuilder`, `WasapiRecorder`, `WasapiPlayer`.
* **UI Threading**:
  * Never call blocking audio operations (`Dispose()`, `Join()`, `Stop()`) synchronously on the
    Windows Forms UI thread. Perform audio cleanup on background tasks or asynchronous paths.
* **Web Client Synchronization**:
  * When editing `ScreenTask/WebServer/viewer.js` or `viewer.css` while the application is
    running, also sync changes to `ScreenTask/bin/Debug/.../WebServer/` so the live process serves
    the updated assets immediately.

## Extensibility Hooks

1. **Custom Capture Sources**:
   * Implement `ScreenTask.Core.Capture.ICaptureSource` (`CaptureFrame()`, `DisplayName`).
   * Can be used to add DirectX Desktop Duplication (DXGI), Windows.Graphics.Capture, or webcam
     overlays.
2. **Audio Processing**:
   * `WasapiAudioCaptureService` exposes `AudioDataAvailable` and `AudioLevelChanged`.
   * Hook into this pipeline to inject noise suppression, audio filters, or Opus compression.
3. **HTTP & WebSocket Endpoints**:
   * Register new endpoints inside `EmbeddedWebServer.MapEndpoints()`.
   * Additional REST routes or streaming protocols can be mounted directly on Kestrel.
4. **Persistent Settings**:
   * Add properties to `ScreenTask.AppSettings`. Settings are automatically serialized to XML in
     `%APPDATA%\ScreenTask\settings.xml`.

## Further Reading

* [README.md](README.md) - End-user setup guide, feature overview, and version comparison.
* [Official Project Website](https://screentask.eslamx.com/) - User tutorials and releases.
* > TODO: Create `docs/ARCH.md` with binary WebSocket frame header specifications.
* > TODO: Create `docs/CONTRIBUTING.md` with guidelines for pull requests and code review.
