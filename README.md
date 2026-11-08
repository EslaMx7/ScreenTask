# ScreenTask 2.0 🖥️⚡

[![Website](https://img.shields.io/badge/Website-screentask.eslamx.com-4f46e5?style=flat-square)](https://screentask.eslamx.com/)
[![GitHub Stars](https://img.shields.io/github/stars/EslaMx7/ScreenTask?style=flat-square&color=yellow)](https://github.com/EslaMx7/ScreenTask/stargazers)
[![Built with .NET](https://img.shields.io/badge/.NET-10.0%20%2F%209.0-512bd4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg?style=flat-square)](https://www.gnu.org/licenses/gpl-3.0.html)

**Free, open-source, and portable screen & audio sharing over local Wi-Fi, Ethernet LAN, or mobile hotspot without internet.** 

Viewers connect instantly via any web browser on desktop, tablet, or smartphone (iOS Safari, Android Chrome)—no client software, browser extensions, or accounts needed.

🌐 **Official Website & Guides**: [https://screentask.eslamx.com/](https://screentask.eslamx.com/)

---

## ✨ What's New in ScreenTask 2.0 (10-Year Anniversary Edition)

ScreenTask 2.0 is a ground-up modernization of the original 2015 utility:

* 🚀 **Real-Time 30–60 FPS Streaming**: Replaced 2 FPS image polling with a high-performance, low-latency binary WebSocket streaming pipeline (<50ms LAN latency).
* 🔊 **System Audio Sharing**: Stream live PC system audio (WASAPI Loopback) synchronized with the video—watch movies, presentations, and games with full sound.
* 🪟 **Window & Application Sharing**: Share individual open applications (e.g. Visual Studio Code, PowerPoint, browser tabs) instead of exposing your entire desktop.
* 📱 **Instant QR Code Pairing**: Desktop app generates a crisp vector QR code—point your phone camera at your PC screen to immediately start viewing.
* 👥 **Connected Viewers & Fleet Management**: Real-time LAN concurrency tracker with capacity gauge, device & browser identification, live RTT ping measurement, active view mode detection (Pinch Zoom, Fullscreen), individual viewer audio mute, disconnect/kick controls, and on-screen broadcast announcements.
* 🧩 **Modular Component Architecture**: Decoupled WinForms `UserControl` views (`BroadcastView`, `ConnectedViewersView`, `CaptureSettingsView`, `NetworkSettingsView`, `PreferencesView`) with full Visual Studio Designer support.
* 💾 **Zero-Disk Architecture**: Video frames and audio chunks are processed entirely in-memory (`ArrayPool<byte>`); zero files are written to SSD/HDD.
* 🔒 **Lock Screen & Sleep Resilience**: Handles Windows lock screen (`Win + L`) and desktop session switching gracefully without freezing or terminating.
* 🎨 **Next-Gen Web Viewer**: Redesigned with the Slate & Dynamic Indigo theme from [screentask.eslamx.com](https://screentask.eslamx.com/), featuring:
  * Fullscreen & pinch-to-zoom (up to 5x) for reading fine text on smartphones.
  * Web Audio API player with volume slider and mute controls.
  * Live stream HUD (FPS counter, resolution, latency).
  * Clean PIN authentication dialog.
* ⚡ **Modern .NET & Standalone Portability**: Upgraded from legacy .NET Framework 4.8 to modern .NET with Single-File publishing.

---

## 📊 Version Comparison

| Feature | ScreenTask 1.x (2015–2022) | ScreenTask 2.0 (2026) |
| :--- | :--- | :--- |
| **Streaming Protocol** | HTTP Polling (`img.src = ...?rand=`) | Binary WebSocket Stream + HTTP Fallback |
| **Frame Rate** | ~2–5 FPS (slideshow) | **30–60 FPS (fluid real-time)** |
| **Latency** | 500ms – 2000ms | **Sub-50ms** |
| **Audio Streaming** | ❌ No | **✅ Yes (WASAPI System Audio)** |
| **Capture Sources** | Full monitors only | **Full monitors + Individual app windows** |
| **Disk I/O** | Constantly writes `ScreenTask.jpg` to disk | **Zero disk writes (100% in RAM)** |
| **Mobile Pairing** | Manual IP & port typing | **Instant QR Code Camera Scan** |
| **Mobile Gestures** | Fixed viewport | **Pinch-to-zoom & two-finger pan** |
| **Host Runtime** | Legacy .NET Framework 4.8 | **Modern .NET 10 / 9 Standalone Single-File** |

---

## 🚀 Getting Started

### 1. Host (Sharing your screen)
1. Download the latest `ScreenTask.exe` from [Releases](https://github.com/EslaMx7/ScreenTask/releases) or [screentask.eslamx.com](https://screentask.eslamx.com/).
2. Launch `ScreenTask.exe` (no installation required).
3. Select your network adapter (Wi-Fi, Hotspot, or Ethernet).
4. Choose your source (Monitor or specific Application Window).
5. Toggle **Stream PC Audio** if you want to include system sound.
6. Click **Start Server**.

### 2. Viewers (Watching the stream)
* **On Mobile**: Scan the QR code shown on the ScreenTask window using your camera app.
* **On Desktop/Laptop**: Open any modern web browser and navigate to the displayed URL (e.g. `http://192.168.1.100:7070`).

---

## 🛠️ Building from Source

### Prerequisites
* Windows 10 (version 1809+) or Windows 11
* [.NET 9.0 or .NET 10.0 SDK](https://dotnet.microsoft.com/download)

### Build Command
```powershell
# Clone repository
git clone https://github.com/EslaMx7/ScreenTask.git
cd ScreenTask

# Build solution
dotnet build ScreenTask/ScreenTask.csproj

# Publish standalone single-file binary
dotnet publish ScreenTask/ScreenTask.csproj -c Release -r win-x64 --self-contained false -p:PublishSingleFile=true
```

The published executable will be available at:
`ScreenTask/bin/Release/net9.0-windows10.0.19041.0/win-x64/publish/ScreenTask.exe`

---

## 📚 Guides & Documentation

Explore step-by-step tutorials on the official website:
* [How to Share PC Screen Over Wi-Fi Without Internet (Mobile Hotspot)](https://screentask.eslamx.com/guides/share-pc-screen-wifi-hotspot-without-internet)
* [Troubleshooting ScreenTask: Fixing Port Conflicts & Firewall](https://screentask.eslamx.com/guides/screentask-troubleshooting-connection-ip-port)
* [How to Stream PC Screen to Phone Browser](https://screentask.eslamx.com/guides/stream-pc-screen-to-phone-tablet-browser)

---

## 📜 License

ScreenTask is released under the **GNU General Public License v3.0 (GPLv3)**. See [LICENSE](LICENSE) for details.
