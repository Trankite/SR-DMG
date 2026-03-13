using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Mission
{
    public class MissionResponseState
    {
        [JsonPropertyName("mission_id")]
        public int MissionId { get; set; }

        [JsonPropertyName("process")]
        public int Process { get; set; }

        [JsonPropertyName("happened_times")]
        public int HappenedTimes { get; set; }

        [JsonPropertyName("is_get_award")]
        public bool IsGetAward { get; set; }

        [JsonPropertyName("mission_key")]
        public string MissionKey { get; set; } = string.Empty;
    }
}