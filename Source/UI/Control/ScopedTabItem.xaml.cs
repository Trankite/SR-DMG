using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StarRailDamage.Source.UI.Control
{
    public partial class ScopedTabItem : TabItem
    {
        public ScopedTabItem()
        {
            InitializeComponent();
        }

        private void TabItemCloseClick(object sender, RoutedEventArgs e)
        {
            if (!e.Handled) RaiseEvent(new RoutedEventArgs(TabItemCloseEvent, this));
        }

        public static readonly RoutedEvent TabItemCloseEvent = EventManager.RegisterRoutedEvent(nameof(TabItemCloseEvent), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ScopedTabItem));

        public event RoutedEventHandler TabItemClose
        {
            add => AddHandler(TabItemCloseEvent, value);
            remove => RemoveHandler(TabItemCloseEvent, value);
        }

        public SolidColorBrush FocusBrush
        {
            get => (SolidColorBrush)GetValue(FocusBrushProperty);
            set => SetValue(FocusBrushProperty, value);
        }

        public static readonly DependencyProperty FocusBrushProperty = DependencyProperty.Register(nameof(FocusBrush), typeof(SolidColorBrush), typeof(ScopedTabItem));
    }
}