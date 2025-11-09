using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.Factory.PropertyExpression
{
    internal class PropertyExpression<TSender, TValue> : IPropertyExpression<TSender, TValue>
    {
        public required Func<TSender, TValue> GetValue { get; init; }

        public required Action<TSender, TValue?> SetValue { get; init; }

        public required Func<TSender, bool> NullCheck { get; init; }

        public bool TryGetValue(TSender sender, out TValue? value)
        {
            return NullCheck(sender) ? false.Invoke(value = default) : true.Invoke(value = GetValue(sender));
        }

        public bool TrySetValue(TSender sender, TValue? value)
        {
            return !NullCheck(sender) && true.Invoke(() => SetValue(sender, value));
        }
    }
}