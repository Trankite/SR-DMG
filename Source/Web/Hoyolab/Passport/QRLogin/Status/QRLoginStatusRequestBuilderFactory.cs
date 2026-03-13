using StarRailDamage.Source.Web.Hoyolab.Builder;
using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder;
using System.Net.Http;
using System.Text.Json;

namespace StarRailDamage.Source.Web.Hoyolab.Passport.QRLogin.Status
{
    public class QRLoginStatusRequestBuilderFactory : HoyolabHttpRequestMessageBuilderFactory
    {
        private const string URL = "https://passport-api.mihoyo.com/account/ma-cn-passport/app/queryQRLoginStatus";

        public string Ticket { get; set; } = string.Empty;

        public QRLoginStatusRequestBuilderFactory() { }

        public QRLoginStatusRequestBuilderFactory(HoyolabToken hoyolabToken) : base(hoyolabToken) { }

        public override HttpRequestMessageBuilder Create()
        {
            return new HoyolabHttpRequestMessageBuilder()
                .SetRequestUri(URL)
                .SetMethod(HttpMethod.Post)
                .SetStringContent(JsonSerializer.Serialize(new QRLoginStatusRequestBody(Ticket)))
                .SetXrpcAppId(HoyolabOptions.HoyolabId)
                .SetXrpcDeviceId(HoyolabToken.Guid);
        }
    }
}