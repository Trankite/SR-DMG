using StarRailDamage.Source.Core.Language;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Model.Metadata.Character.Damage
{
    public static class CharacterDamageExtension
    {
        private static readonly Dictionary<string, CharacterDamage> CharacterDamageMap = [];

        [DebuggerStepThrough]
        public static bool TryGetCharacterDamage(string key, [NotNullWhen(true)] out CharacterDamage characterDamage)
        {
            return CharacterDamageMap.TryGetValue(key, out characterDamage);
        }

        static CharacterDamageExtension()
        {
            foreach (CharacterDamage CharacterDamage in Enum.GetValues<CharacterDamage>())
            {
                if (Enum.TryParse(CharacterDamage.ToString(), out FixedText FixedText))
                {
                    CharacterDamageMap[FixedText.Binding().Text] = CharacterDamage;
                }
            }
        }
    }
}