using StarRailDamage.Source.Web.Hoyolab.Builder;
using StarRailDamage.Source.Web.Hoyolab.DataSign;
using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.GameRole
{
    public class GameRoleRequestBuilderFactory : HoyolabHttpRequestMessageBuilderFactory
    {
        private const string URL = "https://api-takumi.miyoushe.com/binding/api/getUserGameRolesByStoken";

        public GameRoleRequestBuilderFactory() { }

        public GameRoleRequestBuilderFactory(HoyolabToken hoyolabToken) : base(hoyolabToken) { }

        public override HttpRequestMessageBuilder Create()
        {
            return new HoyolabHttpRequestMessageBuilder()
                .SetRequestUri(URL)
                .SetXrpcAppVersion(HoyolabOptions.Version)
                .SetXrpcClientType(ClientType.Android)
                .SetDataSign(DataSignOptions.Create(SaltType.K2, DataSignAlgorithm.Gen1))
                .SetHeader(new HoyolabCookieBuilder(HoyolabToken).SetMid().SetStoken());
        }
    }
}