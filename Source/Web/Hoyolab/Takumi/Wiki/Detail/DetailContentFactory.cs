using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder;
using StarRailDamage.Source.Web.Request.Builder.Abstraction;
using System.Collections.Specialized;
using System.Web;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Detail
{
    public class DetailContentFactory : IHttpRequestMessageBuilderFactory
    {
        public int ContentId { get; set; }

        public const string URL = "https://act-api-takumi-static.mihoyo.com/common/blackboard/sr_wiki/v1/content/info";

        public DetailContentFactory() { }

        public DetailContentFactory(int contentId)
        {
            ContentId = contentId;
        }

        public HttpRequestMessageBuilder Create()
        {
            UriBuilder UriBuilder = new(URL);
            NameValueCollection RequestQuery = HttpUtility.ParseQueryString(UriBuilder.Query);
            RequestQuery[HoyolabQuery.ApplicationSerialNumber] = HoyolabQuery.StarRailWikiApplicationSerialNumber;
            RequestQuery[HoyolabQuery.ContentId] = ContentId.ToString();
            UriBuilder.Query = RequestQuery.ToString();
            return new HttpRequestMessageBuilder().SetRequestUri(UriBuilder.Uri);
        }
    }
}