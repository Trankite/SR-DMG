using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Detail
{
    public class DetailDataTempUserInfo
    {
        [JsonPropertyName("avatarId")]
        public string AvatarId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("icon")]
        public string Icon { get; set; } = string.Empty;

        [JsonPropertyName("figurePath")]
        public string FigurePath { get; set; } = string.Empty;

        [JsonPropertyName("baseType")]
        public string BaseType { get; set; } = string.Empty;

        [JsonPropertyName("elementId")]
        public string ElementId { get; set; } = string.Empty;

        [JsonPropertyName("rarity")]
        public int Rarity { get; set; }

        [JsonPropertyName("isFigurePath")]
        public bool IsFigurePath { get; set; }
    }
}