using StarRailDamage.Source.Extension;
using System.Linq.Expressions;

namespace StarRailDamage.Source.Factory.PropertyExpression
{
    internal class PropertyExpressionFactory
    {
        public static PropertyExpression<TSource, TProperty> GetPropertyExpression<TSource, TProperty>(Expression<Func<TSource, TProperty>> expression)
        {
            return new()
            {
                GetValue = expression.Compile(),
                SetValue = CreatePropertySetter(expression),
                NullCheck = CreateNullCheckExpression(expression)
            };
        }

        public static Func<TSource, bool> CreateNullCheckExpression<TSource, TProperty>(Expression<Func<TSource, TProperty>> expression)
        {
            ParameterExpression Parameter = expression.Parameters.First();
            Expression NullCheckExpression = BuildNullCheck(expression.Body.MemberExpression().ThrowIfNull());
            return Expression.Lambda<Func<TSource, bool>>(NullCheckExpression, Parameter).Compile();
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

        public static Action<TSource, TProperty?> CreatePropertySetter<TSource, TProperty>(Expression<Func<TSource, TProperty>> expression)
        {
            ParameterExpression SourceParameter = Expression.Parameter(typeof(TSource));
            ParameterExpression PropertyParameter = Expression.Parameter(typeof(TProperty));
            MemberExpression MemberExpression = expression.Body.MemberExpression().ThrowIfNull();
            MemberExpression PropertyAccess = BuildPropertyAccess(SourceParameter, MemberExpression);
            BinaryExpression AssignExpression = Expression.Assign(PropertyAccess, PropertyParameter);
            return Expression.Lambda<Action<TSource, TProperty?>>(AssignExpression, SourceParameter, PropertyParameter).Compile();
        }

        private static MemberExpression BuildPropertyAccess(ParameterExpression parameterExpression, MemberExpression memberExpression)
        {
            if (memberExpression.Expression is MemberExpression parentMember)
            {
                return Expression.MakeMemberAccess(BuildPropertyAccess(parameterExpression, parentMember), memberExpression.Member);
            }
            return Expression.MakeMemberAccess(parameterExpression, memberExpression.Member);
        }
    }
}