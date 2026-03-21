using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Web.Response;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Home
{
    public sealed class HomeResponse : ResponseWrapper<ListWrapper<HomeResponseWrapper>>
    {
        public IEnumerator<HomeResponseCycle> GetEnumerator()
        {
            if (Content.IsNull()) yield break;
            foreach (HomeResponseWrapper HomeResponseData in Content.List)
            {
                yield return GetHomeContentCycle(HomeResponseData);
            }
        }

        private static HomeResponseCycle GetHomeContentCycle(HomeResponseWrapper homeResponseData)
        {
            HomeResponseCycle HomeResponseCycle = new(homeResponseData.Name, homeResponseData.List);
            foreach (HomeResponseWrapper HomeContentDataChildren in homeResponseData.Children)
            {
                HomeResponseCycle.Children.Add(GetHomeContentCycle(HomeContentDataChildren));
            }
            return HomeResponseCycle;
        }
    }
}