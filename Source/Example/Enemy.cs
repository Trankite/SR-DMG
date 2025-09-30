namespace SR_DMG.Source.Example
{
    public class Enemy : FileCollection
    {
        public string Type = string.Empty;

        public string Name = string.Empty;

        public int Toughness;

        public List<string> Weak = [];

        public List<string> Tag = [];
    }
}