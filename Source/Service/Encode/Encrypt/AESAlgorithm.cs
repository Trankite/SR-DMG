using StarRailDamage.Source.Extension;
using System.IO;
using System.Security.Cryptography;

namespace StarRailDamage.Source.Service.Encode.Encrypt
{
    public sealed class AESAlgorithm : IDisposable
    {
        private readonly Aes Algorithm = Aes.Create();

        public byte[] Key
        {
            get => Algorithm.Key;
            set => Algorithm.Key = value;
        }

        public byte[] IV
        {
            get => Algorithm.IV;
            set => Algorithm.IV = value;
        }

        public CipherMode Mode
        {
            get => Algorithm.Mode;
            set => Algorithm.Mode = value;
        }

        public AESAlgorithm() { }

        public AESAlgorithm(byte[] key, CipherMode mode)
        {
            Key = key;
            Mode = mode;
        }

        public AESAlgorithm(byte[] key, byte[] iv, CipherMode mode)
        {
            Key = key;
            IV = iv;
            Mode = mode;
        }

        public byte[] Encrypt(byte[] input) => Encrypt(input, IV);

        public byte[] Encrypt(byte[] input, byte[] iv)
        {
            ICryptoTransform Encryptor = Algorithm.CreateEncryptor(Key, iv);
            using MemoryStream MemoryStream = new();
            using (CryptoStream CryptoStream = new(MemoryStream, Encryptor, CryptoStreamMode.Write))
            {
                CryptoStream.Write(input, 0, input.Length);
            }
            return MemoryStream.ToArray();
        }

        public byte[] Decrypt(byte[] input) => Decrypt(input, IV);

        public byte[] Decrypt(byte[] input, byte[] iv)
        {
            ICryptoTransform Decryptor = Algorithm.CreateDecryptor(Key, iv);
            using MemoryStream OutMemoryStream = new();
            using MemoryStream MemoryStream = new(input);
            using (CryptoStream CryptoStream = new(MemoryStream, Decryptor, CryptoStreamMode.Read))
            {
                CryptoStream.CopyTo(OutMemoryStream);
            }
            return OutMemoryStream.ToArray();
        }

        public byte[] CreateIV()
        {
            return IV.Configure(x => Algorithm.GenerateIV());
        }

        public void Dispose()
        {
            Algorithm.Dispose();
        }
    }
}