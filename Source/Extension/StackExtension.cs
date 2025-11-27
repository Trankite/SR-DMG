namespace StarRailDamage.Source.Extension
{
    public static class StackExtension
    {
        public static T? PopOrDefault<T>(this Stack<T> value)
        {
            return value.TryPop(out T? result) ? result : default;
        }
    }
}