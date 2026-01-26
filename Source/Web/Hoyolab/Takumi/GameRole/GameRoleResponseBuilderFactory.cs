using StarRailDamage.Source.Web.Hoyolab.DataSign;
using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.GameRole
{
    public class GameRoleResponseBuilderFactory : HoyolabHttpRequestMessageBuilderFactory
    {
        private const string URL = "https://api-takumi.miyoushe.com/binding/api/getUserGameRolesByStoken";

        public GameRoleResponseBuilderFactory() { }

        public GameRoleResponseBuilderFactory(HoyolabToken hoyolabToken) : base(hoyolabToken) { }

        public override HttpRequestMessageBuilder Create()
        {
            return new HoyolabHttpRequestMessageBuilder()
                .SetRequestUri(URL)
                .SetXrpcAppVersion(HoyolabOptions.Version)
                .SetXrpcClientType(ClientType.Android)
                .SetDataSign(new DataSignOptions(SaltType.K2, true, DataSignAlgorithm.Gen1))
                .SetHeader(new HoyolabCookieBuilder(HoyolabToken).SetMid().SetStoken());
        }
    }
}