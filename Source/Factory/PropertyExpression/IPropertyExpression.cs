using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Factory.PropertyExpression
{
    public interface IPropertyExpression<TValue>
    {
        Func<TValue> GetValue { get; init; }

        Action<TValue?> SetValue { get; init; }

        Func<bool> NullCheck { get; init; }

        bool TryGetValue([NotNullWhen(true)] out TValue? value);

        bool TrySetValue(TValue? value);
    }

    public interface IPropertyExpression<TSender, TValue>
    {
        Func<TSender, TValue> GetValue { get; init; }

        Action<TSender, TValue?> SetValue { get; init; }

        Func<TSender, bool> NullCheck { get; init; }

        bool TryGetValue(TSender sender, [NotNullWhen(true)] out TValue? value);

        bool TrySetValue(TSender sender, TValue? value);
    }
}