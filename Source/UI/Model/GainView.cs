using SR_DMG.Source.Employ;
using System.ComponentModel;

namespace SR_DMG.Source.UI.Model
{
    public class GainView : INotifyPropertyChanged
    {
        private bool _Dropdown;

        private GainItem _Select = Simple.UnInit_GainItem;

        private List<GainItem> _Items = [];

        public GainItem Select
        {
            set => Program.SetField(ref _Select, value, nameof(Select), OnPropertyChanged);
            get => _Select;
        }

        public List<GainItem> Items
        {
            set => Program.SetField(ref _Items, value, nameof(Items), OnPropertyChanged);
            get => _Items;
        }

        public bool Dropdown
        {
            set => Program.SetField(ref _Dropdown, value, nameof(Dropdown), OnPropertyChanged);
            get => _Dropdown;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string? PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}