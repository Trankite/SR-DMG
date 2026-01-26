using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder;
using System.Net.Http;
using System.Text.Json;

namespace StarRailDamage.Source.Web.Hoyolab.Passport.Exchange
{
    public class ExchangeResponseBuilderFactory : HoyolabHttpRequestMessageBuilderFactory
    {
        private const string URL = "https://passport-api.mihoyo.com/account/ma-cn-session/app/exchange";

        public ExchangeType Origin { get; set; }

        public ExchangeType Destin { get; set; }

        public ExchangeResponseBuilderFactory() { }

        public ExchangeResponseBuilderFactory(HoyolabToken hoyolabToken) : base(hoyolabToken) { }

        public override HttpRequestMessageBuilder Create()
        {
            return new HoyolabHttpRequestMessageBuilder()
                .SetRequestUri(URL)
                .SetMethod(HttpMethod.Post)
                .SetStringContent(JsonSerializer.Serialize(new { src_token = new { token = Origin.GetToken(HoyolabToken), token_type = Origin }, mid = HoyolabToken.Mid, dst_token_type = Destin }))
                .SetXrpcAppId(HoyolabOptions.HoyolabId);
        }

        public ExchangeResponseBuilderFactory SetOrigin(ExchangeType value)
        {
            return this.Configure(Origin = value);
        }

        public ExchangeResponseBuilderFactory SetDestin(ExchangeType value)
        {
            return this.Configure(Destin = value);
        }
    }
}