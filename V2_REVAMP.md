# ScreenTask 2.0 – Revamp State, Features & Progress Report

> **Branch**: `v2-revamp`  
> **Target Framework**: `.NET 9.0-windows10.0.19041.0` (C# 13)  
> **License**: GNU General Public License v3.0 (GPLv3)  
> **Last Updated**: September 2026  

---

## 1. Executive Summary

ScreenTask 2.0 represents a complete modernization of the original 2015 open-source screen sharing utility. Over the past decade, user expectations for screen sharing have evolved toward high framerate real-time streaming, audio synchronization, zero-configuration local networking, multi-device mobile compatibility, and clean desktop ergonomics.

The 2.0 revamp transitions the application from a legacy .NET Framework 4.8 / HTTP-polling architecture into a modern, low-latency, zero-disk binary streaming server with dual-channel WebSockets, NAudio 3 WASAPI audio capture, a responsive HTML5/CSS3/ES6 web client, and a native Windows Forms desktop interface adhering strictly to Windows system themes and clear, jargon-free terminology.

---

## 2. Architecture & Technology Stack

```mermaid
flowchart TD
    subgraph Host["Host PC (ScreenTask.exe - .NET 9 WinForms)"]
        UI["frmMain (Native WinForms UI)"] -->|Configure| ENGINE["ScreenCaptureEngine"]
        UI -->|Configure| AUDIO["WasapiAudioCaptureService"]
        ENGINE -->|GDI BitBlt / PrintWindow| BROADCASTER["StreamBroadcaster (ArrayPool JPEG)"]
        AUDIO -->|WASAPI 48kHz PCM| BROADCASTER
        BROADCASTER -->|/stream/ws (Video)| KESTREL["EmbeddedWebServer (Kestrel)"]
        BROADCASTER -->|/stream/audio (Audio)| KESTREL
    end

    subgraph Browser["Web Client Viewer (Mobile / Tablet / PC)"]
        KESTREL -->|WebSocket Binary| WS_VIDEO["wsVideo (HTML5 Canvas 2D)"]
        KESTREL -->|WebSocket Binary| WS_AUDIO["wsAudio (Web Audio API)"]
        KESTREL -->|HTTP GET Static| STATIC["index.html + viewer.css + viewer.js"]
        KESTREL -->|REST /api/status| POLL["Status Poller (3s)"]
    end
```

### Key Technologies
* **Runtime**: .NET 9.0 Windows Desktop SDK targeting Windows 10/11 (`net9.0-windows10.0.19041.0`) with C# 13 language features and global nullable reference types enabled.
* **Web Server**: In-process ASP.NET Core Kestrel instance (`Microsoft.AspNetCore.App`) binding exclusively to the selected network interface or loopback (`127.0.0.1`).
* **Audio Engine**: NAudio 3 using modern `WasapiRecorderBuilder` and `WasapiRecorder` for system output loopback and microphone capture.
* **Capture Engine**: High-resolution multimedia timer loop driving Win32 GDI32 (`BitBlt`) and User32 (`PrintWindow`) capture with hardware mouse cursor rendering.
* **Memory Management**: Zero-disk architecture utilizing `System.Buffers.ArrayPool<byte>` and `MemoryStream` buffers for in-memory JPEG compression and PCM packetization.
* **Web Client**: Pure ES6 JavaScript IIFE (`viewer.js`) and standard CSS3 (`viewer.css`) requiring zero build steps or npm packages.

---

## 3. Core Features & Capabilities

### Real-Time Low-Latency Streaming
* **High Framerates**: Supports 60 FPS (Ultra Smooth), 30 FPS (Standard), 15 FPS (Balanced), and 5 FPS (Low Bandwidth).
* **Low Latency**: Sub-50ms latency over local Wi-Fi, mobile hotspots, or Ethernet LAN.
* **Drop-Frame Protection**: Broadcaster automatically drops intermediate frames for slow network clients without stalling the server or impacting other viewers.

### Dual-Channel Audio & Microphone Streaming
* **System Sound & Microphone**: Captures system loopback audio (speakers, browser, games) or microphone input via NAudio 3 WASAPI.
* **High-Definition Audio**: Normalized PCM 16-bit 48kHz stereo stream pushed over a dedicated WebSocket (`/stream/audio`).
* **Real-Time VU Metering**: Live decibel (`dB`) and percentage volume calculation displayed on the desktop UI and in the web client.

### Display & Application Window Sharing
* **Multi-Monitor Support**: Automatically enumerates and captures any physical display with native resolution detection.
* **Individual Window Capture**: Captures specific application windows (e.g., Visual Studio Code, PowerPoint, browser tabs) via Win32 window handles (`HWND`) and `PrintWindow`.
* **Hardware Mouse Pointer**: Composites the live system cursor with drop shadow directly into the stream frame when enabled.

### Zero-Configuration Local Networking & Pairing
* **Adapter Detection**: Automatically detects Wi-Fi, Ethernet, and virtual hotspot adapters, highlighting recommended active interfaces.
* **Instant QR Code Pairing**: Generates crisp vector QR codes for immediate smartphone/tablet pairing without manual IP typing.
* **Firewall Awareness**: Integrated port conflict detection and Windows Defender Firewall helper logic.

### Session Security & Access Control
* **4-Digit PIN Session Protection**: Optional random or user-defined 4-digit PIN code.
* **WebSocket Authentication**: Viewers must authenticate via `/api/auth` before WebSocket connections are granted; unauthenticated sockets are terminated with `PolicyViolation` (1008).

### Next-Gen HTML5 Web Viewer
* **Responsive Layout**: Designed for seamless viewing across iOS Safari, Android Chrome, and desktop browsers.
* **Simulated Fullscreen & Gestures**: Supports true fullscreen, simulated fullscreen for mobile Safari, double-tap zoom, and pinch-to-zoom (up to 5x) for reading fine text.
* **Web Audio API**: Low-latency audio playback with dynamic jitter buffer, mute toggle, and volume control.
* **Live HUD**: Overlay displaying real-time stream status, resolution, FPS counter, and connection health.

---

## 4. Desktop UI/UX Revamp

The desktop user interface was completely redesigned from the ground up to achieve modern visual hierarchy while strictly adhering to native Windows Forms controls, Windows system themes (`uxtheme`), and natural, user-friendly language.

### Design Principles
1. **Native OS Integration**: Uses native system colors (`SystemColors.Control`, `SystemColors.Window`, `SystemColors.ControlText`, `SystemColors.GrayText`) instead of arbitrary hardcoded color hexes, ensuring full compatibility with Windows light/dark themes and high-contrast accessibility modes.
2. **Solid Container Framing**: All container cards (`FluentCard`) and navigation pills utilize crisp single-line solid borders (`BorderStyle.FixedSingle`) to establish visual separation and clarity without artificial drop shadows or gradients.
3. **No Owner-Drawn Button Artifacts**: Buttons use standard OS rendering (`FlatStyle.Standard`, `UseVisualStyleBackColor = true`), eliminating artificial dark fills and non-standard borders.

### Vertical Side Navigation Structure

```
+------------------+-------------------------------------------------------------+
| [Icon] ScreenTask|                                 ● Ready to Stream   [🗖 Mini Bar] |
|        Local Share|                                                             |
+------------------+-------------------------------------------------------------+
| [📡 Broadcast]   |                                                             |
|                  |  Active View Content (Broadcast / Screen & Audio / etc.)    |
| [🎥 Screen/Audio]|                                                             |
|                  |  - Live Confidence Monitor & Start/Stop Controls            |
| [🛡️ Network/PIN] |  - Stream URL & Pairing                                     |
|                  |  - Metric Chips                                             |
| [⚙️ Settings/Log]|                                                             |
| ---------------- |                                                             |
| [🔇 Audio Off]   |                                                             |
+------------------+-------------------------------------------------------------+
| ScreenTask 2.0 • Free & Open Source under GPLv3 • © Eslam Hamouda              |
+--------------------------------------------------------------------------------+
```

#### Tab 1: Live Broadcast & Preview
* **Live Confidence Monitor**: Scaled-down preview of the active screen/window with resolution badge, live status indicator (`● LIVE` / `● IDLE`), and framerate counter.
* **Broadcast Controls**: Prominent `▶  START BROADCAST` / `⏹  STOP BROADCAST` hero action button with live volume level VU meter.
* **Stream Web Address Card**: Displays the local stream URL (`http://192.168.x.x:7070`), status pills (`Local Network (Zero Config)`, `PIN: Not Required`), `📋 Copy Link`, `📱 Show QR`, and `🌐 Open Browser` actions.
* **Metric Chips**: Four discrete stat cards tracking `Connected Viewers`, `Smoothness (FPS)`, `Data Transferred`, and `Stream Time`.

#### Tab 2: Screen & Audio Settings
* **Screen & Window Selection**: Dropdown selecting full monitors (`🖥️ Display 1: 1920x1080 (Primary)`) or open application windows (`🪟 App Window: Visual Studio Code`), with `🔄 Refresh` and mouse pointer toggle.
* **Audio & Microphone**: Channel toggle, device selection, `🔄 Refresh`, `🔔 Test Speakers` chime, and access to the dedicated audio diagnostics dialog.
* **Video Smoothness & Quality**: Framerate selector (60, 30, 15, 5 FPS) and continuous image quality slider (20%–100%) with dynamic feedback (`Image Quality: 80% (High Detail)`).

#### Tab 3: Network Connection & Security
* **Network Connection**: Network adapter picker with IP address preview, port number selector, and live availability verification (`✓ Port 7070 is available for streaming`).
* **Access Security & PIN**: Checkbox to require PIN code, 4-digit PIN input with obfuscation, `🎲 New PIN` generator, and `📋 Copy PIN` button.

#### Tab 4: Startup Preferences & Activity Log
* **Startup & Window Preferences**: Options to auto-start broadcast on launch, launch minimized to tray, and minimize to tray upon closing.
* **Activity & Connection Log**: Real-time event feed tracking connections, disconnections, device changes, and errors, with `Clear Log` and `📋 Copy Log`.

---

## 5. User-Friendly Terminology Harmonization

All developer jargon, obscure abbreviations, and legacy recording terms have been replaced with clear, natural language:

| Location / Component | Old Jargon / Technical String | Harmonized User-Friendly String |
| :--- | :--- | :--- |
| **Subtitle** | `Dual-Channel LAN Streaming Engine` | `Instant Local Screen Sharing` |
| **Main Action** | `START SERVER` / `STOP SERVER` | `▶  START BROADCAST` / `⏹  STOP BROADCAST` |
| **Status Badge** | `● BROADCASTING` / `● READY TO STREAM` | `● Live Broadcasting` / `● Ready to Stream` |
| **Audio Card** | `NAudio 3 WASAPI Sound Stream` | `Audio & Microphone` |
| **Audio Pill** | `PCM 16-bit 48kHz` | `High Quality Audio (48 kHz)` |
| **Audio Toggle** | `Enable Audio Streaming Channel` | `Include sound in stream (Speakers or Microphone)` |
| **Audio Meter** | `System Sound Level: {db} dB (Active)` | `Sound Volume: {db} dB` / `Sound Volume: Muted` |
| **Audio Test** | `Play Chime Test` | `🔔 Test Speakers` |
| **Audio Dialog Button** | `Open Audio Diagnostics` | `⚙ Audio Device Setup & Test` |
| **Capture Card** | `Video Capture Source` | `Screen & Window Selection` |
| **Capture Label** | `Source:` | `Share Display / Window:` |
| **Cursor Option** | `Show Mouse Cursor (Renders hardware pointer...)` | `Show mouse cursor on stream` |
| **Cursor Status** | `Hardware Cursor: Visible` / `Hidden` | `Mouse Pointer: Visible` / `Hidden` |
| **Preview Badge** | `● REC` (ScreenTask shares screen, does not record) | `● LIVE` / `● IDLE` |
| **Preview Placeholder** | `Capturing Stream...` | `Sharing Screen...` / `Ready to Share` |
| **Quality Card** | `Compression & Framerate` | `Video Smoothness & Quality` |
| **Framerate Label** | `Target Framerate:` | `Framerate (Smoothness):` |
| **Quality Slider** | `JPEG Compression Quality: {val}%` | `Image Quality: {val}% (High Detail)` |
| **Network Card** | `Network Interface Adapter` | `Network Connection` |
| **Network Label** | `Network Adapter:` | `Network Connection (IP Address):` |
| **Port Status** | `✓ Port 7070 Ready for WebSockets` | `✓ Port 7070 is available for streaming` |
| **Security Card** | `PIN Session Protection` | `Access Security & PIN` |
| **PIN Status** | `PIN: Open` | `PIN: Not Required` |
| **PIN Action** | `🎲 Regenerate` | `🎲 New PIN` |
| **Preferences Card** | `Startup & Behavior Preferences` | `Startup & Window Preferences` |
| **Log Card** | `Diagnostic Event Log` | `Activity & Connection Log` |
| **Mini Bar Expand** | `🗖 Full App` | `🗖 Full View` |
| **Metric: Viewers** | `Viewers` | `Connected Viewers` |
| **Metric: Framerate** | `Framerate` | `Smoothness (FPS)` |
| **Metric: Data** | `Data Sent` | `Data Transferred` |
| **Metric: Uptime** | `Uptime` | `Stream Time` |
| **QR Code Window** | `ScreenTask 2.0 – Pair Device` | `ScreenTask 2.0 – Connect via QR Code` |
| **QR Code Header** | `📱 Scan QR Code` | `📱 Scan with Phone or Tablet` |
| **Audio Dialog Title**| `Audio Diagnostics & Loopback Monitor (Zoom Style)` | `ScreenTask 2.0 – Audio Setup & Testing` |

---

## 6. Automated Testing & Verification Suite

The repository features an automated xUnit test suite (`ScreenTask.Tests`) covering unit logic, embedded server integration, and STA-threaded WinForms UI behavior:

```
ScreenTask.Tests/
├── Core/
│   ├── AppSettingsTests.cs           # XML settings persistence & defaults
│   ├── NetworkHelperTests.cs         # Adapter filtering, IPv4 sorting, loopback
│   ├── QrCodeHelperTests.cs          # Vector QR generation & PNG validity
│   ├── StreamBroadcasterTests.cs     # Client tracking, FPS calc, drop-frame logic
│   └── WasapiAudioCaptureTests.cs    # Device enumeration & peak volume calculation
├── Integration/
│   └── EmbeddedWebServerIntegrationTests.cs
│       ├── HTTP endpoints (/, /viewer.css, /viewer.js, /api/status, /api/qrcode)
│       ├── WebSocket video channel (/stream/ws) binary header & JPEG integrity
│       ├── WebSocket audio channel (/stream/audio) format & PCM chunk verification
│       ├── PIN security validation, 401/403 rejection & PolicyViolation disconnect
│       └── Node.js static syntax validation of viewer.js
└── UI/
    ├── MainFormUiTests.cs            # Form sizing, control lookup, navigation tabs
    └── AudioDiagnosticsFormUiTests.cs # Audio dialog controls, timers, device list
```

### Verification Results
* **Test Suite**: `dotnet test` -> **46 / 46 Passed (100% success rate)**.
* **Release Build**: `dotnet build -c Release` -> **0 Warnings, 0 Errors**.
* **Code Formatting**: `dotnet format --verify-no-changes` -> **Clean, 0 format changes**.
* **JavaScript Syntax**: `node -c ScreenTask/WebServer/viewer.js` -> **Valid ES6 syntax**.

---

## 7. Progress & Milestone Tracking

| Milestone / Task | Status | Details |
| :--- | :---: | :--- |
| **.NET 9 & C# 13 Upgrade** | ✅ Completed | Fully migrated from .NET Framework 4.8; single-file publish ready. |
| **Kestrel Embedded Server** | ✅ Completed | Embedded ASP.NET Core server hosting WebSockets & static viewer. |
| **Dual-Channel WebSockets** | ✅ Completed | Separate `/stream/ws` (JPEG) and `/stream/audio` (PCM) channels. |
| **NAudio 3 WASAPI Capture** | ✅ Completed | Loopback & microphone audio streaming with peak volume calculation. |
| **HTML5/CSS3 Web Viewer 2.0**| ✅ Completed | Canvas rendering, Web Audio player, pinch-zoom, HUD overlay, toasts. |
| **Connected Viewers & Fleet**| ✅ Completed | Live concurrency gauge, ping RTT latency, mute/kick, and announcements. |
| **Component UserControl Architecture**| ✅ Completed | Extracted 5 decoupled views with 100% Visual Studio Designer support. |
| **Zero-Disk In-Memory Compression** | ✅ Completed | Replaced physical JPEG file writing with `ArrayPool<byte>` memory buffers. |
| **Display & Window Capture** | ✅ Completed | GDI32 `BitBlt` for screens and User32 `PrintWindow` for windows. |
| **Vertical Side Navigation** | ✅ Completed | Native vertical sidebar with branding, active indicators, and 5 tab groups. |
| **Native Windows Theming** | ✅ Completed | Replaced custom colors with native system colors and solid borders. |
| **Native Control Standardization** | ✅ Completed | Removed redundant Fluent wrapper controls; standardized on native WinForms controls. |
| **Terminology Harmonization**| ✅ Completed | Removed developer jargon across all controls, forms, dialogs, and tooltips. |
| **Automated Test Suite (46 tests)** | ✅ Completed | Unit, integration, and STA UI tests passing with 100% coverage. |
| **DirectX Desktop Duplication (DXGI)** | ⏳ Future | Planned GPU-accelerated capture pipeline for ultra-low CPU overhead. |
| **Opus Audio Compression** | ⏳ Future | Planned optional audio codec for ultra-low bandwidth WAN streaming. |
| **GitHub Actions CI/CD** | ⏳ Future | Automated Windows test and release publish workflow. |

---

## 8. Summary of Active Files

* `ScreenTask/frmMain.cs` - Main desktop form orchestrator, navigation dispatcher, and event forwarder.
* `ScreenTask/frmMain.Designer.cs` - Lightweight WinForms shell (293 lines) hosting navigation and content container.
* `ScreenTask/UI/Views/` - Decoupled page UserControls with full Visual Studio Designer support:
  * `BroadcastView.cs` & `.Designer.cs` - Monitor preview, broadcast controls, URL card, and telemetry chips.
  * `ConnectedViewersView.cs` & `.Designer.cs` - Live concurrency gauges, session plate cards, and pairing pass.
  * `CaptureSettingsView.cs` & `.Designer.cs` - Video source selection, audio devices, framerate, and quality.
  * `NetworkSettingsView.cs` & `.Designer.cs` - Network adapter enumeration, port binding, and PIN session security.
  * `PreferencesView.cs` & `.Designer.cs` - Startup preferences, tray minimization, and activity log.
* `ScreenTask/UI/Controls/MonitorPreviewControl.cs` & `.Designer.cs` - Composite UserControl confidence monitor (native PictureBox + Labels with zero custom GDI+ drawing).
* `ScreenTask/UI/QrCodeForm.cs` - Secondary dialog for instant smartphone pairing via QR code camera scan.
* `ScreenTask/UI/AudioDiagnosticsForm.cs` - Secondary dialog for microphone testing and device diagnostics.
* `ScreenTask/WebServer/viewer.js` - Client-side WebSocket receiver, Canvas renderer, Web Audio player, and telemetry reporter.
* `ScreenTask/WebServer/viewer.css` - Responsive mobile-first stylesheet with simulated fullscreen and floating HUD.
* `ScreenTask/WebServer/index.html` - Minimal semantic viewer shell.
* `ScreenTask.Tests/` - Full automated test suite verifying core engines, Kestrel endpoints, and UI controls.
