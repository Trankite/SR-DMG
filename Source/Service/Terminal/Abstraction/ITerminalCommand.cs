namespace StarRailDamage.Source.Service.Terminal.Abstraction
{
    public interface ITerminalCommand
    {
        string Name { get; }

        ITerminalResponse Invoke(ITerminalCommandLine command);
    }
}