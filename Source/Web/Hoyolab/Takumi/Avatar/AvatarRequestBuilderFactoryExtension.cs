using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Avatar
{
    public static class AvatarRequestBuilderFactoryExtension
    {
        public static AvatarRequestBuilderFactory SetUid(this AvatarRequestBuilderFactory builder, string uid)
        {
            return builder.Configure(builder.Uid = uid);
        }

        public static AvatarRequestBuilderFactory SetServer(this AvatarRequestBuilderFactory builder, string server)
        {
            return builder.Configure(builder.Server = server);
        }
    }
}