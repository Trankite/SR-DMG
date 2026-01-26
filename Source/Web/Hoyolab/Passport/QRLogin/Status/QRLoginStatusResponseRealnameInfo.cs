using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Passport.QRLogin.Status
{
    public class QRLoginStatusResponseRealnameInfo
    {
        [JsonPropertyName("required")]
        public bool Required { get; set; }

        [JsonPropertyName("action_type")]
        public string ActionType { get; set; } = string.Empty;

        [JsonPropertyName("action_ticket")]
        public string ActionTicket { get; set; } = string.Empty;
    }
}