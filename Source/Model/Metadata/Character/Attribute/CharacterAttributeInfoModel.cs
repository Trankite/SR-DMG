using StarRailDamage.Source.Model.Text;
using StarRailDamage.Source.UI.Model.View;
using System.Windows.Media.Imaging;

namespace StarRailDamage.Source.Model.Metadata.Character.Attribute
{
    public class CharacterAttributeInfoModel : LabelTextBoxModel
    {
        public TextBinding _FullName = TextBinding.Default;

        public CharacterAttributeInfoModel(BitmapImage bitmapImage, TextBinding fullNameTextBinding, TextBinding simpleTextBinding, int digits, TextBinding unitTextBinding)
        {
            Icon = bitmapImage;
            FullName = fullNameTextBinding;
            Title = simpleTextBinding;
            Digits = digits;
            Unit = unitTextBinding;
        }

        public TextBinding FullName
        {
            get => _FullName;
            set => SetField(ref _FullName, value);
        }
    }
}