using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostResponseUserExtension
    {
        [JsonPropertyName("avatar_type")]
        public int AvatarType { get; set; }

        [JsonPropertyName("avatar_assets_id")]
        public string AvatarAssetsId { get; set; } = string.Empty;

        [JsonPropertyName("resources")]
        public ImmutableArray<FullPostResponseUserResource> Resources { get; set; }

        [JsonPropertyName("hd_resources")]
        public ImmutableArray<FullPostResponseUserHdResource> HdResources { get; set; }
    }
}