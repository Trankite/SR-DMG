namespace StarRailDamage.Source.Factory.PropertyExpression
{
    public interface IPropertyExpression<TKey, TValue>
    {
        Func<TKey, TValue> GetValue { get; init; }

        Action<TKey, TValue> SetValue { get; init; }
    }
}