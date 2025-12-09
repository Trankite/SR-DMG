using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Home
{
    public class HomeContentChildren
    {
        [JsonPropertyName("content_id")]
        public int ContentId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("ext")]
        public string Extension { get; set; } = string.Empty;

        [JsonPropertyName("icon")]
        public string Icon { get; set; } = string.Empty;

        [JsonPropertyName("bbs_url")]
        public string BbsUrl { get; set; } = string.Empty;

        [JsonPropertyName("article_user_name")]
        public string ArticleUserName { get; set; } = string.Empty;

        [JsonPropertyName("article_time")]
        public string ArticleTime { get; set; } = string.Empty;

        [JsonPropertyName("avatar_url")]
        public string AvatarUrl { get; set; } = string.Empty;

        [JsonPropertyName("summary")]
        public string Summary { get; set; } = string.Empty;

        [JsonPropertyName("alias_name")]
        public string AliasName { get; set; } = string.Empty;

        [JsonPropertyName("corner_mark")]
        public string CornerMark { get; set; } = string.Empty;
    }
}