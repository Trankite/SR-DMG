using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Extension
{
    public static class CollectionExtension
    {
        [DebuggerStepThrough]
        public static T? Index<T>(this T[]? value, int index)
        {
            return value.IndexTry(index, out T? Result).Capture(Result);
        }

        [DebuggerStepThrough]
        public static T? Index<T>(this IList<T>? value, int index)
        {
            return value.IndexTry(index, out T? Result).Capture(Result);
        }

        [DebuggerStepThrough]
        public static bool IndexTry<T>(this T[]? value, int index, [NotNullWhen(true)] out T? result)
        {
            if (value is not null && index > 0 && index < value.Length)
            {
                return true.Configure(result = value[index]);
            }
            return false.Configure(result = default);
        }

        [DebuggerStepThrough]
        public static bool IndexTry<T>(this IList<T>? value, int index, [NotNullWhen(true)] out T? result)
        {
            if (value is not null && index > 0 && index < value.Count)
            {
                return true.Configure(result = value[index]);
            }
            return false.Configure(result = default);
        }

        [DebuggerStepThrough]
        public static void Foreach<T>(this T[]? values, Action<T> action)
        {
            if (values is null) return;
            foreach (T Item in values)
            {
                action.Invoke(Item);
            }
        }
    }
}