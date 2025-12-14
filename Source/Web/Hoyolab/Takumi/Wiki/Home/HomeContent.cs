using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Web.Response;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Home
{
    public sealed class HomeContent : ResponseWrapper<ListWrapper<HomeContentData>>
    {
        public IEnumerator<HomeContentCycle> GetEnumerator()
        {
            if (Data.IsNull()) yield break;
            foreach (HomeContentData HomeContentData in Data.List)
            {
                yield return GetHomeContentCycle(HomeContentData);
            }
        }

        private static HomeContentCycle GetHomeContentCycle(HomeContentData homeContentData)
        {
            HomeContentCycle HomeContentCycle = new(homeContentData.Name, homeContentData.List);
            foreach (HomeContentData HomeContentDataChildren in homeContentData.Children)
            {
                HomeContentCycle.Children.Add(GetHomeContentCycle(HomeContentDataChildren));
            }
            return HomeContentCycle;
        }
    }
}