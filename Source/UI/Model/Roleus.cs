using SR_DMG.Source.Employ;
using SR_DMG.Source.Example;
using System.ComponentModel;

namespace SR_DMG.Source.UI.Model
{
    public class Roleus : INotifyPropertyChanged
    {
        private float _Role_Level;

        private float _Role_Type;

        private float _Break_Element;

        private float _Enemy_Level;

        private float _Enemy_Number;

        private float _Toughness;

        private float _RES_Boost;

        private float _DMG_Reduction;

        private float _DMG_Taken;

        private float _ATK;

        private float _HP;

        private float _DEF;

        private float _SPD;

        private float _ATK_Base;

        private float _HP_Base;

        private float _DEF_Base;

        private float _SPD_Base;

        private float _CRIT_Rate;

        private float _CRIT_DMG;

        private float _DMG_Boost;

        private float _DEF_Reduced;

        private float _RES_Reduced;

        private float _Break_Equal;

        private float _Break_Effect;

        private float _Break_Efficiency;

        private float _Break_Boost;

        private float _Toughness_Reduced;

        private float _Effect_Rate;

        private float _Effect_Rest;

        private float _Heal_Rate;

        private float _Accumulate;

        private float _Energy_Rate;

        private float _Energy_Toplimit;

        public float Role_Level
        {
            set => Program.SetField(ref _Role_Level, value, nameof(Role_Level), OnPropertyChanged);
            get => _Role_Level;
        }

        public float Role_Type
        {
            set => Program.SetField(ref _Role_Type, value, nameof(Role_Type), OnPropertyChanged);
            get => _Role_Type;
        }

        public float Break_Element
        {
            set => Program.SetField(ref _Break_Element, value, nameof(Break_Element), OnPropertyChanged);
            get => _Break_Element;
        }

        public float Enemy_Level
        {
            set => Program.SetField(ref _Enemy_Level, value, nameof(Enemy_Level), OnPropertyChanged);
            get => _Enemy_Level;
        }

        public float Enemy_Number
        {
            set => Program.SetField(ref _Enemy_Number, value, nameof(Enemy_Number), OnPropertyChanged);
            get => _Enemy_Number;
        }

        public float Toughness
        {
            set => Program.SetField(ref _Toughness, value, nameof(Toughness), OnPropertyChanged);
            get => _Toughness;
        }

        public float RES_Boost
        {
            set => Program.SetField(ref _RES_Boost, value, nameof(RES_Boost), OnPropertyChanged);
            get => _RES_Boost;
        }

        public float DMG_Reduction
        {
            set => Program.SetField(ref _DMG_Reduction, value, nameof(DMG_Reduction), OnPropertyChanged);
            get => _DMG_Reduction;
        }

        public float DMG_Taken
        {
            set => Program.SetField(ref _DMG_Taken, value, nameof(DMG_Taken), OnPropertyChanged);
            get => _DMG_Taken;
        }

        public float ATK
        {
            set => Program.SetField(ref _ATK, value, nameof(ATK), OnPropertyChanged);
            get => _ATK;
        }

        public float HP
        {
            set => Program.SetField(ref _HP, value, nameof(HP), OnPropertyChanged);
            get => _HP;
        }

        public float DEF
        {
            set => Program.SetField(ref _DEF, value, nameof(DEF), OnPropertyChanged);
            get => _DEF;
        }

        public float SPD
        {
            set => Program.SetField(ref _SPD, value, nameof(SPD), OnPropertyChanged);
            get => _SPD;
        }

        public float ATK_Base
        {
            set => Program.SetField(ref _ATK_Base, value, nameof(ATK_Base), OnPropertyChanged);
            get => _ATK_Base;
        }

        public float HP_Base
        {
            set => Program.SetField(ref _HP_Base, value, nameof(HP_Base), OnPropertyChanged);
            get => _HP_Base;
        }

        public float DEF_Base
        {
            set => Program.SetField(ref _DEF_Base, value, nameof(DEF_Base), OnPropertyChanged);
            get => _DEF_Base;
        }

        public float SPD_Base
        {
            set => Program.SetField(ref _SPD_Base, value, nameof(SPD_Base), OnPropertyChanged);
            get => _SPD_Base;
        }

        public float CRIT_Rate
        {
            set => Program.SetField(ref _CRIT_Rate, value, nameof(CRIT_Rate), OnPropertyChanged);
            get => _CRIT_Rate;
        }

        public float CRIT_DMG
        {
            set => Program.SetField(ref _CRIT_DMG, value, nameof(CRIT_DMG), OnPropertyChanged);
            get => _CRIT_DMG;
        }

        public float DMG_Boost
        {
            set => Program.SetField(ref _DMG_Boost, value, nameof(DMG_Boost), OnPropertyChanged);
            get => _DMG_Boost;
        }

        public float DEF_Reduced
        {
            set => Program.SetField(ref _DEF_Reduced, value, nameof(DEF_Reduced), OnPropertyChanged);
            get => _DEF_Reduced;
        }

        public float RES_Reduced
        {
            set => Program.SetField(ref _RES_Reduced, value, nameof(RES_Reduced), OnPropertyChanged);
            get => _RES_Reduced;
        }

        public float Break_Equal
        {
            set => Program.SetField(ref _Break_Equal, value, nameof(Break_Equal), OnPropertyChanged);
            get => _Break_Equal;
        }

        public float Break_Effect
        {
            set => Program.SetField(ref _Break_Effect, value, nameof(Break_Effect), OnPropertyChanged);
            get => _Break_Effect;
        }

