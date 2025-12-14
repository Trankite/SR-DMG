using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Detail
{
    public class DetailChannel
    {
        [JsonPropertyName("slice")]
        public ImmutableArray<DetailChannelSlice> Slice { get; set; }
    }
}