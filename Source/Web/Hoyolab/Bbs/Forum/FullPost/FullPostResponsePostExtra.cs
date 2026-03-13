using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostResponsePostExtra
    {
        [JsonPropertyName("ugc_master_post_extra")]
        public FullPostResponsePostExtraUgcMaster UgcMasterPostExtra { get; set; } = new();

        [JsonPropertyName("minos_aigc_info")]
        public FullPostResponsePostExtraMinosAigc MinosAigcInfo { get; set; } = new();
    }
}