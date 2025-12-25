using StarRailDamage.Source.Core.Language;
using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.Text;
using StarRailDamage.Source.Service.IO.Manifest;
using StarRailDamage.Source.UI.Assets.Lang;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;

namespace StarRailDamage.Source.Model.Metadata.Character.Attribute
{
    public static class CharacterAttributeExtension
    {
        private static readonly Dictionary<string, CharacterAttributeInfoModel> AttributeMap = [];

        [DebuggerStepThrough]
        public static CharacterAttributeInfoModel GetModel(string target)
        {
            return AttributeMap.GetValueOrDefault(target).ThrowIfNull();
        }

        private static void AppendModel(string characterAttribute, int digits)
        {
            AppendModel(BitmapImageExtension.DefaultImage, characterAttribute, digits);
        }

        private static BitmapImage AppendModel(this BitmapImage bitmapImage, string characterAttribute, int digits)
        {
            return AppendModel(bitmapImage, characterAttribute, digits, digits > 0 ? FixedTextExtension.Binding(nameof(FixedText.PercentUnit)) : TextBinding.Default);
        }

        private static BitmapImage AppendModel(this BitmapImage bitmapImage, string characterAttribute, int digits, TextBinding unitTextBinding)
        {
            string CharacterAttributeText = characterAttribute.ToString();
            TextBinding FullNameTextBinding = FixedTextExtension.Binding(CharacterAttributeText);
            TextBinding SimpleTextBinding = FixedTextExtension.Binding(CharacterAttributeText + "Simple");
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
            GetImage("Attack").AppendModel(nameof(FixedText.Attack), 0).AppendModel(nameof(FixedText.AttackBase), 0);
            GetImage("Health").AppendModel(nameof(FixedText.Health), 0).AppendModel(nameof(FixedText.HealthBase), 0).AppendModel(nameof(FixedText.CharacterLevel), 0, FixedTextExtension.Binding(nameof(FixedText.LevelUnit))).AppendModel(nameof(FixedText.EnemyLevel), 0, FixedTextExtension.Binding(nameof(FixedText.LevelUnit)));
            GetImage("Defense").AppendModel(nameof(FixedText.Defense), 0).AppendModel(nameof(FixedText.DefenseBase), 0).AppendModel(nameof(FixedText.DefenseDecrease), 0).AppendModel(nameof(FixedText.DamageDecrease), 1);
            GetImage("Speed").AppendModel(nameof(FixedText.Speed), 0).AppendModel(nameof(FixedText.SpeedBase), 0);
            GetImage("Limit").AppendModel(nameof(FixedText.Toughness), 0).AppendModel(nameof(FixedText.ToughnessReduced), 0).AppendModel(nameof(FixedText.HealingAmount), 0).AppendModel(nameof(FixedText.MaxEnergy), 0);
            GetImage("Critical").AppendModel(nameof(FixedText.CriticalHitRate), 1);
            GetImage("Damage").AppendModel(nameof(FixedText.CriticalDamage), 1);
            GetImage("Break").AppendModel(nameof(FixedText.SuperBreakEqual), 1).AppendModel(nameof(FixedText.BreakEffect), 1).AppendModel(nameof(FixedText.BreakEfficiency), 1);
            GetImage("Effect").AppendModel(nameof(FixedText.EffectHitRate), 1);
            GetImage("Resist").AppendModel(nameof(FixedText.EffectResistance), 1);
            GetImage("Treatment").AppendModel(nameof(FixedText.OutgoingHealingBoost), 1);
            GetImage("Charging").AppendModel(nameof(FixedText.EnergyRegenerationRate), 1);
            AppendModel(nameof(FixedText.ElementResistance), 1);
            AppendModel(nameof(FixedText.BreakDamageBoost), 1);
            AppendModel(nameof(FixedText.DamageBoost), 1);
            AppendModel(nameof(FixedText.EnemyAmount), 0);
            AppendModel(nameof(FixedText.ResistanceDecrease), 1);
            AppendModel(nameof(FixedText.DamageIncrease), 1);
        }
    }
}