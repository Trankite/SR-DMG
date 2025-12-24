using StarRailDamage.Source.UI.Control.Panel;
using StarRailDamage.Source.UI.Factory.NotifyPropertyChanged;

namespace StarRailDamage.Source.UI.Model.Control
{
    public class ScopedTabItemModel : NotifyPropertyChangedFactory
    {
        private string _Header = string.Empty;

        private ScopedPage? _Content;

        public ScopedTabItemModel(string header)
        {
            Header = header;
        }

        public ScopedTabItemModel(string header, ScopedPage? content) : this(header)
        {
            Content = content;
        }

        public string Header
        {
            get => _Header;
            set => SetField(ref _Header, value);
        }

        public ScopedPage? Content
        {
            get => _Content;
            set => SetField(ref _Content, value);
        }
    }
}