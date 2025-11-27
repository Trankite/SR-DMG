using StarRailDamage.Source.UI.Control;
using StarRailDamage.Source.UI.Control.Panel;
using StarRailDamage.Source.UI.Model.Control;
using StarRailDamage.Source.UI.Xaml.Page;
using System.Collections.ObjectModel;
using System.Windows;

namespace StarRailDamage.Source.UI.Xaml.Window
{
    public partial class MainWindow : ScopedWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            TabItems = [new("首页", new CombatSimulationPage()), new("白厄"), new("丹恒•腾荒"), new("刻律德菈"), new("星期日"),];
        }

        private void TabItemClose(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is not ScopedTabItem ScopedTabItem) return;
            if (ScopedTabItem.Content is ScopedTabItemModel ScopedTabItemModel)
            {
                TabItems.Remove(ScopedTabItemModel);
            }
        }

        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(nameof(SelectedIndex), typeof(int), typeof(MainWindow));

        public ObservableCollection<ScopedTabItemModel> TabItems
        {
            get => (ObservableCollection<ScopedTabItemModel>)GetValue(TabItemsProperty);
            set => SetValue(TabItemsProperty, value);
        }

        public static readonly DependencyProperty TabItemsProperty = DependencyProperty.Register(nameof(TabItems), typeof(ObservableCollection<ScopedTabItemModel>), typeof(MainWindow));
    }
}