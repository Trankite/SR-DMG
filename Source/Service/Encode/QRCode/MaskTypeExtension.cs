using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.Service.Encode.QRCode
{
    public static class MaskTypeExtension
    {
        private static readonly Func<int, int, bool>[] MaskMethod;

        public static Func<int, int, bool> GetMethod(this MaskType mask)
        {
            return MaskMethod[mask.ToInt()];
        }

        private static bool Mask000(int x, int y) => (x + y) % 2 == 0;

        private static bool Mask001(int x, int y) => y % 2 == 0;

        private static bool Mask010(int x, int y) => x % 3 == 0;

        private static bool Mask011(int x, int y) => (x + y) % 3 == 0;

        private static bool Mask100(int x, int y) => (x / 3 + y / 2) % 2 == 0;

        private static bool Mask101(int x, int y) => x * y % 2 + x * y % 3 == 0;

        private static bool Mask110(int x, int y) => (x * y % 2 + x * y % 3) % 2 == 0;

        private static bool Mask111(int x, int y) => ((x + y) % 2 + x * y % 3) % 2 == 0;

        static MaskTypeExtension()
        {
            MaskMethod = [Mask000, Mask001, Mask010, Mask011, Mask100, Mask101, Mask110, Mask111];
        }
    }
}