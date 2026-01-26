using System.Collections.Immutable;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Wiki.Home
{
    public class HomeResponseCycle
    {
        public string Name { get; set; } = string.Empty;

        public List<HomeResponseCycle> Children { get; set; } = [];

        public ImmutableArray<HomeResponseChildren> Content { get; set; }

        public HomeResponseCycle() { }

        public HomeResponseCycle(string name, ImmutableArray<HomeResponseChildren> content)
        {
            Name = name;
            Content = content;
        }
    }
}