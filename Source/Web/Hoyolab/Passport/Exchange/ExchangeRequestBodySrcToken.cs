using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Passport.Exchange
{
    public class ExchangeRequestBodySrcToken
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;

        [JsonPropertyName("token_type")]
        public int TokenType { get; set; }

        public ExchangeRequestBodySrcToken(string token, int tokenType)
        {
            Token = token;
            TokenType = tokenType;
        }
    }
}