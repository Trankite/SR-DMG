using StarRailDamage.Source.Core.Setting;
using StarRailDamage.Source.Service.Terminal.Abstraction;

namespace StarRailDamage.Source.Service.Terminal.Command
{
    public class UseConsoleCommand : ITerminalCommand
    {
        public const string Alias = "console";

        string ITerminalCommand.Name => Alias;

        public ITerminalResponse Invoke(ITerminalCommandLine command)
        {
            if (TerminalHelper.ConsoleMode)
            {
                return new TerminalResponse(false, "Console Already Exists");
            }
            else
            {
                if (TerminalHelper.Alloc())
                {
                    Console.Title = AppSetting.AppName;
                }
            }
            return new TerminalResponse(TerminalHelper.ConsoleMode, $"Console Started : {TerminalHelper.ConsoleMode}");
        }
    }
}