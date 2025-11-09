using StarRailDamage.Source.Core.Language;
using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Service.IO.CommaSeparated;

namespace StarRailDamage.Source.Service.Language
{
    public static class LanguageWriter
    {
        public static void Create(string language = LanguageManager.DefaultLanguage)
        {
            Create<FixedText>(language, nameof(FixedText));
            Create<UnFixedText>(language, nameof(UnFixedText));
        }

        private static void Create<T>(string language, string name)
        {
            string FilePath = LanguageManager.GetPath(language, name);
            using CommaSeparatedWriter Writer = new(FilePath.BuildFile());
            foreach (string EnumName in Enum.GetNames(typeof(T)))
            {
                Writer.WriteLine(EnumName);
            }
        }
    }
}