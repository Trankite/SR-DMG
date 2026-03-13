using StarRailDamage.Source.Web.Hoyolab.Builder;
using StarRailDamage.Source.Web.Request;
using StarRailDamage.Source.Web.Request.Builder;
using System.Net.Http;

namespace StarRailDamage.Source.Web.Hoyolab.Passport.QRLogin
{
    public class QRLoginRequestBuilderFactory : HoyolabHttpRequestMessageBuilderFactory
    {
        private static readonly string URL = "https://passport-api.mihoyo.com/account/ma-cn-passport/app/createQRLogin";

        public QRLoginRequestBuilderFactory() { }

        public QRLoginRequestBuilderFactory(HoyolabToken hoyolabToken) : base(hoyolabToken) { }

        public override HttpRequestMessageBuilder Create()
        {
            return new HoyolabHttpRequestMessageBuilder()
                .SetRequestUri(URL)
                .SetMethod(HttpMethod.Post)
                .SetXrpcAppId(HoyolabOptions.HoyolabId)
                .SetXrpcDeviceId(HoyolabToken.Guid);
        }
    }
}