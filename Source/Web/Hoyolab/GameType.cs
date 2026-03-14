namespace StarRailDamage.Source.Web.Hoyolab
{
    [Flags]
    public enum GameType
    {
        None,

        HoyolabChina = 0x01,
        HoyolabGlobal = 0x02,
        Hoyolab = HoyolabChina | HoyolabGlobal,

        GenshinChina = 0x04,
        GenshinGlobal = 0x08,
        Genshin = GenshinChina | GenshinGlobal,

        StarRailChina = 0x10,
        StarRailGlobal = 0x20,
        StarRail = StarRailChina | StarRailGlobal,

        Honkai3China = 0x40,
        Honkai3Global = 0x80,
        Honkai3 = Honkai3China | Honkai3Global,

        ZenlessChina = 0x100,
        ZenlessGlobal = 0x200,
        Zenless = ZenlessChina | ZenlessGlobal
    }
}