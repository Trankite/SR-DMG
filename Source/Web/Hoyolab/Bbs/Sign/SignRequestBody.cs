using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Sign
{
    public class SignRequestBody
    {
        [JsonPropertyName("gids")]
        public string Gids { get; set; } = string.Empty;

        public SignRequestBody() { }

        public SignRequestBody(string gids)
        {
            Gids = gids;
        }
    }
}