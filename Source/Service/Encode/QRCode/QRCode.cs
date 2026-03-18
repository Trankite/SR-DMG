using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.DataStruct;
using StarRailDamage.Source.Model.DataStruct.Point;

namespace StarRailDamage.Source.Service.Encode.QRCode
{
    public class QRCode
    {
        private static readonly int[][] AlignmentPointTable;

        private static readonly LocalPoint[,] FormatPointTable;

        private readonly QRCodeBit[,] Content;

        private readonly QRCodeEncoder Encoder;

        public int Version => Encoder.Version;

        public EncodeMode EncodeMode => Encoder.EncodeMode;

        public ECCodeLevel ECCodeLevel => Encoder.ECCodeLevel;

        public ECCodeGroup ECCodeGroup => Encoder.ECCodeGroup;

        public int Capacity => Encoder.Capacity;

        public MaskType MaskType { get; }

        private int FormatBinary;

        public int Size { get; }

        public QRCode(MaskType maskType, QRCodeEncoder encoder, int size, QRCodeBit[,] content)
        {
            MaskType = maskType;
            Encoder = encoder;
            Size = size;
            Content = content;
        }

        private static QRCode Create(QRCodeEncoder encoder, MaskType mask)
        {
            int BlockSize = encoder.Version * 4 + 17;
            return new QRCode(mask, encoder, BlockSize, new QRCodeBit[BlockSize, BlockSize]);
        }

        public static QRCode Create(byte[] data, ECCodeLevel level = default, MaskType mask = default)
        {
            return Create(EncodeModeExtension.GetAutoMode(data).CreateEncoder().SetECCodeLevel(level).SetAutoSize(data.Length).Complete(), mask).Initialize(data);
        }

        public static QRCode Create(byte[] data, EncodeMode mode, ECCodeLevel level = default, MaskType mask = default)
        {
            return Create(mode.CreateEncoder().SetECCodeLevel(level).SetAutoSize(data.Length).Complete(), mask).Initialize(data);
        }

        public static QRCode Create(byte[] data, int version, ECCodeLevel level = default, MaskType mask = default)
        {
            return Create(EncodeModeExtension.GetAutoMode(data).CreateEncoder().SetECCodeLevel(level).SetVersion(version).Complete(), mask).Initialize(data);
        }

        public static QRCode Create(byte[] data, EncodeMode mode, int version, ECCodeLevel level = default, MaskType mask = default)
        {
            return Create(mode.CreateEncoder().SetECCodeLevel(level).SetVersion(version).Complete(), mask).Initialize(data);
        }

        private QRCode Initialize(byte[] data)
        {
            SetPositionPattern();
            SetAlignmentPattern();
            SetTimingPattern();
            SetFormatInformation();
            SetVersionInformation();
            SetFlags(data.Length);
            SetData(data);
            SetMask();
            return this;
        }

