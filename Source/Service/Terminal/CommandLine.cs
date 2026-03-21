using StarRailDamage.Source.Service.Terminal.Abstraction;

namespace StarRailDamage.Source.Service.Terminal
{
    public class CommandLine : ITerminalCommandLine
    {
        public string Name { get; set; } = string.Empty;

        public List<string> Expand { get; set; } = [];

        public CommandLine() { }

        public CommandLine(string name)
        {
            Name = name;
        }

        public CommandLine(string name, List<string> arguments) : this(name)
        {
            Expand = arguments;
        }

        public override string ToString()
        {
            return $"-{Name} {string.Join((char)0x20, Expand)}";
        }
    }
}