using SR_DMG.Source.Employ;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SR_DMG.Source.Example.Json
{
    public class EnemyInfo
    {
        const string Markey = "partKey";

        public static EnemyInfo[] FromJson(string Json)
        {
            return JsonSerializer.Deserialize<EnemyInfo[]>(Json, JsonOption) ?? [];
        }

        [JsonPropertyName(Markey)] public string PartKey = string.Empty;
        [JsonPropertyName("tmplKey")] public string TmplKey = string.Empty;

        public class Main : EnemyInfo
        {
            [JsonPropertyName("data")] public TData Data = new();
            public class TData
            {
                [JsonPropertyName("layout_")] public string Layout = string.Empty;
                [JsonPropertyName("name")] public string Name = string.Empty;
                [JsonPropertyName("weak")] public string Weak = string.Empty;
                [JsonPropertyName("kind")] public string Kind = string.Empty;
                [JsonPropertyName("area")] public string Area = string.Empty;
                [JsonPropertyName("material")] public TMaterial_Item[] Material = [];
                [JsonPropertyName("resistance")] public string Resistance = string.Empty;
                [JsonPropertyName("introduce")] public string Introduce = string.Empty;
                [JsonPropertyName("image")] public string Image = string.Empty;
                public class TMaterial_Item
                {
                    [JsonPropertyName("name")] public string Name = string.Empty;
                    [JsonPropertyName("num")] public string Num = string.Empty;
                    [JsonPropertyName("url")] public string Url = string.Empty;
                    [JsonPropertyName("icon")] public string Icon = string.Empty;
                }
            }
        }

        public class Skill : EnemyInfo
        {
            [JsonPropertyName("data")] public TData Data = new();
            public class TData
            {
                [JsonPropertyName("layout_")] public string Layout = string.Empty;
                [JsonPropertyName("name")] public string Name = string.Empty;
                [JsonPropertyName("attr")] public TAttr_Item[] Attr = [];
                public class TAttr_Item
                {
                    [JsonPropertyName("name_")] public string Name = string.Empty;
                    [JsonPropertyName("title")] public string Title = string.Empty;
                    [JsonPropertyName("introduction")] public string Introduction = string.Empty;
                    [JsonPropertyName("gif")] public string Gif = string.Empty;
                }
            }
        }

        public class GainMethod : EnemyInfo
        {
            [JsonPropertyName("data")] public TData Data = new();
            public class TData
            {
                [JsonPropertyName("layout_")] public string Layout = string.Empty;
                [JsonPropertyName("title")] public string Title = string.Empty;
                [JsonPropertyName("gainMethod")] public TMethod_Item[] Method = [];
                public class TMethod_Item
                {
                    [JsonPropertyName("key")] public string Key = string.Empty;
                    [JsonPropertyName("value")] public string Value = string.Empty;
                }
            }
        }

        private static readonly JsonSerializerOptions JsonOption = new() { IncludeFields = true, Converters = { new EnemyInfoConverter() } };

        public class EnemyInfoConverter : JsonConverter<EnemyInfo>
        {
            public override EnemyInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                using JsonDocument Doc = JsonDocument.ParseValue(ref reader);
                JsonElement Root = Doc.RootElement;
                return (Root.TryGetProperty(Markey, out JsonElement TypeProp) ? TypeProp.GetString() : null) switch
                {
                    "main" => JsonSerializer.Deserialize<Main>(Root.GetRawText(), options),
                    "skill" => JsonSerializer.Deserialize<Skill>(Root.GetRawText(), options),
                    "gainMethod" => JsonSerializer.Deserialize<GainMethod>(Root.GetRawText(), options),
                    _ => JsonSerializer.Deserialize<EnemyInfo>(Root.GetRawText(), Simple.JsonOptions)
                };
            }

            public override void Write(Utf8JsonWriter writer, EnemyInfo value, JsonSerializerOptions options)
            {
                JsonSerializer.Serialize(writer, value, options);
            }
        }
    }
}