        private void SetPositionPattern()
        {
            byte[,] Pattern = new byte[9, 9];
            byte[] Template = [0, 1, 0, 1, 1];
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    Pattern[x, y] = Template[4 - Math.Max(Math.Abs(x - 4), Math.Abs(y - 4))];
                }
            }
            CopyTable(Pattern, -1, -1);
            CopyTable(Pattern, -1, Size - 8);
            CopyTable(Pattern, Size - 8, -1);
        }

        private void SetAlignmentPattern()
        {
            if (Version == 1) return;
            byte[,] Pattern = new byte[5, 5]
            {
                { 1, 1, 1, 1, 1 },
                { 1, 0, 0, 0, 1 },
                { 1, 0, 1, 0, 1 },
                { 1, 0, 0, 0, 1 },
                { 1, 1, 1, 1, 1 }
            };
            int[] Points = AlignmentPointTable[Version - 2];
            for (int x = 1; x < Points.Length - 1; x++)
            {
                CopyTable(Pattern, Points[x] - 2, Points[0] - 2);
            }
            for (int y = 1; y < Points.Length - 1; y++)
            {
                CopyTable(Pattern, Points[0] - 2, Points[y] - 2);
            }
            for (int x = 1; x < Points.Length; x++)
            {
                for (int y = 1; y < Points.Length; y++)
                {
                    CopyTable(Pattern, Points[x] - 2, Points[y] - 2);
                }
            }
        }

        private void CopyTable(byte[,] table, int dstx, int dsty)
        {
            int Count = table.GetLength(0);
            for (int x = dstx; x < dstx + Count; x++)
            {
                if (x < 0 || x >= Size) continue;
                for (int y = dsty; y < dsty + Count; y++)
                {
                    if (y >= 0 && y < Size)
                    {
                        SetBitType(new LocalPoint(x, y), table[x - dstx, y - dsty] != 0, QRCodeBitType.Fixed);
                    }
                }
            }
        }

        private void SetTimingPattern()
        {
            bool HasBit = true;
            for (int x = 8; x < Size - 8; x++)
            {
                if (Content[x, 6].Type == QRCodeBitType.Unused)
                {
                    SetBitType(new LocalPoint(x, 6), HasBit, QRCodeBitType.Timing);
                }
                HasBit = !HasBit;
            }
            HasBit = true;
            for (int y = 8; y < Size - 8; y++)
            {
                if (Content[6, y].Type == QRCodeBitType.Unused)
                {
                    SetBitType(new LocalPoint(6, y), HasBit, QRCodeBitType.Timing);
                }
                HasBit = !HasBit;
            }
        }

        private void SetFormatInformation()
        {
            FormatBinary = ((ECCodeLevel.ToInt() ^ 1) << 13) | (MaskType.ToInt() << 10);
            FormatBinary = (FormatBinary | GaloisField.PolynomMod(FormatBinary, 0b10100110111)) ^ 0b101010000010010;
            WriteFormatInformation(FormatBinary);
            SetBitType(GetAbsolutePoint(8, -8), true, QRCodeBitType.FixedPoint);
        }

        private void SetVersionInformation()
        {
            if (Version < 7) return;
            int Bits = Version << 12;
            Bits |= GaloisField.PolynomMod(Bits, 0b1111100100101);
            for (int i = 0; i < 6; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    bool HasBit = (Bits & (1 << (i * 3 + k))) != 0;
                    SetBitType(GetAbsolutePoint(-11 + k, i), HasBit, QRCodeBitType.Version);
                    SetBitType(GetAbsolutePoint(i, -11 + k), HasBit, QRCodeBitType.Version);
                }
            }
        }

        private LocalPoint GetAbsolutePoint(int x, int y)
        {
            return new LocalPoint(x < 0 ? x + Size : x, y < 0 ? y + Size : y);
        }

        private void SetFlags(int length)
        {
            int ContentTotalBits = Encoder.GetBitCount(length);
            int ContentPaddingTotalBits = Capacity * 8 - ContentTotalBits;
            int ECCodeTotalBits = (ECCodeGroup.BlocksInGroup1 + ECCodeGroup.BlocksInGroup2) * ECCodeGroup.ECCodePerBytes * 8;
            IEnumerator<SpacePoint> PointArray = GetPoints(QRCodeBitType.Unused).GetEnumerator();
            void SetBitTypeFlag(int length, QRCodeBitType type)
            {
                for (int i = length; i > 0 && PointArray.MoveNext(); i--)
                {
                    Content[PointArray.Current.X, PointArray.Current.Y].Type = type;
                }
            }
            SetBitTypeFlag(ContentTotalBits, QRCodeBitType.Content);
            SetBitTypeFlag(ContentPaddingTotalBits, QRCodeBitType.ContentPadding);
            SetBitTypeFlag(ECCodeTotalBits, QRCodeBitType.ECCode);
            while (PointArray.MoveNext())
            {
                Content[PointArray.Current.X, PointArray.Current.Y].Type = QRCodeBitType.Padding;
            }
        }

        public void WriteFormatInformation(int formatBinary)
        {
            for (int i = 0; i < 15; i++)
            {
                bool HasBit = (formatBinary & (1 << i)) != 0;
                for (int k = 0; k < 2; k++)
                {
                    LocalPoint Point = FormatPointTable[k, i];
                    Point = GetAbsolutePoint(Point.X, Point.Y);
                    SetBitType(Point, HasBit, QRCodeBitType.Format);
                }
            }
        }

        private void SetBitType(LocalPoint point, bool hasBit, QRCodeBitType type)
        {
            Content[point.X, point.Y].SetValue(hasBit, type);
        }

        private void SetData(byte[] data)
        {
            BitSet Bits = BitSet.FromBitBytes(Encoder.Encode(data));
            QRCodeBitType[] Types = [QRCodeBitType.Unused, QRCodeBitType.Content, QRCodeBitType.ContentPadding, QRCodeBitType.ECCode];
            IEnumerator<SpacePoint> PointArray = GetPoints(Types).GetEnumerator();
            while (PointArray.TryGetNext(out SpacePoint Point) && Point.Z <= Bits.Count)
            {
                Content[Point.X, Point.Y].HasBit = Bits[Point.Z];
            }
        }

        public IEnumerable<SpacePoint> GetPoints(params QRCodeBitType[] types)
        {
            int Index = 0;
            int Flags = types.GetFlags();
            int MagicWorld = Size * (Size - 1);
            int Enchantment = (Size - 7) / 2;
            for (int i = 0; i < MagicWorld; i++)
            {
                int Position1 = Math.DivRem(i, Size * 2, out int Magic);
                int Position2 = Math.DivRem(Magic, 2, out Magic);
                int X = Size - 1 - (Position1 < Enchantment ? Position1 * 2 + Magic : Position1 * 2 + Magic + 1);
                int Y = Size - 1 - (Position1 % 2 == 0 ? Position2 : Size - 1 - Position2);
                if (((1 << (int)Content[X, Y].Type) & Flags) != 0)
                {
                    yield return new SpacePoint(X, Y, Index++);
                }
            }
        }

        private void SetMask()
        {
            Func<int, int, bool> Method = MaskType.GetMethod();
            QRCodeBitType[] Types = [QRCodeBitType.Unused, QRCodeBitType.Content, QRCodeBitType.ContentPadding, QRCodeBitType.ECCode, QRCodeBitType.Padding];
            int Pending = Types.GetFlags();
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    if (((1 << (int)Content[x, y].Type) & Pending) != 0)
                    {
                        Content[x, y].HasBit = Method(x, y) != Content[x, y].HasBit;
                    }
                }
            }
        }

        public ref QRCodeBit this[int x, int y] => ref Content[x, y];

        static QRCode()
        {
            FormatPointTable = new LocalPoint[2, 15]
            {
                {
                    new LocalPoint(8, 0),
                    new LocalPoint(8, 1),
                    new LocalPoint(8, 2),
                    new LocalPoint(8, 3),
                    new LocalPoint(8, 4),
                    new LocalPoint(8, 5),
                    new LocalPoint(8, 7),
                    new LocalPoint(8, 8),
                    new LocalPoint(7, 8),
                    new LocalPoint(5, 8),
                    new LocalPoint(4, 8),
                    new LocalPoint(3, 8),
                    new LocalPoint(2, 8),
                    new LocalPoint(1, 8),
                    new LocalPoint(0, 8)
                },
                {
                    new LocalPoint(-1, 8),
                    new LocalPoint(-2, 8),
                    new LocalPoint(-3, 8),
                    new LocalPoint(-4, 8),
                    new LocalPoint(-5, 8),
                    new LocalPoint(-6, 8),
                    new LocalPoint(-7, 8),
                    new LocalPoint(-8, 8),
                    new LocalPoint(8, -7),
                    new LocalPoint(8, -6),
                    new LocalPoint(8, -5),
                    new LocalPoint(8, -4),
                    new LocalPoint(8, -3),
                    new LocalPoint(8, -2),
                    new LocalPoint(8, -1)
                }
            };
            AlignmentPointTable =
            [
                [6,               /* Version 2 */               18],
                [6,               /* Version 3 */               22],
                [6,               /* Version 4 */               26],
                [6,               /* Version 5 */               30],
                [6,               /* Version 6 */               34],
                [6, 22,             /* Version 7 */             38],
                [6, 24,             /* Version 8 */             42],
                [6, 26,             /* Version 9 */             46],
                [6, 28,             /* Version 10 */            50],
                [6, 30,             /* Version 11 */            54],
                [6, 32,             /* Version 12 */            58],
                [6, 34,               /* Version 13 */          62],
                [6, 26, 46,           /* Version 14 */          66],
                [6, 26, 48,           /* Version 15 */          70],
                [6, 26, 50,           /* Version 16 */          74],
                [6, 30, 54,           /* Version 17 */          78],
                [6, 30, 56,           /* Version 18 */          82],
                [6, 30, 58,           /* Version 19 */          86],
                [6, 34, 62,           /* Version 20 */          90],
                [6, 28, 50, 72,        /* Version 21 */         94],
                [6, 26, 50, 74,        /* Version 22 */         98],
                [6, 30, 54, 78,        /* Version 23 */         102],
                [6, 28, 54, 80,        /* Version 24 */         106],
                [6, 32, 58, 84,        /* Version 25 */         110],
                [6, 30, 58, 86,        /* Version 26 */         114],
                [6, 34, 62, 90,        /* Version 27 */         118],
                [6, 26, 50, 74, 98,      /* Version 28 */       122],
                [6, 30, 54, 78, 102,     /* Version 29 */       126],
                [6, 26, 52, 78, 104,     /* Version 30 */       130],
                [6, 30, 56, 82, 108,     /* Version 31 */       134],
                [6, 34, 60, 86, 112,     /* Version 32 */       138],
                [6, 30, 58, 86, 114,     /* Version 33 */       142],
                [6, 34, 62, 90, 118,     /* Version 34 */       146],
                [6, 30, 54, 78, 102, 126,    /* Version 35 */   150],
                [6, 24, 50, 76, 102, 128,    /* Version 36 */   154],
                [6, 28, 54, 80, 106, 132,    /* Version 37 */   158],
                [6, 32, 58, 84, 110, 136,    /* Version 38 */   162],
                [6, 26, 54, 82, 110, 138,    /* Version 39 */   166],
                [6, 30, 58, 86, 114, 142,    /* Version 40 */   170]
            ];
        }
    }
}