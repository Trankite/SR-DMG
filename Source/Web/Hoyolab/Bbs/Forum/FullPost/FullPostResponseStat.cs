using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostResponseStat
    {
        [JsonPropertyName("view_num")]
        public int ViewNum { get; set; }

        [JsonPropertyName("reply_num")]
        public int ReplyNum { get; set; }

        [JsonPropertyName("like_num")]
        public int LikeNum { get; set; }

        [JsonPropertyName("bookmark_num")]
        public int BookmarkNum { get; set; }

        [JsonPropertyName("forward_num")]
        public int ForwardNum { get; set; }

        [JsonPropertyName("post_upvote_stat")]
        public ImmutableArray<FullPostResponseStatUpvote> PostUpvoteStat { get; set; }

        [JsonPropertyName("original_like_num")]
        public int OriginalLikeNum { get; set; }

        [JsonPropertyName("share_num")]
        public int ShareNum { get; set; }
    }
}