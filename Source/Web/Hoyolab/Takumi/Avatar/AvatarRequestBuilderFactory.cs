using StarRailDamage.Source.Web.Hoyolab.Builder;
using StarRailDamage.Source.Web.Hoyolab.DataSign;
using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Avatar
{
    public class AvatarRequestBuilderFactory : HoyolabHttpRequestMessageBuilderFactory
    {
        private const string URL = "https://api-takumi-record.mihoyo.com/game_record/app/hkrpg/api/avatar/info";

        public string Uid { get; set; } = string.Empty;

        public string Server { get; set; } = string.Empty;

        public AvatarRequestBuilderFactory() { }

        public AvatarRequestBuilderFactory(HoyolabToken hoyolabToken) : base(hoyolabToken) { }

        public override HttpRequestMessageBuilder Create()
        {
            return new HoyolabHttpRequestMessageBuilder()
                .SetRequestUri(new HoyolabHttpUriBuilder(URL).SetServer(Server).SetRoleId(Uid))
                .SetXrpcAppVersion(HoyolabOptions.Version)
                .SetXrpcDeviceFp(HoyolabToken.Device)
                .SetXrpcClientType(ClientType.Other)
                .SetDataSignWithQuery(DataSignOptions.Create(SaltType.X4, DataSignAlgorithm.Gen2))
                .SetHeader(new HoyolabCookieBuilder(HoyolabToken).SetLtuid().SetLtoken());
        }
    }
}