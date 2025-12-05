using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.Text;

namespace StarRailDamage.Source.Core.Language
{
    public static class FixedTextExtension
    {
        public static readonly Dictionary<FixedText, TextBinding> FixedTextMap = [];

        public static TextBinding Binding(this FixedText fixedText)
        {
            if (FixedTextMap.TryGetValue(fixedText, out TextBinding? TextBinding))
            {
                return TextBinding;
            }
            else
            {
                TextBinding = new() { Text = $"Unknown FixedText:{fixedText}" };
                return FixedTextMap[fixedText] = TextBinding;
            }
        }

        public static string Text(this FixedText fixedText)
        {
            return fixedText.Binding().Text;
        }

        public static string Text(this FixedText fixedText, params string[] args)
        {
            return fixedText.Binding().Text.Format(args);
        }
    }
}