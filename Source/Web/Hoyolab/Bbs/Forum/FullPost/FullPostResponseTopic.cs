using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostResponseTopic
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("cover")]
        public string Cover { get; set; } = string.Empty;

        [JsonPropertyName("is_top")]
        public bool IsTop { get; set; }

        [JsonPropertyName("is_good")]
        public bool IsGood { get; set; }

        [JsonPropertyName("is_interactive")]
        public bool IsInteractive { get; set; }

        [JsonPropertyName("game_id")]
        public int GameId { get; set; }

        [JsonPropertyName("content_type")]
        public int ContentType { get; set; }
    }
}