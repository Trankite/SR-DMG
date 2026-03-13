using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Sign.Home
{
    public class SignHomeResponseShortExtraAward
    {
        [JsonPropertyName("has_extra_award")]
        public bool HasExtraAward { get; set; }

        [JsonPropertyName("start_time")]
        public string StartTime { get; set; } = string.Empty;

        [JsonPropertyName("end_time")]
        public string EndTime { get; set; } = string.Empty;

        [JsonPropertyName("list")]
        public ImmutableArray<SignHomeResponseAward> List { get; set; }

        [JsonPropertyName("start_timestamp")]
        public string StartTimestamp { get; set; } = string.Empty;

        [JsonPropertyName("end_timestamp")]
        public string EndTimestamp { get; set; } = string.Empty;
    }
}