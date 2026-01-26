using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder.Abstraction;

namespace StarRailDamage.Source.Web.Hoyolab
{
    public abstract class HoyolabHttpRequestMessageBuilderFactory : IHttpRequestMessageBuilderFactory
    {
        public HoyolabToken HoyolabToken { get; set; }

        public HoyolabHttpRequestMessageBuilderFactory()
        {
            HoyolabToken = HoyolabToken.Create();
        }

        public HoyolabHttpRequestMessageBuilderFactory(HoyolabToken hoyolabToken)
        {
            HoyolabToken = hoyolabToken;
        }

        public abstract HttpRequestMessageBuilder Create();
    }
}