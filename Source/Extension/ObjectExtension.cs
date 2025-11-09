namespace StarRailDamage.Source.Extension
{
    internal static class ObjectExtension
    {
        public static T ThrowIfNull<T>(this T? value)
        {
            return value ?? throw new NullReferenceException();
        }

        public static bool IsDefault<T>(this T? value)
        {
            return EqualityComparer<T>.Default.Equals(value, default);
        }

        public static T UseDefault<T>(this T? value, Func<T?, bool> func, T defaultValue)
        {
            return func(value) || value == null ? defaultValue : value;
        }

        public static T Invoke<T>(this T value, Action<T> action)
        {
            action.Invoke(value);
            return value;
        }

        public static T1 Invoke<T1, T2>(this T1 value, T2? _) => value;
    }
}