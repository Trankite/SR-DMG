using StarRailDamage.Source.UI.Factory.PropertyBinding;
using StarRailDamage.Source.UI.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StarRailDamage.Source.UI.Xaml.View
{
    public partial class GainItem : UserControl
    {
        private static readonly PropertyBindingFactory<GainItem> BindingFactory = new();

        public GainItem()
        {
            InitializeComponent();
        }

        private void ModifyItem(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {

        }

        public GainItemModel Model
        {
            get => (GainItemModel)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }

        private static readonly DependencyProperty ModelProperty = BindingFactory.ModelBinding<GainItemModel>(nameof(Model));

        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        private static readonly DependencyProperty IconProperty = BindingFactory.DependBinding(x => x.Model.Icon, x => x.Icon);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        private static readonly DependencyProperty TitleProperty = BindingFactory.DependBinding(x => x.Model.Title, x => x.Title);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static readonly DependencyProperty TextProperty = BindingFactory.DependBinding(x => x.Model.Text, x => x.Text);

        public bool Flag
        {
            get => (bool)GetValue(FlagProperty);
            set => SetValue(FlagProperty, value);
        }

        private static readonly DependencyProperty FlagProperty = BindingFactory.DependBinding(x => x.Model.Flag, x => x.Flag, PropertyBindingMode.TwoWay);

        public ObservableCollection<string> MarkItems
        {
            get => (ObservableCollection<string>)GetValue(MarkItemsProperty);
            set => SetValue(MarkItemsProperty, value);
        }

        private static readonly DependencyProperty MarkItemsProperty = BindingFactory.DependBinding(x => x.Model.MarkItems, x => x.MarkItems);

        public ObservableCollection<string> TempItems
        {
            get => (ObservableCollection<string>)GetValue(TempItemsProperty);
            set => SetValue(TempItemsProperty, value);
        }

        private static readonly DependencyProperty TempItemsProperty = BindingFactory.DependBinding(x => x.Model.TempItems, x => x.TempItems);

        public double Level
        {
            get => (double)GetValue(LevelProperty);
            set => SetValue(LevelProperty, value);
        }

        private static readonly DependencyProperty LevelProperty = BindingFactory.DependBinding(x => x.Model.Level, x => x.Level, PropertyBindingMode.TwoWay);

        public int MaxLevel
        {
            get => (int)GetValue(MaxLevelProperty);
            set => SetValue(MaxLevelProperty, value);
        }

        private static readonly DependencyProperty MaxLevelProperty = BindingFactory.DependBinding(x => x.Model.MaxLevel, x => x.MaxLevel);

        public double Layer
        {
            get => (double)GetValue(LayerProperty);
            set => SetValue(LayerProperty, value);
        }

        private static readonly DependencyProperty LayerProperty = BindingFactory.DependBinding(x => x.Model.Layer, x => x.Layer, PropertyBindingMode.TwoWay);

        public int MaxLayer
        {
            get => (int)GetValue(MaxLayerProperty);
            set => SetValue(MaxLayerProperty, value);
        }

        private static readonly DependencyProperty MaxLayerProperty = BindingFactory.DependBinding(x => x.Model.MaxLayer, x => x.MaxLayer);

        public double TitleFontSize
        {
            get => (double)GetValue(TitleFontSizeProperty);
            set => SetValue(TitleFontSizeProperty, value);
        }

        private static readonly DependencyProperty TitleFontSizeProperty = BindingFactory.DependProperty<double>(nameof(TitleFontSize));

        public SolidColorBrush FocusBrush
        {
            get => (SolidColorBrush)GetValue(FocusBrushProperty);
            set => SetValue(FocusBrushProperty, value);
        }

        private static readonly DependencyProperty FocusBrushProperty = BindingFactory.DependProperty<SolidColorBrush>(nameof(FocusBrush));

        public SolidColorBrush TitleBrush
        {
            get => (SolidColorBrush)GetValue(TitleBrushProperty);
            set => SetValue(TitleBrushProperty, value);
        }

        private static readonly DependencyProperty TitleBrushProperty = BindingFactory.DependProperty<SolidColorBrush>(nameof(TitleBrush));
    }
}