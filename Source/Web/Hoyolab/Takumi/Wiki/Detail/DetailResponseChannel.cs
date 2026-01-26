using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Detail
{
    public class DetailResponseChannel
    {
        [JsonPropertyName("slice")]
        public ImmutableArray<DetailResponseChannelSlice> Slice { get; set; }
    }
}