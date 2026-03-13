using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.Web.Hoyolab.Bbs.Forum.Newest
{
    public static class NewestRequestBuilderFactoryExtension
    {
        public static NewestRequestBuilderFactory SetZoneType(this NewestRequestBuilderFactory builder, ZoneType value)
        {
            return builder.Configure(builder.ZoneType = value);
        }

        public static NewestRequestBuilderFactory SetSortType(this NewestRequestBuilderFactory builder, SortType value)
        {
            return builder.Configure(builder.SortType = value);
        }

        public static NewestRequestBuilderFactory SetPageSize(this NewestRequestBuilderFactory builder, int value)
        {
            return builder.Configure(builder.PageSize = value);
        }
    }
}