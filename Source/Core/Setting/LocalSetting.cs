using System.IO;

namespace StarRailDamage.Source.Core.Setting
{
    internal static class LocalSetting
    {
        public static string LocalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), AppSetting.AppName);
    }
}