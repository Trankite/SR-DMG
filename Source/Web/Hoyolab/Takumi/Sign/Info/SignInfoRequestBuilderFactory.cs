using StarRailDamage.Source.Web.Hoyolab.Builder;
using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Sign.Info
{
    public class SignInfoRequestBuilderFactory : HoyolabHttpRequestMessageBuilderFactory
    {
        private const string URL = "https://api-takumi.mihoyo.com/event/luna/hkrpg/info";

        public string Language { get; set; } = string.Empty;

        public string ActionId { get; set; } = string.Empty;

        public string Server { get; set; } = string.Empty;

        public string Uid { get; set; } = string.Empty;

        public SignInfoRequestBuilderFactory() { }

        public SignInfoRequestBuilderFactory(HoyolabToken hoyolabToken) : base(hoyolabToken) { }

        public override HttpRequestMessageBuilder Create()
        {
            return new HoyolabHttpRequestMessageBuilder()
                .SetRequestUri(new HoyolabHttpUriBuilder(URL).SetLanguage(Language).SetActionId(ActionId).SetRegion(Server).SetUid(Uid))
                .SetHeader(new HoyolabCookieBuilder(HoyolabToken).SetAccountMid().SetCookieToken());
        }
    }
}