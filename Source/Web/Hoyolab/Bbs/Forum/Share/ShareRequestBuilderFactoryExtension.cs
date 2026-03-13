using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.Share
{
    public static class ShareRequestBuilderFactoryExtension
    {
        public static ShareRequestBuilderFactory SetEntityType(this ShareRequestBuilderFactory builder, EntityType value)
        {
            return builder.Configure(builder.EntityType = value);
        }

        public static ShareRequestBuilderFactory SetEntityId(this ShareRequestBuilderFactory builder, string value)
        {
            return builder.Configure(builder.EntityId = value);
        }
    }
}