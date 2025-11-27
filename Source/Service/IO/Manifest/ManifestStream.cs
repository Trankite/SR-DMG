using StarRailDamage.Source.Extension;
using System.IO;

namespace StarRailDamage.Source.Service.IO.Manifest
{
    public static class ManifestStream
    {
        public static Stream GetStream(string path)
        {
            return ManifestFinder.Assembly.GetManifestResourceStream(path).ThrowIfNull();
        }

        public static Stream FindAndGetStream(string separator)
        {
            return GetStream(ManifestFinder.Find(separator));
        }

        public static Stream FindAndGetStream(Func<string, bool> predicate)
        {
            return GetStream(ManifestFinder.Find(predicate));
        }
    }
}