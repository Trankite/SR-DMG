using SR_DMG.Source.Material;
using System.ComponentModel;

namespace SR_DMG.Source.Example
{
    public class Damage : INotifyPropertyChanged
    {
        private int _Base;
        private int _Crit;
        private int _Expect;
        private int _Super;
        private int _Break;
        private int _Delay;
        public int Base
        {
            set => Program.SetField(ref _Base, value, nameof(Base), OnPropertyChanged);
            get => _Base;
        }
        public int Crit
        {
            set => Program.SetField(ref _Crit, value, nameof(Crit), OnPropertyChanged);
            get => _Crit;
        }
        public int Expect
        {
            set => Program.SetField(ref _Expect, value, nameof(Expect), OnPropertyChanged);
            get => _Expect;
        }
        public int Super
        {
            set => Program.SetField(ref _Super, value, nameof(Super), OnPropertyChanged);
            get => _Super;
        }
        public int Break
        {
            set => Program.SetField(ref _Break, value, nameof(Break), OnPropertyChanged);
            get => _Break;
        }
        public int Delay
        {
            set => Program.SetField(ref _Delay, value, nameof(Delay), OnPropertyChanged);
            get => _Delay;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string? PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}