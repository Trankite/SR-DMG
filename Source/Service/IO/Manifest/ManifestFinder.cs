using StarRailDamage.Source.Extension;
using System.Reflection;

namespace StarRailDamage.Source.Service.IO.Manifest
{
    public static class ManifestFinder
    {
        private static readonly string[] ManifestResources;

        public static readonly Assembly Assembly = Assembly.GetExecutingAssembly();

        public static string Find(string separator)
        {
            return Find(value => value.Contains(separator, StringComparison.OrdinalIgnoreCase));
        }

        public static string Find(Func<string, bool> predicate)
        {
            return ManifestResources.FirstOrDefault(predicate).ThrowIfNull();
        }

        public static IEnumerable<string> FindAll(Func<string, bool> predicate)
        {
            return ManifestResources.Where(predicate);
        }

        static ManifestFinder()
        {
            ManifestResources = Assembly.GetManifestResourceNames();
        }
    }
}