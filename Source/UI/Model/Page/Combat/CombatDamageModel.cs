using StarRailDamage.Source.UI.Factory.NotifyPropertyChanged;

namespace StarRailDamage.Source.UI.Model.Page.Combat
{
    public class CombatDamageModel : NotifyPropertyChangedFactory
    {
        private double _Base_Damage;

        private double _Critical_Strike_Damage;

        private double _Expected_Damage;

        private double _Broken_Damage;

        private double _Super_Broken_Damage;

        private double _Delayed_Damage;

        public double Base_Damage
        {
            get => _Base_Damage;
            set => SetField(ref _Base_Damage, value);
        }

        public double Critical_Strike_Damage
        {
            get => _Critical_Strike_Damage;
            set => SetField(ref _Critical_Strike_Damage, value);
        }

        public double Expected_Damage
        {
            get => _Expected_Damage;
            set => SetField(ref _Expected_Damage, value);
        }

        public double Broken_Damage
        {
            get => _Broken_Damage;
            set => SetField(ref _Broken_Damage, value);
        }

        public double Super_Broken_Damage
        {
            get => _Super_Broken_Damage;
            set => SetField(ref _Super_Broken_Damage, value);
        }

        public double Delayed_Damage
        {
            get => _Delayed_Damage;
            set => SetField(ref _Delayed_Damage, value);
        }
    }
}