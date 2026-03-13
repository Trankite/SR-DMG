using StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost;
using StarRailDamage.Source.Web.Response;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.Newest
{
    public class NewestResponseWrapper : ListWrapper<FullPostResponseWrapper>
    {
        [JsonPropertyName("last_id")]
        public string LastId { get; set; } = string.Empty;

        [JsonPropertyName("is_last")]
        public bool IsLast { get; set; }

        [JsonPropertyName("is_origin")]
        public bool IsOrigin { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("databox")]
        public ImmutableDictionary<string, string> Databox { get; set; } = [];
    }
}