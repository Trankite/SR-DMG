using StarRailDamage.Source.Core.Abstraction;
using StarRailDamage.Source.Core.Setting;
using StarRailDamage.Source.Service.Encode.Encrypt;
using StarRailDamage.Source.Service.Encode.Hashing;
using StarRailDamage.Source.Service.IO;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab
{
    public static class HoyolabTokenManage
    {
        private static readonly JsonSerializerOptions JsonOptions;

        private const string Salt = "B9176A0A08605E7EE16428AB13199AC2";

        public static List<HoyolabToken> Load()
        {
            string JsonString = File.ReadAllText(GetFilePath());
            List<HoyolabToken> HoyolabTokens = JsonSerializer.Deserialize<List<HoyolabToken>>(JsonString, JsonOptions) ?? [];
            return HoyolabTokens;
        }

        public static void Save(params HoyolabToken[] hoyolabTokens)
        {
            string FilePath = FileManage.BuildFilePath(GetFilePath());
            string JsonString = JsonSerializer.Serialize(hoyolabTokens, JsonOptions);
            File.WriteAllText(FilePath, JsonString);
        }

        public static string GetFilePath()
        {
            return Path.Combine(LocalSetting.LocalPath, "HoyolabToken.json");
        }

        private static AESAlgorithm GetAlgorithm()
        {
            return new AESAlgorithm(HashMethod.HashData(HashAlgorithmName.SHA256, AppSetting.UserSid + Salt), CipherMode.CBC);
        }

        static HoyolabTokenManage()
        {
            JsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            JsonOptions.Converters.Add(new JsonHoyolabTokenConverter());
        }

        private class JsonHoyolabTokenConverter : JsonConverter<Dictionary<HoyolabTokenType, string>>, IEmployedEncoding
        {
            public Encoding Encoding { get => Encoding.UTF8; }

            public override Dictionary<HoyolabTokenType, string>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                using AESAlgorithm Algorithm = GetAlgorithm();
                Dictionary<HoyolabTokenType, string> HoyolabTokens = [];
                while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
                {
                    string? TokenName = reader.GetString();
                    if (Enum.TryParse(TokenName, out HoyolabTokenType TokenType) && reader.Read())
                    {
                        string? Token = reader.GetString();
                        if (string.IsNullOrEmpty(Token)) continue;
                        HoyolabTokens[TokenType] = Encoding.GetString(Algorithm.DecryptFromBase64String(Token));
                    }
                }
                return HoyolabTokens;
            }

            public override void Write(Utf8JsonWriter writer, Dictionary<HoyolabTokenType, string> value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();
                using AESAlgorithm Algorithm = GetAlgorithm();
                foreach (KeyValuePair<HoyolabTokenType, string> Token in value)
                {
                    writer.WriteString(Token.Key.ToString(), Algorithm.EncryptToBase64String(Encoding.GetBytes(Token.Value)));
                }
                writer.WriteEndObject();
            }
        }
    }
}