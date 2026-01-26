using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Avatar
{
    public class AvatarResponseWrapper
    {
        [JsonPropertyName("avatar_list")]
        public ImmutableArray<AvatarResponseAvatar> AvatarList { get; set; }

        [JsonPropertyName("equip_wiki")]
        public AvatarResponseEquipWiki EquipWiki { get; set; } = new();

        [JsonPropertyName("relic_wiki")]
        public AvatarResponseRelicWiki RelicWiki { get; set; } = new();

        [JsonPropertyName("property_info")]
        public ImmutableDictionary<string, AvatarResponsePropertyInfo> PropertyInfo { get; set; } = [];

        [JsonPropertyName("recommend_property")]
        public ImmutableDictionary<string, AvatarResponseRecommendProperty> RecommendProperty { get; set; } = [];

        [JsonPropertyName("relic_properties")]
        public ImmutableArray<AvatarResponseRelicProperty> RelicProperties { get; set; }
    }
}