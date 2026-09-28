using System;
using System.Globalization;
using System.Windows;

namespace ClaudeCreditsWidget;

public partial class AddAccountWindow : Window
{
    private static readonly string[] TimeFormats = { "HH:mm", "H:mm", "HH:mm:ss" };

    public string Email { get; private set; } = string.Empty;
    public DateTime? AvailableAt { get; private set; }

    public AddAccountWindow()
    {
        InitializeComponent();
    }

    private void Add_Click(object sender, RoutedEventArgs e)
    {
        var email = EmailBox.Text.Trim();
        if (string.IsNullOrEmpty(email))
        {
            ThemedMessage.Show(this, "Missing email", "Enter an email.");
            return;
        }

        Email = email;

        var timeText = TimeBox.Text.Trim();
        if (!string.IsNullOrEmpty(timeText))
        {
            if (!DateTime.TryParseExact(timeText, TimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed))
            {
                ThemedMessage.Show(this, "Invalid input", "Enter a time like 16:00.");
                return;
            }

            var target = DateTime.Today + parsed.TimeOfDay;
            if (target <= DateTime.Now)
                target = target.AddDays(1);

            AvailableAt = target;
        }

        DialogResult = true;
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}
