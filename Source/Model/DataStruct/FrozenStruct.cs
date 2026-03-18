namespace StarRailDamage.Source.Model.DataStruct
{
    public readonly struct FrozenStruct<TContent, TExtend>
    {
        public readonly TContent Content;

        public readonly TExtend Extend;

        public FrozenStruct(TContent content, TExtend extend)
        {
            Content = content;
            Extend = extend;
        }
    }

    public static class FrozenStruct
    {
        public static FrozenStruct<TContent, TExtend> Create<TContent, TExtend>(TContent content, TExtend extend)
        {
            return new FrozenStruct<TContent, TExtend>(content, extend);
        }
    }
}