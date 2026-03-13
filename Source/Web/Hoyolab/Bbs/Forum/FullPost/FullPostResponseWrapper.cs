using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostResponseWrapper
    {
        [JsonPropertyName("post")]
        public FullPostResponsePost Post { get; set; } = new();

        [JsonPropertyName("forum")]
        public FullPostResponseForum Forum { get; set; } = new();

        [JsonPropertyName("topics")]
        public ImmutableArray<FullPostResponseTopic> Topics { get; set; }

        [JsonPropertyName("user")]
        public FullPostResponseUser User { get; set; } = new();

        [JsonPropertyName("self_operation")]
        public FullPostResponseOperation SelfOperation { get; set; } = new();

        [JsonPropertyName("stat")]
        public FullPostResponseStat Stat { get; set; } = new();

        [JsonPropertyName("help_sys")]
        public FullPostResponseHelp HelpSys { get; set; } = new();

        [JsonPropertyName("cover")]
        public FullPostResponseImage Cover { get; set; } = new();

        [JsonPropertyName("image_list")]
        public ImmutableArray<FullPostResponseImage> ImageList { get; set; }

        [JsonPropertyName("is_official_master")]
        public bool IsOfficialMaster { get; set; }

        [JsonPropertyName("is_user_master")]
        public bool IsUserMaster { get; set; }

        [JsonPropertyName("hot_reply_exist")]
        public bool HotReplyExist { get; set; }

        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; }

        [JsonPropertyName("last_modify_time")]
        public int LastModifyTime { get; set; }

        [JsonPropertyName("recommend_type")]
        public string RecommendType { get; set; } = string.Empty;

        [JsonPropertyName("collection")]
        public FullPostResponseCollection Collection { get; set; } = new();

        [JsonPropertyName("vod_list")]
        public ImmutableArray<FullPostResponseVideo> VodList { get; set; }

        [JsonPropertyName("is_block_on")]
        public bool IsBlockOn { get; set; }

        [JsonPropertyName("forum_rank_info")]
        public FullPostResponseForumRank ForumRankInfo { get; set; } = new();

        [JsonPropertyName("link_card_list")]
        public ImmutableArray<FullPostResponseLinkCard> LinkCardList { get; set; }

        [JsonPropertyName("news_meta")]
        public FullPostResponseNewsMeta NewsMeta { get; set; } = new();

        [JsonPropertyName("recommend_reason")]
        public FullPostResponseRecommend RecommendReason { get; set; } = new();

        [JsonPropertyName("villa_card")]
        public FullPostResponseVillaCard VillaCard { get; set; } = new();

        [JsonPropertyName("is_mentor")]
        public bool IsMentor { get; set; }

        [JsonPropertyName("villa_room_card")]
        public FullPostResponseVillaRoomCard VillaRoomCard { get; set; } = new();

        [JsonPropertyName("reply_avatar_action_info")]
        public FullPostResponseReplyAction ReplyAvatarActionInfo { get; set; } = new();

        [JsonPropertyName("challenge")]
        public FullPostResponseChallenge Challenge { get; set; } = new();

        [JsonPropertyName("hot_reply_list")]
        public ImmutableArray<FullPostResponseHotReply> HotReplyList { get; set; }

        [JsonPropertyName("villa_msg_image_list")]
        public ImmutableArray<FullPostResponseVillaMsgImage> VillaMsgImageList { get; set; }

        [JsonPropertyName("contribution_act")]
        public FullPostResponseContributionAction ContributionAction { get; set; } = new();

        [JsonPropertyName("is_has_vote")]
        public bool IsHasVote { get; set; }

        [JsonPropertyName("is_has_lottery")]
        public bool IsHasLottery { get; set; }

        [JsonPropertyName("release_time_type")]
        public string ReleaseTimeType { get; set; } = string.Empty;

        [JsonPropertyName("future_release_time")]
        public int FutureReleaseTime { get; set; }

        [JsonPropertyName("external_link")]
        public FullPostResponseExternalLink ExternalLink { get; set; } = new();

        [JsonPropertyName("text_summary")]
        public string TextSummary { get; set; } = string.Empty;

        [JsonPropertyName("brief_structured_content")]
        public string BriefStructuredContent { get; set; } = string.Empty;

        [JsonPropertyName("post_full_extra_info")]
        public FullPostResponsePostFullExtra PostFullExtraInfo { get; set; } = new();

        [JsonPropertyName("post_attachment_info")]
        public FullPostResponsePostAttachment PostAttachmentInfo { get; set; } = new();

        [JsonPropertyName("feed_attachment_info")]
        public FullPostResponseFeedAttachment FeedAttachmentInfo { get; set; } = new();
    }
}