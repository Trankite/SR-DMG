namespace SR_DMG.Source.Example
{
    public class Enemy
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public List<string> Weak { get; set; } = [];
        public List<string> Tag { get; set; } = [];
        public List<int> Resistance { get; set; } = [];
        public int Toughness { get; set; }
    }
}