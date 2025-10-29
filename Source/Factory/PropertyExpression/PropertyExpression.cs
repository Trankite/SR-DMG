namespace StarRailDamage.Source.Factory.PropertyExpression
{
    internal class PropertyExpression<TKey, TValue> : IPropertyExpression<TKey, TValue>
    {
        public required Func<TKey, TValue> GetValue { get; init; }

        public required Action<TKey, TValue> SetValue { get; init; }
    }
}