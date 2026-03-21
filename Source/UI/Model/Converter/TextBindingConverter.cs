using StarRailDamage.Source.Core.LocalText.Fixed;
using System.Globalization;
using System.Windows.Data;

namespace StarRailDamage.Source.UI.Model.Converter
{
    public class TextBindingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return FixedTextManage.Binding((string)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}