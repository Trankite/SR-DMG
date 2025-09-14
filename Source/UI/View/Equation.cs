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

        public string Text
        {
            set { SetValue(TextProperty, value); }
            get { return (string)GetValue(TextProperty); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(Equation));

        public float Value
        {
            set { SetValue(ValueProperty, value); }
            get { return (float)GetValue(ValueProperty); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(float), typeof(Equation));

        public bool Dropdown
        {
            set { SetValue(DropdownProperty, value); }
            get { return (bool)GetValue(DropdownProperty); }
        }

        public static readonly DependencyProperty DropdownProperty = DependencyProperty.Register(nameof(Dropdown), typeof(bool), typeof(Equation));
    }
}