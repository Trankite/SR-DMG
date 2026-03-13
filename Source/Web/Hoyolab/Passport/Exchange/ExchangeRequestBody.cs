using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Passport.Exchange
{
    public class ExchangeRequestBody
    {
        [JsonPropertyName("src_token")]
        public ExchangeRequestBodySrcToken SrcToken { get; set; }

        [JsonPropertyName("mid")]
        public string Mid { get; set; } = string.Empty;

        [JsonPropertyName("dst_token_type")]
        public int DstTokenType { get; set; }

        public ExchangeRequestBody(ExchangeRequestBodySrcToken srcToken, string mid, int dstTokenType)
        {
            SrcToken = srcToken;
            Mid = mid;
            DstTokenType = dstTokenType;
        }
    }
}