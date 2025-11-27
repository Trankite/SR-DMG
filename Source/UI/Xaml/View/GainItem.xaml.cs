using StarRailDamage.Source.UI.Factory.PropertyBinding;
using StarRailDamage.Source.UI.Model.Control;
using StarRailDamage.Source.UI.Model.View;
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
            Unloaded += (sender, e) =>
            {
                BindingFactory.ClearModelBinding(Model);
            };
        }

        private void ModifyItemClick(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteItemClick(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(DeleteEvent, this));
        }

        public static readonly RoutedEvent DeleteEvent = EventManager.RegisterRoutedEvent(nameof(DeleteEvent), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(GainItem));

        public event RoutedEventHandler DeleteClick
        {
            add => AddHandler(DeleteEvent, value);
            remove => RemoveHandler(DeleteEvent, value);
        }

        public GainItemModel Model
        {
            get => (GainItemModel)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }

        public static readonly DependencyProperty ModelProperty = BindingFactory.ModelBinding(x => x.Model);

        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty = BindingFactory.DependBinding(x => x.Model.Icon, x => x.Icon);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty = BindingFactory.DependBinding(x => x.Model.Title, x => x.Title);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty = BindingFactory.DependBinding(x => x.Model.Text, x => x.Text);

        public bool Flag
        {
            get => (bool)GetValue(FlagProperty);
            set => SetValue(FlagProperty, value);
        }

        public static readonly DependencyProperty FlagProperty = BindingFactory.DependBinding(x => x.Model.Flag, x => x.Flag, PropertyBindingMode.TwoWay);

        public ObservableCollection<string> MarkItems
        {
            get => (ObservableCollection<string>)GetValue(MarkItemsProperty);
            set => SetValue(MarkItemsProperty, value);
        }

        public static readonly DependencyProperty MarkItemsProperty = BindingFactory.DependBinding(x => x.Model.MarkItems, x => x.MarkItems);

        public ObservableCollection<string> TempItems
        {
            get => (ObservableCollection<string>)GetValue(TempItemsProperty);
            set => SetValue(TempItemsProperty, value);
        }

        public static readonly DependencyProperty TempItemsProperty = BindingFactory.DependBinding(x => x.Model.TempItems, x => x.TempItems);

        public ObservableCollection<ScopedSliderModel> SliderItems
        {
            get => (ObservableCollection<ScopedSliderModel>)GetValue(SliderItemsProperty);
            set => SetValue(SliderItemsProperty, value);
        }

        public static readonly DependencyProperty SliderItemsProperty = BindingFactory.DependBinding(x => x.Model.SliderItems, x => x.SliderItems);

        public double TitleFontSize
        {
            get => (double)GetValue(TitleFontSizeProperty);
            set => SetValue(TitleFontSizeProperty, value);
        }

        public static readonly DependencyProperty TitleFontSizeProperty = BindingFactory.DependProperty(x => x.TitleFontSize);

        public SolidColorBrush FocusBrush
        {
            get => (SolidColorBrush)GetValue(FocusBrushProperty);
            set => SetValue(FocusBrushProperty, value);
        }

        public static readonly DependencyProperty FocusBrushProperty = BindingFactory.DependProperty(x => x.FocusBrush);

        public SolidColorBrush TitleBrush
        {
            get => (SolidColorBrush)GetValue(TitleBrushProperty);
            set => SetValue(TitleBrushProperty, value);
        }

        public static readonly DependencyProperty TitleBrushProperty = BindingFactory.DependProperty(x => x.TitleBrush);
    }
}