# ✻ Claude Credits Widget

## Introduction

A fixed-position desktop widget for Windows that tracks your Claude accounts, each with its own live countdown to when credits should be back.

- Stays behind all other windows and is hidden from the taskbar and Alt-Tab
- One independent countdown per account, turning green when credits should be back
- Enter a 24h time like `16:00` and the remaining time is worked out for you
- Controlled from the tray icon (position, size, opacity)
- Accounts and layout are saved automatically

> Unofficial fan project. Not affiliated with or endorsed by Anthropic.

## Requirements

- Windows 10 (1607+) or Windows 11
- .NET 8 SDK

**Install the .NET 8 SDK**

1. Download it from https://dotnet.microsoft.com/download/dotnet/8.0
2. Pick the installer that matches your PC (x64, x86 or Arm64).
3. Open a new PowerShell window and check it worked:

```powershell
dotnet --version
```

It should print `8.x.x`.

> Not sure which PC type you have? Open **Settings → System → About** and check **System type**.

## Installation

**1. Clone the repo**

```powershell
git clone https://github.com/<you>/<repo>.git
cd <repo>\ClaudeCreditsWidget
```

**2. Compile the app**

Run the command that matches your machine from inside the `ClaudeCreditsWidget` folder.

| Your PC | Command |
|---|---|
| Intel / AMD 64-bit (most PCs) | `dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true` |
| Intel / AMD 32-bit | `dotnet publish -c Release -r win-x86 --self-contained true -p:PublishSingleFile=true` |
| ARM (Snapdragon, Surface Pro X, Windows on ARM) | `dotnet publish -c Release -r win-arm64 --self-contained true -p:PublishSingleFile=true` |

**3. Find the app**

Open the publish folder (swap `win-x64` for `win-x86` or `win-arm64` if needed):

```
ClaudeCreditsWidget\bin\Release\net8.0-windows\win-x64\publish\
```

`ClaudeCreditsWidget.exe` is the app. Leave it in that folder.

**4. Make a shortcut**

Right-click `ClaudeCreditsWidget.exe` → **Show more options** → **Create shortcut** (on Windows 10: **Create shortcut**).

**5. Place the shortcut**

Move it to the Desktop, the Start menu, or the Startup folder to run at login. To open the Startup folder, press `Win + R`, type `shell:startup` and press Enter.

## Notes

**Using the widget**

- Look for the app icon near the clock (click **^** if it's hidden).
- Right-click it to add accounts, change the widget's position, size and opacity, or exit.
- Exiting from the tray icon is the only way to close the app.

**Good to know**

- There is no public API for checking a Claude account's real credit status. The widget counts down to times **you** enter.
- Data is stored in `%AppData%\ClaudeCreditsWidget\`.
- 32-bit ARM (`win-arm`) is not supported.
- This project was largely coded by AI (vibe coding), so expect rough edges. Issues and suggestions are welcome. If the app crashes, please attach `crash.log` and your Windows version and architecture.

**License:** MIT
