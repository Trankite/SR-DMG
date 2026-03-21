namespace StarRailDamage.Source.Service.Terminal.Abstraction
{
    public interface ITerminalCommandLine
    {
        string Name { get; set; }

        List<string> Expand { get; set; }
    }
}