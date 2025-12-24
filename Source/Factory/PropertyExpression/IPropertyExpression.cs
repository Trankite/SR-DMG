using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Factory.PropertyExpression
{
    public interface IPropertyExpression<TValue>
    {
        bool TryGetValue([NotNullWhen(true)] out TValue? value);

        bool TrySetValue(TValue? value);
    }

    public interface IPropertyExpression<TSender, TValue>
    {
        Func<TSender, TValue> GetValue { get; }

        Action<TSender, TValue?> SetValue { get; }

        Func<TSender?, bool> NullCheck { get; }

        bool TryGetValue(TSender? sender, [NotNullWhen(true)] out TValue? value);

        bool TrySetValue(TSender? sender, TValue? value);
    }
}