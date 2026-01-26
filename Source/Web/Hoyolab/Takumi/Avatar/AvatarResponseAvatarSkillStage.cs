using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Avatar
{
    public class AvatarResponseAvatarSkillStage
    {
        [JsonPropertyName("desc")]
        public string Desc { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("remake")]
        public string Remake { get; set; } = string.Empty;

        [JsonPropertyName("item_url")]
        public string ItemUrl { get; set; } = string.Empty;

        [JsonPropertyName("is_activated")]
        public bool IsActivated { get; set; }

        [JsonPropertyName("is_rank_work")]
        public bool IsRankWork { get; set; }

        [JsonPropertyName("exclusive_skill")]
        public AvatarResponseExclusiveSkill ExclusiveSkill { get; set; } = new();

        [JsonPropertyName("skill_id")]
        public string SkillId { get; set; } = string.Empty;

        [JsonPropertyName("special_point_type")]
        public string SpecialPointType { get; set; } = string.Empty;
    }
}