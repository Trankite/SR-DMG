using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Passport.QRLogin
{
    public class QRLoginResponseWrapper
    {
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("ticket")]
        public string Ticket { get; set; } = string.Empty;
    }
}