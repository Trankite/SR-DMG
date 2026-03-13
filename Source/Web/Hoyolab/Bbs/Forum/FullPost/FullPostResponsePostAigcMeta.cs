using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostResponsePostAigcMeta
    {
        [JsonPropertyName("PropagateID")]
        public string PropagateID { get; set; } = string.Empty;

        [JsonPropertyName("AIGCLabel")]
        public string AIGCLabel { get; set; } = string.Empty;

        [JsonPropertyName("ContentPropagator")]
        public string ContentPropagator { get; set; } = string.Empty;
    }
}