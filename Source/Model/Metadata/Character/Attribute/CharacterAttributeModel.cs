using StarRailDamage.Source.UI.Factory.NotifyPropertyChanged;

namespace StarRailDamage.Source.Model.Metadata.Character.Attribute
{
    public class CharacterAttributeModel : NotifyPropertyChangedFactory
    {
        private double _CharacterLevel;

        private double _EnemyLevel;

        private double _EnemyAmount;

        private double _ElementResistance;

        private double _DamageDecrease;

        private double _DamageIncrease;

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

        private double _DamageBoost;

        private double _DefenseDecrease;

        private double _ResistanceDecrease;

        private double _SuperBreakEqual;

        private double _BreakEffect;

        private double _BreakDamageBoost;

        private double _BreakEfficiency;

        private double _ToughnessReduced;

        private double _EffectHitRate;

        private double _EffectResistance;

        private double _OutgoingHealingBoost;

        private double _HealingAmount;

        private double _EnergyRegenerationRate;

        private double _MaxEnergy;

        public double CharacterLevel
        {
            get => _CharacterLevel;
            set => SetField(ref _CharacterLevel, value);
        }

        public double EnemyLevel
        {
            get => _EnemyLevel;
            set => SetField(ref _EnemyLevel, value);
        }

        public double EnemyAmount
        {
            get => _EnemyAmount;
            set => SetField(ref _EnemyAmount, value);
        }

        public double ElementResistance
        {
            get => _ElementResistance;
            set => SetField(ref _ElementResistance, value);
        }

        public double DamageDecrease
        {
            get => _DamageDecrease;
            set => SetField(ref _DamageDecrease, value);
        }

        public double DamageIncrease
        {
            get => _DamageIncrease;
            set => SetField(ref _DamageIncrease, value);
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

        public double DamageBoost
        {
            get => _DamageBoost;
            set => SetField(ref _DamageBoost, value);
        }

        public double DefenseDecrease
        {
            get => _DefenseDecrease;
            set => SetField(ref _DefenseDecrease, value);
        }

        public double ResistanceDecrease
        {
            get => _ResistanceDecrease;
            set => SetField(ref _ResistanceDecrease, value);
        }

        public double SuperBreakEqual
        {
            get => _SuperBreakEqual;
            set => SetField(ref _SuperBreakEqual, value);
        }

        public double BreakEffect
        {
            get => _BreakEffect;
            set => SetField(ref _BreakEffect, value);
        }

        public double BreakDamageBoost
        {
            get => _BreakDamageBoost;
            set => SetField(ref _BreakDamageBoost, value);
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

        public double OutgoingHealingBoost
        {
            get => _OutgoingHealingBoost;
            set => SetField(ref _OutgoingHealingBoost, value);
        }

        public double HealingAmount
        {
            get => _HealingAmount;
            set => SetField(ref _HealingAmount, value);
        }

        public double EnergyRegenerationRate
        {
            get => _EnergyRegenerationRate;
            set => SetField(ref _EnergyRegenerationRate, value);
        }

        public double MaxEnergy
        {
            get => _MaxEnergy;
            set => SetField(ref _MaxEnergy, value);
        }
    }
}