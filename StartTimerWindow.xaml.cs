using System;
using System.Globalization;
using System.Windows;

namespace ClaudeCreditsWidget;

public partial class StartTimerWindow : Window
{
    private static readonly string[] TimeFormats = { "HH:mm", "H:mm", "HH:mm:ss" };

    public DateTime AvailableAt { get; private set; }

    public StartTimerWindow(string email)
    {
        InitializeComponent();
        EmailLabel.Text = email;
    }

    private void Start_Click(object sender, RoutedEventArgs e)
    {
        var timeText = TimeBox.Text.Trim();
        if (!DateTime.TryParseExact(timeText, TimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed))
        {
            ThemedMessage.Show(this, "Invalid input", "Enter a time like 16:00.");
            return;
        }

        var target = DateTime.Today + parsed.TimeOfDay;
        if (target <= DateTime.Now)
            target = target.AddDays(1);

        AvailableAt = target;
        DialogResult = true;
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}
