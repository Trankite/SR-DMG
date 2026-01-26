using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Avatar
{
    public class AvatarResponseAvatarRank
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("pos")]
        public int Pos { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("icon")]
        public string Icon { get; set; } = string.Empty;

        [JsonPropertyName("desc")]
        public string Desc { get; set; } = string.Empty;

        [JsonPropertyName("is_unlocked")]
        public bool IsUnlocked { get; set; }
    }
}