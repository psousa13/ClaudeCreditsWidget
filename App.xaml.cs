using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Application = System.Windows.Application;

namespace ClaudeCreditsWidget;

public partial class App : Application
{
    private NotifyIcon? _trayIcon;
    private MainWindow? _mainWindow;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _mainWindow = new MainWindow();
        _mainWindow.Show();

        var menu = TrayMenuRenderer.Apply(new ContextMenuStrip());
        menu.Items.Add("Add Account", null, (_, _) => _mainWindow.OpenAddAccountDialog());
        menu.Items.Add("Widget Settings", null, (_, _) => _mainWindow.OpenSettingsDialog());
        menu.Items.Add(new ToolStripSeparator());
        menu.Items.Add("Exit", null, (_, _) => Shutdown());

        _trayIcon = new NotifyIcon
        {
            Icon = LoadTrayIcon(),
            Visible = true,
            Text = "Claude Credits Widget",
            ContextMenuStrip = menu
        };
    }

    /// <summary>Uses Assets/app.ico if it was added to the project, otherwise the default Windows icon.</summary>
    private static Icon LoadTrayIcon()
    {
        try
        {
            var info = GetResourceStream(new Uri("pack://application:,,,/Assets/app.ico"));
            if (info != null)
            {
                using var stream = info.Stream;
                return new Icon(stream, SystemInformation.SmallIconSize);
            }
        }
        catch
        {
            // No icon embedded yet - fall through.
        }

        return SystemIcons.Application;
    }

    // Shared dialog behaviour (wired up via the DialogWindow style in App.xaml)
    private void Dialog_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is Window w && e.ButtonState == MouseButtonState.Pressed && e.GetPosition(w).Y <= 40)
            w.DragMove();
    }

    private void Dialog_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key != Key.Escape || sender is not Window w)
            return;

        e.Handled = true;
        try { w.DialogResult = false; }
        catch (InvalidOperationException) { w.Close(); }
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _trayIcon?.Dispose();
        base.OnExit(e);
    }
}
