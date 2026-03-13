using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.Web.Hoyolab.Passport.Exchange
{
    public static class ExchangeRequestBuilderFactoryExtension
    {
        public static ExchangeRequestBuilderFactory SetOrigin(this ExchangeRequestBuilderFactory builder, ExchangeType value)
        {
            return builder.Configure(builder.Origin = value);
        }

        public static ExchangeRequestBuilderFactory SetDestin(this ExchangeRequestBuilderFactory builder, ExchangeType value)
        {
            return builder.Configure(builder.Destin = value);
        }
    }
}