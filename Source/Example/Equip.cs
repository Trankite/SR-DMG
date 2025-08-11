namespace SR_DMG.Source.Example
{
    /// <summary>
    /// 光锥
    /// </summary>
    internal class Equip
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Star { get; set; }
        public int HP { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public Skill Skill { get; set; } = new();
    }
}