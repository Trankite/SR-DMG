using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.Web.Hoyolab
{
    public static class HoyolabHttpUriBuilderExtension
    {
        public static HoyolabHttpUriBuilder SetChannal(this HoyolabHttpUriBuilder builder, string value)
        {
            return builder.SetQuery("channel_id", value);
        }

        public static HoyolabHttpUriBuilder SetContent(this HoyolabHttpUriBuilder builder, string value)
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

        public static HoyolabHttpUriBuilder SetQuery(this HoyolabHttpUriBuilder builder, string name, string value)
        {
            return builder.Configure(builder.Query[name] = value);
        }
    }
}