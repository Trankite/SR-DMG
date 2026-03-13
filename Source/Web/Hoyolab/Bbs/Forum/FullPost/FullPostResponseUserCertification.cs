using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostResponseUserCertification
    {
        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;
    }
}