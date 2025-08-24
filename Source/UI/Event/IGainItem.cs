using SR_DMG.Source.UI.Model;
using System.Windows.Input;

namespace SR_DMG.Source.UI.Event
{
    public class IGainItem
    {
        public ICommand Modify_Click { get; } = new Command<(GainView Gview, GainItem Gitem)>(Tuple =>
        {

        });

        public ICommand Delete_Click { get; } = new Command<(GainView Gview, GainItem Gitem)>(Tuple =>
        {

        });
    }
}