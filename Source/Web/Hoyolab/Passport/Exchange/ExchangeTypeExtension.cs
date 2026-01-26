namespace StarRailDamage.Source.Web.Hoyolab.Passport.Exchange
{
    public static class ExchangeTypeExtension
    {
        public static string GetToken(this ExchangeType exchangeType, HoyolabToken hoyolabToken)
        {
            return exchangeType switch { ExchangeType.SToken => hoyolabToken.Stoken, ExchangeType.LToken => hoyolabToken.Ltoken, ExchangeType.Cookie => hoyolabToken.Cookie, _ => string.Empty };
        }
    }
}