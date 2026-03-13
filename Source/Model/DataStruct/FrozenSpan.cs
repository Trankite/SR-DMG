namespace StarRailDamage.Source.Model.DataStruct
{
    public readonly ref struct FrozenSpan<TContent, TExtend>
    {
        public ReadOnlySpan<TContent> Content { get; }

        public ReadOnlySpan<TExtend> Extend { get; }

        public FrozenSpan() { }

        public FrozenSpan(ReadOnlySpan<TContent> content, ReadOnlySpan<TExtend> extend)
        {
            Content = content;
            Extend = extend;
        }
    }
}