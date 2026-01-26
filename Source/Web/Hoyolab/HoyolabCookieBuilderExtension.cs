using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Web.Request.Builder;

namespace StarRailDamage.Source.Web.Hoyolab
{
    public static class HoyolabCookieBuilderExtension
    {
        public static HoyolabCookieBuilder SetMid(this HoyolabCookieBuilder builder)
        {
            return builder.Configure(builder.SetCookie("mid", builder.HoyolabToken.Mid));
        }

        public static HoyolabCookieBuilder SetStoken(this HoyolabCookieBuilder builder)
        {
            return builder.Configure(builder.SetCookie("stoken", builder.HoyolabToken.Stoken));
        }

        public static HoyolabCookieBuilder SetLtuid(this HoyolabCookieBuilder builder)
        {
            return builder.Configure(builder.SetCookie("ltuid", builder.HoyolabToken.Aid));
        }

        public static HoyolabCookieBuilder SetLtoken(this HoyolabCookieBuilder builder)
        {
            return builder.Configure(builder.SetCookie("ltoken", builder.HoyolabToken.Ltoken));
        }
    }
}