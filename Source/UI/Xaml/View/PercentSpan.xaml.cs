using StarRailDamage.Source.UI.Factory.PropertyBinding;
using StarRailDamage.Source.UI.Model.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StarRailDamage.Source.UI.Xaml.View
{
    public partial class PercentSpan : UserControl
    {
        private static readonly PropertyBindingFactory<PercentSpan> BindingFactory = new();

        public PercentSpan()
        {
            InitializeComponent();
            Unloaded += (sender, e) =>
            {
                BindingFactory.ClearModelBinding(Model);
            };
        }

        private static void ValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PercentSpan PercentSpan)
            {
                PercentSpan.Percent = PercentSpan.Value / PercentSpan.TempValue - 1;
            }
        }

        private static void PercentChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PercentSpan PercentSpan)
            {
                PercentSpan.TempBrush = PercentSpan.Percent >= 0 ? PercentSpan.PlusBrush : PercentSpan.MinuBrush;
            }
        }

        public PercentSpanModel Model
        {
            get => (PercentSpanModel)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }

        public static readonly DependencyProperty ModelProperty = BindingFactory.ModelBinding(x => x.Model);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty = BindingFactory.DependBinding(x => x.Model.Title, x => x.Title);

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty ValueProperty = BindingFactory.DependBinding(x => x.Model.Value, x => x.Value, PropertyBindingMode.OneWay, default, ValueChangedCallback);

        public double TempValue
        {
            get => (double)GetValue(TempValueProperty);
            set => SetValue(TempValueProperty, value);
        }

        public static readonly DependencyProperty TempValueProperty = BindingFactory.DependBinding(x => x.Model.TempValue, x => x.TempValue, PropertyBindingMode.OneWay, default, ValueChangedCallback);

        public double Percent
        {
            get => (double)GetValue(PercentProperty);
            set => SetValue(PercentProperty, value);
        }

        public static readonly DependencyProperty PercentProperty = BindingFactory.DependBinding(x => x.Model.Percent, x => x.Percent, PropertyBindingMode.OneWay, default, PercentChangedCallback);

        public SolidColorBrush PlusBrush
        {
            get => (SolidColorBrush)GetValue(PlusBrushProperty);
            set => SetValue(PlusBrushProperty, value);
        }

        public static readonly DependencyProperty PlusBrushProperty = BindingFactory.DependProperty(x => x.PlusBrush);

        public SolidColorBrush MinuBrush
        {
            get => (SolidColorBrush)GetValue(MinuBrushProperty);
            set => SetValue(MinuBrushProperty, value);
        }

        public static readonly DependencyProperty MinuBrushProperty = BindingFactory.DependProperty(x => x.MinuBrush);

        public SolidColorBrush TempBrush
        {
            get => (SolidColorBrush)GetValue(TempBrushProperty);
            set => SetValue(TempBrushProperty, value);
        }

        public static readonly DependencyProperty TempBrushProperty = BindingFactory.DependProperty(x => x.TempBrush);

        public double DropFontSize
        {
            get => (double)GetValue(DropFontSizeProperty);
            set => SetValue(DropFontSizeProperty, value);
        }

        public static readonly DependencyProperty DropFontSizeProperty = BindingFactory.DependProperty(x => x.DropFontSize);
    }
}