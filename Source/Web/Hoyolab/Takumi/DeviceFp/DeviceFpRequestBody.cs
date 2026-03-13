using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.DeviceFp
{
    public class DeviceFpRequestBody
    {
        [JsonPropertyName("device_id")]
        public string DeviceId { get; set; } = string.Empty;

        [JsonPropertyName("seed_id")]
        public string SeedId { get; set; } = string.Empty;

        [JsonPropertyName("seed_time")]
        public string SeedTime { get; set; } = string.Empty;

        [JsonPropertyName("platform")]
        public string Platform { get; set; } = string.Empty;

        [JsonPropertyName("device_fp")]
        public string DeviceFp { get; set; } = string.Empty;

        [JsonPropertyName("app_name")]
        public string AppName { get; set; } = string.Empty;

        [JsonPropertyName("ext_fields")]
        public string ExtFields { get; set; } = string.Empty;

        [JsonPropertyName("bbs_device_id")]
        public string BbsDeviceId { get; set; } = string.Empty;
    }
}