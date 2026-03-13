using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.Upvote
{
    public static class UpvoteRequestBuilderFactoryExtension
    {
        public static UpvoteRequestBuilderFactory SetIsCancel(this UpvoteRequestBuilderFactory builder, bool value)
        {
            return builder.Configure(builder.IsCancel = value);
        }

        public static UpvoteRequestBuilderFactory SetPostId(this UpvoteRequestBuilderFactory builder, string value)
        {
            return builder.Configure(builder.PostId = value);
        }
    }
}