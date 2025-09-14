using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SR_DMG.Source.UI.View
{
    public class PlexScroll : ScrollViewer
    {
        static PlexScroll()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PlexScroll), new FrameworkPropertyMetadata(typeof(PlexScroll)));
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (VerticalScrollBarVisibility == ScrollBarVisibility.Disabled)
            {
                ScrollToHorizontalOffset(HorizontalOffset - e.Delta); e.Handled = true;
            }
            else if (e.Delta >= 0 ? VerticalOffset > 0 : ViewportHeight + VerticalOffset < ExtentHeight)
            {
                ScrollToVerticalOffset(VerticalOffset - e.Delta);
            }
        }
    }
}