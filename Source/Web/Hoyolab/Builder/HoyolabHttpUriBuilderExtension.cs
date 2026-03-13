using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Web.Hoyolab.Bbs.Forum;
using StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.Share;

namespace StarRailDamage.Source.Web.Hoyolab.Builder
{
    public static class HoyolabHttpUriBuilderExtension
    {
        public static HoyolabHttpUriBuilder SetChannalId(this HoyolabHttpUriBuilder builder, string value)
        {
            return builder.SetQuery("channel_id", value);
        }

        public static HoyolabHttpUriBuilder SetContentId(this HoyolabHttpUriBuilder builder, string value)
        {
            return builder.SetQuery("content_id", value);
        }

        public static HoyolabHttpUriBuilder SetAppSn(this HoyolabHttpUriBuilder builder, string value)
        {
            return builder.SetQuery("app_sn", value);
        }

        public static HoyolabHttpUriBuilder SetServer(this HoyolabHttpUriBuilder builder, string value)
        {
            return builder.SetQuery("server", value);
        }

        public static HoyolabHttpUriBuilder SetRoleId(this HoyolabHttpUriBuilder builder, string value)
        {
            return builder.SetQuery("role_id", value);
        }

        public static HoyolabHttpUriBuilder SetLanguage(this HoyolabHttpUriBuilder builder, string value)
        {
            return builder.SetQuery("lang", value);
        }

        public static HoyolabHttpUriBuilder SetActionId(this HoyolabHttpUriBuilder builder, string value)
        {
            return builder.SetQuery("act_id", value);
        }

        public static HoyolabHttpUriBuilder SetRegion(this HoyolabHttpUriBuilder builder, string value)
        {
            return builder.SetQuery("region", value);
        }

        public static HoyolabHttpUriBuilder SetUid(this HoyolabHttpUriBuilder builder, string value)
        {
            return builder.SetQuery("uid", value);
        }

        public static HoyolabHttpUriBuilder SetForumId(this HoyolabHttpUriBuilder builder, ZoneType value)
        {
            return builder.SetQuery("forum_id", value.ToIntString());
        }

        public static HoyolabHttpUriBuilder SetSortType(this HoyolabHttpUriBuilder builder, SortType value)
        {
            return builder.SetQuery("sort_type", value.ToIntString());
        }

        public static HoyolabHttpUriBuilder SetPageSize(this HoyolabHttpUriBuilder builder, int value)
        {
            return builder.SetQuery("page_size", value.ToString());
        }

        public static HoyolabHttpUriBuilder SetEntityType(this HoyolabHttpUriBuilder builder, EntityType value)
        {
            return builder.SetQuery("entity_type", value.ToIntString());
        }

        public static HoyolabHttpUriBuilder SetEntityId(this HoyolabHttpUriBuilder builder, string value)
        {
            return builder.SetQuery("entity_id", value);
        }

        public static HoyolabHttpUriBuilder SetPostId(this HoyolabHttpUriBuilder builder, string value)
        {
            return builder.SetQuery("post_id", value);
        }

        public static HoyolabHttpUriBuilder SetQuery(this HoyolabHttpUriBuilder builder, string name, string value)
        {
            return builder.Configure(builder.Query[name] = value);
        }
    }
}