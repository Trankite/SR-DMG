using StarRailDamage.Source.Service.Encode.Hashing;
using System.Security.Cryptography;

namespace StarRailDamage.Source.Web.Hoyolab.DataSign
{
    public static class DataSignOptionsExtension
    {
        public static string GetDataSign(this DataSignOptions options)
        {
            long UnixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            string Content = $"salt={options.Salt}&t={UnixTime}&r={options.Factor}";
            if (options.Algorithm >= DataSignAlgorithm.Gen2)
            {
                Content = $"{Content}&b={options.Body}&q={options.Query}";
            }
            string Check = HashMethod.ToHexStringLower(HashAlgorithmName.MD5, Content);
            return $"{UnixTime},{options.Factor},{Check}";
        }
    }
}