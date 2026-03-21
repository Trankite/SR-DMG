namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Note
{
    public class NoteAnalyzedBody
    {
        public int Current { get; set; }

        public int Maximum { get; set; }

        public DateTimeOffset FullTime { get; set; }

        public NoteAnalyzedBody() { }

        public NoteAnalyzedBody(int current, int maximum)
        {
            Current = current;
            Maximum = maximum;
        }

        public NoteAnalyzedBody(int current, int maximum, DateTimeOffset fullTime) : this(current, maximum)
        {
            FullTime = fullTime;
        }
    }
}