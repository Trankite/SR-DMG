using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.Web.Hoyolab
{
    public class HoyolabToken
    {
        public string Name { get; set; } = string.Empty;

        public string Aid { get; set; } = string.Empty;

        public string Mid { get; set; } = string.Empty;

        public string Device { get; set; } = string.Empty;

        public string Guid { get; set; } = string.Empty;

        public Dictionary<HoyolabTokenType, string> Tokens { get; set; } = [];

        public List<HoyolabUserRole> UserRoles { get; set; } = [];

        public static HoyolabToken Create()
        {
            return new HoyolabToken().Configure(HoyolabToken => HoyolabToken.Guid = System.Guid.NewGuid().ToString());
        }
    }
}