        public float Break_Efficiency
        {
            set => Program.SetField(ref _Break_Efficiency, value, nameof(Break_Efficiency), OnPropertyChanged);
            get => _Break_Efficiency;
        }

        public float Break_Boost
        {
            set => Program.SetField(ref _Break_Boost, value, nameof(Break_Boost), OnPropertyChanged);
            get => _Break_Boost;
        }

        public float Toughness_Reduced
        {
            set => Program.SetField(ref _Toughness_Reduced, value, nameof(Toughness_Reduced), OnPropertyChanged);
            get => _Toughness_Reduced;
        }

        public float Effect_Rate
        {
            set => Program.SetField(ref _Effect_Rate, value, nameof(Effect_Rate), OnPropertyChanged);
            get => _Effect_Rate;
        }

        public float Effect_Rest
        {
            set => Program.SetField(ref _Effect_Rest, value, nameof(Effect_Rest), OnPropertyChanged);
            get => _Effect_Rest;
        }

        public float Heal_Rate
        {
            set => Program.SetField(ref _Heal_Rate, value, nameof(Heal_Rate), OnPropertyChanged);
            get => _Heal_Rate;
        }

        public float Accumulate
        {
            set => Program.SetField(ref _Accumulate, value, nameof(Accumulate), OnPropertyChanged);
            get => _Accumulate;
        }

        public float Energy_Rate
        {
            set => Program.SetField(ref _Energy_Rate, value, nameof(Energy_Rate), OnPropertyChanged);
            get => _Energy_Rate;
        }

        public float Energy_Toplimit
        {
            set => Program.SetField(ref _Energy_Toplimit, value, nameof(Energy_Toplimit), OnPropertyChanged);
            get => _Energy_Toplimit;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string? PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private static readonly Dictionary<string, Property<Roleus, float>> Properties = [];

        public float this[string PropertyName]
        {
            get => Properties.GetValueOrDefault(PropertyName)?.GetValue(this) ?? float.NaN;
        }

        public bool TrySet(string PropertyName, float Value)
        {
            if (Properties.TryGetValue(PropertyName, out Property<Roleus, float>? Property))
            {
                Property.SetValue(this, Value);
            }
            return false;
        }

        public void UpdataDamage(Damage Damage)
        {
            float Factor = (_Role_Level + 20) / (_Role_Level + 20 + (_Enemy_Level + 20) * (1 - Math.Min(_DEF_Reduced, 100) * 0.01f));
            Factor *= (1 - Math.Min(Math.Max(_RES_Boost, -100), 90) * 0.01f) * (1 + _DMG_Taken * 0.01f) * (1 - _DMG_Reduction * 0.01f);
            Damage.Delay = Factor * Damage.Base * (1 + _DMG_Boost * 0.01f);
            Damage.Crit = Damage.Delay * (1 + _CRIT_DMG * 0.01f);
            Damage.Expect = Damage.Delay * (1 + _CRIT_Rate * 0.01f * _CRIT_DMG * 0.01f);
            Factor *= (1 + _Break_Effect * 0.01f) * (1 + _Break_Boost * 0.01f) * 3767.55f;
            Damage.Break = Factor * (_Toughness / 20 - 0.5f) * (_Role_Type switch { 0 or 1 => 2, 2 or 3 => 1, 4 => 1.5f, _ => 0.5f });
            Damage.Super = Factor * (_Toughness_Reduced * (1 + _Break_Efficiency * 0.01f) * 0.1f);
        }

        static Roleus()
        {
            Property<Roleus, float>.Initialize(Properties, new()
            {
                [nameof(Role_Level)] = "角色等级",
                [nameof(Role_Type)] = "角色类型",
                [nameof(Break_Element)] = "击破属性",
                [nameof(Enemy_Level)] = "怪物等级",
                [nameof(Enemy_Number)] = "怪物数量",
                [nameof(Toughness)] = "韧性",
                [nameof(RES_Boost)] = "抗性",
                [nameof(DMG_Reduction)] = "免伤",
                [nameof(DMG_Taken)] = "易伤",
                [nameof(ATK)] = "攻击力",
                [nameof(HP)] = "生命值",
                [nameof(DEF)] = "防御力",
                [nameof(SPD)] = "速度",
                [nameof(ATK_Base)] = "基础攻击",
                [nameof(HP_Base)] = "基础生命",
                [nameof(DEF_Base)] = "基础防御",
                [nameof(SPD_Base)] = "基础速度",
                [nameof(CRIT_Rate)] = "暴击率",
                [nameof(CRIT_DMG)] = "暴击伤害",
                [nameof(DMG_Boost)] = "伤害提高",
                [nameof(DEF_Reduced)] = "无视防御",
                [nameof(RES_Reduced)] = "抗性穿透",
                [nameof(Break_Equal)] = "超击破",
                [nameof(Break_Effect)] = "击破特攻",
                [nameof(Break_Efficiency)] = "击破效率",
                [nameof(Break_Boost)] = "击破增伤",
                [nameof(Toughness_Reduced)] = "削韧",
                [nameof(Effect_Rate)] = "效果命中",
                [nameof(Effect_Rest)] = "效果抵抗",
                [nameof(Heal_Rate)] = "治疗提高",
                [nameof(Accumulate)] = "特殊数值",
                [nameof(Energy_Rate)] = "充能效率",
                [nameof(Energy_Toplimit)] = "能量上限",
            });
        }
    }
}