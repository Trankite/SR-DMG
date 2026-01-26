using StarRailDamage.Source.Web.Hoyolab.DataSign;

namespace StarRailDamage.Source.Web.Hoyolab
{
    public static class HoyolabOptions
    {
        public const string Version = "2.71.1";

        public const string HoyolabId = "bll8iq97cem8";

        public static readonly Dictionary<SaltType, string> Salts = [];

        static HoyolabOptions()
        {
            Salts[SaltType.K2] = "rtvTthKxEyreVXQCnhluFgLXPOFKPHlA";
            Salts[SaltType.LK2] = "EJncUPGnOHajenjLhBOsdpwEMZmiCmQX";
            Salts[SaltType.X4] = "xV8v4Qu54lUKrEYFZkJhB8cuOh9Asafs";
            Salts[SaltType.X6] = "t0qEgfub6cvueAPgR5m9aQWWVciEer7v";
            Salts[SaltType.PROD] = "JwYDpKvLj6MrMqqYU6jTKF17KNO2PXoS";
        }
    }
}