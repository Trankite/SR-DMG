using System.Windows.Controls;

namespace StarRailDamage.Source.Extension
{
    public static class ControlExtension
    {
        public static void SetFocusable(this Control control)
        {
            control.Focusable = true;
            control.FocusVisualStyle = null;
        }
    }
}