using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.Text;
using System.Diagnostics;

namespace StarRailDamage.Source.Core.Language
{
    public static class FixedTextExtension
    {
        public static readonly Dictionary<string, TextBinding> FixedTextMap = [];

        [DebuggerStepThrough]
        public static TextBinding Binding(this FixedText fixedText) => Binding(fixedText.ToString());

        [DebuggerStepThrough]
        public static TextBinding Binding(string target)
        {
            if (FixedTextMap.TryGetValue(target, out TextBinding? TextBinding))
            {
                return TextBinding;
            }
            else
            {
                return FixedTextMap[target] = new($"Unknown FixedText:{target}");
            }
        }

        [DebuggerStepThrough]
        public static string Text(this FixedText fixedText)
        {
            return fixedText.Binding().Text;
        }

        [DebuggerStepThrough]
        public static string Text(this FixedText fixedText, params string[] args)
        {
            return fixedText.Binding().Text.Format(args);
        }
    }
}