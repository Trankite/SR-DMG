using StarRailDamage.Source.Service.Terminal.Abstraction;
using StarRailDamage.Source.Service.Terminal.Command;
using StarRailDamage.Source.Service.Terminal.Command.Hoyolab;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Service.Terminal
{
    public static class TerminalManage
    {
        private static readonly FrozenDictionary<string, TerminalCommand> TerminalCommandTable;

        public static ITerminalResponse Invoke(this ITerminalCommandLine commandLine)
        {
            if (TryGetTerminalCommand(commandLine.Name, out TerminalCommand? Command))
            {
                return Command.Invoke(commandLine);
            }
            return GetUnknownCommandResponse(commandLine);
        }

        public static async ValueTask<ITerminalResponse> AsyncInvoke(this ITerminalCommandLine commandLine)
        {
            if (TryGetTerminalCommand(commandLine.Name, out TerminalCommand? Command))
            {
                return await Command.AsyncInvoke(commandLine);
            }
            return await new ValueTask<ITerminalResponse>(GetUnknownCommandResponse(commandLine));
        }

        private static TerminalResponse GetUnknownCommandResponse(ITerminalCommandLine commandLine)
        {
            return new TerminalResponse(false, $"Unknown Command : {commandLine.Name}");
        }

        public static bool TryGetTerminalCommand(string name, [NotNullWhen(true)] out TerminalCommand? command)
        {
            return TerminalCommandTable.TryGetValue(name, out command);
        }

        static TerminalManage()
        {
            TerminalCommandTable = new ITerminalCommand[]
            {
                new UseConsoleCommand(),
                new GameNoteCommand(),
                new ExitConsoleCommand(),
            }
            .ToFrozenDictionary(Command => Command.Name, TerminalCommand.Create);
        }
    }
}