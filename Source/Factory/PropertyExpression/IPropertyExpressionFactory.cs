using System.Linq.Expressions;

namespace StarRailDamage.Source.Factory.PropertyExpression
{
    public interface IPropertyExpressionFactory<TSender, TProperty>
    {
        IPropertyExpression<TSender, TProperty> GetPropertyExpression(Expression<Func<TSender, TProperty>> expression);
    }
}