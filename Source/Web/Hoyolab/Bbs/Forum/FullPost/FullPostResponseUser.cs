using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostResponseUser
    {
        [JsonPropertyName("uid")]
        public string Uid { get; set; } = string.Empty;

        [JsonPropertyName("nickname")]
        public string Nickname { get; set; } = string.Empty;

        [JsonPropertyName("introduce")]
        public string Introduce { get; set; } = string.Empty;

        [JsonPropertyName("avatar")]
        public string Avatar { get; set; } = string.Empty;

        [JsonPropertyName("gender")]
        public int Gender { get; set; }

        [JsonPropertyName("certification")]
        public FullPostResponseUserCertification Certification { get; set; } = new();

        [JsonPropertyName("level_exp")]
        public FullPostResponseUserLevel LevelExp { get; set; } = new();

        [JsonPropertyName("is_following")]
        public bool IsFollowing { get; set; }

        [JsonPropertyName("is_followed")]
        public bool IsFollowed { get; set; }

        [JsonPropertyName("avatar_url")]
        public string AvatarUrl { get; set; } = string.Empty;

        [JsonPropertyName("pendant")]
        public string Pendant { get; set; } = string.Empty;

        [JsonPropertyName("certifications")]
        public ImmutableArray<FullPostResponseUserCertification> Certifications { get; set; }

        [JsonPropertyName("is_creator")]
        public bool IsCreator { get; set; }

        [JsonPropertyName("avatar_ext")]
        public FullPostResponseUserExtension AvatarExtension { get; set; } = new();
    }
}