using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Avatar
{
    public class AvatarResponseAvatarRelic
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("pos")]
        public int Pos { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("desc")]
        public string Desc { get; set; } = string.Empty;

        [JsonPropertyName("icon")]
        public string Icon { get; set; } = string.Empty;

        [JsonPropertyName("rarity")]
        public int Rarity { get; set; }

        [JsonPropertyName("main_property")]
        public AvatarResponseAvatarRelicProperty MainProperty { get; set; } = new();

        [JsonPropertyName("properties")]
        public ImmutableArray<AvatarResponseAvatarRelicProperty> Properties { get; set; }
    }
}