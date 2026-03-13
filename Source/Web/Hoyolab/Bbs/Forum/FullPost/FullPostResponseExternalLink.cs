using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostResponseExternalLink
    {
        [JsonPropertyName("external_link")]
        public string ExternalLink { get; set; } = string.Empty;

        [JsonPropertyName("external_link_title")]
        public string ExternalLinkTitle { get; set; } = string.Empty;
    }
}