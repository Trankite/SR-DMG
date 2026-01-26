using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Passport.Exchange
{
    public class ExchangeResponseUserInfo
    {
        [JsonPropertyName("aid")]
        public string Aid { get; set; } = string.Empty;

        [JsonPropertyName("mid")]
        public string Mid { get; set; } = string.Empty;

        [JsonPropertyName("account_name")]
        public string AccountName { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("is_email_verify")]
        public int IsEmailVerify { get; set; }

        [JsonPropertyName("area_code")]
        public string AreaCode { get; set; } = string.Empty;

        [JsonPropertyName("mobile")]
        public string Mobile { get; set; } = string.Empty;

        [JsonPropertyName("safe_area_code")]
        public string SafeAreaCode { get; set; } = string.Empty;

        [JsonPropertyName("safe_mobile")]
        public string SafeMobile { get; set; } = string.Empty;

        [JsonPropertyName("realname")]
        public string Realname { get; set; } = string.Empty;

        [JsonPropertyName("identity_code")]
        public string IdentityCode { get; set; } = string.Empty;

        [JsonPropertyName("rebind_area_code")]
        public string RebindAreaCode { get; set; } = string.Empty;

        [JsonPropertyName("rebind_mobile")]
        public string RebindMobile { get; set; } = string.Empty;

        [JsonPropertyName("rebind_mobile_time")]
        public string RebindMobileTime { get; set; } = string.Empty;

        [JsonPropertyName("links")]
        public ImmutableArray<ExchangeResponseUserInfoLink> Links { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;

        [JsonPropertyName("password_time")]
        public string PasswordTime { get; set; } = string.Empty;

        [JsonPropertyName("is_adult")]
        public int IsAdult { get; set; }

        [JsonPropertyName("unmasked_email")]
        public string UnmaskedEmail { get; set; } = string.Empty;

        [JsonPropertyName("unmasked_email_type")]
        public int UnmaskedEmailType { get; set; }
    }
}