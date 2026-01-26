using StarRailDamage.Source.Model.Metadata.Character.Damage;
using StarRailDamage.Source.Model.Metadata.Character.Element;

namespace StarRailDamage.Source.Model.Metadata.Character.Attribute
{
    public static class CharacterAttributeModelExtension
    {
        private static readonly int[] BreakMap = new int[80];

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

        public static double GetNormalBreak(double level) => BreakMap[Math.Max(0, Math.Min(Convert.ToInt32(level), 80))];

        public static double GetBreakEqual(CharacterElement element)
        {
            return element switch { CharacterElement.Quantum or CharacterElement.Imaginary => 0.5, CharacterElement.Ice or CharacterElement.Lightning => 1, CharacterElement.Wind => 1.5, CharacterElement.Fire or CharacterElement.Physical => 2, _ => double.NaN };
        }

        static CharacterAttributeModelExtension()
        {
            BreakMap[0] = 0;
            BreakMap[1] = 54;
            BreakMap[2] = 58;
            BreakMap[3] = 62;
            BreakMap[4] = 68;
            BreakMap[5] = 71;
            BreakMap[6] = 74;
            BreakMap[7] = 77;
            BreakMap[8] = 80;
            BreakMap[9] = 83;
            BreakMap[10] = 86;
            BreakMap[11] = 91;
            BreakMap[12] = 97;
            BreakMap[13] = 103;
            BreakMap[14] = 108;
            BreakMap[15] = 113;
            BreakMap[16] = 119;
            BreakMap[17] = 124;
            BreakMap[18] = 129;
            BreakMap[19] = 135;
            BreakMap[20] = 140;
            BreakMap[21] = 149;
            BreakMap[22] = 159;
            BreakMap[23] = 168;
            BreakMap[24] = 177;
            BreakMap[25] = 187;
            BreakMap[26] = 196;
            BreakMap[27] = 205;
            BreakMap[28] = 214;
            BreakMap[29] = 222;
            BreakMap[30] = 231;
            BreakMap[31] = 246;
            BreakMap[32] = 261;
            BreakMap[33] = 275;
            BreakMap[34] = 289;
            BreakMap[35] = 303;
            BreakMap[36] = 316;
            BreakMap[37] = 328;
            BreakMap[38] = 340;
            BreakMap[39] = 352;
            BreakMap[40] = 364;
            BreakMap[41] = 408;
            BreakMap[42] = 452;
            BreakMap[43] = 495;
            BreakMap[44] = 537;
            BreakMap[45] = 578;
            BreakMap[46] = 619;
            BreakMap[47] = 659;
            BreakMap[48] = 698;
            BreakMap[49] = 737;
            BreakMap[50] = 775;
            BreakMap[51] = 871;
            BreakMap[52] = 965;
            BreakMap[53] = 1056;
            BreakMap[54] = 1146;
            BreakMap[55] = 1233;
            BreakMap[56] = 1318;
            BreakMap[57] = 1402;
            BreakMap[58] = 1483;
            BreakMap[59] = 1563;
            BreakMap[60] = 1640;
            BreakMap[61] = 1752;
            BreakMap[62] = 1862;
            BreakMap[63] = 1969;
            BreakMap[64] = 2074;
            BreakMap[65] = 2177;
            BreakMap[66] = 2277;
            BreakMap[67] = 2376;
            BreakMap[68] = 2472;
            BreakMap[69] = 2567;
            BreakMap[70] = 2660;
            BreakMap[71] = 2780;
            BreakMap[72] = 2899;
            BreakMap[73] = 3015;
            BreakMap[74] = 3128;
            BreakMap[75] = 3240;
            BreakMap[76] = 3349;
            BreakMap[77] = 3457;
            BreakMap[78] = 3562;
            BreakMap[79] = 3666;
            BreakMap[80] = 3768;
        }
    }
}