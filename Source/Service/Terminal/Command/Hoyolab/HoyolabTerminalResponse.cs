using StarRailDamage.Source.Web.Hoyolab;

namespace StarRailDamage.Source.Service.Terminal.Command.Hoyolab
{
    public static class HoyolabTerminalResponse
    {
        public static TerminalResponse NotFindToken(string? aid)
        {
            return new TerminalResponse(false, $"Not Find Token" + (string.IsNullOrEmpty(aid) ? string.Empty : $" : {aid}"));
        }

        public static TerminalResponse NotFindUserRole(GameType gameType)
        {
            return new TerminalResponse(false, $"Not Find Role : {gameType}");
        }
    }
}