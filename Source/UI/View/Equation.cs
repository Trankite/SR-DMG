using SR_DMG.Source.UI.Event;
using System.Windows;
using System.Windows.Controls;

namespace SR_DMG.Source.UI.View
{
    public class Equation : Control
    {
        static Equation()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Equation), new FrameworkPropertyMetadata(typeof(Equation)));
        }

        public static IEquation Event { get; } = new();

        public Model.Equation Model
        {
            set { SetValue(ModelProperty, value); }
            get { return (Model.Equation)GetValue(ModelProperty); }
        }

        public static readonly DependencyProperty ModelProperty = DependencyProperty.Register(nameof(Model), typeof(Model.Equation), typeof(Equation));

        public bool Dropdown
        {
            set { SetValue(DropdownProperty, value); }
            get { return (bool)GetValue(DropdownProperty); }
        }

        public static readonly DependencyProperty DropdownProperty = DependencyProperty.Register(nameof(Dropdown), typeof(bool), typeof(Equation));
    }
}