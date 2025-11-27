using StarRailDamage.Source.UI.Factory.NotifyPropertyChanged;
using System.Windows.Controls;

namespace StarRailDamage.Source.UI.Model.Control
{
    public class ScopedTabItemModel : NotifyPropertyChangedFactory
    {
        private string _Header = string.Empty;

        private Page? _Content;

        public ScopedTabItemModel() { }

        public ScopedTabItemModel(string header)
        {
            _Header = header;
        }

        public ScopedTabItemModel(string header, Page? content) : this(header)
        {
            _Content = content;
        }

        public string Header
        {
            get => _Header;
            set => SetField(ref _Header, value);
        }

        public Page? Content
        {
            get => _Content;
            set => SetField(ref _Content, value);
        }
    }
}