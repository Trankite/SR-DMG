using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Detail
{
    public class DetailDataTempModuleComponent
    {
        [JsonPropertyName("componentId")]
        public string ComponentId { get; set; } = string.Empty;

        [JsonPropertyName("layout")]
        public string Layout { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public string Data { get; set; } = string.Empty;

        [JsonPropertyName("style")]
        public string Style { get; set; } = string.Empty;
    }
}