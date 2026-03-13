using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Sign.Home
{
    public class SignHomeResponseWrapper
    {
        [JsonPropertyName("month")]
        public int Month { get; set; }

        [JsonPropertyName("awards")]
        public ImmutableArray<SignHomeResponseAward> Awards { get; set; }

        [JsonPropertyName("biz")]
        public string Biz { get; set; } = string.Empty;

        [JsonPropertyName("resign")]
        public bool Resign { get; set; }

        [JsonPropertyName("short_extra_award")]
        public SignHomeResponseShortExtraAward ShortExtraAward { get; set; } = new();
    }
}