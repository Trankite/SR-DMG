using System.IO;
using System.Windows;

namespace StarRailDamage.Source.Service.IO.AppManifest
{
    internal class AppManifestStream
    {
        public static Stream GetStream(string path)
        {
            return Application.GetResourceStream(new Uri(path, UriKind.Relative)).Stream;
        }

        public static Stream FindAndGetStream(string separator)
        {
            return GetStream(AppManifestFinder.Find(separator));
        }

        public static Stream FindAndGetStream(Func<string, bool> predicate)
        {
            return GetStream(AppManifestFinder.Find(predicate));
        }
    }
}