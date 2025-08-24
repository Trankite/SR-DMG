using SR_DMG.Source.Example;
using System.Windows;
using System.Windows.Controls;

namespace SR_DMG.Source.UI.Model
{
    public class DamageView : Control
    {
        static DamageView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DamageView), new FrameworkPropertyMetadata(typeof(DamageView)));
        }

        public Damage Damage
        {
            set { SetValue(DamageProperty, value); }
            get { return (Damage)GetValue(DamageProperty); }
        }
        public static readonly DependencyProperty DamageProperty =
            DependencyProperty.Register(nameof(Damage), typeof(Damage), typeof(DamageView),
                new PropertyMetadata(new Damage()));
    }
}