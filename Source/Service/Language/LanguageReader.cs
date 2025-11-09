using StarRailDamage.Source.Core.Language;
using StarRailDamage.Source.Extension.Language;
using StarRailDamage.Source.Model.Text;
using StarRailDamage.Source.Service.IO.CommaSeparated;

namespace StarRailDamage.Source.Service.Language
{
    public static class LanguageReader
    {
        public static void Load(string language = LanguageManager.DefaultLanguage)
        {
            Load(language, nameof(FixedText), FixedTextExtension.FixedTextMap);
            Load(language, nameof(UnFixedText), UnFixedTextExtension.UnFixedTextMap);
            LanguageManager.Language = language;
        }

        private static void Load<T>(string language, string name, Dictionary<T, TextBinding> languageMap) where T : Enum
        {
            using CommaSeparatedReader Reader = new(LanguageManager.GetPath(language, name));
            Dictionary<string, T> EnumDictionary = Enum.GetNames(typeof(T)).ToDictionary(name => name, name => (T)Enum.Parse(typeof(T), name));
            foreach (string[]? Line in Reader)
            {
                if (Line == null || Line.Length < 2) continue;
                if (EnumDictionary.TryGetValue(Line[0], out T? Enum))
                {
                    if (string.IsNullOrEmpty(Line[1])) continue;
                    if (languageMap.TryGetValue(Enum, out TextBinding? TextBinding))
                    {
                        TextBinding.Text = Line[1];
                    }
                    else
                    {
                        languageMap[Enum] = new() { Text = Line[1] };
                    }
                }
            }
        }
    }
}