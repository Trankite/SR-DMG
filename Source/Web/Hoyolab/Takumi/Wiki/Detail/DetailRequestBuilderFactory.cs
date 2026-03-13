using StarRailDamage.Source.Web.Hoyolab.Builder;
using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder;
using StarRailDamage.Source.Web.Request.Builder.Abstraction;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Detail
{
    public class DetailRequestBuilderFactory : IHttpRequestMessageBuilderFactory
    {
        private const string URL = "https://act-api-takumi-static.mihoyo.com/common/blackboard/sr_wiki/v1/content/info";

        public int ContentId { get; set; }

        public DetailRequestBuilderFactory() { }

        public HttpRequestMessageBuilder Create()
        {
            return new HttpRequestMessageBuilder().SetRequestUri(new HoyolabHttpUriBuilder(URL).SetChannalId(HoyolabQuery.StarRailWiki).SetContentId(ContentId.ToString()));
        }
    }
}