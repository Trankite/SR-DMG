using StarRailDamage.Source.Core.LocalText.Fixed;
using StarRailDamage.Source.Model.Text;
using System.Windows.Media.Imaging;

namespace StarRailDamage.Source.Model.Metadata.Character.Occupation
{
    public class CharacterOccupationModel
    {
        public TextBinding Name { get; }

        public BitmapImage Icon { get; }

        private CharacterOccupationModel(TextBinding name, BitmapImage icon)
        {
            Name = name;
            Icon = icon;
        }

        public static CharacterOccupationModel Create(string occupation)
        {
            return new CharacterOccupationModel(GetNameBinding(occupation), GetOccupationImage(occupation));
        }

        private static TextBinding GetNameBinding(string occupation)
        {
            return FixedTextManage.Binding(occupation + "Occupation");
        }

        private static BitmapImage GetOccupationImage(string occupation)
        {
            return new BitmapImage(new Uri($"/Source/UI/Assets/Icon/Occupation/{occupation}.png", UriKind.Relative));
        }
    }
}