using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Detail
{
    public class DetailDataTempTabModuleGroup
    {
        [JsonPropertyName("layout")]
        public string Layout { get; set; } = string.Empty;

        [JsonPropertyName("module")]
        public ImmutableArray<DetailDataTempTabModule> Module { get; set; }

        [JsonPropertyName("moduleGroupId")]
        public string ModuleGroupId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("parentGroupId")]
        public string ParentGroupId { get; set; } = string.Empty;
    }
}