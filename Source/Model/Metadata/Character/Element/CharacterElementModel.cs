using StarRailDamage.Source.Model.Text;
using StarRailDamage.Source.UI.Factory.NotifyPropertyChanged;
using System.Windows.Media.Imaging;

namespace StarRailDamage.Source.Model.Metadata.Character.Element
{
    public class CharacterElementModel(TextBinding fullName, TextBinding breakName, BitmapImage elementIcon, BitmapImage damageIcon, BitmapImage resistIcon) : NotifyPropertyChangedFactory
    {
        private TextBinding _FullName = fullName;

        private TextBinding _BreakName = breakName;

        private BitmapImage _ElementIcon = elementIcon;

        private BitmapImage _DamageIcon = damageIcon;

        private BitmapImage _ResistIcon = resistIcon;

        public TextBinding FullName
        {
            get => _FullName;
            set => SetField(ref _FullName, value);
        }

        public TextBinding BreakName
        {
            get => _BreakName;
            set => SetField(ref _BreakName, value);
        }

        public BitmapImage ElementIcon
        {
            get => _ElementIcon;
            set => SetField(ref _ElementIcon, value);
        }

        public BitmapImage DamageIcon
        {
            get => _DamageIcon;
            set => SetField(ref _DamageIcon, value);
        }

        public BitmapImage ResistIcon
        {
            get => _ResistIcon;
            set => SetField(ref _ResistIcon, value);
        }
    }
}