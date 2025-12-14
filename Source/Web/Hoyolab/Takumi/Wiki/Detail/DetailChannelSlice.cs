using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Detail
{
    public class DetailChannelSlice
    {
        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("ch_ext")]
        public string ChannelExtension { get; set; } = string.Empty;

        [JsonPropertyName("is_hidden")]
        public bool IsHidden { get; set; }
    }
}