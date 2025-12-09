using StarRailDamage.Source.Core.Language;
using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.DataStruct;
using StarRailDamage.Source.Model.Text;
using StarRailDamage.Source.Service.IO.AppManifest;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Media.Imaging;

namespace StarRailDamage.Source.Model.Metadata.Character.Attribute
{
    public static class CharacterAttributeExtension
    {
        private static readonly Dictionary<string, CharacterAttribute> CharacterAttributeMap = [];

        private static readonly Dictionary<CharacterAttribute, CharacterAttributeInfoModel> CharacterAttributeModelMap = [];

        [DebuggerStepThrough]
        public static bool TryGetAttribute(string key, [NotNullWhen(true)] out CharacterAttribute characterAttribute)
        {
            return CharacterAttributeMap.TryGetValue(key, out characterAttribute);
        }

        [DebuggerStepThrough]
        public static CharacterAttributeInfoModel GetModel(this CharacterAttribute characterAttribute)
        {
            if (!CharacterAttributeModelMap.TryGetValue(characterAttribute, out CharacterAttributeInfoModel? Model))
            {
                return CharacterAttributeModelMap.GetValueOrDefault(CharacterAttribute.Unknown).ThrowIfNull();
            }
            return Model;
        }

        private static BitmapImage AppendModel(this BitmapImage bitmapImage, FrozenStruct<CharacterAttribute, int> frozenStruct)
        {
            TextBinding? FullNameTextBinding = null, SimpleTextBinding = null;
            if (Enum.TryParse(frozenStruct.Name.ToString(), out FixedText fixedText))
            {
                FullNameTextBinding = FixedTextExtension.Binding(fixedText);
                CharacterAttributeMap[FullNameTextBinding.Text] = frozenStruct.Name;
            }
            if (Enum.TryParse(frozenStruct.Name.ToString() + "Simple", out fixedText))
            {
                SimpleTextBinding = FixedTextExtension.Binding(fixedText);
            }
            CharacterAttributeModelMap[frozenStruct.Name] = new(bitmapImage, FullNameTextBinding, SimpleTextBinding, frozenStruct.Value);
            return bitmapImage;
        }

        private static BitmapImage GetImage(string name)
        {
            using Stream Stream = AppManifestStream.FindAndGetStream($"Attribute_Icon_{name}");
            return BitmapImageExtension.GetBitmapImage(Stream);
        }

        static CharacterAttributeExtension()
        {
            GetImage("Unknown").AppendModel(new(CharacterAttribute.Unknown, 0));
            GetImage("Attack").AppendModel(new(CharacterAttribute.Attack, 0)).AppendModel(new(CharacterAttribute.AttackBase, 0));
            GetImage("Health").AppendModel(new(CharacterAttribute.Health, 0)).AppendModel(new(CharacterAttribute.HealthBase, 0)).AppendModel(new(CharacterAttribute.CharacterLevel, 0)).AppendModel(new(CharacterAttribute.MonsterLevel, 0));
            GetImage("Defense").AppendModel(new(CharacterAttribute.Defense, 0)).AppendModel(new(CharacterAttribute.DefenseBase, 0)).AppendModel(new(CharacterAttribute.DefenseFailure, 0)).AppendModel(new(CharacterAttribute.DamageImmunity, 1));
            GetImage("Speed").AppendModel(new(CharacterAttribute.Speed, 0)).AppendModel(new(CharacterAttribute.SpeedBase, 0));
            GetImage("Limit").AppendModel(new(CharacterAttribute.Toughness, 0)).AppendModel(new(CharacterAttribute.ToughnessReduced, 0)).AppendModel(new(CharacterAttribute.SpecialNumericalValues, 0)).AppendModel(new(CharacterAttribute.ChargingLimit, 0));
            GetImage("Critical").AppendModel(new(CharacterAttribute.CriticalHitRate, 1));
            GetImage("Damage").AppendModel(new(CharacterAttribute.CriticalDamage, 1));
            GetImage("Break").AppendModel(new(CharacterAttribute.SuperBreakDamage, 1)).AppendModel(new(CharacterAttribute.BreakStrength, 1)).AppendModel(new(CharacterAttribute.BreakEfficiency, 1));
            GetImage("Effect").AppendModel(new(CharacterAttribute.EffectHitRate, 1));
            GetImage("Resist").AppendModel(new(CharacterAttribute.EffectResistance, 1));
            GetImage("Treatment").AppendModel(new(CharacterAttribute.TreatmentImprovement, 1));
            GetImage("Charging").AppendModel(new(CharacterAttribute.ChargingEfficiency, 1));
            BitmapImageExtension.DefaultImage.AppendModel(new(CharacterAttribute.ElementResistance, 1));
            BitmapImageExtension.DefaultImage.AppendModel(new(CharacterAttribute.DamageMoreProne, 1));
            BitmapImageExtension.DefaultImage.AppendModel(new(CharacterAttribute.DamageIncrease, 1));
            BitmapImageExtension.DefaultImage.AppendModel(new(CharacterAttribute.ResistanceFailure, 1));
            BitmapImageExtension.DefaultImage.AppendModel(new(CharacterAttribute.BreakDamageIncrease, 1));
            BitmapImageExtension.DefaultImage.AppendModel(new(CharacterAttribute.MonsterCount, 0));
        }
    }
}