using StarRailDamage.Source.UI.Factory.PropertyBinding;
using StarRailDamage.Source.UI.Model.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StarRailDamage.Source.UI.Xaml.View
{
    public partial class LabelTextBox : UserControl
    {
        private static readonly PropertyBindingFactory<LabelTextBox> BindingFactory = new();

        public LabelTextBox()
        {
            InitializeComponent();
            Unloaded += (sender, e) =>
            {
                BindingFactory.ClearModelBinding(Model);
            };
        }

        public LabelTextBoxModel Model
        {
            get => (LabelTextBoxModel)GetValue(ModelProperty);
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

        public string Unit
        {
            get => (string)GetValue(UnitProperty);
            set => SetValue(UnitProperty, value);
        }

        public static readonly DependencyProperty UnitProperty = BindingFactory.DependBinding(x => x.Model.Unit, x => x.Unit);

        public bool ReadOnly
        {
            get => (bool)GetValue(ReadOnlyProperty);
            set => SetValue(ReadOnlyProperty, value);
        }

        public static readonly DependencyProperty ReadOnlyProperty = BindingFactory.DependProperty(x => x.ReadOnly);

        public SolidColorBrush FocusBrush
        {
            get => (SolidColorBrush)GetValue(FocusBrushProperty);
            set => SetValue(FocusBrushProperty, value);
        }

        public static readonly DependencyProperty FocusBrushProperty = BindingFactory.DependProperty(x => x.FocusBrush);
    }
}