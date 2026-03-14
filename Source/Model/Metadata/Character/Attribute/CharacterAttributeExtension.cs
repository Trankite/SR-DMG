using StarRailDamage.Source.Core.Language;
using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.Text;
using StarRailDamage.Source.UI.Assets.Lang;
using System.Collections.Frozen;
using System.Diagnostics;
using System.Windows.Media.Imaging;

namespace StarRailDamage.Source.Model.Metadata.Character.Attribute
{
    public static class CharacterAttributeExtension
    {
        private static readonly FrozenDictionary<string, CharacterAttributeInfo> AttributeMap;

        [DebuggerStepThrough]
        public static CharacterAttributeInfo GetModel(string target)
        {
            return AttributeMap.GetValueOrDefault(target).ThrowIfNull();
        }

        private static KeyValuePair<string, CharacterAttributeInfo> GetAttribute(string attribute, BitmapImage icon, TextBinding unit, int digits)
        {
            return KeyValuePair.Create(attribute, CharacterAttributeInfo.Create(attribute, icon, unit, digits));
        }

        static CharacterAttributeExtension()
        {
            TextBinding LevelUnit = FixedTextExtension.Binding(nameof(FixedText.LevelUnit));
            TextBinding PercentUnit = FixedTextExtension.Binding(nameof(FixedText.PercentUnit));
            AttributeMap = FrozenDictionary.Create([
                GetAttribute(nameof(FixedText.CharacterLevel),          AttributeImage.Health,      LevelUnit,              0),
                GetAttribute(nameof(FixedText.EnemyLevel),              AttributeImage.Health,      LevelUnit,              0),
                GetAttribute(nameof(FixedText.EnemyAmount),             AttributeImage.Unknown,     TextBinding.Default,    0),
                GetAttribute(nameof(FixedText.ElementResistance),       AttributeImage.Unknown,     PercentUnit,            1),
                GetAttribute(nameof(FixedText.DamageDecrease),          AttributeImage.Defense,     PercentUnit,            1),
                GetAttribute(nameof(FixedText.DamageIncrease),          AttributeImage.Unknown,     PercentUnit,            1),
                GetAttribute(nameof(FixedText.Toughness),               AttributeImage.Battery,     TextBinding.Default,    0),
                GetAttribute(nameof(FixedText.Attack),                  AttributeImage.Attack,      TextBinding.Default,    0),
                GetAttribute(nameof(FixedText.AttackBase),              AttributeImage.Attack,      TextBinding.Default,    0),
                GetAttribute(nameof(FixedText.Health),                  AttributeImage.Health,      TextBinding.Default,    0),
                GetAttribute(nameof(FixedText.HealthBase),              AttributeImage.Health,      TextBinding.Default,    0),
                GetAttribute(nameof(FixedText.Defense),                 AttributeImage.Defense,     TextBinding.Default,    0),
                GetAttribute(nameof(FixedText.DefenseBase),             AttributeImage.Defense,     TextBinding.Default,    0),
                GetAttribute(nameof(FixedText.Speed),                   AttributeImage.Speed,       TextBinding.Default,    0),
                GetAttribute(nameof(FixedText.SpeedBase),               AttributeImage.Speed,       TextBinding.Default,    0),
                GetAttribute(nameof(FixedText.CriticalHitRate),         AttributeImage.Augment,    PercentUnit,            1),
                GetAttribute(nameof(FixedText.CriticalDamage),          AttributeImage.Offense,     PercentUnit,            1),
                GetAttribute(nameof(FixedText.DamageBoost),             AttributeImage.Unknown,     PercentUnit,            1),
                GetAttribute(nameof(FixedText.DefenseDecrease),         AttributeImage.Defense,     PercentUnit,            1),
                GetAttribute(nameof(FixedText.ResistanceDecrease),      AttributeImage.Unknown,     PercentUnit,            1),
                GetAttribute(nameof(FixedText.SuperBreakEqual),         AttributeImage.Break,       PercentUnit,            1),
                GetAttribute(nameof(FixedText.BreakEffect),             AttributeImage.Break,       PercentUnit,            1),
                GetAttribute(nameof(FixedText.BreakDamageBoost),        AttributeImage.Break,       PercentUnit,            1),
                GetAttribute(nameof(FixedText.BreakEfficiency),         AttributeImage.Break,       PercentUnit,            1),
                GetAttribute(nameof(FixedText.ToughnessReduced),        AttributeImage.Battery,     TextBinding.Default,    0),
                GetAttribute(nameof(FixedText.EffectHitRate),           AttributeImage.Stroke,      PercentUnit,            1),
                GetAttribute(nameof(FixedText.EffectResistance),        AttributeImage.Shielding,   PercentUnit,            1),
                GetAttribute(nameof(FixedText.OutgoingHealingBoost),    AttributeImage.Treatment,   PercentUnit,            1),
                GetAttribute(nameof(FixedText.HealingAmount),           AttributeImage.Battery,     TextBinding.Default,    0),
                GetAttribute(nameof(FixedText.EnergyRegeneratRate),     AttributeImage.Charging,    PercentUnit,            1),
                GetAttribute(nameof(FixedText.MaxEnergy),               AttributeImage.Battery,     TextBinding.Default,    0),
                ]);
        }
    }
}