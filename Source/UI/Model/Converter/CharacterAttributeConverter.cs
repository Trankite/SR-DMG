using StarRailDamage.Source.Model.Metadata.Character.Attribute;
using StarRailDamage.Source.UI.Model.View;
using System.Globalization;
using System.Windows.Data;

namespace StarRailDamage.Source.UI.Model.Converter
{
    public class CharacterAttributeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Program.IsDesignMode)
            {
                return Binding.DoNothing;
            }
            CharacterAttributeInfo Attribute = CharacterAttributeExtension.GetModel((string)parameter);
            return new LabelTextBoxModel(Attribute.Icon, Attribute.Simple, string.Empty, Attribute.Unit, Attribute.Digits);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}