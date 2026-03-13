using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Sign.Info
{
    public class SignInfoResponseWrapper
    {
        [JsonPropertyName("total_sign_day")]
        public int TotalSignDay { get; set; }

        [JsonPropertyName("today")]
        public string Today { get; set; } = string.Empty;

        [JsonPropertyName("is_sign")]
        public bool IsSign { get; set; }

        [JsonPropertyName("is_sub")]
        public bool IsSub { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; } = string.Empty;

        [JsonPropertyName("sign_cnt_missed")]
        public int SignCntMissed { get; set; }

        [JsonPropertyName("short_sign_day")]
        public int ShortSignDay { get; set; }

        [JsonPropertyName("send_first")]
        public bool SendFirst { get; set; }
    }
}