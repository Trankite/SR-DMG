namespace StarRailDamage.Source.Factory.PropertyExpression
{
    public interface IPropertyExpression<TSender, TValue>
    {
        Func<TSender, TValue> GetValue { get; init; }

        Action<TSender, TValue?> SetValue { get; init; }

        Func<TSender, bool> NullCheck { get; init; }

        bool TryGetValue(TSender sender, out TValue? value);

        bool TrySetValue(TSender sender, TValue? value);
    }
}