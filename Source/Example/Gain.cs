using SR_DMG.Source.Material;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace SR_DMG.Source.Example
{
    public class Gain : INotifyPropertyChanged
    {
        private bool _Flag;
        private int _Level;
        private int _Layer;
        private int _Enemy;
        public BitmapImage Icon { set; get; } = new();
        public string Title { set; get; } = string.Empty;
        public List<string> Tags { set; get; } = [];
        public List<string> Values { set; get; } = [];
        public string Text { set; get; } = string.Empty;
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
        public int Layer
        {
            set => Program.SetField(ref _Layer, value, nameof(Layer), OnPropertyChanged);
            get => _Layer;
        }
        public int Enemy
        {
            set => Program.SetField(ref _Enemy, value, nameof(Enemy), OnPropertyChanged);
            get => _Enemy;
        }
        public int MaxLevel { set; get; }
        public int MaxLayer { set; get; }
        public int MaxEnemy { set; get; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string? PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}