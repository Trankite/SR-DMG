using StarRailDamage.Source.UI.Factory.NotifyPropertyChanged;

namespace StarRailDamage.Source.UI.Model.Control
{
    public class ScopedSliderModel : NotifyPropertyChangedFactory
    {
        private string _Title = string.Empty;

        private double _Value;

        private double _Maximum;

        private double _Minimum;

        public string Title
        {
            get => _Title;
            set => SetField(ref _Title, value);
        }

        public double Value
        {
            get => _Value;
            set => SetField(ref _Value, value);
        }

        public double Maximum
        {
            get => _Maximum;
            set => SetField(ref _Maximum, value);
        }

        public double Minimun
        {
            get => _Minimum;
            set => SetField(ref _Minimum, value);
        }
    }
}