using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.GameRole
{
    public class GameRoleResponseWrapper
    {
        [JsonPropertyName("game_biz")]
        public string GameBiz { get; set; } = string.Empty;

        [JsonPropertyName("region")]
        public string Region { get; set; } = string.Empty;

        [JsonPropertyName("game_uid")]
        public string GameUid { get; set; } = string.Empty;

        [JsonPropertyName("nickname")]
        public string Nickname { get; set; } = string.Empty;

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("is_chosen")]
        public bool IsChosen { get; set; }

        [JsonPropertyName("region_name")]
        public string RegionName { get; set; } = string.Empty;

        [JsonPropertyName("is_official")]
        public bool IsOfficial { get; set; }

        [JsonPropertyName("unmask")]
        public ImmutableArray<GameRoleResponseUnmask> Unmask { get; set; }
    }
}