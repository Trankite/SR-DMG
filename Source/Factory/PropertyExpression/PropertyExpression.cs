using StarRailDamage.Source.Extension;
using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Factory.PropertyExpression
{
    public class PropertyExpression<TValue> : IPropertyExpression<TValue>
    {
        public Func<TValue> GetValue { get; init; }

        public Action<TValue?> SetValue { get; init; }

        public Func<bool> NullCheck { get; init; }

        public PropertyExpression(Func<TValue> getter, Action<TValue?> setter)
        {
            GetValue = getter;
            SetValue = setter;
            NullCheck = () => false;
        }

        public PropertyExpression(Func<TValue> getter, Action<TValue?> setter, Func<bool> nullCheck)
        {
            GetValue = getter;
            SetValue = setter;
            NullCheck = nullCheck;
        }

        public bool TryGetValue([NotNullWhen(true)] out TValue? value)
        {
            return !NullCheck() ? true.Configure(value = GetValue()) : false.Configure(value = default);
        }

        public bool TrySetValue(TValue? value)
        {
            return !NullCheck() && true.Configure(() => SetValue(value));
        }
    }

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

        public bool TryGetValue(TSender sender, [NotNullWhen(true)] out TValue? value)
        {
            return !NullCheck(sender) ? true.Configure(value = GetValue(sender)) : false.Configure(value = default);
        }

        public bool TrySetValue(TSender sender, TValue? value)
        {
            return !NullCheck(sender) && true.Configure(() => SetValue(sender, value));
        }
    }
}