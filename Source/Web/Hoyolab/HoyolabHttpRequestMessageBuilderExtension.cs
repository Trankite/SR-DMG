using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Web.Hoyolab.DataSign;
using StarRailDamage.Source.Web.Request.Builder;

namespace StarRailDamage.Source.Web.Hoyolab
{
    public static class HoyolabHttpRequestMessageBuilderExtension
    {
        public static HoyolabHttpRequestMessageBuilder SetXrpcAppId(this HoyolabHttpRequestMessageBuilder builder, string value)
        {
            return builder.SetHeader("x-rpc-app_id", value);
        }

        public static HoyolabHttpRequestMessageBuilder SetXrpcAppVersion(this HoyolabHttpRequestMessageBuilder builder, string value)
        {
            return builder.SetHeader("x-rpc-app_version", value);
        }

        public static HoyolabHttpRequestMessageBuilder SetXrpcDeviceId(this HoyolabHttpRequestMessageBuilder builder, string value)
        {
            return builder.SetHeader("x-rpc-device_id", value);
        }

        public static HoyolabHttpRequestMessageBuilder SetXrpcClientType(this HoyolabHttpRequestMessageBuilder builder, ClientType clientType)
        {
            return builder.SetHeader("x-rpc-client_type", Convert.ToInt32(clientType).ToString());
        }

        public static HoyolabHttpRequestMessageBuilder SetXrpcDeviceFp(this HoyolabHttpRequestMessageBuilder builder, string value)
        {
            return builder.SetHeader("x-rpc-device_fp", value);
        }

        public static HoyolabHttpRequestMessageBuilder SetDataSign(this HoyolabHttpRequestMessageBuilder builder, DataSignOptions dataSignOptions)
        {
            return builder.SetHeader("DS", dataSignOptions.GetDataSign());
        }

        public static HoyolabHttpRequestMessageBuilder SetDataSignWithBody(this HoyolabHttpRequestMessageBuilder builder, DataSignOptions dataSignOptions, string? body)
        {
            return builder.SetDataSign(dataSignOptions.SetDataSignBody(body));
        }

        public static HoyolabHttpRequestMessageBuilder SetDataSignWithQuery(this HoyolabHttpRequestMessageBuilder builder, DataSignOptions dataSignOptions)
        {
            return builder.SetDataSign(dataSignOptions.SetDataSignQuery(builder));
        }

        public static HoyolabHttpRequestMessageBuilder SetDataSignWithBodyAndQuery(this HoyolabHttpRequestMessageBuilder builder, DataSignOptions dataSignOptions, string? body)
        {
            return builder.SetDataSign(dataSignOptions.SetDataSignBody(body).SetDataSignQuery(builder));
        }

        private static DataSignOptions SetDataSignBody(this DataSignOptions dataSignOptions, string? body)
        {
            return dataSignOptions.Configure(dataSignOptions.Body = body);
        }

        private static DataSignOptions SetDataSignQuery(this DataSignOptions dataSignOptions, HoyolabHttpRequestMessageBuilder builder)
        {
            return dataSignOptions.Configure(dataSignOptions.Query = builder.RequestUri?.Query);
        }
    }
}