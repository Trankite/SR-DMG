using StarRailDamage.Source.Extension;
using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Web.Hoyolab
{
    public static class HoyolabTokenExtension
    {
        public static bool TryGetUserRole(this HoyolabToken hoyolabToken, GameType value, [NotNullWhen(true)] out HoyolabUserRole? userRole)
        {
            for (int i = 0; i < hoyolabToken.UserRoles.Count; i++)
            {
                userRole = hoyolabToken.UserRoles[i];
                if (GameTypeExtension.TryGetGameType(userRole.Game, out GameType GameType))
                {
                    if (GameType.HasFlag(value)) return true;
                }
            }
            return false.Configure(userRole = default);
        }
    }
}