using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Home
{
    public class HomeContentData : ListWrapper<HomeContentChildren>
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("parent_id")]
        public int ParentId { get; set; }

        [JsonPropertyName("depth")]
        public int Depth { get; set; }

        [JsonPropertyName("ch_ext")]
        public string ChannelExtension { get; set; } = string.Empty;

        [JsonPropertyName("children")]
        public ImmutableArray<HomeContentData> Children { get; set; }

        [JsonPropertyName("layout")]
        public string Layout { get; set; } = string.Empty;

        [JsonPropertyName("entry_limit")]
        public int EntryLimit { get; set; }

        [JsonPropertyName("hidden")]
        public bool Hidden { get; set; }
    }
}