using SR_DMG.Source.UI.Model;
using System.Windows.Input;

namespace SR_DMG.Source.UI.Event
{
    public class IProgress
    {
        public ICommand Retry_Click { get; } = new Command<Progress>(Model =>
        {
            Model.IsRetry = true;
        });

        public ICommand Cancel_Click { get; } = new Command<Progress>(Model =>
        {
            Model.Canceller.Cancel();
            Model.Dorpdown = false;
        });
    }
}