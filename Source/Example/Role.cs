namespace SR_DMG.Source.Example
{
    /// <summary>
    /// 角色
    /// </summary>
    internal class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Element { get; set; } = string.Empty;
        public int Star { get; set; }
        public int HP { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public int SPD { get; set; }
        public List<Skill> Skills { get; set; } = [];
        public List<Skill> Ranks { get; set; } = [];
    }
}