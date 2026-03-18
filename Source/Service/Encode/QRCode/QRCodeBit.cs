namespace StarRailDamage.Source.Service.Encode.QRCode
{
    public struct QRCodeBit
    {
        private byte Bits;

        public bool HasBit
        {
            readonly get => (Bits & 1) != 0;
            set => Bits = (byte)(value ? Bits | 1 : Bits & 0xfe);
        }

        public QRCodeBitType Type
        {
            readonly get => (QRCodeBitType)(Bits >> 1);
            set => Bits = (byte)(((int)value << 1) | (Bits & 1));
        }

        public void SetValue(bool hasBit, QRCodeBitType type)
        {
            Bits = (byte)(((int)type << 1) | (hasBit ? 1 : 0));
        }
    }
}