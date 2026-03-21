using StarRailDamage.Source.Core.LocalText.Fixed;
using StarRailDamage.Source.Model.Text;
using System.Windows.Media.Imaging;

namespace StarRailDamage.Source.Model.Metadata.Character.Element
{
    public class CharacterElementModel
    {
        public TextBinding Name { get; }

        public TextBinding Break { get; }

        public BitmapImage Element { get; }

        public BitmapImage Offense { get; }

        public BitmapImage Defense { get; }

        private CharacterElementModel(TextBinding name, TextBinding @break, BitmapImage element, BitmapImage offense, BitmapImage defense)
        {
            Name = name;
            Break = @break;
            Element = element;
            Offense = offense;
            Defense = defense;
        }

        public static CharacterElementModel Create(string element)
        {
            return new CharacterElementModel(GetNameBinding(element), GetBreakBinding(element), GetElementImage(element), GetOffenseImage(element), GetDefenseImage(element));
        }

        private static TextBinding GetNameBinding(string element)
        {
            return FixedTextManage.Binding(element + "Element");
        }

        private static TextBinding GetBreakBinding(string element)
        {
            return FixedTextManage.Binding(element + "DelayedDamage");
        }

        private static BitmapImage GetElementImage(string element)
        {
            return new BitmapImage(new Uri($"/Source/UI/Assets/Icon/Element/{element}.png", UriKind.Relative));
        }

        private static BitmapImage GetOffenseImage(string element)
        {
            return new BitmapImage(new Uri($"/Source/UI/Assets/Icon/Element/Offense/{element}.png", UriKind.Relative));
        }

        private static BitmapImage GetDefenseImage(string element)
        {
            return new BitmapImage(new Uri($"/Source/UI/Assets/Icon/Element/Defense/{element}.png", UriKind.Relative));
        }
    }
}