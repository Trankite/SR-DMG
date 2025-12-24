using StarRailDamage.Source.Extension;
using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Factory.PropertyExpression
{
    public class PropertyExpression<TSender, TValue>(Func<TSender, TValue> getter, Action<TSender, TValue?> setter) : IPropertyExpression<TValue>, IPropertyExpression<TSender, TValue>
    {
        public TSender? Sender { get; set; }

        public Func<TSender, TValue> GetValue { get; } = getter;

        public Action<TSender, TValue?> SetValue { get; } = setter;

        public Func<TSender?, bool> NullCheck { get; } = ObjectExtension.IsNotNull;

        public PropertyExpression(Func<TSender, TValue> getter, Action<TSender, TValue?> setter, TSender? sender) : this(getter, setter)
        {
            Sender = sender;
        }

        public PropertyExpression(Func<TSender, TValue> getter, Action<TSender, TValue?> setter, Func<TSender?, bool> nullCheck) : this(getter, setter)
        {
            NullCheck = nullCheck;
        }

        public bool TryGetValue(TSender? sender, [NotNullWhen(true)] out TValue? value)
        {
            return SenderIsNotNull(sender) ? true.Configure(value = GetValue(sender)) : false.Configure(value = default);
        }

        public bool TrySetValue(TSender? sender, TValue? value)
        {
            return SenderIsNotNull(sender) && true.Configure(() => SetValue(sender, value));
        }

        public bool SenderIsNotNull([NotNullWhen(true)] TSender? sender) => !NullCheck(sender);

        public bool TryGetValue([NotNullWhen(true)] out TValue? value) => TryGetValue(Sender, out value);

        public bool TrySetValue(TValue? value) => TrySetValue(Sender, value);
    }
}