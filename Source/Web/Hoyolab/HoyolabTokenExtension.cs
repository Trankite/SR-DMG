namespace StarRailDamage.Source.Web.Hoyolab
{
    public static class HoyolabTokenExtension
    {
        public static HoyolabUserRole? GetUserRole(this HoyolabToken hoyolabToken, GameType value)
        {
            foreach (HoyolabUserRole UserRole in hoyolabToken.UserRoles)
            {
                if (GameTypeExtension.GameTypeMap.TryGetValue(UserRole.Game, out GameType GameType))
                {
                    if (GameType.HasFlag(value)) return UserRole;
                }
            }
            return null;
        }
    }
}