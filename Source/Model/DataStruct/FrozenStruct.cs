namespace StarRailDamage.Source.Model.DataStruct
{
    public class FrozenStruct<TContent, TExtend>(TContent name, TExtend value)
    {
        public TContent Content { get; } = name;

        public TExtend Extend { get; } = value;
    }
}