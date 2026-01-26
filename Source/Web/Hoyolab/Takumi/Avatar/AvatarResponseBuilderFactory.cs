using StarRailDamage.Source.Web.Hoyolab.DataSign;
using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Avatar
{
    public class AvatarResponseBuilderFactory : HoyolabHttpRequestMessageBuilderFactory
    {
        private const string URL = "https://api-takumi-record.mihoyo.com/game_record/app/hkrpg/api/avatar/info";

        public AvatarResponseBuilderFactory() { }

        public AvatarResponseBuilderFactory(HoyolabToken hoyolabToken) : base(hoyolabToken) { }

        public override HttpRequestMessageBuilder Create()
        {
            return new HoyolabHttpRequestMessageBuilder()
                .SetRequestUri(new HoyolabHttpUriBuilder(URL).SetServer(HoyolabToken.Server).SetRoleId(HoyolabToken.Uid).Uri)
                .SetXrpcAppVersion(HoyolabOptions.Version)
                .SetXrpcDeviceFp(HoyolabToken.Device)
                .SetXrpcClientType(ClientType.Other)
                .SetDataSignWithQuery(new DataSignOptions(SaltType.X4, true, DataSignAlgorithm.Gen2))
                .SetHeader(new HoyolabCookieBuilder(HoyolabToken).SetLtuid().SetLtoken());
        }
    }
}