namespace SR_DMG.Source.Example
{
    /// <summary>
    /// 技能效果
    /// </summary>
    public class Skill
    {
        public List<string> Tag { get; set; } = [];
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public List<List<string>> Values { get; set; } = [];
        public List<string> Gains { get; set; } = [];
    }
}