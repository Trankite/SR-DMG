using SR_DMG.Source.Employ;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SR_DMG.Source.Example.Json
{
    public class EquipInfo
    {
        const string Markey = "partKey";

        public static EquipInfo[] FromJson(string Json)
        {
            return JsonSerializer.Deserialize<EquipInfo[]>(Json, JsonOption) ?? [];
        }

        [JsonPropertyName(Markey)] public string PartKey = string.Empty;
        [JsonPropertyName("tmplKey")] public string TmplKey = string.Empty;

        public class Main : EquipInfo
        {
            [JsonPropertyName("data")] public TData Data = new();
            public class TData
            {
                [JsonPropertyName("layout_")] public string Layout = string.Empty;
                [JsonPropertyName("career")] public string Career = string.Empty;
                [JsonPropertyName("rate")] public string Rate = string.Empty;
                [JsonPropertyName("skill")] public string Skill = string.Empty;
                [JsonPropertyName("desc")] public string Desc = string.Empty;
                [JsonPropertyName("image")] public string Image = string.Empty;
            }
        }

        public class Value : EquipInfo
        {
            [JsonPropertyName("data")] public TData Data = new();
            public class TData
            {
                [JsonPropertyName("layout_")] public string Layout = string.Empty;
                [JsonPropertyName("material")] public TMaterial_Item[] Material = [];
                public class TMaterial_Item
                {
                    [JsonPropertyName("name")] public string Name = string.Empty;
                    [JsonPropertyName("life")] public int Life;
                    [JsonPropertyName("attack")] public int Attack;
                    [JsonPropertyName("defense")] public int Defense;
                }
            }
        }

        public class Material : EquipInfo
        {
            [JsonPropertyName("data")] public TData Data = new();
            public class TData
            {
                [JsonPropertyName("layout_")] public string Layout = string.Empty;
                [JsonPropertyName("attr")] public TAttr_Item[] Attr = [];
                public class TAttr_Item
                {
                    [JsonPropertyName("name_")] public string Name = string.Empty;
                    [JsonPropertyName("material")] public TMaterial_Item[] TMaterial = [];
                    public class TMaterial_Item
                    {
                        [JsonPropertyName("name")] public string Name = string.Empty;
                        [JsonPropertyName("num")] public string Num = string.Empty;
                        [JsonPropertyName("url")] public string Url = string.Empty;
                        [JsonPropertyName("icon")] public string Icon = string.Empty;
                    }
                }
            }
        }

        public class Desc : EquipInfo
        {
            [JsonPropertyName("data")] public TData Data = new();
            public class TData
            {
                [JsonPropertyName("layout_")] public string Layout = string.Empty;
                [JsonPropertyName("title")] public string Title = string.Empty;
                [JsonPropertyName("text")] public string Text = string.Empty;
            }
        }

        private static readonly JsonSerializerOptions JsonOption = new() { IncludeFields = true, Converters = { new EquipInfoConverter() } };

        public class EquipInfoConverter : JsonConverter<EquipInfo>
        {
            public override EquipInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                using JsonDocument Doc = JsonDocument.ParseValue(ref reader);
                JsonElement Root = Doc.RootElement;
                return (Root.TryGetProperty(Markey, out JsonElement TypeProp) ? TypeProp.GetString() : null) switch
                {
                    "main" => JsonSerializer.Deserialize<Main>(Root.GetRawText(), options),
                    "value" => JsonSerializer.Deserialize<Value>(Root.GetRawText(), options),
                    "material" => JsonSerializer.Deserialize<Material>(Root.GetRawText(), options),
                    "desc" => JsonSerializer.Deserialize<Desc>(Root.GetRawText(), options),
                    _ => JsonSerializer.Deserialize<EquipInfo>(Root.GetRawText(), Simple.JsonOptions)
                };
            }

            public override void Write(Utf8JsonWriter writer, EquipInfo value, JsonSerializerOptions options)
            {
                JsonSerializer.Serialize(writer, value, options);
            }
        }
    }
}