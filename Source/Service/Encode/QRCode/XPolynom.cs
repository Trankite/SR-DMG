namespace StarRailDamage.Source.Service.Encode.QRCode
{
    public readonly struct XPolynom
    {
        private static readonly Dictionary<int, XPolynom> PolynomCache = [];

        private readonly GaloisField[] Polynoms;

        public readonly int Count => Polynoms.Length;

        public XPolynom(int count)
        {
            Polynoms = new GaloisField[count];
        }

        public XPolynom(params GaloisField[] polynoms)
        {
            Polynoms = polynoms;
        }

        public readonly ref GaloisField this[int index] => ref Polynoms[index];

        public static byte[] RSEncode(ReadOnlySpan<byte> content, int count)
        {
            byte[] ECCodeBytes = new byte[count];
            Span<byte> ECCodeSpan = ECCodeBytes.AsSpan();
            if (!PolynomCache.TryGetValue(count, out XPolynom XPolynom))
            {
                XPolynom = new(GaloisField.One, GaloisField.One);
                for (int i = 1; i < count; i++)
                {
                    XPolynom *= new XPolynom(GaloisField.FromExponent(i), GaloisField.One);
                }
                PolynomCache[count] = XPolynom;
            }
            content[..Math.Min(content.Length, ECCodeSpan.Length)].CopyTo(ECCodeSpan);
            for (int i = 0; i < content.Length; i++)
            {
                GaloisField GaloisField = new(ECCodeSpan[0]);
                for (int k = 1; k < count; k++)
                {
                    ECCodeSpan[k - 1] = (new GaloisField(ECCodeSpan[k]) + XPolynom[count - k] * GaloisField).Polynom;
                }
                if (i + count < content.Length)
                {
                    ECCodeSpan[count - 1] = (new GaloisField(content[i + count]) + XPolynom[0] * GaloisField).Polynom;
                }
                else
                {
                    ECCodeSpan[count - 1] = (XPolynom[0] * GaloisField).Polynom;
                }
            }
            return ECCodeBytes;
        }

        public static XPolynom operator *(XPolynom left, XPolynom right)
        {
            XPolynom XPolynom = new(left.Count + right.Count - 1);
            for (int i = 0; i < left.Count; i++)
            {
                for (int j = 0; j < right.Count; j++)
                {
                    XPolynom[i + j] += left[i] * right[j];
                }
            }
            return XPolynom;
        }
    }
}