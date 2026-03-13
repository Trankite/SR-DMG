namespace StarRailDamage.Source.Extension
{
    public static class EnumExtension
    {
        public static string ToIntString<TEnum>(this TEnum value) where TEnum : Enum
        {
            return value.ToString("D");
        }
    }
}