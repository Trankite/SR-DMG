using StarRailDamage.Source.Model.Text;
using StarRailDamage.Source.UI.Factory.NotifyPropertyChanged;

namespace StarRailDamage.Source.UI.Model.View
{
    public class PercentSpanModel : NotifyPropertyChangedFactory
    {
        private TextBinding _Title = TextBinding.Default;

        private double _Value;

        private double _TempValue;

        private double _Percent;

        public TextBinding Title
        {
            get => _Title;
            set => SetField(ref _Title, value);
        }

        public double Value
        {
            get => _Value;
            set => SetField(ref _Value, value);
        }

        public double TempValue
        {
            get => _TempValue;
            set => SetField(ref _TempValue, value);
        }

        public double Percent
        {
            get => _Percent;
            set => SetField(ref _Percent, value);
        }
    }
}