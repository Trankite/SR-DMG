using StarRailDamage.Source.UI.Factory.NotifyPropertyChanged;
using StarRailDamage.Source.UI.Model.Control;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace StarRailDamage.Source.UI.Model.View
{
    public class GainItemModel : NotifyPropertyChangedFactory
    {
        private ImageSource? _Icon;

        private string _Title = string.Empty;

        private string _Text = string.Empty;

        private bool _Flag;

        private ObservableCollection<string> _MarkItems = [];

        private ObservableCollection<string> _TempItems = [];

        private ObservableCollection<ScopedSliderModel> _SliderItems = [];

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

        public ObservableCollection<ScopedSliderModel> SliderItems
        {
            get => _SliderItems;
            set => SetField(ref _SliderItems, value);
        }
    }
}