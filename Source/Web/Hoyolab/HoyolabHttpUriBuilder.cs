using StarRailDamage.Source.Web.Request.Builder;

namespace StarRailDamage.Source.Web.Hoyolab
{
    public class HoyolabHttpUriBuilder : HttpUriBuilder
    {
        public HoyolabHttpUriBuilder() { }

        public HoyolabHttpUriBuilder(string uri) : base(uri) { }

        public HoyolabHttpUriBuilder(Uri uri) : base(uri) { }
    }
}