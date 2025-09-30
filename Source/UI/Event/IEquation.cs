using SR_DMG.Source.UI.View;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace SR_DMG.Source.UI.Event
{
    public class IEquation
    {
        public ICommand TextBox_Enter { get; } = new Command<Equation>(Model =>
        {
            Model.Focus();
        });

        public ICommand TextBox_LostFocus { get; } = new Command<Equation>(Model =>
        {
            Model.Dropdown = false;
        });

        public ICommand TextBox_TextChanged { get; } = new Command<object[]>(Pams =>
        {
            TextBox Tebox = (TextBox)Pams[1];
            StringBuilder Builder = new(Tebox.Text);
            for (int i = 0; i < Builder.Length; i++)
            {
                Builder[i] = Builder[i] switch
                {
                    '【' => '[',
                    '】' => ']',
                    '（' => '(',
                    '）' => ')',
                    '：' => ':',
                    _ => Builder[i]
                };
            }
            int Index = Tebox.SelectionStart;
            Tebox.Text = Builder.ToString();
            Tebox.SelectionStart = Index;
            Equation Equton = (Equation)Pams[0];
            if (Index > 0 && Index <= Tebox.Text.Length)
            {
                Equton.Dropdown = "[:".Contains(Tebox.Text[Index - 1]);
            }
            else
            {
                Equton.Dropdown = false;
            }
        });

        public ICommand ListBox_SelectionChanged { get; } = new Command<object[]>(Pams =>
        {
            ListBox Ltbox = (ListBox)Pams[0];
            if (Ltbox.SelectedIndex < 0) return;
            TextBox Tebox = (TextBox)Pams[1];
            int Index = Tebox.SelectionStart;
            string Select = ((ImageLabel)Ltbox.SelectedItem).Title;
            Ltbox.SelectedIndex = -1;
            int EndIndex = -1;
            for (int i = Index; i < Tebox.Text.Length; i++)
            {
                if (Tebox.Text[i] == '[') break;
                else if (Tebox.Text[i] == ']')
                {
                    EndIndex = i;
                    break;
                }
            }
            if (EndIndex > 0)
            {
                Tebox.Text = Tebox.Text[..Index] + Select + Tebox.Text[EndIndex..];
            }
            else
            {
                Tebox.Text = Tebox.Text.Insert(Index, Select + ']');
            }
            Tebox.SelectionStart = Index + Select.Length + 1;
            Tebox.Focus();
        });
    }
}