using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Sign
{
    public class SignRequestBody
    {
        [JsonPropertyName("act_id")]
        public string ActionId { get; set; } = string.Empty;

        [JsonPropertyName("region")]
        public string Region { get; set; } = string.Empty;

        [JsonPropertyName("uid")]
        public string Uid { get; set; } = string.Empty;

        [JsonPropertyName("lang")]
        public string Lang { get; set; } = string.Empty;

        public SignRequestBody() { }

        public SignRequestBody(string actionId, string region, string uid, string lang)
        {
            ActionId = actionId;
            Region = region;
            Uid = uid;
            Lang = lang;
        }
    }
}