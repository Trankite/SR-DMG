using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostResponsePostExtraUgcMaster
    {
        [JsonPropertyName("game_uid")]
        public string GameUid { get; set; } = string.Empty;

        [JsonPropertyName("game_region")]
        public string GameRegion { get; set; } = string.Empty;
    }
}