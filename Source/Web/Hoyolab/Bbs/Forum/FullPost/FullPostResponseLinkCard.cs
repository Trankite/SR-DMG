using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostResponseLinkCard
    {
        [JsonPropertyName("link_type")]
        public int LinkType { get; set; }

        [JsonPropertyName("origin_url")]
        public string OriginUrl { get; set; } = string.Empty;

        [JsonPropertyName("landing_url")]
        public string LandingUrl { get; set; } = string.Empty;

        [JsonPropertyName("cover")]
        public string Cover { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("origin_user_avatar")]
        public string OriginUserAvatar { get; set; } = string.Empty;

        [JsonPropertyName("origin_user_nickname")]
        public string OriginUserNickname { get; set; } = string.Empty;

        [JsonPropertyName("card_id")]
        public string CardId { get; set; } = string.Empty;

        [JsonPropertyName("card_status")]
        public int CardStatus { get; set; }

        [JsonPropertyName("market_price")]
        public string MarketPrice { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public string Price { get; set; } = string.Empty;

        [JsonPropertyName("button_text")]
        public string ButtonText { get; set; } = string.Empty;

        [JsonPropertyName("landing_url_type")]
        public int LandingUrlType { get; set; }

        [JsonPropertyName("card_meta")]
        public FullPostResponseLinkCardMeta CardMeta { get; set; } = new();

        [JsonPropertyName("origin_user_avatar_url")]
        public string OriginUserAvatarUrl { get; set; } = string.Empty;
    }
}