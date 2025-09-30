using SR_DMG.Source.Employ;
using SR_DMG.Source.Example;
using System.ComponentModel;

namespace SR_DMG.Source.UI.Model
{
    public class Damage : INotifyPropertyChanged
    {
        private float _Base;

        private float _Crit;

        private float _Expect;

        private float _Break;

        private float _Super;

        private float _Delay;

        public float Base
        {
            set => Program.SetField(ref _Base, value, nameof(Base), OnPropertyChanged);
            get => _Base;
        }

        public float Crit
        {
            set => Program.SetField(ref _Crit, value, nameof(Crit), OnPropertyChanged);
            get => _Crit;
        }

        public float Expect
        {
            set => Program.SetField(ref _Expect, value, nameof(Expect), OnPropertyChanged);
            get => _Expect;
        }

        public float Break
        {
            set => Program.SetField(ref _Break, value, nameof(Break), OnPropertyChanged);
            get => _Break;
        }

        public float Super
        {
            set => Program.SetField(ref _Super, value, nameof(Super), OnPropertyChanged);
            get => _Super;
        }

        public float Delay
        {
            set => Program.SetField(ref _Delay, value, nameof(Delay), OnPropertyChanged);
            get => _Delay;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string? PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private static readonly Dictionary<string, Property<Damage, float>> Properties = [];

        public float this[string PropertyName]
        {
            get => Properties.GetValueOrDefault(PropertyName)?.GetValue(this) ?? float.NaN;
        }

        public bool TrySet(string PropertyName, float Value)
        {
            if (Properties.TryGetValue(PropertyName, out Property<Damage, float>? Property))
            {
                Property.SetValue(this, Value);
            }
            return false;
        }

        static Damage()
        {
            Property<Damage, float>.Initialize(Properties, new()
            {
                [nameof(Base)] = "基础伤害",
                [nameof(Crit)] = "全暴击伤害",
                [nameof(Expect)] = "期望伤害",
                [nameof(Break)] = "击破伤害",
                [nameof(Super)] = "超击破伤害",
                [nameof(Delay)] = "持续伤害"
            });
        }
    }
}