using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.Service.Encode.QRCode
{
    public static class ECCodeInfo
    {
        private static readonly ECCodeGroup[,] ECCodeTable;

        public static ECCodeGroup GetECCodeInfo(int version, ECCodeLevel level)
        {
            return ECCodeTable[version - 1, level.ToInt()];
        }

        static ECCodeInfo()
        {
            ECCodeTable = new ECCodeGroup[,]
            {
                {
                    new ECCodeGroup(7,      1,      19,     0,      0),
                    new ECCodeGroup(10,     1,      16,     0,      0),
                    new ECCodeGroup(13,     1,      13,     0,      0),
                    new ECCodeGroup(17,     1,      9,      0,      0)
                },
                {
                    new ECCodeGroup(10,     1,      34,     0,      0),
                    new ECCodeGroup(16,     1,      28,     0,      0),
                    new ECCodeGroup(22,     1,      22,     0,      0),
                    new ECCodeGroup(28,     1,      16,     0,      0)
                },
                {
                    new ECCodeGroup(15,     1,      55,     0,      0),
                    new ECCodeGroup(26,     1,      44,     0,      0),
                    new ECCodeGroup(18,     2,      17,     0,      0),
                    new ECCodeGroup(22,     2,      13,     0,      0)
                },
                {
                    new ECCodeGroup(20,     1,      80,     0,      0),
                    new ECCodeGroup(18,     2,      32,     0,      0),
                    new ECCodeGroup(26,     2,      24,     0,      0),
                    new ECCodeGroup(16,     4,      9,      0,      0)
                },
                {
                    new ECCodeGroup(26,     1,      108,    0,      0),
                    new ECCodeGroup(24,     2,      43,     0,      0),
                    new ECCodeGroup(18,     2,      15,     2,      16),
                    new ECCodeGroup(22,     2,      11,     2,      12)
                },
                {
                    new ECCodeGroup(18,     2,      68,     0,      0),
                    new ECCodeGroup(16,     4,      27,     0,      0),
                    new ECCodeGroup(24,     4,      19,     0,      0),
                    new ECCodeGroup(28,     4,      15,     0,      0)
                },
                {
                    new ECCodeGroup(20,     2,      78,     0,      0),
                    new ECCodeGroup(18,     4,      31,     0,      0),
                    new ECCodeGroup(18,     2,      14,     4,      15),
                    new ECCodeGroup(26,     4,      13,     1,      14)
                },
                {
                    new ECCodeGroup(24,     2,      97,     0,      0),
                    new ECCodeGroup(22,     2,      38,     2,      39),
                    new ECCodeGroup(22,     4,      18,     2,      19),
                    new ECCodeGroup(26,     4,      14,     2,      15)
                },
                {
                    new ECCodeGroup(30,     2,      116,    0,      0),
                    new ECCodeGroup(22,     3,      36,     2,      37),
                    new ECCodeGroup(20,     4,      16,     4,      17),
                    new ECCodeGroup(24,     4,      12,     4,      13)
                },
                {
                    new ECCodeGroup(18,     2,      68,     2,      69),
                    new ECCodeGroup(26,     4,      43,     1,      44),
                    new ECCodeGroup(24,     6,      19,     2,      20),
                    new ECCodeGroup(28,     6,      15,     2,      16)
                },
                {
                    new ECCodeGroup(20,     4,     81,      0,      0),
                    new ECCodeGroup(30,     1,     50,      4,      51),
                    new ECCodeGroup(28,     4,     22,      4,      23),
                    new ECCodeGroup(24,     3,     12,      8,      13)
                },
                {
                    new ECCodeGroup(24,     2,      92,     2,      93),
                    new ECCodeGroup(22,     6,      36,     2,      37),
                    new ECCodeGroup(26,     4,      20,     6,      21),
                    new ECCodeGroup(28,     7,      14,     4,      15)
                },
                {
                    new ECCodeGroup(26,     4,      107,    0,      0),
                    new ECCodeGroup(22,     8,      37,     1,      38),
                    new ECCodeGroup(24,     8,      20,     4,      21),
                    new ECCodeGroup(22,     12,     11,     4,      12)
                },
                {
                    new ECCodeGroup(30,     3,      115,    1,      116),
                    new ECCodeGroup(24,     4,      40,     5,      41),
                    new ECCodeGroup(20,     11,     16,     5,      17),
                    new ECCodeGroup(24,     11,     12,     5,      13)
                },
                {
                    new ECCodeGroup(22,     5,      87,     1,      88),
                    new ECCodeGroup(24,     5,      41,     5,      42),
                    new ECCodeGroup(30,     5,      24,     7,      25),
                    new ECCodeGroup(24,     11,     12,     7,      13)
                },
                {
                    new ECCodeGroup(24,     5,      98,     1,      99),
                    new ECCodeGroup(28,     7,      45,     3,      46),
                    new ECCodeGroup(24,     15,     19,     2,      20),
                    new ECCodeGroup(30,     3,      15,     13,     16)
                },
                {
                    new ECCodeGroup(28,     1,      107,    5,      108),
                    new ECCodeGroup(28,     10,     46,     1,      47),
                    new ECCodeGroup(28,     1,      22,     15,     23),
                    new ECCodeGroup(28,     2,      14,     17,     15)
                },
                {
                    new ECCodeGroup(30,     5,      120,    1,      121),
                    new ECCodeGroup(26,     9,      43,     4,      44),
                    new ECCodeGroup(28,     17,     22,     1,      23),
                    new ECCodeGroup(28,     2,      14,     19,     15)
                },
                {
                    new ECCodeGroup(28,     3,      113,    4,      114),
                    new ECCodeGroup(26,     3,      44,     11,     45),
                    new ECCodeGroup(26,     17,     21,     4,      22),
                    new ECCodeGroup(26,     9,      13,     16,     14)
                },
                {
                    new ECCodeGroup(28,     3,      107,    5,      108),
                    new ECCodeGroup(26,     3,      41,     13,     42),
                    new ECCodeGroup(30,     15,     24,     5,      25),
                    new ECCodeGroup(28,     15,     15,     10,     16)
                },
                {
                    new ECCodeGroup(28,     4,      116,    4,      117),
                    new ECCodeGroup(26,     17,     42,     0,      0),
                    new ECCodeGroup(28,     17,     22,     6,      23),
                    new ECCodeGroup(30,     19,     16,     6,      17)
                },
                {
                    new ECCodeGroup(28,     2,      111,    7,      112),
                    new ECCodeGroup(28,     17,     46,     0,      0),
                    new ECCodeGroup(30,     7,      24,     16,     25),
                    new ECCodeGroup(24,     34,     13,     0,      0)
                },
                {
                    new ECCodeGroup(30,     4,      121,    5,      122),
                    new ECCodeGroup(28,     4,      47,     14,     48),
                    new ECCodeGroup(30,     11,     24,     14,     25),
                    new ECCodeGroup(30,     16,     15,     14,     16)
                },
                {
                    new ECCodeGroup(30,     6,      117,    4,      118),
                    new ECCodeGroup(28,     6,      45,     14,     46),
                    new ECCodeGroup(30,     11,     24,     16,     25),
                    new ECCodeGroup(30,     30,     16,     2,      17)
                },
                {
                    new ECCodeGroup(26,     8,      106,    4,      107),
                    new ECCodeGroup(28,     8,      47,     13,     48),
                    new ECCodeGroup(30,     7,      24,     22,     25),
                    new ECCodeGroup(30,     22,     15,     13,     16)
                },
                {
                    new ECCodeGroup(28,     10,     114,    2,      115),
                    new ECCodeGroup(28,     19,     46,     4,      47),
                    new ECCodeGroup(28,     28,     22,     6,      23),
                    new ECCodeGroup(30,     33,     16,     4,      17)
                },
                {
                    new ECCodeGroup(30,     8,      122,    4,      123),
                    new ECCodeGroup(28,     22,     45,     3,      46),
                    new ECCodeGroup(30,     8,      23,     26,     24),
                    new ECCodeGroup(30,     12,     15,     28,     16)
                },
                {
                    new ECCodeGroup(30,     3,      117,    10,     118),
                    new ECCodeGroup(28,     3,      45,     23,     46),
                    new ECCodeGroup(30,     4,      24,     31,     25),
                    new ECCodeGroup(30,     11,     15,     31,     16)
                },
                {
                    new ECCodeGroup(30,     7,      116,    7,      117),
                    new ECCodeGroup(28,     21,     45,     7,      46),
                    new ECCodeGroup(30,     1,      23,     37,     24),
                    new ECCodeGroup(30,     19,     15,     26,     16)
                },
                {
                    new ECCodeGroup(30,     5,      115,    10,     116),
                    new ECCodeGroup(28,     19,     47,     10,     48),
                    new ECCodeGroup(30,     15,     24,     25,     25),
                    new ECCodeGroup(30,     23,     15,     25,     16)
                },
                {
                    new ECCodeGroup(30,     13,     115,    3,      116),
                    new ECCodeGroup(28,     2,      46,     29,     47),
                    new ECCodeGroup(30,     42,     24,     1,      25),
                    new ECCodeGroup(30,     23,     15,     28,     16)
                },
                {
                    new ECCodeGroup(30,     17,     115,    0,      0),
                    new ECCodeGroup(28,     10,     46,     23,     47),
                    new ECCodeGroup(30,     10,     24,     35,     25),
                    new ECCodeGroup(30,     19,     15,     35,     16)
                },
                {
                    new ECCodeGroup(30,     17,     115,    1,      116),
                    new ECCodeGroup(28,     14,     46,     21,     47),
                    new ECCodeGroup(30,     29,     24,     19,     25),
                    new ECCodeGroup(30,     11,     15,     46,     16)
                },
                {
                    new ECCodeGroup(30,     13,     115,    6,      116),
                    new ECCodeGroup(28,     14,     46,     23,     47),
                    new ECCodeGroup(30,     44,     24,     7,      25),
                    new ECCodeGroup(30,     59,     16,     1,      17)
                },
                {
                    new ECCodeGroup(30,     12,     121,    7,      122),
                    new ECCodeGroup(28,     12,     47,     26,     48),
                    new ECCodeGroup(30,     39,     24,     14,     25),
                    new ECCodeGroup(30,     22,     15,     41,     16)
                },
                {
                    new ECCodeGroup(30,     6,      121,    14,     122),
                    new ECCodeGroup(28,     6,      47,     34,     48),
                    new ECCodeGroup(30,     46,     24,     10,     25),
                    new ECCodeGroup(30,     2,      15,     64,     16)
                },
                {
                    new ECCodeGroup(30,     17,     122,    4,      123),
                    new ECCodeGroup(28,     29,     46,     14,     47),
                    new ECCodeGroup(30,     49,     24,     10,     25),
                    new ECCodeGroup(30,     24,     15,     46,     16)
                },
                {
                    new ECCodeGroup(30,     4,      122,    18,     123),
                    new ECCodeGroup(28,     13,     46,     32,     47),
                    new ECCodeGroup(30,     48,     24,     14,     25),
                    new ECCodeGroup(30,     42,     15,     32,     16)
                },
                {
                    new ECCodeGroup(30,     20,     117,    4,      118),
                    new ECCodeGroup(28,     40,     47,     7,      48),
                    new ECCodeGroup(30,     43,     24,     22,     25),
                    new ECCodeGroup(30,     10,     15,     67,     16)
                },
                {
                    new ECCodeGroup(30,     19,     118,    6,      119),
                    new ECCodeGroup(28,     18,     47,     31,     48),
                    new ECCodeGroup(30,     34,     24,     34,     25),
                    new ECCodeGroup(30,     20,     15,     61,     16)
                }
            };
        }
    }
}