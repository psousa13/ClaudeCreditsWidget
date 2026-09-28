using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ClaudeCreditsWidget.Models;

namespace ClaudeCreditsWidget.Services;

public class StorageService
{
    private readonly string _accountsPath;
    private readonly string _settingsPath;

    public StorageService()
    {
        var folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "ClaudeCreditsWidget");
        Directory.CreateDirectory(folder);
        _accountsPath = Path.Combine(folder, "accounts.json");
        _settingsPath = Path.Combine(folder, "widget-settings.json");
    }

    public List<AccountEntry> Load()
    {
        if (!File.Exists(_accountsPath))
            return new List<AccountEntry>();

        try
        {
            var json = File.ReadAllText(_accountsPath);
            var list = JsonSerializer.Deserialize<List<AccountEntry>>(json);
            return list ?? new List<AccountEntry>();
        }
        catch
        {
            return new List<AccountEntry>();
        }
    }

    public void Save(IEnumerable<AccountEntry> accounts)
    {
        var json = JsonSerializer.Serialize(accounts, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_accountsPath, json);
    }

    public WidgetSettings LoadSettings()
    {
        if (!File.Exists(_settingsPath))
            return new WidgetSettings();

        try
        {
            var json = File.ReadAllText(_settingsPath);
            return JsonSerializer.Deserialize<WidgetSettings>(json) ?? new WidgetSettings();
        }
        catch
        {
            return new WidgetSettings();
        }
    }

    public void SaveSettings(WidgetSettings settings)
    {
        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_settingsPath, json);
    }
}
