using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Extension
{
    public static class CollectionExtension
    {
        public static T? Index<T>(this T[]? value, int index)
        {
            return value.IndexTry(index, out T? Result).Extract(Result);
        }

        public static T? Index<T>(this IList<T>? value, int index)
        {
            return value.IndexTry(index, out T? Result).Extract(Result);
        }

        public static bool IndexTry<T>(this T[]? value, int index, [NotNullWhen(true)] out T? result)
        {
            if (value != null && index > 0 && index < value.Length)
            {
                return true.With(result = value[index]);
            }
            return false.With(result = default);
        }

        public static bool IndexTry<T>(this IList<T>? value, int index, [NotNullWhen(true)] out T? result)
        {
            if (value != null && index > 0 && index < value.Count)
            {
                return true.With(result = value[index]);
            }
            return false.With(result = default);
        }

        public static void Foreach<T>(this T[]? values, Action<T> action)
        {
            if (values == null) return;
            foreach (T Item in values)
            {
                action.Invoke(Item);
            }
        }
    }
}