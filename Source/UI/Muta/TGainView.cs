using SR_DMG.Source.UI.Model;
using System.Globalization;
using System.Windows.Data;

namespace SR_DMG.Source.UI.Muta
{
    internal class TGainView : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return ((GainView)values[0], (GainItem)values[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}