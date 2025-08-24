using SR_DMG.Source.UI.Model;
using System.Windows.Input;

namespace SR_DMG.Source.UI.Event
{
    public class ITicker
    {
        public ICommand TickChanged { get; } = new Command<ComplexTicker>(Tick => Tick.Flag = !Tick.Flag);
    }
}