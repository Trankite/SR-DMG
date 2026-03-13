namespace StarRailDamage.Source.Web.Hoyolab.Passport.Exchange
{
    public static class ExchangeTypeExtension
    {
        public static string GetToken(this ExchangeType exchangeType, HoyolabToken hoyolabToken)
        {
            return hoyolabToken.Tokens.GetValueOrDefault(exchangeType.GetTokenType()) ?? string.Empty;
        }

        public static HoyolabTokenType GetTokenType(this ExchangeType exchangeType)
        {
            return exchangeType switch { ExchangeType.SToken => HoyolabTokenType.SToken, ExchangeType.LToken => HoyolabTokenType.LToken, ExchangeType.Cookie => HoyolabTokenType.Cookie, _ => HoyolabTokenType.None };
        }
    }
}