using SR_DMG.Source.UI.Model;
using System.Windows.Input;

namespace SR_DMG.Source.UI.Event
{
    public class IGainView
    {
        public ICommand SelectItem_Click { get; } = new Command<GainView>(Model =>
        {
            Model.Dropdown = true;
        });

        public ICommand GainItem_Click { get; } = new Command<object[]>(Pams =>
        {
            GainView Model_GainView = (GainView)Pams[0];
            GainItem Model_GainItem = (GainItem)Pams[1];
            Model_GainView.Select = Model_GainItem;
            Model_GainView.Dropdown = false;
        });
    }
}