using SR_DMG.Source.Employ;
using System.Text.Json.Serialization;

namespace SR_DMG.Source.Example.Json
{
    public class PageInfo
    {
        public static async Task<PageInfo> FormUrl(int Content_Id)
        {
            Internet Request = new()
            {
                Url = $"{Mihomo.Url_Wiki_Home}content/info?app_sn=sr_wiki&content_id={Content_Id}"
            };
            string Json = await Request.Result() ?? throw Simple.Exp_Network_Error;
            return Program.FormJson<PageInfo>(Json);
        }

        [JsonPropertyName("retcode")] public int Retcode;
        [JsonPropertyName("message")] public string Message = string.Empty;
        [JsonPropertyName("data")] public TData Data = new();
        public class TData
        {
            [JsonPropertyName("content")] public TContent Content = new();
            [JsonPropertyName("channel_list")] public TChannel_Item[] Channel_List = [];
            public class TContent
            {
                [JsonPropertyName("id")] public int Id;
                [JsonPropertyName("title")] public string Title = string.Empty;
                [JsonPropertyName("content")] public string Content = string.Empty;
                [JsonPropertyName("ext")] public string Ext = string.Empty;
                [JsonPropertyName("author_name")] public string Author_Name = string.Empty;
                [JsonPropertyName("editor_name")] public string Editor_Name = string.Empty;
                [JsonPropertyName("ctime")] public string Ctime = string.Empty;
                [JsonPropertyName("mtime")] public string Mtime = string.Empty;
                [JsonPropertyName("version")] public int Version;
                [JsonPropertyName("icon")] public string Icon = string.Empty;
                [JsonPropertyName("summary")] public string Summary = string.Empty;
                [JsonPropertyName("url")] public string Url = string.Empty;
                [JsonPropertyName("type")] public int Type;
                [JsonPropertyName("bbs_url")] public string BBS_Url = string.Empty;
                [JsonPropertyName("article_user_name")] public string Article_User_Name = string.Empty;
                [JsonPropertyName("article_time")] public string Article_Time = string.Empty;
                [JsonPropertyName("avatar_url")] public string Avatar_Url = string.Empty;
                [JsonPropertyName("contents")] public TContent_Item[] Contents = [];
                [JsonPropertyName("forbid_correct_error")] public bool Forbid_Correct_Error;
                [JsonPropertyName("tmp_type")] public string Tmp_Type = string.Empty;
                [JsonPropertyName("rpg_new_tmp_content")] public TRpg_Content Rpg_Content = new();
                public class TContent_Item
                {
                    [JsonPropertyName("name")] public string Name = string.Empty;
                    [JsonPropertyName("text")] public string Text = string.Empty;
                }
                public class TRpg_Content
                {
                    [JsonPropertyName("type")] public string Type = string.Empty;
                    [JsonPropertyName("base")] public TBase Base = new();
                    [JsonPropertyName("modules")] public TModule_Item[] Modules = [];
                    [JsonPropertyName("tabs")] public Ttab_Item[] Tabs = [];
                    public class TBase
                    {
                        [JsonPropertyName("userInfo")] public TUserInfo UserInfo = new();
                        public class TUserInfo
                        {
                            [JsonPropertyName("avatarId")] public string AvatarId = string.Empty;
                            [JsonPropertyName("name")] public string Name = string.Empty;
                            [JsonPropertyName("icon")] public string Icon = string.Empty;
                            [JsonPropertyName("figurePath")] public string FigurePath = string.Empty;
                            [JsonPropertyName("baseType")] public string BaseType = string.Empty;
                            [JsonPropertyName("elementId")] public string ElementId = string.Empty;
                            [JsonPropertyName("rarity")] public int Rarity;
                            [JsonPropertyName("isFigurePath")] public bool IsFigurePath;
                        }
                    }
                    public class TModule_Item
                    {
                        [JsonPropertyName("id")] public string Id = string.Empty;
                        [JsonPropertyName("name")] public string Name = string.Empty;
                        [JsonPropertyName("switch")] public bool Switch;
                        [JsonPropertyName("components")] public TComponent_Item[] Components = [];
                        public class TComponent_Item
                        {
                            [JsonPropertyName("componentId")] public string ComponentId = string.Empty;
                            [JsonPropertyName("layout")] public string Layout = string.Empty;
                            [JsonPropertyName("data")] public string Data = string.Empty;
                            [JsonPropertyName("style")] public string Style = string.Empty;
                        }
                    }
                    public class Ttab_Item
                    {
                        [JsonPropertyName("type")] public string Type = string.Empty;
                    }
                }
            }
            public class TChannel_Item
            {
                [JsonPropertyName("slice")] public TSlice_Item[] Slice = [];
                public class TSlice_Item
                {
                    [JsonPropertyName("channel_id")] public int Channel_Id;
                    [JsonPropertyName("name")] public string Name = string.Empty;
                    [JsonPropertyName("ch_ext")] public string Ch_Ext = string.Empty;
                    [JsonPropertyName("is_hidden")] public bool Is_Hidden;
                }
            }
        }
    }
}