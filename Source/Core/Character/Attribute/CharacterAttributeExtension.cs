using StarRailDamage.Source.Core.Language;
using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.Text;
using StarRailDamage.Source.Service.IO.AppManifest;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Media.Imaging;

namespace StarRailDamage.Source.Core.Character.Attribute
{
    public static class CharacterAttributeExtension
    {
        private static readonly Dictionary<string, CharacterAttribute> CharacterAttributeMap = [];

        private static readonly Dictionary<CharacterAttribute, CharacterAttributeInfoModel> CharacterAttributeInfoModelMap = [];

        public static bool TryGetAttribute(string key, [NotNullWhen(true)] out CharacterAttribute characterAttribute)
        {
            return CharacterAttributeMap.TryGetValue(key, out characterAttribute);
        }

        public static CharacterAttributeInfoModel GetInfoModel(this CharacterAttribute characterAttribute)
        {
            if (!CharacterAttributeInfoModelMap.TryGetValue(characterAttribute, out CharacterAttributeInfoModel? Model))
            {
                return CharacterAttributeInfoModelMap.GetValueOrDefault(CharacterAttribute.Unknown).ThrowIfNull();
            }
            return Model;
        }

        private static void AppendModelInfo(BitmapImage? bitmapImage, params KeyValuePair<CharacterAttribute, int>[] attributeDigits)
        {
            foreach (KeyValuePair<CharacterAttribute, int> AttributeDigit in attributeDigits)
            {
                TextBinding? FullNameTextBinding = null, SimpleTextBinding = null;
                if (Enum.TryParse(AttributeDigit.Key.ToString(), out FixedText fixedText))
                {
                    FullNameTextBinding = FixedTextExtension.Binding(fixedText);
                    CharacterAttributeMap[FullNameTextBinding.Text] = AttributeDigit.Key;
                }
                if (Enum.TryParse(AttributeDigit.Key.ToString() + "Simple", out fixedText))
                {
                    SimpleTextBinding = FixedTextExtension.Binding(fixedText);
                }
                CharacterAttributeInfoModelMap[AttributeDigit.Key] = new(bitmapImage, FullNameTextBinding, SimpleTextBinding, AttributeDigit.Value);
            }
        }

        private static BitmapImage GetImage(string name)
        {
            using Stream Stream = AppManifestStream.FindAndGetStream($"Attribute_Icon_{name}");
            return BitmapImageExtension.GetBitmapImage(Stream);
        }

        static CharacterAttributeExtension()
        {
            AppendModelInfo(GetImage("Unknown"), CharacterAttribute.Unknown.ToPair(0));
            AppendModelInfo(GetImage("Attack"), CharacterAttribute.Attack.ToPair(0), CharacterAttribute.AttackBase.ToPair(0));
            AppendModelInfo(GetImage("Health"), CharacterAttribute.Health.ToPair(0), CharacterAttribute.HealthBase.ToPair(0), CharacterAttribute.CharacterLevel.ToPair(0), CharacterAttribute.MonsterLevel.ToPair(0));
            AppendModelInfo(GetImage("Defense"), CharacterAttribute.Defense.ToPair(0), CharacterAttribute.DefenseBase.ToPair(0), CharacterAttribute.DefenseFailure.ToPair(0), CharacterAttribute.DamageImmunity.ToPair(1));
            AppendModelInfo(GetImage("Speed"), CharacterAttribute.Speed.ToPair(0), CharacterAttribute.SpeedBase.ToPair(0));
            AppendModelInfo(GetImage("Limit"), CharacterAttribute.Toughness.ToPair(0), CharacterAttribute.ToughnessReduced.ToPair(0), CharacterAttribute.SpecialNumericalValues.ToPair(0), CharacterAttribute.ChargingLimit.ToPair(0));
            AppendModelInfo(GetImage("Critical"), CharacterAttribute.CriticalHitRate.ToPair(1));
            AppendModelInfo(GetImage("Damage"), CharacterAttribute.CriticalDamage.ToPair(1));
            AppendModelInfo(GetImage("Break"), CharacterAttribute.SuperBreakDamage.ToPair(1), CharacterAttribute.BreakStrength.ToPair(1), CharacterAttribute.BreakEfficiency.ToPair(1));
            AppendModelInfo(GetImage("Effect"), CharacterAttribute.EffectHitRate.ToPair(1));
            AppendModelInfo(GetImage("Resist"), CharacterAttribute.EffectResistance.ToPair(1));
            AppendModelInfo(GetImage("Treatment"), CharacterAttribute.TreatmentImprovement.ToPair(1));
            AppendModelInfo(GetImage("Charging"), CharacterAttribute.ChargingEfficiency.ToPair(1));
            AppendModelInfo(null, CharacterAttribute.ElementResistance.ToPair(1));
            AppendModelInfo(null, CharacterAttribute.DamageMoreProne.ToPair(1));
            AppendModelInfo(null, CharacterAttribute.DamageIncrease.ToPair(1));
            AppendModelInfo(null, CharacterAttribute.ResistanceFailure.ToPair(1));
            AppendModelInfo(null, CharacterAttribute.BreakDamageIncrease.ToPair(1));
            AppendModelInfo(null, CharacterAttribute.MonsterCount.ToPair(0));
        }
    }
}