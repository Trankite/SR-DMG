using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Detail
{
    public class DetailDataWrapper
    {
        [JsonPropertyName("content")]
        public DetailData? Content { get; set; }

        [JsonPropertyName("channel_list")]
        public ImmutableArray<DetailChannel> ChannelList { get; set; }
    }
}