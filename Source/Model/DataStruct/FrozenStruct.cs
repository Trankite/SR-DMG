namespace StarRailDamage.Source.Model.DataStruct
{
    public class FrozenStruct<TContent, TExtend>
    {
        public TContent Content { get; }

        public TExtend Extend { get; }

        public FrozenStruct(TContent content, TExtend extend)
        {
            Content = content;
            Extend = extend;
        }
    }
}