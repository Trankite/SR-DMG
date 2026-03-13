using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostResponseUserLevel
    {
        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("exp")]
        public int Experience { get; set; }
    }
}