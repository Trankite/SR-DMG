namespace StarRailDamage.Source.Service.Encode.QRCode
{
    public enum EncodeMode : byte
    {
        Numeric = 0b0001,
        Alphanumeric = 0b0010,
        Byte = 0b0100
    }
}