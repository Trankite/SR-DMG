using StarRailDamage.Source.Extension;
using System.Diagnostics;

namespace StarRailDamage.Source.Model.Metadata.Character.Element
{
    public static class CharacterElementExtension
    {
        private static readonly Dictionary<string, CharacterElementModel> ElementMap = [];

        [DebuggerStepThrough]
        public static CharacterElementModel GetModel(this CharacterElement characterElement)
        {
            return ElementMap.GetValueOrDefault(characterElement.ToString()).ThrowIfNull();
        }

        static CharacterElementExtension()
        {
            foreach (string ElementTypeString in Enum.GetNames<CharacterElement>())
            {
                ElementMap[ElementTypeString] = CharacterElementModel.Create(ElementTypeString);
            }
        }
    }
}