using StarRailDamage.Source.Core.Language;
using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.Text;
using StarRailDamage.Source.Service.IO.AppManifest;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Media.Imaging;

namespace StarRailDamage.Source.Core.Character.Element
{
    public static class CharacterElementExtension
    {
        private static readonly Dictionary<string, CharacterElement> CharacterElementMap = [];

        private static readonly Dictionary<CharacterElement, CharacterElementModel> CharacterElementModelMap = [];

        public static bool TryGetElement(string key, [NotNullWhen(true)] out CharacterElement characterElement)
        {
            return CharacterElementMap.TryGetValue(key, out characterElement);
        }

        public static CharacterElementModel GetModel(this CharacterElement characterElement)
        {
            if (!CharacterElementModelMap.TryGetValue(characterElement, out CharacterElementModel? Model))
            {
                return CharacterElementModelMap.GetValueOrDefault(CharacterElement.Physical).ThrowIfNull();
            }
            return Model;
        }

        private static FixedText GetFullNameFixedText(CharacterElement characterElement)
        {
            return Enum.TryParse(characterElement.ToString() + "Element", out FixedText fixedText) ? fixedText : FixedText.PhysicalElement;
        }

        private static FixedText GetBreakNameFixedText(CharacterElement characterElement)
        {
            return Enum.TryParse(characterElement.ToString() + "DelayedDamage", out FixedText fixedText) ? fixedText : FixedText.PhysicalDelayedDamage;
        }

        private static BitmapImage GetBitmapImage(CharacterElement characterAttribute, string prefix)
        {
            using Stream Stream = AppManifestStream.FindAndGetStream($"Element_{prefix}_{characterAttribute}");
            return BitmapImageExtension.GetBitmapImage(Stream);
        }

        static CharacterElementExtension()
        {
            foreach (CharacterElement CharacterElement in Enum.GetValues<CharacterElement>())
            {
                TextBinding FullName = GetFullNameFixedText(CharacterElement).Binding();
                CharacterElementMap[FullName.Text] = CharacterElement;
                CharacterElementModelMap[CharacterElement] = new CharacterElementModel(FullName, GetBreakNameFixedText(CharacterElement).Binding(), GetBitmapImage(CharacterElement, "Icon"), GetBitmapImage(CharacterElement, "Damage"), GetBitmapImage(CharacterElement, "Resistance"));
            }
        }
    }
}