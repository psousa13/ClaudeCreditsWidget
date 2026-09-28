using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ClaudeCreditsWidget.Models;

public class AccountEntry : INotifyPropertyChanged
{
    private DateTime? _resetAt;
    private string _email = string.Empty;

    public string Email
    {
        get => _email;
        set { _email = value; OnChanged(nameof(Email)); }
    }

    public DateTime? ResetAt
    {
        get => _resetAt;
        set
        {
            _resetAt = value;
            OnChanged(nameof(ResetAt));
            OnChanged(nameof(StatusText));
            OnChanged(nameof(IsReady));
            OnChanged(nameof(HasTimer));
        }
    }

    [JsonIgnore]
    public bool HasTimer => ResetAt.HasValue;

    [JsonIgnore]
    public bool IsReady => ResetAt.HasValue && ResetAt.Value <= DateTime.Now;

    [JsonIgnore]
    public string StatusText
    {
        get
        {
            if (!ResetAt.HasValue)
                return "No timer set";

            var remaining = ResetAt.Value - DateTime.Now;
            if (remaining <= TimeSpan.Zero)
                return "Credits should be back";

            return $"{(int)remaining.TotalHours}h {remaining.Minutes}m {remaining.Seconds}s remaining";
        }
    }

    public void Refresh()
    {
        OnChanged(nameof(StatusText));
        OnChanged(nameof(IsReady));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
