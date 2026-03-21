using System.Diagnostics;

namespace StarRailDamage.Source.Extension
{
    public static class DoubleExtension
    {
        [DebuggerStepThrough]
        public static double Ceiling(this double value, int digits = 0)
        {
            double Factor = Math.Pow(10, digits);
            return Math.Ceiling(value * Factor) / Factor;
        }
    }
}