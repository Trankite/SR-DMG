using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostResponseCollection
    {
        [JsonPropertyName("prev_post_id")]
        public string PrevPostId { get; set; } = string.Empty;

        [JsonPropertyName("next_post_id")]
        public string NextPostId { get; set; } = string.Empty;

        [JsonPropertyName("collection_id")]
        public string CollectionId { get; set; } = string.Empty;

        [JsonPropertyName("cur")]
        public int Current { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("collection_title")]
        public string CollectionTitle { get; set; } = string.Empty;

        [JsonPropertyName("prev_post_game_id")]
        public int PrevPostGameId { get; set; }

        [JsonPropertyName("next_post_game_id")]
        public int NextPostGameId { get; set; }

        [JsonPropertyName("prev_post_view_type")]
        public int PrevPostViewType { get; set; }

        [JsonPropertyName("next_post_view_type")]
        public int NextPostViewType { get; set; }
    }
}