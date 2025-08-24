using SR_DMG.Source.Example;
using SR_DMG.Source.UI.Event;
using System.Windows;
using System.Windows.Controls;

namespace SR_DMG.Source.UI.Model
{
    public class GainView : Control
    {
        static GainView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GainView), new FrameworkPropertyMetadata(typeof(GainView)));
        }

        public static IGainView Event { get; } = new();

        public Gain Select
        {
            set { SetValue(SelectProperty, value); }
            get { return (Gain)GetValue(SelectProperty); }
        }
        public static readonly DependencyProperty SelectProperty =
            DependencyProperty.Register(nameof(Select), typeof(Gain), typeof(GainView),
                new PropertyMetadata(new Gain()));

        public List<Gain> Items
        {
            set { SetValue(ItemsProperty, value); }
            get { return (List<Gain>)GetValue(ItemsProperty); }
        }
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register(nameof(Items), typeof(List<Gain>), typeof(GainView),
                new PropertyMetadata(new List<Gain>()));

        public bool IsOpen
        {
            set { SetValue(IsOpenProperty, value); }
            get { return (bool)GetValue(IsOpenProperty); }
        }
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(GainView),
                new PropertyMetadata(false));
    }
}