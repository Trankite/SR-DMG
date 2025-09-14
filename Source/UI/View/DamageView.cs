using SR_DMG.Source.UI.Model;
using System.Windows;
using System.Windows.Controls;

namespace SR_DMG.Source.UI.View
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

        public static readonly DependencyProperty DamageProperty = DependencyProperty.Register(nameof(Damage), typeof(Damage), typeof(DamageView));

        public Damage Compare
        {
            set { SetValue(CompareProperty, value); }
            get { return (Damage)GetValue(CompareProperty); }
        }

        public static readonly DependencyProperty CompareProperty = DependencyProperty.Register(nameof(Compare), typeof(Damage), typeof(DamageView));
        
        public string Breaker
        {
            set { SetValue(BreakerProperty, value); }
            get { return (string)GetValue(BreakerProperty); }
        }

        public static readonly DependencyProperty BreakerProperty = DependencyProperty.Register(nameof(Breaker), typeof(string), typeof(DamageView));
    }
}