using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Web.Response;
using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.DeviceFp
{
    public class DeviceFpResponse : ResponseWrapper<DeviceFpResponseWrapper>
    {
        public bool TryGetDeviceFp([NotNullWhen(true)] out string? deviceFp)
        {
            return Data.IsNotNull() && !string.IsNullOrEmpty(Data.DeviceFp) ? true.Configure(deviceFp = Data.DeviceFp) : false.Configure(deviceFp = default);
        }
    }
}