using SR_DMG.Source.UI.Model;
using System.Windows.Input;

namespace SR_DMG.Source.UI.Event
{
    public class IGainView
    {
        public ICommand SelectItem_Click { get; } = new Command<GainView>(Obj => Obj.IsOpen = true);
        public ICommand GainItem_Click { get; } = new Command<(GainView Gview, GainItem Gitem)>(Obj =>
        {
            Obj.Gview.Select = Obj.Gitem.Gain;
            Obj.Gview.IsOpen = false;
        });
    }
}