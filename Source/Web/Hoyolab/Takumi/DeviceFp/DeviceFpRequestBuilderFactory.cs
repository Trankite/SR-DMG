using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Web.Hoyolab.Builder;
using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder;
using System.Net.Http;
using System.Text.Json;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.DeviceFp
{
    public class DeviceFpRequestBuilderFactory : HoyolabHttpRequestMessageBuilderFactory
    {
        private const string URL = "https://public-data-api.mihoyo.com/device-fp/api/getFp";

        public DeviceFpRequestBuilderFactory() { }

        public DeviceFpRequestBuilderFactory(HoyolabToken hoyolabToken) : base(hoyolabToken) { }

        public override HttpRequestMessageBuilder Create()
        {
            return new HttpRequestMessageBuilder()
                .SetRequestUri(URL)
                .SetMethod(HttpMethod.Post)
                .SetStringContent(JsonSerializer.Serialize(GetBody));
        }

        private DeviceFpRequestBody GetBody()
        {
            return new DeviceFpRequestBody
            {
                DeviceId = Random.Shared.GetLowerHexString(16),
                SeedId = Guid.NewGuid().ToString(),
                SeedTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(),
                Platform = "2",
                DeviceFp = string.IsNullOrEmpty(HoyolabToken.Device) ? Random.Shared.GetLowerHexString(13) : HoyolabToken.Device,
                AppName = "bbs_cn",
                ExtFields = JsonSerializer.Serialize(GetExtendFields()),
                BbsDeviceId = Guid.NewGuid().ToString()
            };
        }

        private static Dictionary<string, object> GetExtendFields()
        {
            string Device = Random.Shared.GetUpperAndNumberString(12);
            string Product = Random.Shared.GetUpperAndNumberString(6);
            Dictionary<string, object> ExtendFields = new()
            {
                ["proxyStatus"] = 0,
                ["isRoot"] = 0,
                ["romCapacity"] = "512",
                ["deviceName"] = Device,
                ["productName"] = Product,
                ["romRemain"] = "512",
                ["hostname"] = "Android-XiaoMi",
                ["screenSize"] = "900x1600",
                ["isTablet"] = 0,
                ["aaid"] = string.Empty,
                ["model"] = Device,
                ["brand"] = "XiaoMi",
                ["hardware"] = "XiaoMi",
                ["deviceType"] = Product,
                ["devId"] = "REL",
                ["serialNumber"] = "unknown",
                ["sdCapacity"] = 512000,
                ["buildTime"] = "1600000000000",
                ["buildUser"] = "android-build",
                ["simState"] = 5,
                ["ramRemain"] = "128000",
                ["appUpdateTimeDiff"] = 1600000000000,
                ["deviceInfo"] = "XiaoMi",
                ["vaid"] = string.Empty,
                ["buildType"] = "user",
                ["sdkVersion"] = "32",
                ["ui_mode"] = "UI_MODE_TYPE_NORMAL",
                ["isMockLocation"] = 0,
                ["cpuType"] = "arm64-v8a",
                ["isAirMode"] = 0,
                ["ringMode"] = 2,
                ["chargeStatus"] = 1,
                ["manufacturer"] = "XiaoMi",
                ["emulatorStatus"] = 0,
                ["appMemory"] = "512",
                ["osVersion"] = "12",
                ["vendor"] = "unknown",
                ["accelerometer"] = "0.10000000x9.800000x0.2000000",
                ["sdRemain"] = 240000,
                ["buildTags"] = "release-keys",
                ["packageName"] = "com.mihoyo.hyperion",
                ["networkType"] = "WiFi",
                ["oaid"] = string.Empty,
                ["debugStatus"] = 1,
                ["ramCapacity"] = "460000",
                ["magnetometer"] = "15.000x-28.00x-32.000",
                ["display"] = $"[product release-keys",
                ["appInstallTimeDiff"] = 1600000000000,
                ["packageVersion"] = "2.35.0",
                ["gyroscope"] = "0.0x0.0x0.0",
                ["batteryStatus"] = 100,
                ["hasKeyboard"] = 0,
                ["board"] = Device
            };
            return ExtendFields;
        }
    }
}