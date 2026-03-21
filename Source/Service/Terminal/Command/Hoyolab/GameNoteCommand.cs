using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Service.Terminal.Abstraction;
using StarRailDamage.Source.Web.Hoyolab;
using StarRailDamage.Source.Web.Hoyolab.Takumi.Note;
using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Response;

namespace StarRailDamage.Source.Service.Terminal.Command.Hoyolab
{
    public class GameNoteCommand : AsyncTerminalCommand
    {
        public const string Alias = "note";

        public override string Name => Alias;

        public override async ValueTask<ITerminalResponse> AsyncInvoke(ITerminalCommandLine command)
        {
            if (!HoyolabTokenManage.TryGetTokenOrFirst(command.Expand.FirstOrDefault().OutSelf(out string? Aid), out HoyolabToken? Token))
            {
                return HoyolabTerminalResponse.NotFindToken(Aid);
            }
            if (!Token.TryGetUserRole(GameType.StarRailChina.OutSelf(out GameType Game), out HoyolabUserRole? UserRole))
            {
                return HoyolabTerminalResponse.NotFindUserRole(Game);
            }
            NoteRequestBuilderFactory Factory = new NoteRequestBuilderFactory(Token).SetUserRole(UserRole);
            FinalizedResponse<NoteResponse> Response = await Factory.Create().SendAsync<NoteResponse>(Program.HttpClient);
            if (Response.Body.IsNull())
            {
                return new TerminalResponse(false, Response.ToString());
            }
            return TerminalResponse.Create(Response.Body.IsSuccess(), Response.Body.ToString(), Response.Body);
        }
    }
}