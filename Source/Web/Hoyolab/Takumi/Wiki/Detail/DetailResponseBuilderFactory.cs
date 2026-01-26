using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder;
using StarRailDamage.Source.Web.Request.Builder.Abstraction;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Detail
{
    public class DetailResponseBuilderFactory : IHttpRequestMessageBuilderFactory
    {
        public int ContentId { get; set; }

        private const string URL = "https://act-api-takumi-static.mihoyo.com/common/blackboard/sr_wiki/v1/content/info";

        public DetailResponseBuilderFactory() { }

        public DetailResponseBuilderFactory(int id)
        {
            ContentId = id;
        }

        public HttpRequestMessageBuilder Create()
        {
            return new HttpRequestMessageBuilder().SetRequestUri(new HoyolabHttpUriBuilder(URL).SetChannal(HoyolabQuery.StarRailWiki).SetContent(ContentId.ToString()).Uri);
        }

        public DetailResponseBuilderFactory SetContentId(int value)
        {
            return this.Configure(ContentId = value);
        }
    }
}