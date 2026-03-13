using StarRailDamage.Source.Extension;
using System.Security.Principal;
using System.Windows;
using System.Windows.Media;

namespace StarRailDamage.Source.Core.Setting
{
    public static class AppSetting
    {
        public const string AppName = "StarRailDamage";

        public static readonly string UserSid;

        public static readonly double PixelsPerDip;

        static AppSetting()
        {
            SecurityIdentifier? User = WindowsIdentity.GetCurrent().User;
            UserSid = User.IsNotNull() ? User.ToString().LastSplit('-').Content.ToString() : Guid.NewGuid().ToString();
            PixelsPerDip = !Program.IsDesignMode && Application.Current.IsNotNull() ? VisualTreeHelper.GetDpi(Application.Current.MainWindow).PixelsPerDip : 1;
        }
    }
}