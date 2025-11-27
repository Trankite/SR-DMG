using StarRailDamage.Source.UI.Factory.NotifyPropertyChanged;

namespace StarRailDamage.Source.UI.Model.View
{
    public class NumberTextBoxModel : NotifyPropertyChangedFactory
    {
        private string _Title = string.Empty;

        private string _Text = string.Empty;

        private double _Value;

        public string Title
        {
            get => _Title;
            set => SetField(ref _Title, value);
        }

        public string Text
        {
            get => _Text;
            set => SetField(ref _Text, value);
        }

        public double Value
        {
            get => _Value;
            set => SetField(ref _Value, value);
        }
    }
}