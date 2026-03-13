using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostResponseForum
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("icon")]
        public string Icon { get; set; } = string.Empty;

        [JsonPropertyName("game_id")]
        public int GameId { get; set; }

        [JsonPropertyName("forum_cate")]
        public FullPostResponseForumCate ForumCate { get; set; } = new();
    }
}