using System.Windows;

namespace StarRailDamage.Source.Extension
{
    public static class SizeExtension
    {
        public static bool IsHidden(this Size size)
        {
            return size.Width == 0 || size.Height == 0;
        }
    }
}