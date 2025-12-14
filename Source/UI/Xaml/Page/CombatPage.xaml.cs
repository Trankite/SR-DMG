using StarRailDamage.Source.Core.Language;
using StarRailDamage.Source.Model.DataStruct.Formula;
using StarRailDamage.Source.Model.DataStruct.Formula.Evaluator;
using StarRailDamage.Source.Model.DataStruct.Formula.Factory;
using StarRailDamage.Source.Model.Metadata.Character.Attribute;
using StarRailDamage.Source.Model.Metadata.Character.Damage;
using StarRailDamage.Source.Model.Metadata.Character.Element;
using StarRailDamage.Source.Model.Text;
using StarRailDamage.Source.UI.Control.Panel;
using StarRailDamage.Source.UI.Factory.PropertyBinding;
using StarRailDamage.Source.UI.Model.Page.Combat;
using StarRailDamage.Source.UI.Xaml.View;
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

        private void NumberTextBoxOnEvaluate(object sender, RoutedEventArgs e)
        {
            //NumberTextBox NumberTextBox = (NumberTextBox)sender;
            //MathFormula? MathFormula = new MathFormulaFactory().FormulaParse(NumberTextBox.Text);
            //NumberTextBox.Value = MathFormulaEvaluator.GetValue(MathFormula);
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

        public static CharacterAttributeInfoModel CharacterLevelAttributeInfo { get; } = CharacterAttribute.CharacterLevel.GetModel();

        public static CharacterAttributeInfoModel MonsterLevelAttributeInfo { get; } = CharacterAttribute.MonsterLevel.GetModel();

        public static CharacterAttributeInfoModel ElementResistanceAttributeInfo { get; } = CharacterAttribute.ElementResistance.GetModel();

        public static CharacterAttributeInfoModel DamageImmunityAttributeInfo { get; } = CharacterAttribute.DamageImmunity.GetModel();

        public static CharacterAttributeInfoModel DamageMoreProneAttributeInfo { get; } = CharacterAttribute.DamageMoreProne.GetModel();

        public static CharacterAttributeInfoModel ToughnessAttributeInfo { get; } = CharacterAttribute.Toughness.GetModel();

        public static CharacterAttributeInfoModel MonsterCountAttributeInfo { get; } = CharacterAttribute.MonsterCount.GetModel();

        public static CharacterAttributeInfoModel AttackAttributeInfo { get; } = CharacterAttribute.Attack.GetModel();

        public static CharacterAttributeInfoModel AttackBaseAttributeInfo { get; } = CharacterAttribute.AttackBase.GetModel();

        public static CharacterAttributeInfoModel HealthAttributeInfo { get; } = CharacterAttribute.Health.GetModel();

        public static CharacterAttributeInfoModel HealthBaseAttributeInfo { get; } = CharacterAttribute.HealthBase.GetModel();

        public static CharacterAttributeInfoModel DefenseAttributeInfo { get; } = CharacterAttribute.Defense.GetModel();

        public static CharacterAttributeInfoModel DefenseBaseAttributeInfo { get; } = CharacterAttribute.DefenseBase.GetModel();

        public static CharacterAttributeInfoModel SpeedAttributeInfo { get; } = CharacterAttribute.Speed.GetModel();

        public static CharacterAttributeInfoModel SpeedBaseAttributeInfo { get; } = CharacterAttribute.SpeedBase.GetModel();

        public static CharacterAttributeInfoModel CriticalHitRateAttributeInfo { get; } = CharacterAttribute.CriticalHitRate.GetModel();

        public static CharacterAttributeInfoModel CriticalDamageAttributeInfo { get; } = CharacterAttribute.CriticalDamage.GetModel();

        public static CharacterAttributeInfoModel DamageIncreaseAttributeInfo { get; } = CharacterAttribute.DamageIncrease.GetModel();

        public static CharacterAttributeInfoModel DefenseFailureAttributeInfo { get; } = CharacterAttribute.DefenseFailure.GetModel();

        public static CharacterAttributeInfoModel ResistanceFailureAttributeInfo { get; } = CharacterAttribute.ResistanceFailure.GetModel();

        public static CharacterAttributeInfoModel SuperBreakDamageAttributeInfo { get; } = CharacterAttribute.SuperBreakDamage.GetModel();

        public static CharacterAttributeInfoModel BreakStrengthAttributeInfo { get; } = CharacterAttribute.BreakStrength.GetModel();

        public static CharacterAttributeInfoModel BreakDamageIncreaseAttributeInfo { get; } = CharacterAttribute.BreakDamageIncrease.GetModel();

        public static CharacterAttributeInfoModel BreakEfficiencyAttributeInfo { get; } = CharacterAttribute.BreakEfficiency.GetModel();

        public static CharacterAttributeInfoModel ToughnessReducedAttributeInfo { get; } = CharacterAttribute.ToughnessReduced.GetModel();

        public static CharacterAttributeInfoModel EffectHitRateAttributeInfo { get; } = CharacterAttribute.EffectHitRate.GetModel();

        public static CharacterAttributeInfoModel EffectResistanceAttributeInfo { get; } = CharacterAttribute.EffectResistance.GetModel();

        public static CharacterAttributeInfoModel TreatmentImprovementAttributeInfo { get; } = CharacterAttribute.TreatmentImprovement.GetModel();

        public static CharacterAttributeInfoModel SpecialNumericalValuesAttributeInfo { get; } = CharacterAttribute.SpecialNumericalValues.GetModel();

        public static CharacterAttributeInfoModel ChargingEfficiencyAttributeInfo { get; } = CharacterAttribute.ChargingEfficiency.GetModel();

        public static CharacterAttributeInfoModel ChargingLimitAttributeInfo { get; } = CharacterAttribute.ChargingLimit.GetModel();

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