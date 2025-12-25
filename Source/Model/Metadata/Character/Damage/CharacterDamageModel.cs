using StarRailDamage.Source.UI.Factory.NotifyPropertyChanged;

namespace StarRailDamage.Source.Model.Metadata.Character.Damage
{
    public class CharacterDamageModel : NotifyPropertyChangedFactory
    {
        private double _NormalDamage;

        private double _CriticalHitDamage;

        private double _ExpectedDamage;

        private double _BreakDamage;

        private double _SuperBreakDamage;

        private double _DelayedDamage;

        public double NormalDamage
        {
            get => _NormalDamage;
            set => SetField(ref _NormalDamage, value);
        }

        public double CriticalHitDamage
        {
            get => _CriticalHitDamage;
            set => SetField(ref _CriticalHitDamage, value);
        }

        public double ExpectedDamage
        {
            get => _ExpectedDamage;
            set => SetField(ref _ExpectedDamage, value);
        }

        public double BreakDamage
        {
            get => _BreakDamage;
            set => SetField(ref _BreakDamage, value);
        }

        public double SuperBreakDamage
        {
            get => _SuperBreakDamage;
            set => SetField(ref _SuperBreakDamage, value);
        }

        public double DelayedDamage
        {
            get => _DelayedDamage;
            set => SetField(ref _DelayedDamage, value);
        }
    }
}