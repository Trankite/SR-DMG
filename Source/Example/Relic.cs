namespace SR_DMG.Source.Example
{
    public class Relic
    {
        public string Name { get; set; } = string.Empty;
        public string Tpye { get; set; } = string.Empty;
        public Dictionary<string, string> Parts { get; set; } = [];
        public List<Skill> Skills { get; set; } = [];
    }
}