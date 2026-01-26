using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Avatar
{
    public class AvatarResponseServant
    {
        [JsonPropertyName("servant_id")]
        public string ServantId { get; set; } = string.Empty;

        [JsonPropertyName("servant_name")]
        public string ServantName { get; set; } = string.Empty;

        [JsonPropertyName("servant_icon")]
        public string ServantIcon { get; set; } = string.Empty;

        [JsonPropertyName("servant_properties")]
        public ImmutableArray<AvatarResponseAvatarProperty> ServantProperties { get; set; }

        [JsonPropertyName("servant_skills")]
        public ImmutableArray<AvatarResponseServantSkill> ServantSkills { get; set; }

        [JsonPropertyName("is_health_secret")]
        public bool IsHealthSecret { get; set; }
    }
}