using System.Linq.Expressions;

namespace StarRailDamage.Source.Extension
{
    internal static class StringExtension
    {
        public static string[] FirstSplit(this string value, char separator)
        {
            return value.FirstSplit(separator.ToString());
        }

        public static string[] FirstSplit(this string value, string separator)
        {
            return value.IndexOfTry(separator, out int index) ? value.SplitAtWithOutSelf(index) : [];
        }

        public static string[] LastSplit(this string value, char separator)
        {
            return value.LastSplit(separator.ToString());
        }

        public static string[] LastSplit(this string value, string separator)
        {
            return value.LastIndexOfTry(separator, out int index) ? value.SplitAtWithOutSelf(index, separator.Length) : [];
        }

        public static string[] SplitAt(this string value, int index)
        {
            return [value[..index], value[index..]];
        }

        public static string[] SplitAtWithOutSelf(this string value, int index, int length = 1)
        {
            return [value[..index], value[(index + length)..]];
        }

        public static bool IndexOfTry(this string value, string separator, out int index)
        {
            return (index = value.IndexOf(separator)) != -1;
        }

        public static bool LastIndexOfTry(this string value, string separator, out int index)
        {
            return (index = value.LastIndexOf(separator)) != -1;
        }

        public static string FullName<T>(this string value, Expression<Func<T>> expression)
        {
            return GetFullName(value, expression.Body as MemberExpression);
        }

        public static string FullName<T1, T2>(this string value, Expression<Func<T1, T2>> expression)
        {
            return GetFullName(value, expression.Body as MemberExpression);
        }

        private static string GetFullName(string value, MemberExpression? memberExpression)
        {
            ArgumentNullException.ThrowIfNull(memberExpression);
            Stack<string> PathStack = new();
            if (!string.IsNullOrEmpty(value)) PathStack.Push(value);
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