using StarRailDamage.Source.Core.Language;
using StarRailDamage.Source.Core.Setting;
using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Service.IO.CommaSeparated;
using System.IO;

namespace StarRailDamage.Source.Service.Language
{
    public static class LanguageManager
    {
        public const string Default = "zh-cn";

        public static string Current { get; set; } = Default;

        /// <returns>
        /// -1：读取文件时发生错误，否则为缺失的数量。
        /// </returns>
        public static int Load(string language = Default)
        {
            return LanguageReader.Load(language).OutTemp(out int TempValue) && TempValue >= 0 ? TempValue.Configure(Current = language) : TempValue;
        }

        public static Dictionary<string, string> GetLanguages()
        {
            string FolderPath = GetFolder();
            Dictionary<string, string> LanguageMap = [];
            if (!Directory.Exists(FolderPath)) return LanguageMap;
            DirectoryInfo DirectoryInfo = new(FolderPath);
            foreach (DirectoryInfo DirectoryItem in DirectoryInfo.GetDirectories())
            {
                string Language = DirectoryItem.Name;
                string FixedTextPath = GetPath(Language);
                if (!File.Exists(FixedTextPath)) continue;
                using CommaSeparatedReader Reader = new(FixedTextPath);
                string? Text = null;
                if (Reader.Success)
                {
                    Text = Reader.FirstOrDefault(x => x?.FirstOrDefault() == nameof(FixedText.Language)).Index(1);
                }
                LanguageMap[Language] = string.IsNullOrEmpty(Text) ? "???" : Text;
            }
            return LanguageMap;
        }

        public static string GetPath(string language)
        {
            return Path.Combine(GetFolder(), language, nameof(FixedText) + ".csv");
        }

        public static string GetFolder()
        {
            return Path.Combine(LocalSetting.LocalPath, "Language");
        }
    }
}