using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Passport.QRLogin.Status
{
    public class QRLoginStatusRequestBody
    {
        [JsonPropertyName("ticket")]
        public string Ticket { get; set; } = string.Empty;

        public QRLoginStatusRequestBody() { }

        public QRLoginStatusRequestBody(string ticket)
        {
            Ticket = ticket;
        }
    }
}