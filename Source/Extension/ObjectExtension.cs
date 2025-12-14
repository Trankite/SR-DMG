using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Extension
{
    public static class ObjectExtension
    {
        [DebuggerStepThrough]
        public static T ThrowIfNull<T>(this T? value)
        {
            return value ?? throw new NullReferenceException();
        }

        public static bool IsNull<T>([NotNullWhen(false)] this T? value)
        {
            return value is null;
        }

        public static bool IsNotNull<T>([NotNullWhen(true)] this T? value)
        {
            return value is not null;
        }

        [DebuggerStepThrough]
        public static bool IsDefault<T>(this T? value)
        {
            return EqualityComparer<T>.Default.Equals(value, default);
        }

        [DebuggerStepThrough]
        public static T Configure<T>(this T value, Action action)
        {
            action.Invoke();
            return value;
        }

        [DebuggerStepThrough]
        public static T Configure<T>(this T value, Action<T> action)
        {
            action.Invoke(value);
            return value;
        }

        [DebuggerStepThrough]
        public static TSelf Configure<TSelf, TArgument>(this TSelf value, Action<TArgument> action, TArgument argument)
        {
            action.Invoke(argument);
            return value;
        }

        [DebuggerStepThrough]
        public static TSelf Configure<TSelf, TNone>(this TSelf value, TNone? _) => value;

        [DebuggerStepThrough]
        public static bool OutTemp<TSelf>(this TSelf value, out TSelf self) => true.Configure(self = value);
    }
}