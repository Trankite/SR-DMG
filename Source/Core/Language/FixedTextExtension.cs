using StarRailDamage.Source.Model.Text;
using System.Diagnostics;
using static StarRailDamage.Source.Core.Language.FixedTextManager;

namespace StarRailDamage.Source.Core.Language
{
    public static class FixedTextExtension
    {
        [DebuggerStepThrough]
        public static TextBinding Binding(this string target)
        {
            if (FixedTextMap.TryGetValue(target, out TextBinding? TextBinding))
            {
                return TextBinding;
            }
            else
            {
                return FixedTextMap[target] = new TextBinding(GetString(target));
            }
        }
    }
}