namespace StarRailDamage.Source.Model.DataStruct
{
    public class BitSet
    {
        public readonly int Count;

        public readonly byte[] Bytes;

        private BitSet(int count, byte[] bytes)
        {
            Count = count;
            Bytes = bytes;
        }

        public static BitSet FromBitCount(int count)
        {
            return new BitSet(count, new byte[(count + 7) >> 3]);
        }

        public static BitSet FromBitBytes(byte[] bytes)
        {
            return new BitSet(bytes.Length * 8, bytes);
        }

        public bool this[int i]
        {
            get => (Bytes[i >> 3] & (1 << (7 - (i & 7)))) != 0;
            set
            {
                if (value)
                {
                    Bytes[i >> 3] |= (byte)(1 << (7 - (i & 7)));
                }
                else
                {
                    Bytes[i >> 3] &= (byte)~(1 << (7 - (i & 7)));
                }
            }
        }

        public void Write(int dstIndex, int src, int srcBitCount)
        {
            for (int i = 0; i < srcBitCount; i++)
            {
                this[dstIndex + i] = (src & (1 << (srcBitCount - 1 - i))) != 0;
            }
        }

        public void Write(int dstIndex, BitSet src, int srcIndex, int srcCount)
        {
            for (int i = 0; i < srcCount; i++)
            {
                this[dstIndex + i] = src[srcIndex + i];
            }
        }
    }
}