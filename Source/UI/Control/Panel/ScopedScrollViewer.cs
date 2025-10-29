using System.Windows.Controls;
using System.Windows.Input;

namespace StarRailDamage.Source.UI.Control.Panel
{
    public class ScopedScrollViewer : ScrollViewer
    {
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (VerticalScrollBarVisibility == ScrollBarVisibility.Disabled)
            {
                if (HorizontalScrollBarVisibility == ScrollBarVisibility.Disabled) return;
                ScrollToHorizontalOffset(HorizontalOffset - e.Delta); e.Handled = true;
            }
            else if (e.Delta >= 0 ? VerticalOffset > 0 : ViewportHeight + VerticalOffset < ExtentHeight)
            {
                ScrollToVerticalOffset(VerticalOffset - e.Delta);
            }
        }
    }
}