using StarRailDamage.Source.Core.Caching;
using StarRailDamage.Source.Core.Language;
using StarRailDamage.Source.Service.IO.AppManifest;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Media.Imaging;

namespace StarRailDamage.Source.Core.Character.Occupation
{
    public static class CharacterOccupationExtenison
    {
        private static readonly ImageCache<CharacterOccupation> ImageCache = new();

        private static readonly Dictionary<string, CharacterOccupation> CharacterOccupationMap = [];

        public static bool TryGetImage(this CharacterOccupation characterAttribute, [NotNullWhen(true)] out BitmapImage? bitmapImage)
        {
            return ImageCache.TryGetImage(characterAttribute, out bitmapImage);
        }

        static CharacterOccupationExtenison()
        {
            foreach (CharacterOccupation CharacterOccupation in Enum.GetValues<CharacterOccupation>())
            {
                using (Stream Stream = AppManifestStream.FindAndGetStream($"Occupation_Icon_{CharacterOccupation}"))
                {
                    ImageCache.Append(CharacterOccupation, Stream);
                }
                if (Enum.TryParse(CharacterOccupation.ToString() + "Element", out FixedText FixedText))
                {
                    CharacterOccupationMap[FixedText.Binding().Text] = CharacterOccupation;
                }
            }
        }
    }
}