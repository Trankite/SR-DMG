using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostResponseStatUpvote
    {
        [JsonPropertyName("upvote_type")]
        public int UpvoteType { get; set; }

        [JsonPropertyName("upvote_cnt")]
        public int UpvoteCount { get; set; }
    }
}