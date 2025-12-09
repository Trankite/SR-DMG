namespace StarRailDamage.Source.Model.DataStruct
{
    public class FrozenStruct<TName, TValue>(TName name, TValue value)
    {
        public TName Name { get; } = name;

        public TValue Value { get; } = value;
    }
}