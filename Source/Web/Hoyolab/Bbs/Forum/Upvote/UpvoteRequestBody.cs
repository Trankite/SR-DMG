using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.Upvote
{
    public class UpvoteRequestBody
    {
        [JsonPropertyName("csm_source")]
        public string CsmSource { get; set; } = string.Empty;

        [JsonPropertyName("is_cancel")]
        public bool IsCancel { get; set; }

        [JsonPropertyName("post_id")]
        public string PostId { get; set; } = string.Empty;

        [JsonPropertyName("upvote_type")]
        public string UpvoteType { get; set; } = string.Empty;

        public UpvoteRequestBody() { }

        public UpvoteRequestBody(string source, bool isCancel, string postId, string upvoteType)
        {
            CsmSource = source;
            IsCancel = isCancel;
            PostId = postId;
            UpvoteType = upvoteType;
        }
    }
}