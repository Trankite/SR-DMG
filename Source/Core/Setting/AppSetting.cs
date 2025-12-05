using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace StarRailDamage.Source.Core.Setting
{
    public static class AppSetting
    {
        public const string AppName = "SR-DMG";

        public static readonly double PixelsPerDip;

        static AppSetting()
        {
            PixelsPerDip = !DesignerProperties.GetIsInDesignMode(new DependencyObject()) ? VisualTreeHelper.GetDpi(Application.Current.MainWindow).PixelsPerDip : 1;
        }
    }
}