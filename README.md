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
