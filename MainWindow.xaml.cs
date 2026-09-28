using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using ClaudeCreditsWidget.Models;
using ClaudeCreditsWidget.Services;

namespace ClaudeCreditsWidget;

public partial class MainWindow : Window
{
    private readonly StorageService _storage = new();
    private readonly ObservableCollection<AccountEntry> _accounts = new();
    private readonly DispatcherTimer _tickTimer;
    private WidgetSettings _settings;

    public MainWindow()
    {
        InitializeComponent();

        _settings = _storage.LoadSettings();
        ApplySettings(_settings);

        foreach (var account in _storage.Load())
            _accounts.Add(account);

        AccountsList.ItemsSource = _accounts;

        _tickTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _tickTimer.Tick += (_, _) => TickAll();
        _tickTimer.Start();

        Closing += (_, _) => _storage.Save(_accounts);
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);

        var hwnd = new WindowInteropHelper(this).Handle;

        // Tool window: no Alt-Tab entry. No-activate: clicking the widget never pulls it forward.
        var ex = Native.GetWindowLong(hwnd, Native.GWL_EXSTYLE);
        Native.SetWindowLong(hwnd, Native.GWL_EXSTYLE, ex | Native.WS_EX_TOOLWINDOW | Native.WS_EX_NOACTIVATE);

        // Keep the widget at the very bottom of the z-order, always.
        HwndSource.FromHwnd(hwnd)?.AddHook(WndProc);
        Native.SetWindowPos(hwnd, Native.HWND_BOTTOM, 0, 0, 0, 0,
            Native.SWP_NOMOVE | Native.SWP_NOSIZE | Native.SWP_NOACTIVATE);
    }

    private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        if (msg == Native.WM_WINDOWPOSCHANGING)
        {
            var pos = Marshal.PtrToStructure<Native.WINDOWPOS>(lParam);
            if ((pos.flags & Native.SWP_NOZORDER) == 0)
            {
                pos.hwndInsertAfter = Native.HWND_BOTTOM;
                Marshal.StructureToPtr(pos, lParam, false);
            }
        }

        return IntPtr.Zero;
    }

    private void ApplySettings(WidgetSettings settings)
    {
        Left = settings.Left;
        Top = settings.Top;
        Width = settings.Width;
        Height = settings.Height;
        Opacity = settings.Opacity;
    }

    private void TickAll()
    {
        foreach (var account in _accounts)
            account.Refresh();
    }

    public void OpenAddAccountDialog()
    {
        var dialog = new AddAccountWindow();
        if (dialog.ShowDialog() != true)
            return;

        if (_accounts.Any(a => string.Equals(a.Email, dialog.Email, StringComparison.OrdinalIgnoreCase)))
        {
            ThemedMessage.Show(null, "Duplicate account", "That account is already in the list.");
            return;
        }

        var account = new AccountEntry { Email = dialog.Email };
        if (dialog.AvailableAt.HasValue)
            account.ResetAt = dialog.AvailableAt.Value;

        _accounts.Add(account);
        _storage.Save(_accounts);
    }

    public void OpenSettingsDialog()
    {
        var dialog = new SettingsWindow(_settings, ApplySettings);
        if (dialog.ShowDialog() != true)
            return;

        _settings = dialog.Result;
        ApplySettings(_settings);
        _storage.SaveSettings(_settings);
    }

    private void AddAccount_Click(object sender, RoutedEventArgs e)
    {
        OpenAddAccountDialog();
    }

    private void StartTimer_Click(object sender, RoutedEventArgs e)
    {
        if (((Button)sender).Tag is not AccountEntry account)
            return;

        var dialog = new StartTimerWindow(account.Email);
        if (dialog.ShowDialog() != true)
            return;

        account.ResetAt = dialog.AvailableAt;
        _storage.Save(_accounts);
    }

    private void Remove_Click(object sender, RoutedEventArgs e)
    {
        if (((Button)sender).Tag is not AccountEntry account)
            return;

        if (!ThemedMessage.Show(null, "Remove account", $"Remove {account.Email}?", "Remove", showCancel: true))
            return;

        _accounts.Remove(account);
        _storage.Save(_accounts);
    }
}
