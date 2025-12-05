using StarRailDamage.Source.Extension;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Media.Imaging;

namespace StarRailDamage.Source.Core.Caching
{
    public class ImageCache<TKey> where TKey : notnull
    {
        private readonly Dictionary<TKey, BitmapImage> ImageMap = [];

        public void Append(TKey key, Stream stream)
        {
            ImageMap[key] = BitmapImageExtension.GetBitmapImage(stream);
        }

        public bool TryGetImage(TKey key, [NotNullWhen(true)] out BitmapImage? bitmapImage)
        {
            return ImageMap.TryGetValue(key, out bitmapImage);
        }
    }
}