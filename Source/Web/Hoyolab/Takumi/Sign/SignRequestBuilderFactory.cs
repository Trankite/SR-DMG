using StarRailDamage.Source.Web.Hoyolab.Builder;
using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder;
using System.Net.Http;
using System.Text.Json;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Sign
{
    public class SignRequestBuilderFactory : HoyolabHttpRequestMessageBuilderFactory
    {
        private const string URL = "https://api-takumi.mihoyo.com/event/luna/hkrpg/sign";

        public SignRequestBody? Body { get; set; }

        public SignRequestBuilderFactory() { }

        public SignRequestBuilderFactory(HoyolabToken hoyolabToken) : base(hoyolabToken) { }

        public override HttpRequestMessageBuilder Create()
        {
            return new HoyolabHttpRequestMessageBuilder()
                .SetRequestUri(URL)
                .SetMethod(HttpMethod.Post)
                .SetStringContent(JsonSerializer.Serialize(Body))
                .SetHeader(new HoyolabCookieBuilder(HoyolabToken).SetAccountMid().SetCookieToken());
        }
    }
}