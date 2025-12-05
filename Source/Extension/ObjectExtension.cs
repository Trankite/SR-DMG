namespace StarRailDamage.Source.Extension
{
    public static class ObjectExtension
    {
        public static T ThrowIfNull<T>(this T? value)
        {
            return value ?? throw new NullReferenceException();
        }

        public static bool IsDefault<T>(this T? value)
        {
            return EqualityComparer<T>.Default.Equals(value, default);
        }

        public static KeyValuePair<TKey, TValue> ToPair<TKey, TValue>(this TKey key, TValue value)
        {
            return new KeyValuePair<TKey, TValue>(key, value);
        }

        public static T Invoke<T>(this T value, Action action)
        {
            action.Invoke();
            return value;
        }

        public static T Invoke<T>(this T value, Action<T> action)
        {
            action.Invoke(value);
            return value;
        }

        public static TSelf Invoke<TSelf, TArg>(this TSelf value, Action<TArg> action, TArg arg)
        {
            action.Invoke(arg);
            return value;
        }

        public static TSelf With<TSelf, TNone>(this TSelf value, TNone? _) => value;
    }
}