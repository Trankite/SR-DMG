using StarRailDamage.Source.UI.Factory.PropertyBinding;
using StarRailDamage.Source.UI.Model.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StarRailDamage.Source.UI.Xaml.View
{
    public partial class NumberTextBox : UserControl
    {
        private static readonly PropertyBindingFactory<NumberTextBox> BindingFactory = new();

        public NumberTextBox()
        {
            InitializeComponent();
            Unloaded += (sender, e) =>
            {
                BindingFactory.ClearModelBinding(Model);
            };
        }

        public NumberTextBoxModel Model
        {
            get => (NumberTextBoxModel)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }

        public static readonly DependencyProperty ModelProperty = BindingFactory.ModelBinding(x => x.Model);

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

        public static readonly DependencyProperty TextProperty = BindingFactory.DependBinding(x => x.Model.Text, x => x.Text, PropertyBindingMode.TwoWay);

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty ValueProperty = BindingFactory.DependBinding(x => x.Model.Value, x => x.Value);

        public SolidColorBrush FocusBrush
        {
            get => (SolidColorBrush)GetValue(FocusBrushProperty);
            set => SetValue(FocusBrushProperty, value);
        }

        public static readonly DependencyProperty FocusBrushProperty = BindingFactory.DependProperty(x => x.FocusBrush);
    }
}