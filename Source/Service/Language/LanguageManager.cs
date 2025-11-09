using StarRailDamage.Source.Core.Language;
using StarRailDamage.Source.Core.Setting;
using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Service.IO.CommaSeparated;
using System.IO;

namespace StarRailDamage.Source.Service.Language
{
    public static class LanguageManager
    {
        public const string DefaultLanguage = "zh-cn";

        public static string Language { get; set; } = DefaultLanguage;

        public static List<(string Language, string Text)> GetLanguages()
        {
            DirectoryInfo DirectoryInfo = new(GetFolder());
            List<(string Language, string Text)> Languages = [];
            foreach (DirectoryInfo DirectoryItem in DirectoryInfo.GetDirectories())
            {
                string Language = DirectoryItem.Name;
                string FixedTextPath = GetFixedTextPath(Language);
                if (!File.Exists(FixedTextPath) || !File.Exists(GetUnFixedTextPath(Language))) continue;
                using CommaSeparatedReader Reader = new(FixedTextPath);
                string? Text = null;
                if (Reader.Success)
                {
                    Text = Reader.FirstOrDefault(x => x?.FirstOrDefault() == nameof(FixedText.Language)).Index(1);
                }
                Languages.Add((Language, Text.UseDefault(string.IsNullOrEmpty, "???")));
            }
            return Languages;
        }

        public static string GetFixedTextPath(string language)
        {
            return GetPath(language, nameof(FixedText));
        }

        public static string GetUnFixedTextPath(string language)
        {
            return GetPath(language, nameof(UnFixedText));
        }

        public static string GetPath(string language, string name)
        {
            return Path.Combine(GetFolder(), language, name + ".csv");
        }

        public static string GetFolder()
        {
            return Path.Combine(LocalSetting.LocalPath, "Language");
        }
    }
}