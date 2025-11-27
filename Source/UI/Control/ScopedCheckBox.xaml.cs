using System.Windows;
using System.Windows.Controls;

namespace StarRailDamage.Source.UI.Control
{
    public partial class ScopedCheckBox : UserControl
    {
        public ScopedCheckBox()
        {
            InitializeComponent();
        }

        private void CheckedCharge(object sender, RoutedEventArgs e) => Flag = !Flag;

        public bool Flag
        {
            get => (bool)GetValue(FlagProperty);
            set => SetValue(FlagProperty, value);
        }

        public static readonly DependencyProperty FlagProperty = DependencyProperty.Register(nameof(Flag), typeof(bool), typeof(ScopedCheckBox));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(ScopedCheckBox), new PropertyMetadata(OnTextCharged));

        private static void OnTextCharged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ScopedCheckBox)d).Padding = new Thickness(string.IsNullOrEmpty(e.NewValue as string) ? 0 : 5, 0, 0, 0);
        }
    }
}