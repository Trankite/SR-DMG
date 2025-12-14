using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Detail
{
    public class DetailDataTemp : BaseWrapper<DetailDataTempUserInfo>
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("modules")]
        public ImmutableArray<DetailDataTempModule> Modules { get; set; } 

        [JsonPropertyName("tabs")]
        public ImmutableArray<DetailDataTempTab> Tabs { get; set; }
    }
}