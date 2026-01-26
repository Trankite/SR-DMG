using System.Security.Cryptography;
using System.Text;

namespace StarRailDamage.Source.Service.Encode.Hashing
{
    public static class HashMethod
    {
        public static string ToHexString(HashAlgorithmName hashAlgorithm, string input)
        {
            return Convert.ToHexString(HashData(hashAlgorithm, input));
        }

        public static string ToHexStringLower(HashAlgorithmName hashAlgorithm, string input)
        {
            return Convert.ToHexStringLower(HashData(hashAlgorithm, input));
        }

        public static byte[] HashData(HashAlgorithmName hashAlgorithm, string input)
        {
            return CryptographicOperations.HashData(hashAlgorithm, Encoding.UTF8.GetBytes(input));
        }

        public static byte[] HashData(HashAlgorithmName hashAlgorithm, byte[] input)
        {
            return CryptographicOperations.HashData(hashAlgorithm, input);
        }
    }
}