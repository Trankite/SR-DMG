namespace SR_DMG.Source.Example
{
    public class Equip : FileCollection
    {
        public int Star;

        public string Type = string.Empty;

        public string Name = string.Empty;

        public int HP;

        public int ATK;

        public int DEF;

        public FileCollection Skill = new();
    }
}