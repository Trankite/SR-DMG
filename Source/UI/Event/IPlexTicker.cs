using SR_DMG.Source.UI.View;
using System.Windows.Input;

namespace SR_DMG.Source.UI.Event
{
    public class IPlexTicker
    {
        public ICommand TickChanged { get; } = new Command<PlexTicker>(Model =>
        {
            Model.Flag = !Model.Flag;
        });
    }
}