using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder;
using StarRailDamage.Source.Web.Request.Builder.Abstraction;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Home
{
    public class HomeResponseBuilderFactory : IHttpRequestMessageBuilderFactory
    {
        public ChannelType ChannelType { get; set; }

        private const string URL = "https://act-api-takumi-static.mihoyo.com/common/blackboard/sr_wiki/v1/home/content/list";

        public HomeResponseBuilderFactory() { }

        public HomeResponseBuilderFactory(ChannelType channelType)
        {
            ChannelType = channelType;
        }

        public HttpRequestMessageBuilder Create()
        {
            return new HttpRequestMessageBuilder().SetRequestUri(new HoyolabHttpUriBuilder(URL).SetChannal(HoyolabQuery.StarRailWiki).SetAppSn(((int)ChannelType).ToString()).Uri);
        }

        public HomeResponseBuilderFactory SetChannelType(ChannelType value)
        {
            return this.Configure(ChannelType = value);
        }
    }
}