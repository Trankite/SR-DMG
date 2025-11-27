using StarRailDamage.Source.UI.Model.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StarRailDamage.Source.UI.Xaml.View
{
    public partial class GainView : UserControl
    {
        public GainView()
        {
            InitializeComponent();
        }

        private void GainItemDropdown(object sender, MouseButtonEventArgs e)
        {
            if (Items?.Count > 0) Dropdown = true;
        }

        private void GainItemSelected(object sender, MouseButtonEventArgs e)
        {
            Dropdown = false;
            Select = ((GainItem)sender).Model;
        }

        private void GainItemDelete(object sender, RoutedEventArgs e)
        {
            Items.Remove(((GainItem)sender).Model);
        }

        public GainViewModel Model
        {
            get => (GainViewModel)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }

        public static readonly DependencyProperty ModelProperty = DependencyProperty.Register(nameof(Model), typeof(GainViewModel), typeof(GainView));

        public ObservableCollection<GainItemModel> Items
        {
            get => (ObservableCollection<GainItemModel>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(nameof(Items), typeof(ObservableCollection<GainItemModel>), typeof(GainView));

        public GainItemModel Select
        {
            get => (GainItemModel)GetValue(SelectProperty);
            set => SetValue(SelectProperty, value);
        }

        public static readonly DependencyProperty SelectProperty = DependencyProperty.Register(nameof(Select), typeof(GainItemModel), typeof(GainView));

        public bool Dropdown
        {
            get => (bool)GetValue(DropdownProperty);
            set => SetValue(DropdownProperty, value);
        }

        public static readonly DependencyProperty DropdownProperty = DependencyProperty.Register(nameof(Dropdown), typeof(bool), typeof(GainView));
    }
}