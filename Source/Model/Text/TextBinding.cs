using StarRailDamage.Source.UI.Factory.NotifyPropertyChanged;

namespace StarRailDamage.Source.Model.Text
{
    public class TextBinding : NotifyPropertyChangedFactory
    {
        private string _Text = string.Empty;

        public string Text
        {
            get => _Text;
            set => SetField(ref _Text, value);
        }
    }
}