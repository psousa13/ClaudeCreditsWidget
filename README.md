# Claude Credits Widget

A fixed-position desktop widget (.NET 8 WPF) for tracking Claude accounts by email, each with its own independent countdown to when credits should be available again.

## Requirements

1. .NET 8 SDK: https://dotnet.microsoft.com/download/dotnet/8.0
2. Windows 11 on ARM64 (or x64 — both are supported)

## Run it during development

```
cd ClaudeCreditsWidget
dotnet run
```

## Build a standalone ARM64 .exe (no .NET runtime needed on the target machine)

```
cd ClaudeCreditsWidget
dotnet publish -c Release -r win-arm64 --self-contained true -p:PublishSingleFile=true
```

The .exe will be in:
```
bin\Release\net8.0-windows\win-arm64\publish\ClaudeCreditsWidget.exe
```

For an x64 machine instead, swap `win-arm64` for `win-x64`.

## What it is

This behaves like an actual widget, not a normal window:

1. No title bar, no borders, no close button, and it cannot be dragged around.
2. It always opens at the same position and size, which you set once through a settings dialog (see below) — not by clicking and dragging.
3. It lives on the desktop layer: it is always behind every other window (opening a window never puts it in front of them), and it does not show up in the taskbar or Alt-Tab.

Because the widget has no controls of its own besides the account list, everything else (adding accounts, changing position/size, exiting the app) lives in a tray icon.

## Using the tray icon

When the app is running, look for its icon near the clock in the bottom-right of your screen (click the small "^" arrow to show hidden icons if you don't see it). Right-click it for:

1. **Add Account** — same dialog as the button on the widget.
2. **Widget Settings** — set exact position (X/Y), size (width/height), and opacity (0.1–1.0, for a semi-transparent look). Changes apply immediately and are saved.
3. **Exit** — closes the app. This is the only way to close it.

## Using the widget

1. Click "Add Account" (on the widget or from the tray) and enter the email. You can optionally set "Available at" to a clock time (24h, e.g. `16:00`) — the app works out how long that is from now on its own.
2. Each account shows its own live countdown ("Xh Ym Zs remaining"). When it hits zero it turns green and reads "Credits should be back".
3. "Start/Reset Timer" lets you (re)set an account's available-at time the same way.
4. "Remove" deletes an account after confirmation.
5. Data (accounts and widget position/size/opacity) is saved automatically to `%AppData%\ClaudeCreditsWidget\`, so everything persists between launches.

## Notes

- There's no public API to check a Claude account's actual credit status, so this tracks times you set yourself based on what Claude tells you.
- To have it start automatically with Windows, create a shortcut to the published .exe and place it in `shell:startup` (Win+R, paste that, Enter).

## App icon

Put your icon at:

```
ClaudeCreditsWidget\Assets\app.ico
```

(next to `MainWindow.xaml`, in the `Assets` folder). Rebuild/publish and it is picked up automatically for:

1. the tray icon (notification area and the hidden-icons `^` overflow),
2. the `.exe` file in Explorer, Start menu and any shortcuts (including the `shell:startup` one).

The `.ico` should be a multi-size file containing 16, 20, 24, 32, 48, 64 and 256 px images so it stays sharp at every DPI. To make one from a PNG (square, ideally 256x256+):

```
magick icon.png -define icon:auto-resize=256,64,48,32,24,20,16 app.ico
```

Windows caches icons; if the old one still shows, restart Explorer or rename the exe once.
