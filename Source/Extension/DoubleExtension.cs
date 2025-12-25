namespace StarRailDamage.Source.Extension
{
    public static class DoubleExtension
    {
        public static double Ceiling(this double value) => Ceiling(value, 0);

        public static double Ceiling(this double value, int digits)
        {
            return Math.Ceiling(value * Math.Pow(10, digits)) / digits;
        }
    }
}