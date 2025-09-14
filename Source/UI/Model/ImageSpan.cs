using SR_DMG.Source.Employ;
using System.ComponentModel;

namespace SR_DMG.Source.UI.Model
{
    public class ImageSpan : INotifyPropertyChanged
    {
        private string _Icon = string.Empty;

        private string _Text = string.Empty;

        private List<string> _Tags = [];

        public string Icon
        {
            set => Program.SetField(ref _Icon, value, nameof(Icon), OnPropertyChanged);
            get => _Icon;
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

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string? PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}