using SR_DMG.Source.UI.View;
using System.Windows.Input;

namespace SR_DMG.Source.UI.Event
{
    public class ILabelTextBox
    {
        public ICommand TextBox_Enter { get; } = new Command<LabelTextBox>(Ltbox =>
        {
            Ltbox.Focus();
        });

        public ICommand TextBox_GotFocus { get; } = new Command<LabelTextBox>(Ltbox =>
        {
            if (Ltbox.Text == "0") Ltbox.Text = string.Empty;
        });

        public ICommand TextBox_LostFocus { get; } = new Command<LabelTextBox>(Ltbox =>
        {
            if (Ltbox.Text == string.Empty) Ltbox.Text = "0";
        });
    }
}