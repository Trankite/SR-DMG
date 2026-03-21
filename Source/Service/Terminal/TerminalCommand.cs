using StarRailDamage.Source.Service.Terminal.Abstraction;

namespace StarRailDamage.Source.Service.Terminal
{
    public class TerminalCommand : IAsyncTerminalCommand
    {
        public bool IsAsync { get; }

        public ITerminalCommand Command { get; }

        public IAsyncTerminalCommand AsyncCommand => (IAsyncTerminalCommand)Command;

        public string Name => Command.Name;

        private TerminalCommand(bool isAsync, ITerminalCommand command)
        {
            IsAsync = isAsync;
            Command = command;
        }

        public static TerminalCommand Create(ITerminalCommand command)
        {
            return new TerminalCommand(command is IAsyncTerminalCommand, command);
        }

        public static TerminalCommand Create(IAsyncTerminalCommand command)
        {
            return new TerminalCommand(true, command);
        }

        public ITerminalResponse Invoke(ITerminalCommandLine command)
        {
            return Command.Invoke(command);
        }

        public ValueTask<ITerminalResponse> AsyncInvoke(ITerminalCommandLine command)
        {
            return AsyncCommand.AsyncInvoke(command);
        }
    }
}