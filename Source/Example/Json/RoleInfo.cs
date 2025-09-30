using SR_DMG.Source.Employ;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SR_DMG.Source.Example.Json
{
    public class RoleInfo
    {
        const string Markey = "partKey";

        public static RoleInfo[] FromJson(string Json)
        {
            return JsonSerializer.Deserialize<RoleInfo[]>(Json, JsonOptions) ?? [];
        }

        [JsonPropertyName(Markey)] public string PartKey = string.Empty;
        [JsonPropertyName("tmplKey")] public string TmplKey = string.Empty;

        public class NewMain : RoleInfo
        {
            [JsonPropertyName("data")] public TData Data = new();
            public class TData
            {
                [JsonPropertyName("layout_")] public string Layout = string.Empty;
                [JsonPropertyName("name")] public string Name = string.Empty;
                [JsonPropertyName("mainFields")] public TMainField_Item[] MainFields = [];
                [JsonPropertyName("star")] public int Star;
                [JsonPropertyName("property")] public string Property = string.Empty;
                [JsonPropertyName("icon")] public string Icon = string.Empty;
                [JsonPropertyName("mobile")] public string Mobile = string.Empty;
                [JsonPropertyName("pc")] public string Pc = string.Empty;
                public class TMainField_Item
                {
                    [JsonPropertyName("nameL")] public string NameL = string.Empty;
                    [JsonPropertyName("nameR")] public string NameR = string.Empty;
                    [JsonPropertyName("valueL")] public string ValueL = string.Empty;
                    [JsonPropertyName("valueR")] public string ValueR = string.Empty;
                }
            }
        }

        public class Breach : RoleInfo
        {
            [JsonPropertyName("data")] public TData Data = new();
            public class TData
            {
                [JsonPropertyName("layout_")] public string Layout = string.Empty;
                [JsonPropertyName("title")] public string Title = string.Empty;
                [JsonPropertyName("attr")] public TAttr_Item[] Attr = [];
                public class TAttr_Item
                {
                    [JsonPropertyName("name_")] public string Name = string.Empty;
                    [JsonPropertyName("material")] public TMaterial_Item[] Material = [];
                    [JsonPropertyName("attr")] public TAttr_Item1[] Attr = [];
                    public class TMaterial_Item
                    {
                        [JsonPropertyName("icon")] public string Icon = string.Empty;
                        [JsonPropertyName("name")] public string Name = string.Empty;
                        [JsonPropertyName("num")] public string Num = string.Empty;
                        [JsonPropertyName("url")] public string Url = string.Empty;
                    }
                    public class TAttr_Item1
                    {
                        [JsonPropertyName("nameL")] public string NameL = string.Empty;
                        [JsonPropertyName("iconL")] public string IconL = string.Empty;
                        [JsonPropertyName("valueL")] public string ValueL = string.Empty;
                        [JsonPropertyName("nameR")] public string NameR = string.Empty;
                        [JsonPropertyName("iconR")] public string IconR = string.Empty;
                        [JsonPropertyName("valueR")] public string ValueR = string.Empty;
                    }
                }
            }
        }

        public class Trace : RoleInfo
        {
            [JsonPropertyName("data")] public TData Data = new();
            public class TData
            {
                [JsonPropertyName("layout_")] public string Layout = string.Empty;
                [JsonPropertyName("attr")] public TAttr Attr = new();
                public class TAttr
                {
                    [JsonPropertyName("path")] public string Path = string.Empty;
                    [JsonPropertyName("points")] public TPoint_Item[] Points = [];
                    [JsonPropertyName("roleId")] public string RoleId = string.Empty;
                    public class TPoint_Item
                    {
                        [JsonPropertyName("name")] public string Name = string.Empty;
                        [JsonPropertyName("desc")] public string Desc = string.Empty;
                        [JsonPropertyName("icon")] public string Icon = string.Empty;
                        [JsonPropertyName("tableData")] public TtableData TableData = new();
                        public class TtableData
                        {
                            [JsonPropertyName("headers")] public string[] Headers = [];
                            [JsonPropertyName("rows")] public string[][] Rows = [];
                        }
                    }
                }
            }
        }

        public class Desc : RoleInfo
        {
            [JsonPropertyName("data")] public TData Data = new();
            public class TData
            {
                [JsonPropertyName("layout_")] public string Layout = string.Empty;
                [JsonPropertyName("title")] public string Title = string.Empty;
                [JsonPropertyName("text")] public string Text = string.Empty;
            }
        }

        private static readonly JsonSerializerOptions JsonOptions = Program.GetJsonOptions(new RoleInfoConverter());

        public class RoleInfoConverter : JsonConverter<RoleInfo>
        {
            public override RoleInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                using JsonDocument Document = JsonDocument.ParseValue(ref reader);
                JsonElement RootElement = Document.RootElement;
                return (RootElement.TryGetProperty(Markey, out JsonElement TypeProperty) ? TypeProperty.GetString() : null) switch
                {
                    "newMain" => JsonSerializer.Deserialize<NewMain>(RootElement.GetRawText(), options),
                    "breach" => JsonSerializer.Deserialize<Breach>(RootElement.GetRawText(), options),
                    "trace" => JsonSerializer.Deserialize<Trace>(RootElement.GetRawText(), options),
                    "desc" => JsonSerializer.Deserialize<Desc>(RootElement.GetRawText(), options),
                    _ => Program.FormJson<RoleInfo>(RootElement.GetRawText())
                };
            }

            public override void Write(Utf8JsonWriter writer, RoleInfo value, JsonSerializerOptions options)
            {
                JsonSerializer.Serialize(writer, value, options);
            }
        }

        public class Role_Ascent
        {
            [JsonPropertyName("list")] public TItem[] List = [];
            public class TItem
            {
                [JsonPropertyName("tab_id")] public string Tab_Id = string.Empty;
                [JsonPropertyName("tabName")] public string TabName = string.Empty;
                [JsonPropertyName("materials")] public TMaterial_Item[] Materials = [];
                [JsonPropertyName("attr")] public TAttr_Item[] Attr = [];
                public class TMaterial_Item
                {
                    [JsonPropertyName("record_id")] public int Record_Id;
                    [JsonPropertyName("status")] public string Status = string.Empty;
                    [JsonPropertyName("icon")] public string Icon = string.Empty;
                    [JsonPropertyName("title")] public string Title = string.Empty;
                    [JsonPropertyName("version")] public int Version;
                    [JsonPropertyName("amount")] public string Amount = string.Empty;
                    [JsonPropertyName("id")] public int Id;
                    [JsonPropertyName("checked")] public bool Checked;
                    [JsonPropertyName("originIdx")] public int OriginIdx;
                    [JsonPropertyName("channels")] public TChannel_Item[] Channels = [];
                    public class TChannel_Item
                    {
                        [JsonPropertyName("channel_id")] public int Channel_Id;
                        [JsonPropertyName("channel_name")] public string Channel_Name = string.Empty;
                    }
                }
                public class TAttr_Item
                {
                    [JsonPropertyName("key")] public string Key = string.Empty;
                    [JsonPropertyName("value")] public string[] Value = [];
                }
            }
        }

        public class Role_Trace
        {
            [JsonPropertyName("path")] public string Path = string.Empty;
            [JsonPropertyName("points")] public TPoint_Item[] Points = [];
            [JsonPropertyName("roleId")] public string RoleId = string.Empty;
            public class TPoint_Item
            {
                [JsonPropertyName("icon")] public string Icon = string.Empty;
                [JsonPropertyName("name")] public string Name = string.Empty;
                [JsonPropertyName("tag")] public string Tag = string.Empty;
                [JsonPropertyName("subList")] public TSub_Item[] SubList = [];
                [JsonPropertyName("tableData")] public TtableData TableData = new();
                public class TSub_Item
                {
                    [JsonPropertyName("specialTag")] public string SpecialTag = string.Empty;
                    [JsonPropertyName("specialBg")] public string SpecialBg = string.Empty;
                    [JsonPropertyName("image")] public string Image = string.Empty;
                    [JsonPropertyName("subDesc")] public string SubDesc = string.Empty;
                    [JsonPropertyName("subTag")] public string[] SubTag = [];
                    [JsonPropertyName("subTitle")] public string SubTitle = string.Empty;
                    [JsonPropertyName("imageMeta")] public TImageMeta ImageMeta = new();
                    public class TImageMeta
                    {
                        [JsonPropertyName("image")] public TImage Image = new();
                        public class TImage
                        {
                            [JsonPropertyName("height")] public int Height;
                            [JsonPropertyName("url")] public string Url = string.Empty;
                            [JsonPropertyName("width")] public int Width;
                        }
                    }
                }
                public class TtableData
                {
                    [JsonPropertyName("headers")] public string[] Headers = [];
                    [JsonPropertyName("rows")] public string[][] Rows = [];
                }
            }
        }

        public class Role_Rank
        {
            [JsonPropertyName("richText")] public string RichText = string.Empty;
        }
    }
}