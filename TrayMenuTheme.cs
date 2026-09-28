using System.Drawing;
using System.Windows.Forms;

namespace ClaudeCreditsWidget;

/// <summary>Dark, Claude-coloured skin for the WinForms tray context menu.</summary>
internal sealed class TrayMenuRenderer : ToolStripProfessionalRenderer
{
    private static readonly Color Bg = Color.FromArgb(0x30, 0x30, 0x2E);
    private static readonly Color Hover = Color.FromArgb(0x3A, 0x39, 0x36);
    private static readonly Color Border = Color.FromArgb(0x43, 0x42, 0x3E);
    private static readonly Color Text = Color.FromArgb(0xFA, 0xF9, 0xF5);
    private static readonly Color Accent = Color.FromArgb(0xD9, 0x77, 0x57);

    public TrayMenuRenderer() : base(new Colors()) { RoundedEdges = false; }

    public static ContextMenuStrip Apply(ContextMenuStrip menu)
    {
        menu.Renderer = new TrayMenuRenderer();
        menu.BackColor = Bg;
        menu.ForeColor = Text;
        menu.Font = new Font("Segoe UI", 9f);
        menu.ShowImageMargin = false;
        return menu;
    }

    protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
    {
        e.TextColor = Text;
        base.OnRenderItemText(e);
    }

    private sealed class Colors : ProfessionalColorTable
    {
        public override Color ToolStripDropDownBackground => Bg;
        public override Color ImageMarginGradientBegin => Bg;
        public override Color ImageMarginGradientMiddle => Bg;
        public override Color ImageMarginGradientEnd => Bg;
        public override Color MenuBorder => Border;
        public override Color MenuItemBorder => Accent;
        public override Color MenuItemSelected => Hover;
        public override Color MenuItemSelectedGradientBegin => Hover;
        public override Color MenuItemSelectedGradientEnd => Hover;
        public override Color SeparatorDark => Border;
        public override Color SeparatorLight => Border;
    }
}
