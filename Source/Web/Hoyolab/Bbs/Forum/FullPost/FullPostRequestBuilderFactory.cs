using StarRailDamage.Source.Web.Hoyolab.Builder;
using StarRailDamage.Source.Web.Hoyolab.DataSign;
using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.FullPost
{
    public class FullPostRequestBuilderFactory : HoyolabHttpRequestMessageBuilderFactory
    {
        private const string URL = "https://bbs-api.miyoushe.com/post/api/getPostFull";

        public string PostId { get; set; } = string.Empty;

        public FullPostRequestBuilderFactory() { }

        public FullPostRequestBuilderFactory(HoyolabToken hoyolabToken) : base(hoyolabToken) { }

        public override HttpRequestMessageBuilder Create()
        {
            return new HoyolabHttpRequestMessageBuilder()
                .SetRequestUri(new HoyolabHttpUriBuilder(URL).SetPostId(PostId))
                .SetReferer(HoyolabReferer.MihoyoApp)
                .SetXrpcAppVersion(HoyolabOptions.Version)
                .SetXrpcClientType(ClientType.Android)
                .SetDataSign(new DataSignOptions(SaltType.K2, false, DataSignAlgorithm.Gen1))
                .SetHeader(new HoyolabCookieBuilder(HoyolabToken).SetMid().SetStoken());
        }
    }
}