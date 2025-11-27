using System.Linq.Expressions;

namespace StarRailDamage.Source.Factory.PropertyExpression
{
    public interface IPropertyExpressionFactory<TSource, TProperty>
    {
        PropertyExpression<TSource, TProperty> GetPropertyExpression(Expression<Func<TSource, TProperty>> expression);
    }
}