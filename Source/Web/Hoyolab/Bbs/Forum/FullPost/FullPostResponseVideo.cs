using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostResponseVideo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("cover")]
        public string Cover { get; set; } = string.Empty;

        [JsonPropertyName("resolutions")]
        public ImmutableArray<FullPostResponseVideoResolution> Resolutions { get; set; }

        [JsonPropertyName("view_num")]
        public int ViewNum { get; set; }

        [JsonPropertyName("transcoding_status")]
        public int TranscodingStatus { get; set; }

        [JsonPropertyName("review_status")]
        public int ReviewStatus { get; set; }

        [JsonPropertyName("brief_intro")]
        public string BriefIntro { get; set; } = string.Empty;
    }
}