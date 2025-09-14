using SR_DMG.Source.Employ;
using System.ComponentModel;

namespace SR_DMG.Source.UI.Model
{
    public class GainItem : INotifyPropertyChanged
    {
        private string _Icon = string.Empty;

        private string _Title = string.Empty;

        private string _Text = string.Empty;

        private List<string> _Tags = [];

        private List<string> _Values = [];

        private bool _Flag;

        private int _Level;

        private int _MaxLevel;

        private int _Layer;

        private int _MaxLayer;

        public string Icon
        {
            set => Program.SetField(ref _Icon, value, nameof(Icon), OnPropertyChanged);
            get => _Icon;
        }

        public string Title
        {
            set => Program.SetField(ref _Title, value, nameof(Title), OnPropertyChanged);
            get => _Title;
        }

        public string Text
        {
            set => Program.SetField(ref _Text, value, nameof(Text), OnPropertyChanged);
            get => _Text;
        }

        public List<string> Tags
        {
            set => Program.SetField(ref _Tags, value, nameof(Tags), OnPropertyChanged);
            get => _Tags;
        }

        public List<string> Values
        {
            set => Program.SetField(ref _Values, value, nameof(Values), OnPropertyChanged);
            get => _Values;
        }

        public bool Flag
        {
            set => Program.SetField(ref _Flag, value, nameof(Flag), OnPropertyChanged);
            get => _Flag;
        }

        public int Level
        {
            set => Program.SetField(ref _Level, value, nameof(Level), OnPropertyChanged);
            get => _Level;
        }

        public int MaxLevel
        {
            set => Program.SetField(ref _MaxLevel, value, nameof(MaxLevel), OnPropertyChanged);
            get => _MaxLevel;
        }

        public int Layer
        {
            set => Program.SetField(ref _Layer, value, nameof(Layer), OnPropertyChanged);
            get => _Layer;
        }

        public int MaxLayer
        {
            set => Program.SetField(ref _MaxLayer, value, nameof(MaxLayer), OnPropertyChanged);
            get => _MaxLayer;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string? PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}