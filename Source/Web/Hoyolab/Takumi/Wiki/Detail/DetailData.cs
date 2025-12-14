using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Detail
{
    public class DetailData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("ext")]
        public string Extension { get; set; } = string.Empty;

        [JsonPropertyName("author_name")]
        public string AuthorName { get; set; } = string.Empty;

        [JsonPropertyName("editor_name")]
        public string EditorName { get; set; } = string.Empty;

        [JsonPropertyName("ctime")]
        public string CreateTime { get; set; } = string.Empty;

        [JsonPropertyName("mtime")]
        public string ModifyTime { get; set; } = string.Empty;

        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; } = string.Empty;

        [JsonPropertyName("summary")]
        public string Summary { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("bbs_url")]
        public string BbsUrl { get; set; } = string.Empty;

        [JsonPropertyName("article_user_name")]
        public string ArticleUserName { get; set; } = string.Empty;

        [JsonPropertyName("article_time")]
        public string ArticleTime { get; set; } = string.Empty;

        [JsonPropertyName("avatar_url")]
        public string AvatarUrl { get; set; } = string.Empty;

        [JsonPropertyName("contents")]
        public ImmutableArray<DetailDataContent> Contents { get; set; } 

        [JsonPropertyName("forbid_correct_error")]
        public bool ForbidCorrectError { get; set; }

        [JsonPropertyName("tmp_type")]
        public string TempType { get; set; } = string.Empty;

        [JsonPropertyName("rpg_new_tmp_content")]
        public DetailDataTemp? TempContent { get; set; }
    }
}