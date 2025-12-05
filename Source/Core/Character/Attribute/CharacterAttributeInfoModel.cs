using StarRailDamage.Source.Model.Text;
using StarRailDamage.Source.UI.Factory.NotifyPropertyChanged;
using System.Windows.Media.Imaging;

namespace StarRailDamage.Source.Core.Character.Attribute
{
    public class CharacterAttributeInfoModel : NotifyPropertyChangedFactory
    {
        private BitmapImage? _Icon;

        private TextBinding? _FullName;

        private TextBinding? _SimpleName;

        private int _Digits;

        public CharacterAttributeInfoModel() { }

        public CharacterAttributeInfoModel(BitmapImage? icon, TextBinding? fullName, TextBinding? simpleName, int digits)
        {
            _Icon = icon;
            _FullName = fullName;
            _SimpleName = simpleName;
            _Digits = digits;
        }

        public BitmapImage? Icon
        {
            get => _Icon;
            set => SetField(ref _Icon, value);
        }

        public TextBinding? FullName
        {
            get => _FullName;
            set => SetField(ref _FullName, value);
        }

        public TextBinding? SimpleName
        {
            get => _SimpleName;
            set => SetField(ref _SimpleName, value);
        }

        public int Digits
        {
            get => _Digits;
            set => SetField(ref _Digits, value);
        }
    }
}