using StarRailDamage.Source.UI.Factory.NotifyPropertyChanged;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace StarRailDamage.Source.UI.Model
{
    public class GainItemModel : NotifyPropertyChangedFactory
    {
        private ImageSource? _Icon;

        private string _Title = string.Empty;

        private string _Text = string.Empty;

        private bool _Flag;

        private ObservableCollection<string> _MarkItems = [];

        private ObservableCollection<string> _TempItems = [];

        private double _Level;

        private int _MaxLevel;

        private double _Layer;

        private int _MaxLayer;

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

        public bool Flag
        {
            get => _Flag;
            set => SetField(ref _Flag, value);
        }

        public ObservableCollection<string> MarkItems
        {
            get => _MarkItems;
            set => SetField(ref _MarkItems, value);
        }

        public ObservableCollection<string> TempItems
        {
            get => _TempItems;
            set => SetField(ref _TempItems, value);
        }

        public double Level
        {
            get => _Level;
            set => SetField(ref _Level, value);
        }

        public int MaxLevel
        {
            get => _MaxLevel;
            set => SetField(ref _MaxLevel, value);
        }

        public double Layer
        {
            get => _Layer;
            set => SetField(ref _Layer, value);
        }

        public int MaxLayer
        {
            get => _MaxLayer;
            set => SetField(ref _MaxLayer, value);
        }
    }
}