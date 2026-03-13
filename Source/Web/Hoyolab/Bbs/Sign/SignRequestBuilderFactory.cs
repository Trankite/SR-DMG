using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Web.Hoyolab.Builder;
using StarRailDamage.Source.Web.Hoyolab.DataSign;
using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder;
using System.Net.Http;
using System.Text.Json;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Sign
{
    public class SignRequestBuilderFactory : HoyolabHttpRequestMessageBuilderFactory
    {
        private const string URL = "https://bbs-api.miyoushe.com/apihub/app/api/signIn";

        public HoyolabGroup Group { get; set; }

        public SignRequestBuilderFactory() { }

        public SignRequestBuilderFactory(HoyolabToken hoyolabToken) : base(hoyolabToken) { }

        public override HttpRequestMessageBuilder Create()
        {
            return new HoyolabHttpRequestMessageBuilder()
                .SetRequestUri(URL)
                .SetMethod(HttpMethod.Post)
                .SetReferer(HoyolabReferer.MihoyoApp)
                .SetXrpcAppVersion(HoyolabOptions.Version)
                .SetXrpcClientType(ClientType.Android)
                .SetDataSignWithBody(new DataSignOptions(SaltType.X6, true, DataSignAlgorithm.Gen2), JsonSerializer.Serialize(new SignRequestBody(Group.ToIntString())))
                .SetHeader(new HoyolabCookieBuilder(HoyolabToken).SetMid().SetStoken());
        }
    }
}