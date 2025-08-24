using SR_DMG.Source.Example;
using SR_DMG.Source.UI.Event;
using System.Windows;
using System.Windows.Controls;

namespace SR_DMG.Source.UI.Model
{
    public class GainItem : Control
    {
        static GainItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GainItem), new FrameworkPropertyMetadata(typeof(GainItem)));
        }

        public static IGainItem Event { get; } = new();

        public Gain Gain
        {
            set { SetValue(GainProperty, value); }
            get { return (Gain)GetValue(GainProperty); }
        }
        public static readonly DependencyProperty GainProperty =
            DependencyProperty.Register(nameof(Gain), typeof(Gain), typeof(GainItem),
                new PropertyMetadata(new Gain()));
    }
}