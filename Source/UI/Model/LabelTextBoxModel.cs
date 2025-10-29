using StarRailDamage.Source.UI.Factory.NotifyPropertyChanged;
using System.Windows.Media;

namespace StarRailDamage.Source.UI.Model
{
    public class LabelTextBoxModel : NotifyPropertyChangedFactory
    {
        private ImageSource? _Icon;

        private string _Title = string.Empty;

        private string _Text = string.Empty;

        private string _Unit = string.Empty;

        private int _Round;

        public ImageSource? Icon
        {
            get => _Icon;
            set => SetField(ref _Icon, value);
        }

        public string Title
        {
            get => _Title;
            set => SetField(ref _Title, value);
        }

        public string Text
        {
            get => _Text;
            set => SetField(ref _Text, value);
        }

        public string Unit
        {
            get => _Unit;
            set => SetField(ref _Unit, value);
        }

        public int Round
        {
            get => _Round;
            set => SetField(ref _Round, value);
        }
    }
}