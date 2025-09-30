using SR_DMG.Source.Employ;
using System.ComponentModel;

namespace SR_DMG.Source.UI.Model
{
    public class Equation : INotifyPropertyChanged
    {
        private string _Text = string.Empty;

        private float _Value;

        public string Text
        {
            set => Program.SetField(ref _Text, value, nameof(Text), OnPropertyChanged);
            get => _Text;
        }

        public float Value
        {
            set => Program.SetField(ref _Value, value, nameof(Value), OnPropertyChanged);
            get => _Value;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string? PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}