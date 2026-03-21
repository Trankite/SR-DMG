namespace StarRailDamage.Source.Service.Terminal.Abstraction
{
    public abstract class AsyncTerminalCommand : IAsyncTerminalCommand
    {
        public abstract string Name { get; }

        public ITerminalResponse Invoke(ITerminalCommandLine command)
        {
            return AsyncInvoke(command).AsTask().Result;
        }

        public abstract ValueTask<ITerminalResponse> AsyncInvoke(ITerminalCommandLine command);
    }
}