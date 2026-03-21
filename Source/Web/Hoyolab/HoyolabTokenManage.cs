using StarRailDamage.Source.Core.Abstraction;
using StarRailDamage.Source.Core.Setting;
using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Service.Encode.Encrypt;
using StarRailDamage.Source.Service.Encode.Hashing;
using StarRailDamage.Source.Service.IO;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
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

        public static ImmutableArray<HoyolabToken> Tokens => (field.IsDefault && TryLoad(out field)).Captured(field);

        public static bool TryGetTokenOrFirst(string? aid, [NotNullWhen(true)] out HoyolabToken? hoyolabToken)
        {
            return string.IsNullOrEmpty(aid) ? Tokens.TryGetFirst(out hoyolabToken) : TryGetToken(aid, out hoyolabToken);
        }

        public static bool TryGetToken(string aid, [NotNullWhen(true)] out HoyolabToken? hoyolabToken)
        {
            return Tokens.TryGetFirst(Token => Token.Aid == aid, out hoyolabToken);
        }

        public static bool TryLoad([NotNullWhen(true)] out ImmutableArray<HoyolabToken> tokens)
        {
            try
            {
                return true.Configure(tokens = Load());
            }
            catch
            {
                return false.Configure(tokens = default);
            }
        }

        public static ImmutableArray<HoyolabToken> Load()
        {
            return JsonSerializer.Deserialize<ImmutableArray<HoyolabToken>>(File.ReadAllText(GetFilePath()), JsonOptions);
        }

        public static void Save(params HoyolabToken[] hoyolabTokens)
        {
            File.WriteAllText(FileManage.BuildFilePath(GetFilePath()), JsonSerializer.Serialize(hoyolabTokens, JsonOptions));
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
                        try
                        {
                            HoyolabTokens[TokenType] = Encoding.GetString(Algorithm.DecryptFromBase64String(Token));
                        }
                        catch
                        {
                            HoyolabTokens[TokenType] = string.Empty;
                        }
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
                    writer.WriteString(Token.Key.ToString(), Algorithm.Initialize().EncryptToBase64String(Encoding.GetBytes(Token.Value)));
                }
                writer.WriteEndObject();
            }
        }
    }
}