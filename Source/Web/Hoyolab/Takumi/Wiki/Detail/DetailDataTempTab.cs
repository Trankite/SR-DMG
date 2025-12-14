using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Detail
{
    public class DetailDataTempTab
    {
        [JsonPropertyName("tabId")]
        public string TabId { get; set; } = string.Empty;

        [JsonPropertyName("tabName")]
        public string TabName { get; set; } = string.Empty;

        [JsonPropertyName("moduleGroup")]
        public ImmutableArray<DetailDataTempTabModuleGroup> ModuleGroup { get; set; }
    }
}