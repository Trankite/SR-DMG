using System.Windows.Media.Imaging;

namespace StarRailDamage.Source.Model.Metadata.Character.Attribute
{
    public static class AttributeImage
    {
        public static readonly BitmapImage Attack = GetImage("Attack");

        public static readonly BitmapImage Health = GetImage("Health");

        public static readonly BitmapImage Defense = GetImage("Defense");

        public static readonly BitmapImage Speed = GetImage("Speed");

        public static readonly BitmapImage Augment = GetImage("Augment");

        public static readonly BitmapImage Offense = GetImage("Offense");

        public static readonly BitmapImage Shielding = GetImage("Shielding");

        public static readonly BitmapImage Stroke = GetImage("Stroke");

        public static readonly BitmapImage Charging = GetImage("Charging");

        public static readonly BitmapImage Battery = GetImage("Battery");

        public static readonly BitmapImage Break = GetImage("Break");

        public static readonly BitmapImage Treatment = GetImage("Treatment");

        public static readonly BitmapImage Unknown = GetImage("Unknown");

        private static BitmapImage GetImage(string attribute)
        {
            return new BitmapImage(new Uri($"/Source/UI/Assets/Icon/Attribute/{attribute}.png", UriKind.Relative));
        }
    }
}