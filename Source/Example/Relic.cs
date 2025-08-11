namespace SR_DMG.Source.Example
{
    /// <summary>
    /// 侵蚀隧洞 / 位面饰品
    /// </summary>
    internal class Relic
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Tpye { get; set; } = string.Empty;
        public List<Skill> Skills { get; set; } = [];
        public List<string> Gain { get; set; } = [];
    }
}