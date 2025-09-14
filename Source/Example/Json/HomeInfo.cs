using SR_DMG.Source.Employ;
using System.Text.Json.Serialization;

namespace SR_DMG.Source.Example.Json
{
    public class HomeInfo
    {
        public static async Task<HomeInfo> FormUrl()
        {
            Internet Request = new()
            {
                Url = $"{Mihomo.Url_Wiki_Home}home/content/list?app_sn=sr_wiki&channel_id=17"
            };
            string Json = await Request.Result() ?? throw Simple.Exp_Network_Error;
            return Program.FormJson<HomeInfo>(Json);
        }

        [JsonPropertyName("retcode")] public int Retcode;
        [JsonPropertyName("message")] public string Message = string.Empty;
        [JsonPropertyName("data")] public TData Data = new();
        public class TData
        {
            [JsonPropertyName("list")] public TChildren_Item[] List = [];
            public class TChildren_Item
            {
                [JsonPropertyName("id")] public int Id;
                [JsonPropertyName("name")] public string Name = string.Empty;
                [JsonPropertyName("parent_id")] public int Parent_Id;
                [JsonPropertyName("depth")] public int Depth;
                [JsonPropertyName("ch_ext")] public string Ch_Ext = string.Empty;
                [JsonPropertyName("children")] public TChildren_Item[] Children = [];
                [JsonPropertyName("list")] public TList_Item[] List = [];
                [JsonPropertyName("layout")] public string Layout = string.Empty;
                [JsonPropertyName("entry_limit")] public int Entry_Limit;
                [JsonPropertyName("hidden")] public bool Hidden;
                public class TList_Item
                {
                    [JsonPropertyName("content_id")] public int Content_Id;
                    [JsonPropertyName("title")] public string Title = string.Empty;
                    [JsonPropertyName("ext")] public string Ext = string.Empty;
                    [JsonPropertyName("icon")] public string Icon = string.Empty;
                    [JsonPropertyName("bbs_url")] public string BBS_Url = string.Empty;
                    [JsonPropertyName("article_user_name")] public string Article_User_Name = string.Empty;
                    [JsonPropertyName("article_time")] public string Article_Time = string.Empty;
                    [JsonPropertyName("avatar_url")] public string Avatar_Url = string.Empty;
                    [JsonPropertyName("summary")] public string Summary = string.Empty;
                    [JsonPropertyName("alias_name")] public string Alias_Name = string.Empty;
                    [JsonPropertyName("corner_mark")] public string Corner_Mark = string.Empty;
                }
            }
        }
    }
}