using System.Windows;
using System.Windows.Controls;

namespace SR_DMG.Source.UI.Model
{
    public class ComplexSlider : Control
    {
        static ComplexSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ComplexSlider), new FrameworkPropertyMetadata(typeof(ComplexSlider)));
        }

        public string Title
        {
            set { SetValue(TitleProperty, value); }
            get { return (string)GetValue(TitleProperty); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(ComplexSlider),
                new PropertyMetadata(string.Empty));

        public double Value
        {
            set { SetValue(ValueProperty, value); }
            get { return (double)GetValue(ValueProperty); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(ComplexSlider),
                new PropertyMetadata(0d));

        public double Maximum
        {
            set { SetValue(MaximumProperty, value); }
            get { return (double)GetValue(MaximumProperty); }
        }
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(nameof(Maximum), typeof(double), typeof(ComplexSlider),
                new PropertyMetadata(0d));
    }
}