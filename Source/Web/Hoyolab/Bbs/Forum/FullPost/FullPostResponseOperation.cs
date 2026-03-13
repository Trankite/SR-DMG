using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostResponseOperation
    {
        [JsonPropertyName("attitude")]
        public int Attitude { get; set; }

        [JsonPropertyName("is_collected")]
        public bool IsCollected { get; set; }

        [JsonPropertyName("upvote_type")]
        public int UpvoteType { get; set; }
    }
}