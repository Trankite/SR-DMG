using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostResponsePostExtraMinosAigc
    {
        [JsonPropertyName("is_aigc")]
        public bool IsAigc { get; set; }
    }
}