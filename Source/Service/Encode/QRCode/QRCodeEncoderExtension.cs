using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.Service.Encode.QRCode
{
    public static class QRCodeEncoderExtension
    {
        public static QRCodeEncoder SetVersion(this QRCodeEncoder encoder, int version)
        {
            return encoder.Configure(encoder.Version = version);
        }

        public static QRCodeEncoder SetECCodeLevel(this QRCodeEncoder encoder, ECCodeLevel level)
        {
            return encoder.Configure(encoder.ECCodeLevel = level);
        }
    }
}