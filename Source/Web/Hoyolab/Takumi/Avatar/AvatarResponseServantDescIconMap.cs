using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Avatar
{
    public class AvatarResponseServantDescIconMap
    {
        [JsonPropertyName("AvatarCyrene")]
        public string AvatarCyrene { get; set; } = string.Empty;
    }
}