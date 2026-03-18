using StarRailDamage.Source.Model.DataStruct;

namespace StarRailDamage.Source.Service.Encode.QRCode.Encoder
{
    public sealed class AlphanumericEncoder : QRCodeEncoder
    {
        private static readonly int[,] CapacityTable;

        private static readonly int[] AlphanumericTable;

        public override EncodeMode EncodeMode => EncodeMode.Alphanumeric;

        protected override int[,] GetCapacityTable() => CapacityTable;

        protected override int BitsOfDataLength => Version >= 10 ? Version >= 27 ? 13 : 11 : 9;

        protected override int GetValidBitCount(int length)
        {
            return length / 2 * 11 + length % 2 * 6;
        }

        protected override BitSet BinaryEncode(byte[] content)
        {
            int Count = content.Length;
            int DoubleCount = Count / 2;
            BitSet Result = BitSet.FromBitCount(GetValidBitCount(Count));
            int GetCode(int i) => AlphanumericTable[content[i] - 0x20];
            for (int i = 0; i < DoubleCount; i++)
            {
                Result.Write(i * 11, GetCode(2 * i) * 45 + GetCode(2 * i + 1), 11);
            }
            if (Count % 2 != 0)
            {
                Result.Write(DoubleCount * 11, GetCode(2 * DoubleCount), 6);
            }
            return Result;
        }

        public static bool IsAlphanumeric(byte value)
        {
            return value >= 0x20 && value <= 'Z' && AlphanumericTable[value - 0x20] >= 0;
        }

        static AlphanumericEncoder()
        {
            int Index = 0;
            AlphanumericTable = [.. Enumerable.Repeat(-1, 'Z' - 0x20 + 1)];
            for (int i = '0'; i <= '9'; i++)
            {
                AlphanumericTable[i - 0x20] = Index++;
            }
            for (int i = 'A'; i <= 'Z'; i++)
            {
                AlphanumericTable[i - 0x20] = Index++;
            }
            char[] OtherChars = [(char)0x20, '$', '%', '*', '+', '-', '.', '/', ':'];
            for (int i = 0; i < OtherChars.Length; i++)
            {
                AlphanumericTable[OtherChars[i] - 0x20] = Index++;
            }
            CapacityTable = new int[40, 4]
            {
                {25,    20,     16,     10},
                {47,    38,     29,     20},
                {77,    61,     47,     35},
                {114,   90,     67,     50},
                {154,   122,    87,     64},
                {195,   154,    108,    84},
                {224,   178,    125,    93},
                {279,   221,    157,    122},
                {335,   262,    189,    143},
                {395,   311,    221,    174},
                {468,   366,    259,    200},
                {535,   419,    296,    227},
                {619,   483,    352,    259},
                {667,   528,    376,    283},
                {758,   600,    426,    321},
                {854,   656,    470,    365},
                {938,   734,    531,    408},
                {1046,  816,    574,    452},
                {1153,  909,    644,    493},
                {1249,  970,    702,    557},
                {1352,  1035,   742,    587},
                {1460,  1134,   823,    640},
                {1588,  1248,   890,    672},
                {1704,  1326,   963,    744},
                {1853,  1451,   1041,   779},
                {1990,  1542,   1094,   864},
                {2132,  1637,   1172,   910},
                {2223,  1732,   1263,   958},
                {2369,  1839,   1322,   1016},
                {2520,  1994,   1429,   1080},
                {2677,  2113,   1499,   1150},
                {2840,  2238,   1618,   1226},
                {3009,  2369,   1700,   1307},
                {3183,  2506,   1787,   1394},
                {3351,  2632,   1867,   1431},
                {3537,  2780,   1966,   1530},
                {3729,  2894,   2071,   1591},
                {3927,  3054,   2181,   1658},
                {4087,  3220,   2298,   1774},
                {4296,  3391,   2420,   1852}
            };
        }
    }
}