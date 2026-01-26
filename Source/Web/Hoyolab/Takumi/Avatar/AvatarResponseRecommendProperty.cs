using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Avatar
{
    public class AvatarResponseRecommendProperty
    {
        [JsonPropertyName("recommend_relic_properties")]
        public ImmutableArray<int> RecommendRelicProperties { get; set; }

        [JsonPropertyName("custom_relic_properties")]
        public ImmutableArray<object> CustomRelicProperties { get; set; }

        [JsonPropertyName("is_custom_property_valid")]
        public bool IsCustomPropertyValid { get; set; }
    }
}