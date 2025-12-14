using StarRailDamage.Source.Core.Language;
using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Service.IO.CommaSeparated;

namespace StarRailDamage.Source.Service.Language
{
    public static class LanguageWriter
    {
        public static void Create(string language)
        {
            string FilePath = LanguageManager.GetPath(language);
            using CommaSeparatedWriter Writer = new(FilePath.BuildFile());
            foreach (string EnumName in Enum.GetNames<FixedText>())
            {
                Writer.WriteLine(EnumName);
            }
        }
    }
}