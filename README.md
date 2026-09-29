# ✻ Claude Credits Widget

A fixed-position desktop widget for Windows that tracks your Claude accounts, each with its own live countdown to when credits should be back.

> Unofficial fan project. Not affiliated with or endorsed by Anthropic.

## Install

### 1. Clone the repo

```powershell
git clone https://github.com/<you>/<repo>.git
cd <repo>\ClaudeCreditsWidget
```

### 2. Install .NET 8 SDK

Download and install the **.NET 8 SDK** from https://dotnet.microsoft.com/download/dotnet/8.0

Pick the installer that matches your PC (x64, x86 or Arm64). Then open a new PowerShell window and check it worked:

```powershell
dotnet --version
```

It should print `8.x.x`.

### 3. Compile the app

Run the command that matches your machine from inside the `ClaudeCreditsWidget` folder.

**Intel / AMD 64-bit (most PCs)**
```powershell
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

**Intel / AMD 32-bit**
```powershell
dotnet publish -c Release -r win-x86 --self-contained true -p:PublishSingleFile=true
```

**ARM (Snapdragon, Surface Pro X, Windows on ARM)**
```powershell
dotnet publish -c Release -r win-arm64 --self-contained true -p:PublishSingleFile=true
```

Not sure which one you have? Open **Settings → System → About** and check **System type**.

### 4. Find the app

Open the publish folder that matches the command you ran (swap `win-x64` for `win-x86` or `win-arm64`):

```
ClaudeCreditsWidget\bin\Release\net8.0-windows\win-x64\publish\
```

`ClaudeCreditsWidget.exe` is the app. Leave it in that folder.

### 5. Make a shortcut

Right-click `ClaudeCreditsWidget.exe` → **Show more options** → **Create shortcut** (on Windows 10: **Create shortcut**).

### 6. Put the shortcut wherever you want

Move it to the Desktop, the Start menu, or the Startup folder to run at login. To open the Startup folder, press `Win + R`, type `shell:startup` and press Enter.

## Using it

Look for the ✻ icon near the clock (click **^** if it's hidden). Right-click it to add accounts, change the widget's position, size and opacity, or exit.

## Note

I made this project just because it was something I needed. It was largely coded by AI (vibe coding), so expect rough edges. Issues and suggestions are welcome.<div align="center">

<img src="docs/banner.svg" alt="Claude Credits Widget" width="100%"/>

**A fixed-position desktop widget that tracks every Claude account you use, each with its own live countdown to when credits should be back.**

![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![WPF](https://img.shields.io/badge/UI-WPF-D97757?style=flat-square)
![Platform](https://img.shields.io/badge/Windows-10%20%7C%2011-0078D4?style=flat-square&logo=windows11&logoColor=white)
![Arch](https://img.shields.io/badge/arch-x64%20%7C%20x86%20%7C%20ARM64-262624?style=flat-square)
![License](https://img.shields.io/badge/license-MIT-86B37A?style=flat-square)

[Download](../../releases) · [Build from source](#-build-from-source) · [Usage](#-usage)

</div>

---

## ✻ Overview

Running out of Claude usage on one account and switching to another gets confusing fast. **Claude Credits Widget** keeps a small, always-there list of your accounts on your desktop. Enter the time an account will be usable again and it counts down for you. When the timer hits zero the entry turns green.

<div align="center">
<img src="docs/preview.svg" alt="Widget preview" width="640"/>
</div>

## ✨ Features

| | |
|---|---|
| 🪟 **True desktop widget** | No title bar, borders or close button. It stays behind all other windows and is hidden from the taskbar and Alt-Tab. |
| ⏱️ **Independent timers** | One live countdown per account (`2h 14m 09s remaining`), turning green with *Credits should be back* at zero. |
| 🕓 **Clock-time input** | Type a 24h time like `16:00` and the app works out the remaining time. If that time has already passed today, it rolls over to tomorrow. |
| 🎛️ **Tray-controlled** | Add accounts, adjust settings, or exit from the notification-area icon. |
| 📐 **Exact placement** | Set X/Y position, width, height and opacity (0.1–1.0) in a settings dialog. Changes apply immediately. |
| 💾 **Persistent** | Accounts and layout are saved automatically and restored on launch. |
| 🎨 **Claude-inspired dark theme** | Warm charcoal panels and terracotta accents, with custom-styled dialogs, sliders and scrollbars. |

## 📥 Install

1. Grab the zip for your machine from the [**Releases**](../../releases) page (see the table below).
2. Unzip it and run `ClaudeCreditsWidget.exe`. No installer and no .NET runtime needed.
3. Look for the ✻ icon near the clock (click **^** if it's hidden).

| Your PC | Download |
|---|---|
| Intel / AMD 64-bit (most PCs) | `ClaudeCreditsWidget-win-x64.zip` |
| Intel / AMD 32-bit | `ClaudeCreditsWidget-win-x86.zip` |
| ARM (Snapdragon, Surface Pro X / 11, Windows on ARM VMs) | `ClaudeCreditsWidget-win-arm64.zip` |

> Not sure which one you have? Open **Settings → System → About** and read **System type**.

## 🕹️ Usage

1. **Add Account**: use the button on the widget or the tray menu. Enter the email and, optionally, the time it will be available (24h, e.g. `16:00`).
2. **Start/Reset Timer**: (re)set an account's available-at time at any time.
3. **Remove**: deletes an account after confirmation.
4. **Tray icon → Widget Settings**: move, resize and fade the widget.
5. **Tray icon → Exit**: the only way to close the app.

Run at login: press `Win + R`, enter `shell:startup`, and drop a shortcut to the `.exe` in that folder.

Data lives in `%AppData%\ClaudeCreditsWidget\` (`accounts.json`, `widget-settings.json`).

> **Heads up:** there is no public API for checking a Claude account's real credit status. The widget counts down to times **you** enter, based on what Claude tells you.

## 🛠️ Build from source

### Prerequisites

- Windows 10 (1607+) or Windows 11. WPF is Windows-only.
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0). Pick the installer that matches your PC (x64, x86 or Arm64). Any of them can cross-compile for the others.

```powershell
git clone https://github.com/<you>/<repo>.git
cd <repo>
```

### Run in development

```powershell
cd ClaudeCreditsWidget
dotnet run
```

### Build for every architecture at once

```powershell
.\build-all.ps1
```

This produces one zip per architecture in `dist\`. If PowerShell blocks the script, run it once with `powershell -ExecutionPolicy Bypass -File .\build-all.ps1`.

### Build for a specific architecture

Run from the project folder (`ClaudeCreditsWidget`). Each command produces a single self-contained `.exe`.

| Target | Command |
|---|---|
| **x64**: Intel/AMD 64-bit | `dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true` |
| **x86**: 32-bit | `dotnet publish -c Release -r win-x86 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true` |
| **ARM64**: Windows on ARM | `dotnet publish -c Release -r win-arm64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true` |

Output path (swap `win-x64` for your target):

```
bin\Release\net8.0-windows\win-x64\publish\ClaudeCreditsWidget.exe
```

### Smaller, framework-dependent builds

If the target machine already has the [.NET 8 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/8.0) installed, use `--self-contained false` for a much smaller file:

```powershell
dotnet publish -c Release -r win-x64 --self-contained false -p:PublishSingleFile=true
```

### Architecture notes

- **32-bit ARM (`win-arm`) is not supported.** .NET 8 doesn't provide a WPF runtime for it, and Windows on ARM devices today run ARM64 (which can also run x64 and x86 apps through emulation).
- An x64 build runs on ARM64 Windows 11 through emulation, but the native `win-arm64` build is faster and lighter.
- Build one architecture at a time with `dotnet clean` in between if MSBuild reuses stale output.

### App icon

Place a multi-size icon at `ClaudeCreditsWidget\Assets\app.ico` (16, 20, 24, 32, 48, 64 and 256 px). It is picked up automatically for the tray icon and the `.exe`. To make one from a PNG:

```powershell
magick icon.png -define icon:auto-resize=256,64,48,32,24,20,16 app.ico
```

## 🗂️ Project layout

```
ClaudeCreditsWidget/
├── App.xaml(.cs)            # Theme, shared dialog style, tray icon
├── MainWindow.xaml(.cs)     # The desktop widget
├── AddAccountWindow.*       # Add-account dialog
├── StartTimerWindow.*       # Set/reset timer dialog
├── SettingsWindow.*         # Position / size / opacity
├── MessageWindow.*          # Themed message boxes
├── Models/                  # AccountEntry, WidgetSettings
└── Services/StorageService  # JSON persistence in %AppData%
```

## 🤝 Contributing

Issues and pull requests are welcome. If the app crashes, please attach the `crash.log` and your Windows version and architecture.

## 📄 License

MIT. Add a `LICENSE` file to your repo to match the badge.

<sub>Unofficial fan project. Not affiliated with or endorsed by Anthropic. "Claude" is a trademark of Anthropic.</sub>
