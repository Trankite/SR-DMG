using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace StarRailDamage.Source.Service.Encode.Encrypt
{
    public static class AesMethod
    {
        public static byte[] Decrypt(string input, byte[] key, byte[]? vector = null, CipherMode mode = default)
        {
            using Aes Algorithm = Aes.Create();
            Algorithm.Mode = mode;
            ICryptoTransform Decryptor = Algorithm.CreateDecryptor(key, vector);
            using MemoryStream OutMemoryStream = new();
            using MemoryStream MemoryStream = new(Convert.FromHexString(input));
            using (CryptoStream CryptoStream = new(MemoryStream, Decryptor, CryptoStreamMode.Read))
            {
                CryptoStream.CopyTo(OutMemoryStream);
            }
            return OutMemoryStream.ToArray();
        }

        public static byte[] Encrypt(string input, byte[] key, byte[]? vector = null, CipherMode mode = default)
        {
            using Aes Algorithm = Aes.Create();
            Algorithm.Mode = mode;
            ICryptoTransform Encryptor = Algorithm.CreateEncryptor(key, vector);
            using MemoryStream MemoryStream = new();
            using (CryptoStream CryptoStream = new(MemoryStream, Encryptor, CryptoStreamMode.Write))
            {
                CryptoStream.Write(Encoding.UTF8.GetBytes(input), 0, input.Length);
            }
            return MemoryStream.ToArray();
        }
    }
}