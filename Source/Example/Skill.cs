namespace SR_DMG.Source.Example
{
    public class Skill
    {
        public string Icon { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = [];
        public List<string> Gains { get; set; } = [];
        public List<List<string>> Values { get; set; } = [];
    }
}