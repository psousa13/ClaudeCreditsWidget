using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using ClaudeCreditsWidget.Models;

namespace ClaudeCreditsWidget;

public partial class SettingsWindow : Window
{
    private readonly WidgetSettings _original;
    private readonly Action<WidgetSettings> _preview;
    private bool _loading = true;
    private bool _syncingOpacity;
    private double _width;
    private double _height;

    public WidgetSettings Result { get; private set; }

    public SettingsWindow(WidgetSettings current, Action<WidgetSettings> preview)
    {
        InitializeComponent();

        _original = current;
        _preview = preview;
        _width = current.Width;
        _height = current.Height;
        Result = current;

        WidthBox.Text = current.Width.ToString(CultureInfo.InvariantCulture);
        HeightBox.Text = current.Height.ToString(CultureInfo.InvariantCulture);

        UpdateRanges();
        XSlider.Value = current.Left;
        YSlider.Value = current.Top;

        var percent = Math.Round(current.Opacity * 100);
        OpacitySlider.Value = percent;
        OpacityBox.Text = percent.ToString(CultureInfo.InvariantCulture);

        _loading = false;
        UpdateLabels();
    }

    private void UpdateRanges()
    {
        var area = SystemParameters.WorkArea;

        XSlider.Minimum = area.Left;
        XSlider.Maximum = Math.Max(area.Left, area.Right - _width);
        YSlider.Minimum = area.Top;
        YSlider.Maximum = Math.Max(area.Top, area.Bottom - _height);
    }

    private void UpdateLabels()
    {
        XLabel.Text = Math.Round(XSlider.Value).ToString(CultureInfo.InvariantCulture);
        YLabel.Text = Math.Round(YSlider.Value).ToString(CultureInfo.InvariantCulture);
    }

    private WidgetSettings BuildCurrent()
    {
        return new WidgetSettings
        {
            Left = Math.Round(XSlider.Value),
            Top = Math.Round(YSlider.Value),
            Width = _width,
            Height = _height,
            Opacity = OpacitySlider.Value / 100.0
        };
    }

    private void PushPreview()
    {
        if (_loading)
            return;

        _preview(BuildCurrent());
    }

    private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (_loading)
            return;

        UpdateLabels();

        if (ReferenceEquals(sender, OpacitySlider) && !_syncingOpacity)
        {
            _syncingOpacity = true;
            OpacityBox.Text = Math.Round(OpacitySlider.Value).ToString(CultureInfo.InvariantCulture);
            _syncingOpacity = false;
        }

        PushPreview();
    }

    private void OpacityBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (_loading || _syncingOpacity)
            return;

        if (!double.TryParse(OpacityBox.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
            return;

        if (value < 0 || value > 100)
            return;

        _syncingOpacity = true;
        OpacitySlider.Value = value;
        _syncingOpacity = false;

        PushPreview();
    }

    private void SizeBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (_loading)
            return;

        if (!double.TryParse(WidthBox.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var width) ||
            !double.TryParse(HeightBox.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var height))
            return;

        if (width < 150 || height < 150)
            return;

        _width = width;
        _height = height;

        _loading = true;
        UpdateRanges();
        _loading = false;

        UpdateLabels();
        PushPreview();
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        if (!double.TryParse(WidthBox.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var width) ||
            !double.TryParse(HeightBox.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var height))
        {
            ThemedMessage.Show(this, "Invalid input", "Width and height must be valid numbers.");
            return;
        }

        if (width < 150 || height < 150)
        {
            ThemedMessage.Show(this, "Invalid input", "Width and height must be at least 150.");
            return;
        }

        if (!double.TryParse(OpacityBox.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var opacity) ||
            opacity < 0 || opacity > 100)
        {
            ThemedMessage.Show(this, "Invalid input", "Opacity must be a number between 0 and 100.");
            return;
        }

        Result = new WidgetSettings
        {
            Left = Math.Round(XSlider.Value),
            Top = Math.Round(YSlider.Value),
            Width = width,
            Height = height,
            Opacity = opacity / 100.0
        };

        DialogResult = true;
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        base.OnClosing(e);

        if (DialogResult != true)
            _preview(_original);
    }
}
