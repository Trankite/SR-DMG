using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Avatar
{
    public class AvatarResponseServantSkill
    {
        [JsonPropertyName("point_id")]
        public string PointId { get; set; } = string.Empty;

        [JsonPropertyName("point_type")]
        public int PointType { get; set; }

        [JsonPropertyName("item_url")]
        public string ItemUrl { get; set; } = string.Empty;

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("is_activated")]
        public bool IsActivated { get; set; }

        [JsonPropertyName("is_rank_work")]
        public bool IsRankWork { get; set; }

        [JsonPropertyName("pre_point")]
        public string PrePoint { get; set; } = string.Empty;

        [JsonPropertyName("anchor")]
        public string Anchor { get; set; } = string.Empty;

        [JsonPropertyName("remake")]
        public string Remake { get; set; } = string.Empty;

        [JsonPropertyName("skill_stages")]
        public ImmutableArray<AvatarResponseServantSkillStage> SkillStages { get; set; }

        [JsonPropertyName("special_point_type")]
        public string SpecialPointType { get; set; } = string.Empty;

        [JsonPropertyName("link_to_avatar_skills")]
        public ImmutableArray<AvatarResponseServantLinkToAvatarSkill> LinkToAvatarSkills { get; set; }
    }
}