using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Web.Response;

namespace StarRailDamage.Source.Web.Hoyolab.Passport.QRLogin.Status
{
    public class QRLoginStatusResponse : ResponseWrapper<QRLoginStatusResponseWrapper>
    {
        public QRLoginStatus GetStatus()
        {
            return Content.IsNotNull() && Enum.TryParse(Content.Status, out QRLoginStatus Status) ? Status : default;
        }
    }
}