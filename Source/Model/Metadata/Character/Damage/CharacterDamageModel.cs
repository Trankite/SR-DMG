using StarRailDamage.Source.UI.Factory.NotifyPropertyChanged;

namespace StarRailDamage.Source.Model.Metadata.Character.Damage
{
    public class CharacterDamageModel : NotifyPropertyChangedFactory
    {
        private double _BaseDamage;

        private double _CriticalStrikeDamage;

        private double _ExpectedDamage;

        private double _BrokenDamage;

        private double _SuperBrokenDamage;

        private double _DelayedDamage;

        public double BaseDamage
        {
            get => _BaseDamage;
            set => SetField(ref _BaseDamage, value);
        }

        public double CriticalStrikeDamage
        {
            get => _CriticalStrikeDamage;
            set => SetField(ref _CriticalStrikeDamage, value);
        }

        public double ExpectedDamage
        {
            get => _ExpectedDamage;
            set => SetField(ref _ExpectedDamage, value);
        }

        public double BrokenDamage
        {
            get => _BrokenDamage;
            set => SetField(ref _BrokenDamage, value);
        }

        public double SuperBrokenDamage
        {
            get => _SuperBrokenDamage;
            set => SetField(ref _SuperBrokenDamage, value);
        }

        public double DelayedDamage
        {
            get => _DelayedDamage;
            set => SetField(ref _DelayedDamage, value);
        }
    }
}