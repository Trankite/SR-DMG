using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Home
{
    public static class HomeRequestBuilderFactoryExtension
    {
        public static HomeRequestBuilderFactory SetChannelType(this HomeRequestBuilderFactory builder, ChannelType value)
        {
            return builder.Configure(builder.ChannelType = value);
        }
    }
}