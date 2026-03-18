namespace StarRailDamage.Source.Model.DataStruct
{
    public readonly ref struct FrozenSpan<TContent, TExtend>
    {
        public readonly ReadOnlySpan<TContent> Content;

        public readonly ReadOnlySpan<TExtend> Extend;

        public FrozenSpan() { }

        public FrozenSpan(ReadOnlySpan<TContent> content, ReadOnlySpan<TExtend> extend)
        {
            Content = content;
            Extend = extend;
        }
    }
}