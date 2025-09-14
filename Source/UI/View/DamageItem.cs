using System.Windows;
using System.Windows.Controls;

namespace SR_DMG.Source.UI.View
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

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(DamageItem));

        public string Value
        {
            set { SetValue(ValueProperty, value); }
            get { return (string)GetValue(ValueProperty); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(string), typeof(DamageItem));

        public string Enhance
        {
            set { SetValue(EnhanceProperty, value); }
            get { return (string)GetValue(EnhanceProperty); }
        }

        public static readonly DependencyProperty EnhanceProperty = DependencyProperty.Register(nameof(Enhance), typeof(string), typeof(DamageItem));
    }
}