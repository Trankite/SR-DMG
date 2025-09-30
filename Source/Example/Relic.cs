namespace SR_DMG.Source.Example
{
    public class Relic : FileCollection
    {
        public string Type = string.Empty;

        public string Name = string.Empty;

        public Dictionary<string, string> Parts = [];

        public FileCollection Skill = new();
    }
}