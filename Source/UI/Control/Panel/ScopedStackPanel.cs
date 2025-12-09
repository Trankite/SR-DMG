using System.Windows;
using System.Windows.Controls;

namespace StarRailDamage.Source.UI.Control.Panel
{
    public class ScopedStackPanel : StackPanel
    {
        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(nameof(Spacing), typeof(double), typeof(ScopedStackPanel));

        protected override Size MeasureOverride(Size constraint)
        {
            Size PanelSize = base.MeasureOverride(constraint);
            double AccumulatedPosition = (InternalChildren.Count - 1) * Spacing;
            if (AccumulatedPosition > 0)
            {
                if (Orientation == Orientation.Horizontal)
                {
                    PanelSize.Width += AccumulatedPosition;
                }
                else
                {
                    PanelSize.Height += AccumulatedPosition;
                }
            }
            return PanelSize;
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            double AccumulatedPosition = 0;
            UIElementCollection Children = InternalChildren;
            for (int i = 0; i < Children.Count; i++)
            {
                UIElement Child = Children[i];
                if (Child is null) continue;
                Size ChildSize = Child.DesiredSize;
                Rect FinalRect;
                if (Orientation == Orientation.Horizontal)
                {
                    FinalRect = new Rect(AccumulatedPosition, 0, ChildSize.Width, arrangeSize.Height);
                }
                else
                {
                    FinalRect = new Rect(0, AccumulatedPosition, arrangeSize.Width, ChildSize.Height);
                }
                Child.Arrange(FinalRect);
                if (Orientation == Orientation.Horizontal)
                {
                    AccumulatedPosition += ChildSize.Width + Spacing;
                }
                else
                {
                    AccumulatedPosition += ChildSize.Height + Spacing;
                }
            }
            return arrangeSize;
        }
    }
}