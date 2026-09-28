using System.Windows;

namespace ClaudeCreditsWidget;

/// <summary>Themed replacement for MessageBox.</summary>
public partial class MessageWindow : Window
{
    public MessageWindow(string title, string message, string okText, bool showCancel)
    {
        InitializeComponent();
        Title = title;
        MessageText.Text = message;
        OkButton.Content = okText;
        CancelButton.Visibility = showCancel ? Visibility.Visible : Visibility.Collapsed;
    }

    private void Ok_Click(object sender, RoutedEventArgs e) => DialogResult = true;

    private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
}

public static class ThemedMessage
{
    /// <returns>true if the primary button was pressed.</returns>
    public static bool Show(Window? owner, string title, string message, string okText = "OK", bool showCancel = false)
    {
        var window = new MessageWindow(title, message, okText, showCancel);
        if (owner != null)
        {
            window.Owner = owner;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        return window.ShowDialog() == true;
    }
}
