using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Passport.Exchange
{
    public class ExchangeResponseWrapper
    {
        [JsonPropertyName("token")]
        public ExchangeResponseToken Token { get; set; } = new();

        [JsonPropertyName("user_info")]
        public ExchangeResponseUserInfo UserInfo { get; set; } = new();

        [JsonPropertyName("expire_in_seconds")]
        public string ExpireInSeconds { get; set; } = string.Empty;
    }
}