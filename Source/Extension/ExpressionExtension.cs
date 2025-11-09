using System.Linq.Expressions;

namespace StarRailDamage.Source.Extension
{
    internal static class ExpressionExtension
    {
        public static MemberExpression? MemberExpression(this Expression expression)
        {
            if (expression is not MemberExpression MemberExpression)
            {
                if (expression is UnaryExpression UnaryExpression && UnaryExpression.NodeType == ExpressionType.Convert)
                {
                    return UnaryExpression.Operand.MemberExpression();
                }
                return null;
            }
            return MemberExpression;
        }

        public static string FullName<T>(this Expression<Func<T>> expression)
        {
            return GetFullName(expression.Body as MemberExpression);
        }

        public static string FullName<T1, T2>(this Expression<Func<T1, T2>> expression)
        {
            return GetFullName(expression.Body as MemberExpression);
        }

        private static string GetFullName(MemberExpression? memberExpression)
        {
            Stack<string> PathStack = new();
            while (memberExpression != null)
            {
                PathStack.Push(memberExpression.Member.Name);
                if (memberExpression.Expression is not MemberExpression ParentExpression) break;
                memberExpression = ParentExpression;
            }
            return string.Join('.', PathStack);
        }
    }
}