using StarRailDamage.Source.Core.Language;
using StarRailDamage.Source.Model.Text;
using System.Windows.Media.Imaging;

namespace StarRailDamage.Source.Model.Metadata.Character.Attribute
{
    public class CharacterAttributeInfo
    {
        public TextBinding Name { get; }

        public TextBinding Simple { get; }

        public BitmapImage Icon { get; }

        public TextBinding Unit { get; }

        public int Digits { get; }

        private CharacterAttributeInfo(TextBinding name, TextBinding simple, BitmapImage icon, TextBinding unit, int digits)
        {
            Name = name;
            Simple = simple;
            Icon = icon;
            Unit = unit;
            Digits = digits;
        }

        public static CharacterAttributeInfo Create(string attribute, BitmapImage icon, TextBinding unit, int digits)
        {
            return new CharacterAttributeInfo(GetNameBinding(attribute), GetSimpleBinding(attribute), icon, unit, digits);
        }

        private static TextBinding GetNameBinding(string attribute)
        {
            return FixedTextExtension.Binding(attribute);
        }

        private static TextBinding GetSimpleBinding(string attribute)
        {
            return FixedTextExtension.Binding(attribute + "Simple");
        }
    }
}