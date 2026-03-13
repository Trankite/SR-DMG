using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.Upvote
{
    public class UpvoteResponseWrapper
    {
        [JsonPropertyName("current_upvote_type")]
        public int CurrentUpvoteType { get; set; }
    }
}