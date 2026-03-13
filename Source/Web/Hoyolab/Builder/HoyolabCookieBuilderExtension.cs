using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Web.Request.Builder;

namespace StarRailDamage.Source.Web.Hoyolab.Builder
{
    public static class HoyolabCookieBuilderExtension
    {
        public static HoyolabCookieBuilder SetMid(this HoyolabCookieBuilder builder)
        {
            return builder.Configure(builder.SetCookie("mid", builder.HoyolabToken.Mid));
        }

        public static HoyolabCookieBuilder SetStoken(this HoyolabCookieBuilder builder)
        {
            return builder.Configure(builder.SetCookie("stoken", builder.HoyolabToken.Tokens.GetValueOrDefault(HoyolabTokenType.SToken)));
        }

        public static HoyolabCookieBuilder SetLtuid(this HoyolabCookieBuilder builder)
        {
            return builder.Configure(builder.SetCookie("ltuid", builder.HoyolabToken.Aid));
        }

        public static HoyolabCookieBuilder SetLtoken(this HoyolabCookieBuilder builder)
        {
            return builder.Configure(builder.SetCookie("ltoken", builder.HoyolabToken.Tokens.GetValueOrDefault(HoyolabTokenType.LToken)));
        }

        public static HoyolabCookieBuilder SetAccountMid(this HoyolabCookieBuilder builder)
        {
            return builder.Configure(builder.SetCookie("account_mid_v2", builder.HoyolabToken.Mid));
        }

        public static HoyolabCookieBuilder SetCookieToken(this HoyolabCookieBuilder builder)
        {
            return builder.Configure(builder.SetCookie("cookie_token_v2", builder.HoyolabToken.Tokens.GetValueOrDefault(HoyolabTokenType.Cookie)));
        }
    }
}