using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Sign
{
    public static class SignRequestBuilderFactoryExtension
    {
        public static SignRequestBuilderFactory SetBody(this SignRequestBuilderFactory builder, SignRequestBody body)
        {
            return builder.Configure(builder.Body = body);
        }
    }
}