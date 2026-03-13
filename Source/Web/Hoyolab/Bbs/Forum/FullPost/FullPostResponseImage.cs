using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostResponseImage
    {
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; } = string.Empty;

        [JsonPropertyName("size")]
        public string Size { get; set; } = string.Empty;

        [JsonPropertyName("crop")]
        public FullPostResponseImageCrop Crop { get; set; } = new();

        [JsonPropertyName("is_user_set_cover")]
        public bool IsUserSetCover { get; set; }

        [JsonPropertyName("image_id")]
        public string ImageId { get; set; } = string.Empty;

        [JsonPropertyName("entity_type")]
        public string EntityType { get; set; } = string.Empty;

        [JsonPropertyName("entity_id")]
        public string EntityId { get; set; } = string.Empty;

        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; set; }

        [JsonPropertyName("aigc_label")]
        public string AigcLabel { get; set; } = string.Empty;

        [JsonPropertyName("aigc_meta")]
        public string AigcMeta { get; set; } = string.Empty;
    }
}