using System.IO;

namespace StarRailDamage.Source.Core.Setting
{
    public static class LocalSetting
    {
        public static string LocalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), AppSetting.AppName);
    }
}