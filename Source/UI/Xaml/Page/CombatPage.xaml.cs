using StarRailDamage.Source.Model.Metadata.Character.Attribute;
using StarRailDamage.Source.Model.Metadata.Character.Damage;
using StarRailDamage.Source.Model.Metadata.Character.Element;
using StarRailDamage.Source.UI.Control.Panel;
using StarRailDamage.Source.UI.Factory.PropertyBinding;
using StarRailDamage.Source.UI.Model.Page.Combat;
using System.Windows;

namespace StarRailDamage.Source.UI.Xaml.Page
{
    public sealed partial class CombatPage : ScopedPage
    {
        private static readonly PropertyBindingFactory<CombatPage> BindingFactory = new();

        public CombatPage()
        {
            InitializeComponent();
        }

        public CombatPageModel Model
        {
            get => (CombatPageModel)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }

        public static readonly DependencyProperty ModelProperty = BindingFactory.ModelBinding(x => x.Model);

        public CharacterElement CharacterElement
        {
            get => (CharacterElement)GetValue(CharacterElementProperty);
            set => SetValue(CharacterElementProperty, value);
        }

        public static readonly DependencyProperty CharacterElementProperty = BindingFactory.DependBinding(x => x.Model.CharacterElement, x => x.CharacterElement, PropertyBindingMode.TwoWay, default, CharacterElementChangedCallback);

        private static void CharacterElementChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is CharacterElement CharacterElement)
            {
                ((CombatPage)d).CharacterElementModel = CharacterElement.GetModel();
            }
        }

        public CharacterAttributeModel CharacterAttributeModel
        {
            get => (CharacterAttributeModel)GetValue(CharacterAttributeModelProperty);
            set => SetValue(CharacterAttributeModelProperty, value);
        }

        public static readonly DependencyProperty CharacterAttributeModelProperty = BindingFactory.DependBinding(x => x.Model.CharacterAttributeModel, x => x.CharacterAttributeModel, PropertyBindingMode.TwoWay, new CharacterAttributeModel());

        public CharacterElementModel CharacterElementModel
        {
            get => (CharacterElementModel)GetValue(CharacterElementModelProperty);
            set => SetValue(CharacterElementModelProperty, value);
        }

        public static readonly DependencyProperty CharacterElementModelProperty = BindingFactory.DependProperty(x => x.CharacterElementModel, CharacterElement.Physical.GetModel());

        public CharacterDamageModel CharacterDamageModel
        {
            get => (CharacterDamageModel)GetValue(CharacterDamageModelProperty);
            set => SetValue(CharacterDamageModelProperty, value);
        }

        public static readonly DependencyProperty CharacterDamageModelProperty = BindingFactory.DependProperty(x => x.CharacterDamageModel, new CharacterDamageModel());

        public CharacterDamageModel ComparedDamageModel
        {
            get => (CharacterDamageModel)GetValue(ComparedDamageModelProperty);
            set => SetValue(ComparedDamageModelProperty, value);
        }

        public static readonly DependencyProperty ComparedDamageModelProperty = BindingFactory.DependProperty(x => x.ComparedDamageModel, new CharacterDamageModel());
    }
}