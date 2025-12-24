using StarRailDamage.Source.Core.Language;
using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.Text;
using StarRailDamage.Source.Service.IO.CommaSeparated;

namespace StarRailDamage.Source.Service.Language
{
    public static class LanguageReader
    {
        public static int Load(string language)
        {
            int LoadedCount = 0;
            using CommaSeparatedReader Reader = new(LanguageManager.GetPath(language));
            if (!Reader.Success) return -1;
            Dictionary<string, FixedText> FixedTextMap = GetFixedTextMap();
            foreach (string[]? Line in Reader)
            {
                if (Line.IsNull() || Line.Length < 2) continue;
                if (FixedTextMap.TryGetValue(Line[0], out FixedText FixedText))
                {
                    if (AppendFixedText(Line[0], Line[1])) LoadedCount++;
                }
            }
            return FixedTextMap.Count - LoadedCount;
        }

        private static Dictionary<string, FixedText> GetFixedTextMap()
        {
            Dictionary<string, FixedText> FixedTextMap = [];
            foreach (FixedText FixedText in Enum.GetValues<FixedText>())
            {
                FixedTextMap[FixedText.ToString()] = FixedText;
            }
            return FixedTextMap;
        }

        private static bool AppendFixedText(string target, string? text)
        {
            if (string.IsNullOrEmpty(text)) return false;
            if (FixedTextExtension.FixedTextMap.TryGetValue(target, out TextBinding? TextBinding))
            {
                TextBinding.Text = text;
            }
            else
            {
                FixedTextExtension.FixedTextMap[target] = new TextBinding(text);
            }
            return true;
        }
    }
}