using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Service.IO.Manifest;
using System.Collections;
using System.IO;
using System.Resources;

namespace StarRailDamage.Source.Service.IO.AppManifest
{
    public static class AppManifestFinder
    {
        private static readonly string[] ApplicationResources;

        public static string Find(string separator)
        {
            return Find(value => value.Contains(separator, StringComparison.OrdinalIgnoreCase));
        }

        public static string Find(Func<string, bool> predicate)
        {
            return ApplicationResources.FirstOrDefault(predicate).ThrowIfNull();
        }

        public static IEnumerable<string> FindAll(Func<string, bool> predicate)
        {
            return ApplicationResources.Where(predicate);
        }

        static AppManifestFinder()
        {
            List<string> Resources = [];
            using Stream Stream = ManifestStream.FindAndGetStream(x => x.EndsWith(".g.resources"));
            using ResourceReader Reader = new(Stream);
            foreach (DictionaryEntry Entry in Reader)
            {
                string? ResourceKey = Entry.Key.ToString();
                if (string.IsNullOrEmpty(ResourceKey)) continue;
                Resources.Add(ResourceKey);
            }
            ApplicationResources = [.. Resources];
        }
    }
}