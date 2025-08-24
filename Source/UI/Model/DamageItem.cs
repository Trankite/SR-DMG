using System.Windows;
using System.Windows.Controls;

namespace SR_DMG.Source.UI.Model
{
    public class DamageItem : Control
    {
        static DamageItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DamageItem), new FrameworkPropertyMetadata(typeof(DamageItem)));
        }

        public string Title
        {
            set { SetValue(TitleProperty, value); }
            get { return (string)GetValue(TitleProperty); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(DamageItem),
                new PropertyMetadata(string.Empty));

        public string Value
        {
            set { SetValue(ValueProperty, value); }
            get { return (string)GetValue(ValueProperty); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(string), typeof(DamageItem),
                new PropertyMetadata(string.Empty));
    }
}