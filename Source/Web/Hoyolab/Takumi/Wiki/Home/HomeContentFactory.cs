using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder;
using StarRailDamage.Source.Web.Request.Builder.Abstraction;
using System.Collections.Specialized;
using System.Web;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Home
{
    public class HomeContentFactory(ChannelType channelType) : IHttpRequestMessageBuilderFactory
    {
        public ChannelType ChannelType { get; set; } = channelType;

        public const string URL = "https://act-api-takumi-static.mihoyo.com/common/blackboard/sr_wiki/v1/home/content/list";

        public HttpRequestMessageBuilder Create()
        {
            UriBuilder UriBuilder = new(URL);
            NameValueCollection RequestQuery = HttpUtility.ParseQueryString(UriBuilder.Query);
            RequestQuery[HoyolabQuery.ApplicationSerialNumber] = HoyolabQuery.StarRailWikiApplicationSerialNumber;
            RequestQuery[HoyolabQuery.ChannalId] = ((int)ChannelType).ToString();
            UriBuilder.Query = RequestQuery.ToString();
            return new HttpRequestMessageBuilder().SetRequestUri(UriBuilder.Uri);
        }
    }
}