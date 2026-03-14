using StarRailDamage.Source.Extension;
using System.Diagnostics;

namespace StarRailDamage.Source.Model.Metadata.Character.Occupation
{
    public static class CharacterOccupationExtenison
    {
        private static readonly Dictionary<string, CharacterOccupationModel> OccupationMap = [];

        [DebuggerStepThrough]
        public static CharacterOccupationModel GetModel(this CharacterOccupation occupation)
        {
            return OccupationMap.GetValueOrDefault(occupation.ToString()).ThrowIfNull();
        }

        static CharacterOccupationExtenison()
        {
            foreach (string OccupationString in Enum.GetNames<CharacterOccupation>())
            {
                OccupationMap[OccupationString] = CharacterOccupationModel.Create(OccupationString);
            }
        }
    }
}