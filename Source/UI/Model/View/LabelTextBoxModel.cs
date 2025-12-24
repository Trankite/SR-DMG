using StarRailDamage.Source.Model.Text;
using StarRailDamage.Source.UI.Factory.NotifyPropertyChanged;
using System.Windows.Media.Imaging;

namespace StarRailDamage.Source.UI.Model.View
{
    public class LabelTextBoxModel : NotifyPropertyChangedFactory
    {
        private BitmapImage? _Icon;

        private TextBinding _Title = TextBinding.Default;

        private string _Text = string.Empty;

        private TextBinding _Unit = TextBinding.Default;

        private int _Digits;

        public BitmapImage? Icon
        {
            get => _Icon;
            set => SetField(ref _Icon, value);
        }

        public TextBinding Title
        {
            get => _Title;
            set => SetField(ref _Title, value);
        }

        public string Text
        {
            get => _Text;
            set => SetField(ref _Text, value);
        }

        public TextBinding Unit
        {
            get => _Unit;
            set => SetField(ref _Unit, value);
        }

        public int Digits
        {
            get => _Digits;
            set => SetField(ref _Digits, value);
        }
    }
}