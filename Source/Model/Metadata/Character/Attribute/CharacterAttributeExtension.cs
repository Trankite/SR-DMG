using StarRailDamage.Source.Core.Language;
using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.Text;
using StarRailDamage.Source.Service.IO.Manifest;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;

namespace StarRailDamage.Source.Model.Metadata.Character.Attribute
{
    public static class CharacterAttributeExtension
    {
        private static readonly Dictionary<string, CharacterAttributeInfoModel> AttributeMap = [];

        [DebuggerStepThrough]
        public static CharacterAttributeInfoModel GetModel(this CharacterAttribute characterAttribute)
        {
            return GetModel(characterAttribute.ToString());
        }

        [DebuggerStepThrough]
        public static CharacterAttributeInfoModel GetModel(string target)
        {
            return AttributeMap.GetValueOrDefault(target).ThrowIfNull();
        }

        private static void AppendModel(CharacterAttribute characterAttribute, int digits)
        {
            AppendModel(BitmapImageExtension.DefaultImage, characterAttribute, digits);
        }

        private static BitmapImage AppendModel(this BitmapImage bitmapImage, CharacterAttribute characterAttribute, int digits)
        {
            return AppendModel(bitmapImage, characterAttribute, digits, digits > 0 ? FixedText.PercentUnit.Binding() : TextBinding.Default);
        }

        private static BitmapImage AppendModel(this BitmapImage bitmapImage, CharacterAttribute characterAttribute, int digits, TextBinding unitTextBinding)
        {
            TextBinding SimpleTextBinding = TextBinding.Default;
            TextBinding FullNameTextBinding = TextBinding.Default;
            string CharacterAttributeText = characterAttribute.ToString();
            if (Enum.TryParse(CharacterAttributeText, out FixedText fixedText))
            {
                FullNameTextBinding = FixedTextExtension.Binding(fixedText);
            }
            if (Enum.TryParse(CharacterAttributeText + "Simple", out fixedText))
            {
                SimpleTextBinding = FixedTextExtension.Binding(fixedText);
            }
            AttributeMap[CharacterAttributeText] = new(bitmapImage, FullNameTextBinding, SimpleTextBinding, digits, unitTextBinding);
            return bitmapImage;
        }

        private static BitmapImage GetImage(string name)
        {
            using Stream Stream = AppManifestStream.FindAndGetStream($"Attribute_Icon_{name}");
            return BitmapImageExtension.GetBitmapImage(Stream);
        }

        static CharacterAttributeExtension()
        {
            GetImage("Unknown").AppendModel(CharacterAttribute.Unknown, 0);
            GetImage("Attack").AppendModel(CharacterAttribute.Attack, 0).AppendModel(CharacterAttribute.AttackBase, 0);
            GetImage("Health").AppendModel(CharacterAttribute.Health, 0).AppendModel(CharacterAttribute.HealthBase, 0).AppendModel(CharacterAttribute.CharacterLevel, 0, FixedText.LevelUnit.Binding()).AppendModel(CharacterAttribute.MonsterLevel, 0, FixedText.LevelUnit.Binding());
            GetImage("Defense").AppendModel(CharacterAttribute.Defense, 0).AppendModel(CharacterAttribute.DefenseBase, 0).AppendModel(CharacterAttribute.DefenseFailure, 0).AppendModel(CharacterAttribute.DamageImmunity, 1);
            GetImage("Speed").AppendModel(CharacterAttribute.Speed, 0).AppendModel(CharacterAttribute.SpeedBase, 0);
            GetImage("Limit").AppendModel(CharacterAttribute.Toughness, 0).AppendModel(CharacterAttribute.ToughnessReduced, 0).AppendModel(CharacterAttribute.SpecialNumericalValues, 0).AppendModel(CharacterAttribute.ChargingLimit, 0);
            GetImage("Critical").AppendModel(CharacterAttribute.CriticalHitRate, 1);
            GetImage("Damage").AppendModel(CharacterAttribute.CriticalDamage, 1);
            GetImage("Break").AppendModel(CharacterAttribute.SuperBreakDamage, 1).AppendModel(CharacterAttribute.BreakStrength, 1).AppendModel(CharacterAttribute.BreakEfficiency, 1);
            GetImage("Effect").AppendModel(CharacterAttribute.EffectHitRate, 1);
            GetImage("Resist").AppendModel(CharacterAttribute.EffectResistance, 1);
            GetImage("Treatment").AppendModel(CharacterAttribute.TreatmentImprovement, 1);
            GetImage("Charging").AppendModel(CharacterAttribute.ChargingEfficiency, 1);
            AppendModel(CharacterAttribute.ElementResistance, 1);
            AppendModel(CharacterAttribute.DamageMoreProne, 1);
            AppendModel(CharacterAttribute.BreakDamageIncrease, 1);
            AppendModel(CharacterAttribute.MonsterCount, 0);
            AppendModel(CharacterAttribute.ResistanceFailure, 1);
            AppendModel(CharacterAttribute.DamageIncrease, 1);
        }
    }
}