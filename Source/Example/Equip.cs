namespace SR_DMG.Source.Example
{
    public class Equip
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Star { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Denfense { get; set; }
        public List<Skill> Skills { get; set; } = [];
    }
}