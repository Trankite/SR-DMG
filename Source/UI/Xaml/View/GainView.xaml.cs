using StarRailDamage.Source.UI.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace StarRailDamage.Source.UI.Xaml.View
{
    public partial class GainView : UserControl
    {
        public GainView()
        {
            InitializeComponent();
            Select = new()
            {
                Title = "默认的名称",
                Text = "默认的描述",
                MarkItems = ["默认标签1", "默认标签2", "默认标签3"],
                TempItems = ["默认标签4", "默认标签5", "默认标签6"],
                MaxLevel = 10,
                MaxLayer = 15
            };
        }

        public GainViewModel Model
        {
            get => (GainViewModel)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }

        private static readonly DependencyProperty ModelProperty = DependencyProperty.Register(nameof(Model), typeof(GainViewModel), typeof(GainView));

        public ObservableCollection<GainItemModel> Items
        {
            get => (ObservableCollection<GainItemModel>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        private static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(nameof(Items), typeof(ObservableCollection<GainItemModel>), typeof(GainView));

        public GainItemModel Select
        {
            get => (GainItemModel)GetValue(SelectProperty);
            set => SetValue(SelectProperty, value);
        }

        private static readonly DependencyProperty SelectProperty = DependencyProperty.Register(nameof(Select), typeof(GainItemModel), typeof(GainView));
    }
}