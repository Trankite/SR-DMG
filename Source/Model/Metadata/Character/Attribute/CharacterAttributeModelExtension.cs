using StarRailDamage.Source.Model.Metadata.Character.Damage;
using StarRailDamage.Source.Model.Metadata.Character.Element;

namespace StarRailDamage.Source.Model.Metadata.Character.Attribute
{
    public static class CharacterAttributeModelExtension
    {
        private static readonly int[] BreakTable;

        public static CharacterDamageModel Damage(this CharacterAttributeModel model, CharacterElement element, CharacterDamageModel damage)
        {
            double Correction = (1 + model.DamageIncrease / 100) * (1 - model.DamageDecrease / 100);
            double Resistance = 1 - Math.Min(90, Math.Max(model.ElementResistance - model.ResistanceDecrease, -100)) / 100;
            double Defense = (model.CharacterLevel + 20) / (model.CharacterLevel + 20 + (model.EnemyLevel + 20) * Math.Max(0, 1 - model.DefenseDecrease / 100));
            damage.DelayedDamage = damage.NormalDamage * Correction * Resistance * Defense * (1 + model.DamageBoost / 100);
            damage.CriticalHitDamage = damage.NormalDamage * Correction * Resistance * Defense * (1 + model.CriticalDamage / 100);
            damage.ExpectedDamage = damage.NormalDamage * Correction * Resistance * Defense * (1 + model.CriticalHitRate / 100 * model.CriticalHitRate / 100);
            double NormalBreak = (1 + model.BreakEffect / 100) * (1 + model.BreakDamageBoost / 100) * GetNormalBreak(model.CharacterLevel);
            double Toughness = model.ToughnessReduced * (1 + model.BreakEfficiency / 100) / 10;
            damage.BreakDamage = NormalBreak * Correction * Resistance * Defense * (model.Toughness / 20 - 0.5) * GetBreakEqual(element);
            damage.SuperBreakDamage = NormalBreak * Correction * Resistance * Defense * Toughness;
            return damage;
        }

        public static double GetNormalBreak(double level) => BreakTable[Math.Max(1, Math.Min(Convert.ToInt32(level), 80)) - 1];

        public static double GetBreakEqual(CharacterElement element)
        {
            return element switch { CharacterElement.Quantum or CharacterElement.Imaginary => 0.5, CharacterElement.Ice or CharacterElement.Lightning => 1, CharacterElement.Wind => 1.5, CharacterElement.Fire or CharacterElement.Physical => 2, _ => double.NaN };
        }

        static CharacterAttributeModelExtension()
        {
            BreakTable =
            [
                54,     58,     62,     68,     71,     74,     77,     80,     83,     86,
                91,     97,     103,    108,    113,    119,    124,    129,    135,    140,
                149,    159,    168,    177,    187,    196,    205,    214,    222,    231,
                246,    261,    275,    289,    303,    316,    328,    340,    352,    364,
                408,    452,    495,    537,    578,    619,    659,    698,    737,    775,
                871,    965,    1056,   1146,   1233,   1318,   1402,   1483,   1563,   1640,
                1752,   1862,   1969,   2074,   2177,   2277,   2376,   2472,   2567,   2660,
                2780,   2899,   3015,   3128,   3240,   3349,   3457,   3562,   3666,   3768
            ];
        }
    }
}