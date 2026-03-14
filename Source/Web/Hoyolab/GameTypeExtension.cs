using StarRailDamage.Source.Model.Collections;

namespace StarRailDamage.Source.Web.Hoyolab
{
    public static class GameTypeExtension
    {
        public static readonly BidirectFrozenDictionary<string, GameType> GameTypeMap;

        static GameTypeExtension()
        {
            GameTypeMap = BidirectFrozenDictionary.Create([
                KeyValuePair.Create("bbs_cn",GameType.HoyolabChina),
                KeyValuePair.Create("hk4e_cn",GameType.GenshinChina),
                KeyValuePair.Create("hk4e_global",GameType.GenshinGlobal),
                KeyValuePair.Create("hkrpg_cn",GameType.StarRailChina),
                KeyValuePair.Create("hkrpg_global",GameType.StarRailGlobal),
                KeyValuePair.Create("bh3_cn",GameType.Honkai3China),
                KeyValuePair.Create("bh3_global",GameType.Honkai3Global),
                KeyValuePair.Create("nap_cn",GameType.ZenlessChina),
                KeyValuePair.Create("nap_global",GameType.ZenlessGlobal),
                ]);
        }
    }
}