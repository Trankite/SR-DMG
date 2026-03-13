using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Detail
{
    public static class DetailRequestBuilderFactoryExtension
    {
        public static DetailRequestBuilderFactory SetContentId(this DetailRequestBuilderFactory builder, int value)
        {
            return builder.Configure(builder.ContentId = value);
        }
    }
}