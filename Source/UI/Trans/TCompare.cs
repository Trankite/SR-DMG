using SR_DMG.Source.Employ;
using System.Globalization;
using System.Windows.Data;

namespace SR_DMG.Source.UI.Trans
{
    public class TCompare : IMultiValueConverter
    {
        public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return (((float)values[0] / (float)values[1]) - 1).ToString(Simple.Format_PlusMinus);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}