using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Passport.QRLogin.Status
{
    public class QRLoginStatusResponseWrapper
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("app_id")]
        public string AppId { get; set; } = string.Empty;

        [JsonPropertyName("client_type")]
        public int ClientType { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; } = string.Empty;

        [JsonPropertyName("scanned_at")]
        public string ScannedAt { get; set; } = string.Empty;

        [JsonPropertyName("tokens")]
        public ImmutableArray<QRLoginStatusResponseToken> Tokens { get; set; }

        [JsonPropertyName("user_info")]
        public QRLoginStatusResponseUserInfo UserInfo { get; set; } = new();

        [JsonPropertyName("realname_info")]
        public QRLoginStatusResponseRealnameInfo RealnameInfo { get; set; } = new();

        [JsonPropertyName("need_realperson")]
        public bool NeedRealperson { get; set; }

        [JsonPropertyName("ext")]
        public string Extension { get; set; } = string.Empty;

        [JsonPropertyName("scan_game_biz")]
        public string ScanGameBiz { get; set; } = string.Empty;
    }
}