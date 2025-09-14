using SR_DMG.Source.UI.View;
using System.Windows.Input;

namespace SR_DMG.Source.UI.Event
{
    public class IEquation
    {
        public ICommand TextBox_Enter { get; } = new Command<Equation>((Euton) =>
        {
            Euton.Focus();
        });

        public ICommand TextBox_TextChanged { get; } = new Command<Equation>(Euton =>
        {
            Euton.Dropdown = true;
        });

        public ICommand TextBox_LostFocus { get; } = new Command<Equation>((Euton) =>
        {
            Euton.Dropdown = false;
        });

        public ICommand ListBox_SelectionChanged { get; } = new Command<Equation>(Euton =>
        {

        });
    }
}