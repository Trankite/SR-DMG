using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Detail
{
    public class DetailResponseModule
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("switch")]
        public bool Switch { get; set; }

        [JsonPropertyName("components")]
        public ImmutableArray<DetailResponseModuleComponent> Components { get; set; }
    }
}