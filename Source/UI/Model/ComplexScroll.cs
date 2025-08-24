using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SR_DMG.Source.UI.Model
{
    public class ComplexScroll : ScrollViewer
    {
        static ComplexScroll()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ComplexScroll), new FrameworkPropertyMetadata(typeof(ComplexScroll)));
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (e.Delta >= 0 ? HorizontalOffset > 0 : ViewportWidth + HorizontalOffset < ExtentWidth)
            {
                ScrollToHorizontalOffset(HorizontalOffset - e.Delta);
                e.Handled = true;
            }
            else
            {
                if (e.Delta >= 0) { if (VerticalOffset == 0) return; }
                else if (ViewportHeight + VerticalOffset >= ExtentHeight) return;
            }
            base.OnMouseWheel(e);
        }
    }
}