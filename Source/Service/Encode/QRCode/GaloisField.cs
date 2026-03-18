using System.Numerics;

namespace StarRailDamage.Source.Service.Encode.QRCode
{
    public struct GaloisField
    {
        private const int Size = 255;

        public readonly static GaloisField One = new(1);

        public readonly static GaloisField Zero = new();

        public readonly static byte[] AlphaTable = new byte[Size + 1];

        public readonly static byte[] ExponentTable = new byte[Size + 1];

        public byte Polynom;

        public GaloisField() { }

        public GaloisField(int polynom)
        {
            Polynom = (byte)polynom;
        }

        public GaloisField(byte polynom)
        {
            Polynom = polynom;
        }

        public static GaloisField FromExponent(int exponent)
        {
            return new GaloisField(AlphaTable[exponent + 1]);
        }

        public static int PolynomMod(int left, int right)
        {
            int RightLength = GetBitLength(right);
            for (int i = GetBitLength(left) - RightLength - 1; i >= -1; i--)
            {
                if ((left & (1 << (i + RightLength))) != 0)
                {
                    left ^= right << (i + 1);
                }
            }
            return left;
        }

        private static int GetBitLength(int value)
        {
            return 32 - BitOperations.LeadingZeroCount((uint)value);
        }

        public static GaloisField operator +(GaloisField left, GaloisField right)
        {
            return new GaloisField(left.Polynom ^ right.Polynom);
        }

        public static GaloisField operator *(GaloisField left, GaloisField right)
        {
            if (left.Polynom == 0 || right.Polynom == 0) return Zero;
            int Exponent = ExponentTable[left.Polynom] + ExponentTable[right.Polynom];
            return FromExponent(Exponent >= Size ? Exponent - Size : Exponent);
        }

        public override readonly string ToString() => Polynom.ToString();

        static GaloisField()
        {
            AlphaTable[1] = 1;
            for (int i = 2; i < Size + 1; i++)
            {
                int Alpha = AlphaTable[i - 1] << 1;
                AlphaTable[i] = (byte)(Alpha > Size ? Alpha ^ 0b100011101 : Alpha);
            }
            ExponentTable[0] = Size;
            for (int i = 1; i < Size + 1; i++)
            {
                ExponentTable[AlphaTable[i]] = (byte)(i - 1);
            }
        }
    }
}