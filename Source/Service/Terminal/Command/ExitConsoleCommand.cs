using StarRailDamage.Source.Service.Terminal.Abstraction;

namespace StarRailDamage.Source.Service.Terminal.Command
{
    public class ExitConsoleCommand : ITerminalCommand
    {
        public const string Alias = "exit";

        public string Name => Alias;

        public ITerminalResponse Invoke(ITerminalCommandLine command)
        {
            return new TerminalResponse(!TerminalHelper.Close());
        }
    }
}