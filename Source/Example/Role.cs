namespace SR_DMG.Source.Example
{
    public class Role : FileCollection
    {
        public int Star;

        public string Type = string.Empty;

        public string Element = string.Empty;

        public string Name = string.Empty;

        public int HP;

        public int ATK;

        public int DEF;

        public int SPD;

        public int ERG;

        public FileCollection Trace = new();

        public FileCollection Rank = new();
    }
}