using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.DataStruct;

namespace StarRailDamage.Source.Service.Encode.QRCode
{
    public abstract class QRCodeEncoder
    {
        public int Version { get; set; }

        public abstract EncodeMode EncodeMode { get; }

        public ECCodeLevel ECCodeLevel { get; set; }

        public ECCodeGroup ECCodeGroup { get; private set; }

        public int Capacity { get; private set; }

        protected abstract int BitsOfDataLength { get; }

        protected abstract BitSet BinaryEncode(byte[] content);

        protected abstract int GetValidBitCount(int length);

        protected abstract int[,] GetCapacityTable();

        public QRCodeEncoder SetAutoSize(int length)
        {
            int[,] CapacityTable = GetCapacityTable();
            for (int i = CapacityTable.GetLength(0); i > 0; i--)
            {
                if (CapacityTable[i - 1, ECCodeLevel.ToInt()] < length)
                {
                    return this.Configure(Version = i + 1);
                }
            }
            return this.Configure(Version = 1);
        }

        public QRCodeEncoder Complete()
        {
            return this.Configure(Capacity = QRCodeInfo.GetCapacity(Version, ECCodeLevel)).Configure(ECCodeGroup = ECCodeInfo.GetECCodeInfo(Version, ECCodeLevel));
        }

        public byte[] Encode(byte[] content, bool fill = true)
        {
            int Offset = 0;
            BitSet EncodedData = ContentEncode(content, fill);
            int TotalBlockCount = ECCodeGroup.BlocksInGroup1 + ECCodeGroup.BlocksInGroup2;
            byte[][] Content = new byte[TotalBlockCount][];
            byte[] RSEncode(int index, int length)
            {
                Span<byte> SubData = EncodedData.Bytes.AsSpan(Offset, length);
                Content[index] = SubData.ToArray();
                return XPolynom.RSEncode(SubData, ECCodeGroup.ECCodePerBytes);
            }
            byte[][] ECCode = new byte[TotalBlockCount][];
            for (int i = 0; i < ECCodeGroup.BlocksInGroup1; i++, Offset += ECCodeGroup.CodewordsInGroup1)
            {
                ECCode[i] = RSEncode(i, ECCodeGroup.CodewordsInGroup1);
            }
            for (int i = ECCodeGroup.BlocksInGroup1 + 1; i < TotalBlockCount; i++, Offset += ECCodeGroup.CodewordsInGroup2)
            {
                ECCode[i] = RSEncode(i, ECCodeGroup.CodewordsInGroup2);
            }
            return Interlock(Content, ECCode);
        }

        public BitSet ContentEncode(byte[] content, bool fill = true)
        {
            BitSet Binary = BinaryEncode(content);
            BitSet Result = BitSet.FromBitCount(Capacity * 8);
            Result.Write(0, EncodeMode.ToInt(), 4);
            Result.Write(4, content.Length, BitsOfDataLength);
            Result.Write(4 + BitsOfDataLength, Binary, 0, Binary.Count);
            if (fill)
            {
                int Index = (GetBitCount(content.Length) + 7) & ~7;
                while (Index < Result.Count)
                {
                    Result.Write(Index, 0b11101100, 8);
                    if ((Index += 8) < Result.Count)
                    {
                        Result.Write(Index, 0b00010001, 8);
                        Index += 8;
                    }
                }
            }
            return Result;
        }

        public int GetBitCount(int length)
        {
            int ValidBitCount = 4 + BitsOfDataLength + GetValidBitCount(length);
            return ValidBitCount + Math.Min(4, Capacity * 8 - ValidBitCount);
        }

        public static byte[] Interlock(byte[][] content, byte[][] eccode)
        {
            int ECCodeCount = eccode.AllLength();
            int ContentCount = content.AllLength();
            byte[] Result = new byte[ContentCount + ECCodeCount];
            void AppendData(byte[][] data, int index)
            {
                int Count = data.First().Length;
                for (int i = Count - 1; i >= 0; i--)
                {
                    for (int k = data.Length - 1; k >= 0; k--)
                    {
                        Result[index--] = data[k][i];
                    }
                }
            }
            AppendData(content, ContentCount - 1);
            AppendData(eccode, ContentCount + ECCodeCount - 1);
            return Result;
        }
    }
}