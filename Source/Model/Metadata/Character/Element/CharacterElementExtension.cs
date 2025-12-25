using StarRailDamage.Source.Core.Language;
using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.Text;
using StarRailDamage.Source.Service.IO.Manifest;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;

namespace StarRailDamage.Source.Model.Metadata.Character.Element
{
    public static class CharacterElementExtension
    {
        private static readonly Dictionary<string, CharacterElementModel> ElementMap = [];

        [DebuggerStepThrough]
        public static CharacterElementModel GetModel(this CharacterElement characterElement)
        {
            return GetModel(characterElement.ToString());
        }

        [DebuggerStepThrough]
        public static CharacterElementModel GetModel(string target)
        {
            return ElementMap.GetValueOrDefault(target).ThrowIfNull();
        }

        private static TextBinding GetBreakTextBinding(string characterElement)
        {
            return FixedTextExtension.Binding(characterElement + "DelayedDamage");
        }

        private static BitmapImage GetBitmapImage(string characterAttribute, string prefix)
        {
            using Stream Stream = AppManifestStream.FindAndGetStream($"Element_{prefix}_{characterAttribute}");
            return BitmapImageExtension.GetBitmapImage(Stream);
        }

        static CharacterElementExtension()
        {
            foreach (string CharacterElement in Enum.GetNames<CharacterElement>())
            {
                TextBinding FullName = FixedTextExtension.Binding(CharacterElement);
                ElementMap[CharacterElement] = new CharacterElementModel(FullName, GetBreakTextBinding(CharacterElement), GetBitmapImage(CharacterElement, "Icon"), GetBitmapImage(CharacterElement, "Damage"), GetBitmapImage(CharacterElement, "Resistance"));
            }
        }
    }
}