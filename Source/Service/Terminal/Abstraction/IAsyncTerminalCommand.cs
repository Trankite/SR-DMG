namespace StarRailDamage.Source.Service.Terminal.Abstraction
{
    public interface IAsyncTerminalCommand : ITerminalCommand
    {
        ValueTask<ITerminalResponse> AsyncInvoke(ITerminalCommandLine command);
    }
}