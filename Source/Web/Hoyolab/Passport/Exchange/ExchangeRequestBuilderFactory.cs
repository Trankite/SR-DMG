using StarRailDamage.Source.Web.Hoyolab.Builder;
using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder;
using System.Net.Http;
using System.Text.Json;

namespace StarRailDamage.Source.Web.Hoyolab.Passport.Exchange
{
    public class ExchangeRequestBuilderFactory : HoyolabHttpRequestMessageBuilderFactory
    {
        private const string URL = "https://passport-api.mihoyo.com/account/ma-cn-session/app/exchange";

        public ExchangeType Origin { get; set; }

        public ExchangeType Destin { get; set; }

        public ExchangeRequestBuilderFactory() { }

        public ExchangeRequestBuilderFactory(HoyolabToken hoyolabToken) : base(hoyolabToken) { }

        public override HttpRequestMessageBuilder Create()
        {
            ExchangeRequestBodySrcToken SrcToken = new(Origin.GetToken(HoyolabToken), (int)Origin);
            return new HoyolabHttpRequestMessageBuilder()
                .SetRequestUri(URL)
                .SetMethod(HttpMethod.Post)
                .SetStringContent(JsonSerializer.Serialize(new ExchangeRequestBody(SrcToken, HoyolabToken.Mid, (int)Destin)))
                .SetXrpcAppId(HoyolabOptions.HoyolabId);
        }
    }
}