using StarRailDamage.Source.Model.Text;
using StarRailDamage.Source.UI.Factory.NotifyPropertyChanged;
using System.Windows.Media.Imaging;

namespace StarRailDamage.Source.Model.Metadata.Character.Element
{
    public class CharacterElementModel(TextBinding fullName, TextBinding @break, BitmapImage element, BitmapImage damage, BitmapImage resist) : NotifyPropertyChangedFactory
    {
        private TextBinding _FullName = fullName;

        private TextBinding _Break = @break;

        private BitmapImage _Element = element;

        private BitmapImage _Damage = damage;

        private BitmapImage _Resist = resist;

        public TextBinding FullName
        {
            get => _FullName;
            set => SetField(ref _FullName, value);
        }

        public TextBinding Break
        {
            get => _Break;
            set => SetField(ref _Break, value);
        }

        public BitmapImage Element
        {
            get => _Element;
            set => SetField(ref _Element, value);
        }

        public BitmapImage Damage
        {
            get => _Damage;
            set => SetField(ref _Damage, value);
        }

        public BitmapImage Resist
        {
            get => _Resist;
            set => SetField(ref _Resist, value);
        }
    }
}