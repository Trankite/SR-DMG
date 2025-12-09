using StarRailDamage.Source.UI.Factory.NotifyPropertyChanged;

namespace StarRailDamage.Source.Model.Metadata.Character.Attribute
{
    public class CharacterAttributeModel : NotifyPropertyChangedFactory
    {
        private double _CharacterLevel;

        private double _MonsterLevel;

        private double _MonsterCount;

        private double _ElementResistance;

        private double _DamageImmunity;

        private double _DamageMoreProne;

        private double _Toughness;

        private double _Attack;

        private double _AttackBase;

        private double _Health;

        private double _HealthBase;

        private double _Defense;

        private double _DefenseBase;

        private double _Speed;

        private double _SpeedBase;

        private double _CriticalHitRate;

        private double _CriticalDamage;

        private double _DamageIncrease;

        private double _DefenseFailure;

        private double _ResistanceFailure;

        private double _SuperBreakDamage;

        private double _BreakStrength;

        private double _BreakDamageIncrease;

        private double _BreakEfficiency;

        private double _ToughnessReduced;

        private double _EffectHitRate;

        private double _EffectResistance;

        private double _TreatmentImprovement;

        private double _SpecialNumericalValues;

        private double _ChargingEfficiency;

        private double _ChargingLimit;

        public double CharacterLevel
        {
            get => _CharacterLevel;
            set => SetField(ref _CharacterLevel, value);
        }

        public double MonsterLevel
        {
            get => _MonsterLevel;
            set => SetField(ref _MonsterLevel, value);
        }

        public double MonsterCount
        {
            get => _MonsterCount;
            set => SetField(ref _MonsterCount, value);
        }

        public double ElementResistance
        {
            get => _ElementResistance;
            set => SetField(ref _ElementResistance, value);
        }

        public double DamageImmunity
        {
            get => _DamageImmunity;
            set => SetField(ref _DamageImmunity, value);
        }

        public double DamageMoreProne
        {
            get => _DamageMoreProne;
            set => SetField(ref _DamageMoreProne, value);
        }

        public double Toughness
        {
            get => _Toughness;
            set => SetField(ref _Toughness, value);
        }

        public double Attack
        {
            get => _Attack;
            set => SetField(ref _Attack, value);
        }

        public double AttackBase
        {
            get => _AttackBase;
            set => SetField(ref _AttackBase, value);
        }

        public double Health
        {
            get => _Health;
            set => SetField(ref _Health, value);
        }

        public double HealthBase
        {
            get => _HealthBase;
            set => SetField(ref _HealthBase, value);
        }

        public double Defense
        {
            get => _Defense;
            set => SetField(ref _Defense, value);
        }

        public double DefenseBase
        {
            get => _DefenseBase;
            set => SetField(ref _DefenseBase, value);
        }

        public double Speed
        {
            get => _Speed;
            set => SetField(ref _Speed, value);
        }

        public double SpeedBase
        {
            get => _SpeedBase;
            set => SetField(ref _SpeedBase, value);
        }

        public double CriticalHitRate
        {
            get => _CriticalHitRate;
            set => SetField(ref _CriticalHitRate, value);
        }

        public double CriticalDamage
        {
            get => _CriticalDamage;
            set => SetField(ref _CriticalDamage, value);
        }

        public double DamageIncrease
        {
            get => _DamageIncrease;
            set => SetField(ref _DamageIncrease, value);
        }

        public double DefenseFailure
        {
            get => _DefenseFailure;
            set => SetField(ref _DefenseFailure, value);
        }

        public double ResistanceFailure
        {
            get => _ResistanceFailure;
            set => SetField(ref _ResistanceFailure, value);
        }

        public double SuperBreakDamage
        {
            get => _SuperBreakDamage;
            set => SetField(ref _SuperBreakDamage, value);
        }

        public double BreakStrength
        {
            get => _BreakStrength;
            set => SetField(ref _BreakStrength, value);
        }

        public double BreakDamageIncrease
        {
            get => _BreakDamageIncrease;
            set => SetField(ref _BreakDamageIncrease, value);
        }

        public double BreakEfficiency
        {
            get => _BreakEfficiency;
            set => SetField(ref _BreakEfficiency, value);
        }

        public double ToughnessReduced
        {
            get => _ToughnessReduced;
            set => SetField(ref _ToughnessReduced, value);
        }

        public double EffectHitRate
        {
            get => _EffectHitRate;
            set => SetField(ref _EffectHitRate, value);
        }

        public double EffectResistance
        {
            get => _EffectResistance;
            set => SetField(ref _EffectResistance, value);
        }

        public double TreatmentImprovement
        {
            get => _TreatmentImprovement;
            set => SetField(ref _TreatmentImprovement, value);
        }

        public double SpecialNumericalValues
        {
            get => _SpecialNumericalValues;
            set => SetField(ref _SpecialNumericalValues, value);
        }

        public double ChargingEfficiency
        {
            get => _ChargingEfficiency;
            set => SetField(ref _ChargingEfficiency, value);
        }

        public double ChargingLimit
        {
            get => _ChargingLimit;
            set => SetField(ref _ChargingLimit, value);
        }
    }
}