using SR_DMG.Source.UI.Event;
using System.Windows;
using System.Windows.Controls;

namespace SR_DMG.Source.UI.Model
{
    public class ComplexTicker : Control
    {
        static ComplexTicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ComplexTicker), new FrameworkPropertyMetadata(typeof(ComplexTicker)));
        }

        public static ITicker Event { get; } = new();

        public bool Flag
        {
            set { SetValue(FlagProperty, value); }
            get { return (bool)GetValue(FlagProperty); }
        }
        public static readonly DependencyProperty FlagProperty =
            DependencyProperty.Register(nameof(Flag), typeof(bool), typeof(ComplexTicker),
                new PropertyMetadata(false));

        public string Text
        {
            set { SetValue(TextProperty, value); }
            get { return (string)GetValue(TextProperty); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(ComplexTicker),
                new PropertyMetadata(string.Empty));
    }
}