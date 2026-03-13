using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.Share
{
    public class ShareResponseWrapper
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("icon")]
        public string Icon { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }
}