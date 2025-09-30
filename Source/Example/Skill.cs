namespace SR_DMG.Source.Example
{
    public class Skill : FileCollection.FileInfo
    {
        public string Name { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public List<string> Tags { get; set; } = [];

        public List<string> Gains { get; set; } = [];

        public List<List<string>> Values { get; set; } = [];
    }
}