using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Mission
{
    public class MissionResponseWrapper
    {
        [JsonPropertyName("states")]
        public ImmutableArray<MissionResponseState> States { get; set; }

        [JsonPropertyName("already_received_points")]
        public int AlreadyReceivedPoints { get; set; }

        [JsonPropertyName("total_points")]
        public int TotalPoints { get; set; }

        [JsonPropertyName("today_total_points")]
        public int TodayTotalPoints { get; set; }

        [JsonPropertyName("is_unclaimed")]
        public bool IsUnclaimed { get; set; }

        [JsonPropertyName("can_get_points")]
        public int CanGetPoints { get; set; }
    }
}