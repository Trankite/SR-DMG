using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StarRailDamage.Source.UI.Control
{
    public partial class ScopedTabControl : TabControl
    {
        public ScopedTabControl()
        {
            InitializeComponent();
        }

        private void TabItemCloseClick(object sender, RoutedEventArgs e)
        {
            (sender as ScopedTabItem)?.TabItemClose -= TabItemCloseClick;
            if (!e.Handled) RaiseEvent(new RoutedEventArgs(TabItemCloseEvent, sender));
        }

        public static readonly RoutedEvent TabItemCloseEvent = EventManager.RegisterRoutedEvent(nameof(TabItemCloseEvent), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ScopedTabControl));

        public event RoutedEventHandler TabItemClose
        {
            add => AddHandler(TabItemCloseEvent, value);
            remove => RemoveHandler(TabItemCloseEvent, value);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            ScopedTabItem ScopedTabItem = new();
            ScopedTabItem.TabItemClose += TabItemCloseClick;
            return ScopedTabItem;
        }

        protected override bool IsItemItsOwnContainerOverride(object item) => item is ScopedTabItem;

        public SolidColorBrush FocusBrush
        {
            get => (SolidColorBrush)GetValue(FocusBrushProperty);
            set => SetValue(FocusBrushProperty, value);
        }

        public static readonly DependencyProperty FocusBrushProperty = DependencyProperty.Register(nameof(FocusBrush), typeof(SolidColorBrush), typeof(ScopedTabControl));
    }
}