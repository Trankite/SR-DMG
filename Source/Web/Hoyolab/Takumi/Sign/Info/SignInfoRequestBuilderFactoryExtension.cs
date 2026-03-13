using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Sign.Info
{
    public static class SignInfoRequestBuilderFactoryExtension
    {
        public static SignInfoRequestBuilderFactory SetLanguage(this SignInfoRequestBuilderFactory builder, string value)
        {
            return builder.Configure(builder.Language = value);
        }

        public static SignInfoRequestBuilderFactory SetActionId(this SignInfoRequestBuilderFactory builder, string value)
        {
            return builder.Configure(builder.ActionId = value);
        }

        public static SignInfoRequestBuilderFactory SetServer(this SignInfoRequestBuilderFactory builder, string value)
        {
            return builder.Configure(builder.Server = value);
        }

        public static SignInfoRequestBuilderFactory SetUid(this SignInfoRequestBuilderFactory builder, string value)
        {
            return builder.Configure(builder.Uid = value);
        }
    }
}