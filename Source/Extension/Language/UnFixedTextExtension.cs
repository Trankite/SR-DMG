using StarRailDamage.Source.Core.Language;
using StarRailDamage.Source.Model.Text;

namespace StarRailDamage.Source.Extension.Language
{
    internal static class UnFixedTextExtension
    {
        public static readonly Dictionary<UnFixedText, TextBinding> UnFixedTextMap = [];

        public static TextBinding Binding(this UnFixedText unFormText)
        {
            if (UnFixedTextMap.TryGetValue(unFormText, out TextBinding? TextBinding))
            {
                return TextBinding;
            }
            else
            {
                TextBinding = new() { Text = $"Unknown UnFixedText:{unFormText}" };
                return UnFixedTextMap[unFormText] = TextBinding;
            }
        }

        public static string Text(this UnFixedText unFormText, params string[] args)
        {
            return unFormText.Binding().Text.Format(args);
        }
    }
}