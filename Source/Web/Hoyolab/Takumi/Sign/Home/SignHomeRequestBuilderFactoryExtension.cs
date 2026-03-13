using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Sign.Home
{
    public static class SignHomeRequestBuilderFactoryExtension
    {
        public static SignHomeRequestBuilderFactory SetLanguage(this SignHomeRequestBuilderFactory builder, string value)
        {
            return builder.Configure(builder.Language = value);
        }

        public static SignHomeRequestBuilderFactory SetActionId(this SignHomeRequestBuilderFactory builder, string value)
        {
            return builder.Configure(builder.ActionId = value);
        }
    }
}