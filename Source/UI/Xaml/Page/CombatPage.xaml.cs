using StarRailDamage.Source.Core.Character.Attribute;
using StarRailDamage.Source.Core.Character.Damage;
using StarRailDamage.Source.Core.Character.Element;
using StarRailDamage.Source.Core.Language;
using StarRailDamage.Source.Model.Text;
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

        public static CharacterAttributeInfoModel CharacterLevelAttributeInfo { get; } = CharacterAttribute.CharacterLevel.GetInfoModel();

        public static CharacterAttributeInfoModel MonsterLevelAttributeInfo { get; } = CharacterAttribute.MonsterLevel.GetInfoModel();

        public static CharacterAttributeInfoModel ElementResistanceAttributeInfo { get; } = CharacterAttribute.ElementResistance.GetInfoModel();

        public static CharacterAttributeInfoModel DamageImmunityAttributeInfo { get; } = CharacterAttribute.DamageImmunity.GetInfoModel();

        public static CharacterAttributeInfoModel DamageMoreProneAttributeInfo { get; } = CharacterAttribute.DamageMoreProne.GetInfoModel();

        public static CharacterAttributeInfoModel ToughnessAttributeInfo { get; } = CharacterAttribute.Toughness.GetInfoModel();

        public static CharacterAttributeInfoModel MonsterCountAttributeInfo { get; } = CharacterAttribute.MonsterCount.GetInfoModel();

        public static CharacterAttributeInfoModel AttackAttributeInfo { get; } = CharacterAttribute.Attack.GetInfoModel();

        public static CharacterAttributeInfoModel AttackBaseAttributeInfo { get; } = CharacterAttribute.AttackBase.GetInfoModel();

        public static CharacterAttributeInfoModel HealthAttributeInfo { get; } = CharacterAttribute.Health.GetInfoModel();

        public static CharacterAttributeInfoModel HealthBaseAttributeInfo { get; } = CharacterAttribute.HealthBase.GetInfoModel();

        public static CharacterAttributeInfoModel DefenseAttributeInfo { get; } = CharacterAttribute.Defense.GetInfoModel();

        public static CharacterAttributeInfoModel DefenseBaseAttributeInfo { get; } = CharacterAttribute.DefenseBase.GetInfoModel();

        public static CharacterAttributeInfoModel SpeedAttributeInfo { get; } = CharacterAttribute.Speed.GetInfoModel();

        public static CharacterAttributeInfoModel SpeedBaseAttributeInfo { get; } = CharacterAttribute.SpeedBase.GetInfoModel();

        public static CharacterAttributeInfoModel CriticalHitRateAttributeInfo { get; } = CharacterAttribute.CriticalHitRate.GetInfoModel();

        public static CharacterAttributeInfoModel CriticalDamageAttributeInfo { get; } = CharacterAttribute.CriticalDamage.GetInfoModel();

        public static CharacterAttributeInfoModel DamageIncreaseAttributeInfo { get; } = CharacterAttribute.DamageIncrease.GetInfoModel();

        public static CharacterAttributeInfoModel DefenseFailureAttributeInfo { get; } = CharacterAttribute.DefenseFailure.GetInfoModel();

        public static CharacterAttributeInfoModel ResistanceFailureAttributeInfo { get; } = CharacterAttribute.ResistanceFailure.GetInfoModel();

        public static CharacterAttributeInfoModel SuperBreakDamageAttributeInfo { get; } = CharacterAttribute.SuperBreakDamage.GetInfoModel();

        public static CharacterAttributeInfoModel BreakStrengthAttributeInfo { get; } = CharacterAttribute.BreakStrength.GetInfoModel();

        public static CharacterAttributeInfoModel BreakDamageIncreaseAttributeInfo { get; } = CharacterAttribute.BreakDamageIncrease.GetInfoModel();

        public static CharacterAttributeInfoModel BreakEfficiencyAttributeInfo { get; } = CharacterAttribute.BreakEfficiency.GetInfoModel();

        public static CharacterAttributeInfoModel ToughnessReducedAttributeInfo { get; } = CharacterAttribute.ToughnessReduced.GetInfoModel();

        public static CharacterAttributeInfoModel EffectHitRateAttributeInfo { get; } = CharacterAttribute.EffectHitRate.GetInfoModel();

        public static CharacterAttributeInfoModel EffectResistanceAttributeInfo { get; } = CharacterAttribute.EffectResistance.GetInfoModel();

        public static CharacterAttributeInfoModel TreatmentImprovementAttributeInfo { get; } = CharacterAttribute.TreatmentImprovement.GetInfoModel();

        public static CharacterAttributeInfoModel SpecialNumericalValuesAttributeInfo { get; } = CharacterAttribute.SpecialNumericalValues.GetInfoModel();

        public static CharacterAttributeInfoModel ChargingEfficiencyAttributeInfo { get; } = CharacterAttribute.ChargingEfficiency.GetInfoModel();

        public static CharacterAttributeInfoModel ChargingLimitAttributeInfo { get; } = CharacterAttribute.ChargingLimit.GetInfoModel();

        public static TextBinding BaseDamageSimpleTextBinding { get; } = FixedText.BaseDamageSimple.Binding();

        public static TextBinding CriticalStrikeDamageSimpleTextBinding { get; } = FixedText.CriticalStrikeDamageSimple.Binding();

        public static TextBinding ExpectedDamageSimpleTextBinding { get; } = FixedText.ExpectedDamageSimple.Binding();

        public static TextBinding BrokenDamageSimpleTextBinding { get; } = FixedText.BrokenDamageSimple.Binding();

        public static TextBinding SuperBrokenDamageSimpleTextBinding { get; } = FixedText.SuperBrokenDamageSimple.Binding();

        public static TextBinding LevelUnitTextBinding { get; } = FixedText.LevelUnit.Binding();

        public static TextBinding ExportDataTextBinding { get; } = FixedText.ExportData.Binding();

        public static TextBinding ChangeDataTextBinding { get; } = FixedText.ChangeData.Binding();

        public static TextBinding SaveDataTextBinding { get; } = FixedText.SaveData.Binding();

        public static TextBinding SettingTextBinding { get; } = FixedText.Setting.Binding();
    }
}