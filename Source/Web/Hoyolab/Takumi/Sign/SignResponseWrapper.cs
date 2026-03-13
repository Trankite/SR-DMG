using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Sign
{
    public class SignResponseWrapper
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName("risk_code")]
        public int RiskCode { get; set; }

        [JsonPropertyName("gt")]
        public string Gt { get; set; } = string.Empty;

        [JsonPropertyName("challenge")]
        public string Challenge { get; set; } = string.Empty;

        [JsonPropertyName("success")]
        public int Success { get; set; }

        [JsonPropertyName("is_risk")]
        public bool IsRisk { get; set; }
    }
}