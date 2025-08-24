using SR_DMG.Source.Employ;

namespace SR_DMG.Source.Example
{
    /// <summary>
    /// 侵蚀隧洞 / 位面饰品
    /// </summary>
    public class Relic : Thinkan.ISkill
    {
        public string Name { get; set; } = string.Empty;
        public string Tpye { get; set; } = string.Empty;
        public List<Skill> Skills { get; set; } = [];
    }
}