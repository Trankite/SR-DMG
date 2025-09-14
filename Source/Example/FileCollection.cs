using System.IO;

namespace SR_DMG.Source.Example
{
    public class FileCollection
    {
        public string Basicpath = string.Empty;

        public List<UrlInfo> Contents = [];

        public class UrlInfo
        {
            public string Url = string.Empty;
            public string Path = string.Empty;
        }

        public void Initialize()
        {
            Directory.CreateDirectory(Basicpath);
            foreach (UrlInfo Item in Contents)
            {
                Item.Path = Path.Combine(Basicpath, Item.Path);
            }
        }
    }
}