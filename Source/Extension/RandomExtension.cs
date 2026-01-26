namespace StarRailDamage.Source.Extension
{
    public static class RandomExtension
    {
        public static string GetLowerHexString(this Random random, int length)
        {
            Span<char> Buffer = new char[length];
            random.GetItems("0123456789abcdef", Buffer);
            return Buffer.ToString();
        }

        public static string GetLowerAndNumberString(this Random random, int length)
        {
            Span<char> Buffer = new char[length];
            random.GetItems("0123456789abcdefghijklmnopqrstuvwxyz", Buffer);
            return Buffer.ToString();
        }

        public static string GetUpperAndNumberString(this Random random, int length)
        {
            Span<char> Buffer = new char[length];
            random.GetItems("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", Buffer);
            return Buffer.ToString();
        }
    }
}