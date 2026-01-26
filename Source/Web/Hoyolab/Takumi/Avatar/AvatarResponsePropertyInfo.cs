using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Avatar
{
    public class AvatarResponsePropertyInfo
    {
        [JsonPropertyName("property_type")]
        public int PropertyType { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("icon")]
        public string Icon { get; set; } = string.Empty;

        [JsonPropertyName("property_name_relic")]
        public string PropertyNameRelic { get; set; } = string.Empty;

        [JsonPropertyName("property_name_filter")]
        public string PropertyNameFilter { get; set; } = string.Empty;
    }
}