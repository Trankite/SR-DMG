using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.Web.Hoyolab.Passport.QRLogin.Status
{
    public static class QRLoginStatusRequestBuilderFactoryExtension
    {
        public static QRLoginStatusRequestBuilderFactory SetTicket(this QRLoginStatusRequestBuilderFactory builder, string value)
        {
            return builder.Configure(builder.Ticket = value);
        }
    }
}