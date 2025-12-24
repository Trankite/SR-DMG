using StarRailDamage.Source.Model.Metadata.Character.Attribute;
using System.Globalization;
using System.Windows.Data;

namespace StarRailDamage.Source.UI.Model.Converter
{
    public class CharacterAttributeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (App.IsInDesignMode)
            {
                return Binding.DoNothing;
            }
            return CharacterAttributeExtension.GetModel((string)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}