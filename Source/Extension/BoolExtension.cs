namespace StarRailDamage.Source.Extension
{
    public static class BoolExtension
    {
        public static T Extract<T>(this bool _, T result) => result;
    }
}