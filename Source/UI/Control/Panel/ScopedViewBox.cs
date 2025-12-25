using StarRailDamage.Source.Extension;
using System.Windows;
using System.Windows.Controls;

namespace StarRailDamage.Source.UI.Control.Panel
{
    public class ScopedViewBox : Viewbox
    {
        public double MinScale
        {
            get => (double)GetValue(MinScaleProperty);
            set => SetValue(MinScaleProperty, value);
        }

        public static readonly DependencyProperty MinScaleProperty = DependencyProperty.Register(nameof(MinScale), typeof(double), typeof(ScopedViewBox));

        public double MaxScale
        {
            get => (double)GetValue(MaxScaleProperty);
            set => SetValue(MaxScaleProperty, value);
        }

        public static readonly DependencyProperty MaxScaleProperty = DependencyProperty.Register(nameof(MaxScale), typeof(double), typeof(ScopedViewBox), new PropertyMetadata(1D));

        protected override Size MeasureOverride(Size constraint)
        {
            if (Child.IsNull()) return constraint;
            Child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            Size ChildSize = Child.DesiredSize;
            if (ChildSize.IsHidden()) return ChildSize;
            double ScaleWidth = constraint.Width / ChildSize.Width;
            double ScaleHeight = constraint.Height / ChildSize.Height;
            double FinalScale = Math.Max(MinScale, Math.Min(MaxScale, Math.Min(ScaleWidth, ScaleHeight)));
            double FinalWidth = Math.Min(ChildSize.Width * FinalScale, constraint.Width);
            double FinalHeight = Math.Min(ChildSize.Height * FinalScale, constraint.Height);
            Child.Measure(new Size((FinalWidth / FinalScale).Ceiling(2), (FinalHeight / FinalScale).Ceiling(2)));
            return new Size(FinalWidth, FinalHeight);
        }
    }
}