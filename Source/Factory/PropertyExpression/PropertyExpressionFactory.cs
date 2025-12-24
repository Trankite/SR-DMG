using StarRailDamage.Source.Extension;
using System.Linq.Expressions;

namespace StarRailDamage.Source.Factory.PropertyExpression
{
    public class PropertyExpressionFactory<TSender, TProperty> : IPropertyExpressionFactory<TSender, TProperty>
    {
        public IPropertyExpression<TSender, TProperty> GetPropertyExpression(Expression<Func<TSender, TProperty>> expression)
        {
            return new PropertyExpression<TSender, TProperty>(expression.Compile(), GetPropertySetter(expression).Compile(), GetNullCheck(expression).Compile());
        }

        public static Expression<Func<TSender?, bool>> GetNullCheck(Expression<Func<TSender, TProperty>> expression)
        {
            ParameterExpression Parameter = expression.Parameters.First();
            Expression NullCheckExpression = BuildNullCheck(expression.Body.MemberExpression().ThrowIfNull());
            return Expression.Lambda<Func<TSender?, bool>>(NullCheckExpression, Parameter);
        }

        public static Expression<Action<TSender, TProperty?>> GetPropertySetter(Expression<Func<TSender, TProperty>> expression)
        {
            ParameterExpression SenderParameter = Expression.Parameter(typeof(TSender));
            ParameterExpression PropertyParameter = Expression.Parameter(typeof(TProperty));
            MemberExpression MemberExpression = expression.Body.MemberExpression().ThrowIfNull();
            MemberExpression PropertyAccess = BuildPropertyAccess(SenderParameter, MemberExpression);
            BinaryExpression AssignExpression = Expression.Assign(PropertyAccess, PropertyParameter);
            return Expression.Lambda<Action<TSender, TProperty?>>(AssignExpression, SenderParameter, PropertyParameter);
        }

        private static Expression BuildNullCheck(MemberExpression memberExpression)
        {
            if (memberExpression.Expression is MemberExpression ParentMemberExpression)
            {
                return Expression.OrElse(BuildNullCheck(ParentMemberExpression), Expression.Equal(ParentMemberExpression, Expression.Constant(null)));
            }
            else if (memberExpression.Expression is not ParameterExpression)
            {
                return Expression.Equal(memberExpression, Expression.Constant(null));
            }
            return Expression.Constant(false);
        }

        private static MemberExpression BuildPropertyAccess(ParameterExpression parameterExpression, MemberExpression memberExpression)
        {
            if (memberExpression.Expression is MemberExpression ParentMember)
            {
                return Expression.MakeMemberAccess(BuildPropertyAccess(parameterExpression, ParentMember), memberExpression.Member);
            }
            return Expression.MakeMemberAccess(parameterExpression, memberExpression.Member);
        }
    }
}