using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.DataStruct;

namespace StarRailDamage.Source.Service.Encode.Encrypt
{
    public static class AESAlgorithmExtension
    {
        public static string EncryptToBase64String(this AESAlgorithm algorithm, byte[] data)
        {
            return Convert.ToBase64String(algorithm.IV) + '-' + Convert.ToBase64String(algorithm.Encrypt(data));
        }

        public static byte[] DecryptFromBase64String(this AESAlgorithm algorithm, string data)
        {
            FrozenSpan<char, char> AesInfo = data.FirstSplit('-');
            algorithm.IV = Convert.FromBase64String(AesInfo.Content.ToString());
            return algorithm.Decrypt(Convert.FromBase64String(AesInfo.Extend.ToString()));
        }
    }
}