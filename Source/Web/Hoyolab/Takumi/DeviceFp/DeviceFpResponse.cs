using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Web.Response;
using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.DeviceFp
{
    public class DeviceFpResponse : ResponseWrapper<DeviceFpResponseWrapper>
    {
        public bool TryGetDeviceFp([NotNullWhen(true)] out string? deviceFp)
        {
            return Content.IsNotNull() && !string.IsNullOrEmpty(Content.DeviceFp) ? true.Configure(deviceFp = Content.DeviceFp) : false.Configure(deviceFp = default);
        }
    }
}