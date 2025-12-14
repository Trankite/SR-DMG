using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Extension
{
    public static class CollectionExtension
    {
        [DebuggerStepThrough]
        public static T? Index<T>(this T[]? value, int index)
        {
            return value.IndexTry(index, out T? Result).Captured(Result);
        }

        [DebuggerStepThrough]
        public static T? Index<T>(this IList<T>? value, int index)
        {
            return value.IndexTry(index, out T? Result).Captured(Result);
        }

        [DebuggerStepThrough]
        public static bool IndexTry<T>(this T[]? value, int index, [NotNullWhen(true)] out T? result)
        {
            if (value.IsNotNull() && index >= 0 && index < value.Length)
            {
                return true.Configure(result = value[index]);
            }
            return false.Configure(result = default);
        }

        [DebuggerStepThrough]
        public static bool IndexTry<T>(this IList<T>? value, int index, [NotNullWhen(true)] out T? result)
        {
            if (value.IsNotNull() && index >= 0 && index < value.Count)
            {
                return true.Configure(result = value[index]);
            }
            return false.Configure(result = default);
        }

        [DebuggerStepThrough]
        public static bool TryGetFirst<T>(this IEnumerable<T>? value, [NotNullWhen(true)] out T? result)
        {
            return value.IsNotNull() && ObjectExtension.IsNotNull(result = value.FirstOrDefault()) || false.Configure(result = default);
        }

        [DebuggerStepThrough]
        public static bool TryGetLast<T>(this IEnumerable<T>? value, [NotNullWhen(true)] out T? result)
        {
            return value.IsNotNull() && ObjectExtension.IsNotNull(result = value.LastOrDefault()) || false.Configure(result = default);
        }

        [DebuggerStepThrough]
        public static void Foreach<T>(this T[]? values, Action<T> action)
        {
            if (values.IsNull()) return;
            foreach (T Item in values)
            {
                action.Invoke(Item);
            }
        }
    }
}