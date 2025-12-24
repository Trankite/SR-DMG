using StarRailDamage.Source.Core.Caching;
using StarRailDamage.Source.Service.IO.Manifest;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Media.Imaging;

namespace StarRailDamage.Source.Model.Metadata.Character.Occupation
{
    public static class CharacterOccupationExtenison
    {
        private static readonly ImageCache<string> ImageCache = new();

        [DebuggerStepThrough]
        public static bool TryGetImage(this CharacterOccupation characterAttribute, [NotNullWhen(true)] out BitmapImage? bitmapImage)
        {
            return TryGetImage(characterAttribute.ToString(), out bitmapImage);
        }

        [DebuggerStepThrough]
        public static bool TryGetImage(string target, [NotNullWhen(true)] out BitmapImage? bitmapImage)
        {
            return ImageCache.TryGetImage(target, out bitmapImage);
        }

        static CharacterOccupationExtenison()
        {
            foreach (string CharacterOccupation in Enum.GetNames<CharacterOccupation>())
            {
                using Stream Stream = AppManifestStream.FindAndGetStream($"Occupation_Icon_{CharacterOccupation}");
                ImageCache.Append(CharacterOccupation, Stream);
            }
        }
    }
}