using System.Collections.Immutable;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Home
{
    public class HomeContentCycle
    {
        public string Name { get; set; } = string.Empty;

        public List<HomeContentCycle> Children { get; set; } = [];

        public ImmutableArray<HomeContentChildren> Content { get; set; }

        public HomeContentCycle() { }

        public HomeContentCycle(string name, ImmutableArray<HomeContentChildren> content)
        {
            Name = name;
            Content = content;
        }
    }
}