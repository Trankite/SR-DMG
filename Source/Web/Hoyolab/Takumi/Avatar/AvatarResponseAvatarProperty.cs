using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Avatar
{
    public class AvatarResponseAvatarProperty
    {
        [JsonPropertyName("property_type")]
        public int PropertyType { get; set; }

        [JsonPropertyName("base")]
        public string Base { get; set; } = string.Empty;

        [JsonPropertyName("add")]
        public string Add { get; set; } = string.Empty;

        [JsonPropertyName("final")]
        public string Final { get; set; } = string.Empty;
    }
}