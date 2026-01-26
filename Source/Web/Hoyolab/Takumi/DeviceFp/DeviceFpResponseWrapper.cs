using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.DeviceFp
{
    public class DeviceFpResponseWrapper
    {
        [JsonPropertyName("device_fp")]
        public string DeviceFp { get; set; } = string.Empty;

        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
    }
}