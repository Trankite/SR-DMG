using StarRailDamage.Source.Model.Text;
using StarRailDamage.Source.UI.Assets.Lang;
using System.Globalization;

namespace StarRailDamage.Source.Core.Language
{
    public static class FixedTextManager
    {
        public static readonly Dictionary<string, TextBinding> FixedTextMap = [];

        public static CultureInfo Culture
        {
            get => FixedText.Culture;
            set => OnUICultureChanged(value);
        }

        public static void OnUICultureChanged(CultureInfo cultureInfo)
        {
            FixedText.Culture = cultureInfo;
            foreach (KeyValuePair<string, TextBinding> FixedTextPair in FixedTextMap)
            {
                FixedTextPair.Value.Text = GetString(FixedTextPair.Key);
            }
        }

        public static string GetString(string target)
        {
            return FixedText.ResourceManager.GetString(target, Culture) ?? $"Unknown FixedText:{target}";
        }
    }
}