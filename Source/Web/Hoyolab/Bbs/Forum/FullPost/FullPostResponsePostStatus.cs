using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostResponsePostStatus
    {
        [JsonPropertyName("is_top")]
        public bool IsTop { get; set; }

        [JsonPropertyName("is_good")]
        public bool IsGood { get; set; }

        [JsonPropertyName("is_official")]
        public bool IsOfficial { get; set; }

        [JsonPropertyName("post_status")]
        public int PostStatus { get; set; }
    }
}