using StarRailDamage.Source.Core.Character.Attribute;
using StarRailDamage.Source.Core.Character.Element;
using StarRailDamage.Source.UI.Factory.NotifyPropertyChanged;

namespace StarRailDamage.Source.UI.Model.Page.Combat
{
    public class CombatPageModel : NotifyPropertyChangedFactory
    {
        private CharacterElement _CharacterElement;

        private CharacterAttributeModel _CharacterAttributeModel = new();

        public CharacterElement CharacterElement
        {
            get => _CharacterElement;
            set => SetField(ref _CharacterElement, value);
        }

        public CharacterAttributeModel CharacterAttributeModel
        {
            get => _CharacterAttributeModel;
            set => SetField(ref _CharacterAttributeModel, value);
        }
    }
}