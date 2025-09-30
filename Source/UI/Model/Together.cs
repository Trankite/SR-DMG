using SR_DMG.Source.Employ;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;

namespace SR_DMG.Source.UI.Model
{
    public partial class Together : INotifyPropertyChanged
    {
        private ImageSpan _Role = new();

        private ImageSpan _Equip = new();

        private ImageSpan _Enemy = new();

        private Roleus _Roleus = new();

        private Damage _Damage = new();

        private Damage _Compare = new();

        private Equation _Equation = new();

        private bool _NoGain;

        private bool _NoModify;

        private bool _Comparing;

        private bool _UnSaved;

        private string _Icon = string.Empty;

        private GainView _Skills = new();

        private GainView _Gains = new();

        public ImageSpan Role
        {
            set => Program.SetField(ref _Role, value, nameof(Role), OnPropertyChanged);
            get => _Role;
        }

        public ImageSpan Equip
        {
            set => Program.SetField(ref _Equip, value, nameof(Equip), OnPropertyChanged);
            get => _Equip;
        }

        public ImageSpan Enemy
        {
            set => Program.SetField(ref _Enemy, value, nameof(Enemy), OnPropertyChanged);
            get => _Enemy;
        }

        public Roleus Roleus
        {
            set => Program.SetField(ref _Roleus, value, nameof(Roleus), OnPropertyChanged);
            get => _Roleus;
        }

        public Damage Damage
        {
            set => Program.SetField(ref _Damage, value, nameof(Damage), OnPropertyChanged);
            get => _Damage;
        }

        public Damage Compare
        {
            set => Program.SetField(ref _Compare, value, nameof(Compare), OnPropertyChanged);
            get => _Compare;
        }

        public Equation Equation
        {
            set => Program.SetField(ref _Equation, value, nameof(Equation), OnPropertyChanged);
            get => _Equation;
        }

        public bool NoGain
        {
            set => Program.SetField(ref _NoGain, value, nameof(NoGain), OnPropertyChanged);
            get => _NoGain;
        }

        public bool NoModify
        {
            set => Program.SetField(ref _NoModify, value, nameof(NoModify), OnPropertyChanged);
            get => _NoModify;
        }

        public bool Comparing
        {
            set => Program.SetField(ref _Comparing, value, nameof(Comparing), OnPropertyChanged);
            get => _Comparing;
        }

        public bool UnSaved
        {
            set => Program.SetField(ref _UnSaved, value, nameof(UnSaved), OnPropertyChanged);
            get => _UnSaved;
        }

        public string Icon
        {
            set => Program.SetField(ref _Icon, value, nameof(Icon), OnPropertyChanged);
            get => _Icon;
        }

        public GainView Skills
        {
            set => Program.SetField(ref _Skills, value, nameof(Skills), OnPropertyChanged);
            get => _Skills;
        }

        public GainView Gains
        {
            set => Program.SetField(ref _Gains, value, nameof(Gains), OnPropertyChanged);
            get => _Gains;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string? PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        [GeneratedRegex(@"\[(.*?)]|([\d.]+%)")]
        private static partial Regex EquationRegex();
        public void UpdataEquation(Equation Equation)
        {
            string[] Result = EquationRegex().Split(Equation.Text);
            for (int i = 1; i < Result.Length; i += 2)
            {
                if (Result[i].EndsWith('%'))
                {
                    Result[i] = Program.GetFloat(Result[i]).ToString();
                }
                else
                {
                    Result[i] = TryGet(Result[i], out float Value) ? Value.ToString() : string.Empty;
                }
            }
            try
            {
                string Expression = string.Join(string.Empty, Result);
                Equation.Value = Convert.ToSingle(new DataTable().Compute(Expression, null));
            }
            catch
            {
                Equation.Value = float.NaN;
            }
        }

        public bool TryGet(string PropertyName, out float Value)
        {
            Value = Roleus[PropertyName];
            if (!float.IsNaN(Value)) return true;
            Value = Damage[PropertyName];
            if (!float.IsNaN(Value)) return true;
            return false;
        }

        public bool TrySet(string PropertyName, float Value)
        {
            return Roleus.TrySet(PropertyName, Value) || Damage.TrySet(PropertyName, Value);
        }
    }
}