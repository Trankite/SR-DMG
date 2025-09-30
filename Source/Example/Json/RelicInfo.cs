using SR_DMG.Source.Employ;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SR_DMG.Source.Example.Json
{
    public class RelicInfo
    {
        const string Markey = "partKey";

        public static RelicInfo[] FromJson(string Json)
        {
            return JsonSerializer.Deserialize<RelicInfo[]>(Json, JsonOptions) ?? [];
        }

        [JsonPropertyName(Markey)] public string PartKey = string.Empty;
        [JsonPropertyName("tmplKey")] public string TmplKey = string.Empty;

        public class Main : RelicInfo
        {
            [JsonPropertyName("data")] public TData Data = new();
            public class TData
            {
                [JsonPropertyName("layout_")] public string Layout = string.Empty;
                [JsonPropertyName("content")] public TContent_Item[] Content = [];
                [JsonPropertyName("descTitle")] public string DescTitle = string.Empty;
                [JsonPropertyName("desc")] public string Desc = string.Empty;
                [JsonPropertyName("title")] public string Title = string.Empty;
                [JsonPropertyName("story")] public string Story = string.Empty;
                [JsonPropertyName("image")] public string Image = string.Empty;
                public class TContent_Item
                {
                    [JsonPropertyName("name")] public string Name = string.Empty;
                    [JsonPropertyName("value")] public string Value = string.Empty;
                }
            }
        }

        public class Timeline : RelicInfo
        {
            [JsonPropertyName("data")] public TData Data = new();
            public class TData
            {
                [JsonPropertyName("layout_")] public string Layout = string.Empty;
                [JsonPropertyName("attr")] public TAttr_Item[] Attr = [];
                public class TAttr_Item
                {
                    [JsonPropertyName("name_")] public string Name = string.Empty;
                    [JsonPropertyName("detail")] public TDetail_Item[] Detail = [];
                    public class TDetail_Item
                    {
                        [JsonPropertyName("name")] public string Name = string.Empty;
                        [JsonPropertyName("extend_")] public TExtend_Item[] Extend = [];
                        public class TExtend_Item
                        {
                            [JsonPropertyName("name")] public string Name = string.Empty;
                            [JsonPropertyName("content")] public string Content = string.Empty;
                        }
                    }
                }
            }
        }

        private static readonly JsonSerializerOptions JsonOptions = Program.GetJsonOptions(new RelicInfoConverter());

        public class RelicInfoConverter : JsonConverter<RelicInfo>
        {
            public override RelicInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                using JsonDocument Document = JsonDocument.ParseValue(ref reader);
                JsonElement RootElement = Document.RootElement;
                return (RootElement.TryGetProperty(Markey, out JsonElement TypeProp) ? TypeProp.GetString() : null) switch
                {
                    "main" => JsonSerializer.Deserialize<Main>(RootElement.GetRawText(), options),
                    "timeline" => JsonSerializer.Deserialize<Timeline>(RootElement.GetRawText(), options),
                    _ => Program.FormJson<RelicInfo>(RootElement.GetRawText())
                };
            }

            public override void Write(Utf8JsonWriter writer, RelicInfo value, JsonSerializerOptions options)
            {
                JsonSerializer.Serialize(writer, value, options);
            }
        }
    }
}