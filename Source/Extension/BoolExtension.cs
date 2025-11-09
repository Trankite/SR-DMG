namespace StarRailDamage.Source.Extension
{
    internal static class BoolExtension
    {
        public static T Extract<T>(this bool _, T result) => result;
    }
}