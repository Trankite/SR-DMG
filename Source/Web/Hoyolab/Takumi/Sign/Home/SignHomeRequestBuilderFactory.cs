using StarRailDamage.Source.Web.Hoyolab.Builder;
using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder;
using StarRailDamage.Source.Web.Request.Builder.Abstraction;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Sign.Home
{
    public class SignHomeRequestBuilderFactory : IHttpRequestMessageBuilderFactory
    {
        private const string URL = "https://api-takumi.mihoyo.com/event/luna/hkrpg/home";

        public string Language { get; set; } = string.Empty;

        public string ActionId { get; set; } = string.Empty;

        public SignHomeRequestBuilderFactory() { }

        public HttpRequestMessageBuilder Create()
        {
            return new HoyolabHttpRequestMessageBuilder().SetRequestUri(new HoyolabHttpUriBuilder(URL).SetLanguage(Language).SetActionId(ActionId).Uri);
        }
    }
}