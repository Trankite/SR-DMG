namespace SR_DMG.Source.Example
{
    public class Role
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Element { get; set; } = string.Empty;
        public int Star { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Denfense { get; set; }
        public int Speed { get; set; }
        public int Energy { get; set; }
        public List<Skill> Trace { get; set; } = [];
        public List<Skill> Ranks { get; set; } = [];
    }
}