using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Avatar
{
    public class AvatarResponseAvatar
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("element")]
        public string Element { get; set; } = string.Empty;

        [JsonPropertyName("icon")]
        public string Icon { get; set; } = string.Empty;

        [JsonPropertyName("rarity")]
        public int Rarity { get; set; }

        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; } = string.Empty;

        [JsonPropertyName("equip")]
        public AvatarResponseEquip Equip { get; set; } = new();

        [JsonPropertyName("relics")]
        public ImmutableArray<AvatarResponseAvatarRelic> Relics { get; set; }

        [JsonPropertyName("ornaments")]
        public ImmutableArray<AvatarResponseAvatarRelic> Ornaments { get; set; }

        [JsonPropertyName("ranks")]
        public ImmutableArray<AvatarResponseAvatarRank> Ranks { get; set; }

        [JsonPropertyName("properties")]
        public ImmutableArray<AvatarResponseAvatarProperty> Properties { get; set; }

        [JsonPropertyName("skills")]
        public ImmutableArray<AvatarResponseAvatarSkill> Skills { get; set; }

        [JsonPropertyName("base_type")]
        public int BaseType { get; set; }

        [JsonPropertyName("figure_path")]
        public string FigurePath { get; set; } = string.Empty;

        [JsonPropertyName("element_id")]
        public int ElementId { get; set; }

        [JsonPropertyName("servant_detail")]
        public AvatarResponseServant ServantDetail { get; set; } = new();

        [JsonPropertyName("avatar_ld_type")]
        public string AvatarLdType { get; set; } = string.Empty;

        [JsonPropertyName("cur_enhanced_id")]
        public int CurEnhancedId { get; set; }

        [JsonPropertyName("special_skills")]
        public ImmutableArray<AvatarResponseAvatarSkill> SpecialSkills { get; set; }
    }
}