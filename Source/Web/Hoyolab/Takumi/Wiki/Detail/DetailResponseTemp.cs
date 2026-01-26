using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Detail
{
    public class DetailResponseTemp : BaseWrapper<DetailResponseUserInfo>
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("modules")]
        public ImmutableArray<DetailResponseModule> Modules { get; set; } 

        [JsonPropertyName("tabs")]
        public ImmutableArray<DetailResponseTab> Tabs { get; set; }
    }
}