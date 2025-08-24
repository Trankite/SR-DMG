namespace SR_DMG.Source.Example
{
    /// <summary>
    /// 敌对物种
    /// </summary>
    public class Enemy
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public List<string> Weak { get; set; } = [];
        public List<string> Tag { get; set; } = [];
    }
}