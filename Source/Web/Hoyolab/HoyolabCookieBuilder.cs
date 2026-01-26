using StarRailDamage.Source.Web.Request.Builder;

namespace StarRailDamage.Source.Web.Hoyolab
{
    public class HoyolabCookieBuilder : HttpCookiesBuilder
    {
        public HoyolabToken HoyolabToken { get; set; }

        public HoyolabCookieBuilder()
        {
            HoyolabToken = HoyolabToken.Create();
        }

        public HoyolabCookieBuilder(HoyolabToken hoyolabToken)
        {
            HoyolabToken = hoyolabToken;
        }
    }
}