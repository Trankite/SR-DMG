using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.Factory.PropertyExpression
{
    public class PropertyExpression<TSender, TValue> : IPropertyExpression<TSender, TValue>
    {
        public Func<TSender, TValue> GetValue { get; init; }

        public Action<TSender, TValue?> SetValue { get; init; }

        public Func<TSender, bool> NullCheck { get; init; }

        public PropertyExpression(Func<TSender, TValue> getter, Action<TSender, TValue?> setter)
        {
            GetValue = getter;
            SetValue = setter;
            NullCheck = (sender) => false;
        }

        public PropertyExpression(Func<TSender, TValue> getter, Action<TSender, TValue?> setter, Func<TSender, bool> nullCheck)
        {
            GetValue = getter;
            SetValue = setter;
            NullCheck = nullCheck;
        }

        public bool TryGetValue(TSender sender, out TValue? value)
        {
            return NullCheck(sender) ? false.With(value = default) : true.With(value = GetValue(sender));
        }

        public bool TrySetValue(TSender sender, TValue? value)
        {
            return !NullCheck(sender) && true.Invoke(() => SetValue(sender, value));
        }
    }
}