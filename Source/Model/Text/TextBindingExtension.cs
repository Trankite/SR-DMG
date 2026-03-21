using StarRailDamage.Source.Extension;
using System.Diagnostics;

namespace StarRailDamage.Source.Model.Text
{
    public static class TextBindingExtension
    {
        [DebuggerStepThrough]
        public static string Format(this TextBinding value, params object[] args)
        {
            return StringExtension.Format(value.Text, args);
        }
    }
}