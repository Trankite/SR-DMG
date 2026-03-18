using StarRailDamage.Source.Service.Encode.QRCode.Encoder;

namespace StarRailDamage.Source.Service.Encode.QRCode
{
    public static class EncodeModeExtension
    {
        public static EncodeMode GetAutoMode(byte[] data)
        {
            EncodeMode Current = EncodeMode.Numeric;
            foreach (byte Byte in data)
            {
                if (AlphanumericEncoder.IsAlphanumeric(Byte))
                {
                    if (Byte < '0' || Byte > '9')
                    {
                        Current = EncodeMode.Alphanumeric;
                    }
                }
                else
                {
                    return EncodeMode.Byte;
                }
            }
            return Current;
        }

        public static QRCodeEncoder CreateEncoder(this EncodeMode mode)
        {
            return mode switch
            {
                EncodeMode.Numeric => new NumericEncoder(),
                EncodeMode.Alphanumeric => new AlphanumericEncoder(),
                EncodeMode.Byte => new ByteEncoder(),
                _ => throw new NotSupportedException($"Unkonw EncodeMode:{mode}")
            };
        }
    }
}