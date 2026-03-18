using StarRailDamage.Source.Web.Hoyolab.Builder;
using StarRailDamage.Source.Web.Hoyolab.DataSign;
using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.Share
{
    public class ShareRequestBuilderFactory : HoyolabHttpRequestMessageBuilderFactory
    {
        private const string URL = "https://bbs-api.miyoushe.com/apihub/api/getShareConf";

        public EntityType EntityType { get; set; }

        public string EntityId { get; set; } = string.Empty;

        public ShareRequestBuilderFactory() { }

        public ShareRequestBuilderFactory(HoyolabToken hoyolabToken) : base(hoyolabToken) { }

        public override HttpRequestMessageBuilder Create()
        {
            return new HoyolabHttpRequestMessageBuilder()
                .SetRequestUri(new HoyolabHttpUriBuilder(URL).SetEntityType(EntityType).SetEntityId(EntityId))
                .SetReferer(HoyolabReferer.MihoyoApp)
                .SetXrpcAppVersion(HoyolabOptions.Version)
                .SetXrpcClientType(ClientType.Android)
                .SetDataSign(DataSignOptions.Create(SaltType.K2, DataSignAlgorithm.Gen1))
                .SetHeader(new HoyolabCookieBuilder(HoyolabToken).SetMid().SetStoken());
        }
    }
}