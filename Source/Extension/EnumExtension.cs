namespace StarRailDamage.Source.Extension
{
    public static class EnumExtension
    {
        public static int ToInt<TEnum>(this TEnum value) where TEnum : Enum
        {
            return Convert.ToInt32(value);
        }

        public static string ToIntString<TEnum>(this TEnum value) where TEnum : Enum
        {
            return value.ToString("D");
        }

        public static int GetFlags<TEnum>(this IEnumerable<TEnum> values) where TEnum : Enum
        {
            int Flags = 0;
            foreach (TEnum value in values)
            {
                Flags |= 1 << value.ToInt();
            }
            return Flags;
        }
    }
}