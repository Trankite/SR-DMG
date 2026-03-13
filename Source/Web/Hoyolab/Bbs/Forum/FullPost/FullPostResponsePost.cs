using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostResponsePost
    {
        [JsonPropertyName("game_id")]
        public int GameId { get; set; }

        [JsonPropertyName("post_id")]
        public string PostId { get; set; } = string.Empty;

        [JsonPropertyName("f_forum_id")]
        public int ForumId { get; set; }

        [JsonPropertyName("uid")]
        public string Uid { get; set; } = string.Empty;

        [JsonPropertyName("subject")]
        public string Subject { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("cover")]
        public string Cover { get; set; } = string.Empty;

        [JsonPropertyName("view_type")]
        public int ViewType { get; set; }

        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }

        [JsonPropertyName("images")]
        public ImmutableArray<string> Images { get; set; }

        [JsonPropertyName("post_status")]
        public FullPostResponsePostStatus PostStatus { get; set; } = new();

        [JsonPropertyName("topic_ids")]
        public ImmutableArray<int> TopicIds { get; set; }

        [JsonPropertyName("view_status")]
        public int ViewStatus { get; set; }

        [JsonPropertyName("max_floor")]
        public int MaxFloor { get; set; }

        [JsonPropertyName("is_original")]
        public int IsOriginal { get; set; }

        [JsonPropertyName("republish_authorization")]
        public int RepublishAuthorization { get; set; }

        [JsonPropertyName("reply_time")]
        public string ReplyTime { get; set; } = string.Empty;

        [JsonPropertyName("is_deleted")]
        public int IsDeleted { get; set; }

        [JsonPropertyName("is_interactive")]
        public bool IsInteractive { get; set; }

        [JsonPropertyName("structured_content")]
        public string StructuredContent { get; set; } = string.Empty;

        [JsonPropertyName("structured_content_rows")]
        public ImmutableArray<FullPostResponsePostStructure> StructuredContentRows { get; set; }

        [JsonPropertyName("review_id")]
        public int ReviewId { get; set; }

        [JsonPropertyName("is_profit")]
        public bool IsProfit { get; set; }

        [JsonPropertyName("is_in_profit")]
        public bool IsInProfit { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; } = string.Empty;

        [JsonPropertyName("is_missing")]
        public bool IsMissing { get; set; }

        [JsonPropertyName("pre_pub_status")]
        public int PrePubStatus { get; set; }

        [JsonPropertyName("profit_post_status")]
        public int ProfitPostStatus { get; set; }

        [JsonPropertyName("is_showing_missing")]
        public bool IsShowingMissing { get; set; }

        [JsonPropertyName("block_reply_img")]
        public int BlockReplyImg { get; set; }

        [JsonPropertyName("is_mentor")]
        public bool IsMentor { get; set; }

        [JsonPropertyName("updated_at")]
        public int UpdatedAt { get; set; }

        [JsonPropertyName("deleted_at")]
        public int DeletedAt { get; set; }

        [JsonPropertyName("cate_id")]
        public int CateId { get; set; }

        [JsonPropertyName("audit_status")]
        public int AuditStatus { get; set; }

        [JsonPropertyName("meta_content")]
        public string MetaContent { get; set; } = string.Empty;

        [JsonPropertyName("block_latest_reply_time")]
        public int BlockLatestReplyTime { get; set; }

        [JsonPropertyName("selected_comment")]
        public int SelectedComment { get; set; }

        [JsonPropertyName("post_extra")]
        public FullPostResponsePostExtra PostExtra { get; set; } = new();

        [JsonPropertyName("ai_content_type")]
        public string AiContentType { get; set; } = string.Empty;

        [JsonPropertyName("user_ai_content_choice")]
        public string UserAiContentChoice { get; set; } = string.Empty;

        [JsonPropertyName("aigc_meta")]
        public FullPostResponsePostAigcMeta AigcMeta { get; set; } = new();
    }
}