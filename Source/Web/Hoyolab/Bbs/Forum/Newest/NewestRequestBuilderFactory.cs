using StarRailDamage.Source.Web.Hoyolab.Builder;
using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder;
using StarRailDamage.Source.Web.Request.Builder.Abstraction;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.Newest
{
    public class NewestRequestBuilderFactory : IHttpRequestMessageBuilderFactory
    {
        private const string URL = "https://bbs-api.miyoushe.com/painter/api/getRecentForumPostList";

        public ZoneType ZoneType { get; set; }

        public SortType SortType { get; set; }

        public int PageSize { get; set; }

        public NewestRequestBuilderFactory() { }

        public HttpRequestMessageBuilder Create()
        {
            return new HoyolabHttpRequestMessageBuilder().SetRequestUri(new HoyolabHttpUriBuilder(URL).SetForumId(ZoneType).SetSortType(SortType).SetPageSize(PageSize));
        }
    }
}