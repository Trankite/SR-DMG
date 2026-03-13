using StarRailDamage.Source.Web.Hoyolab.DataSign;
using System.Collections.Frozen;

namespace StarRailDamage.Source.Web.Hoyolab
{
    public static class HoyolabOptions
    {
        public const string Version = "2.71.1";

        public const string HoyolabId = "bll8iq97cem8";

        public static FrozenDictionary<SaltType, string> Salts { get; }

        static HoyolabOptions()
        {
            Salts = FrozenDictionary.Create([
                KeyValuePair.Create(SaltType.K2,"rtvTthKxEyreVXQCnhluFgLXPOFKPHlA"),
                KeyValuePair.Create(SaltType.LK2,"EJncUPGnOHajenjLhBOsdpwEMZmiCmQX"),
                KeyValuePair.Create(SaltType.X4,"xV8v4Qu54lUKrEYFZkJhB8cuOh9Asafs"),
                KeyValuePair.Create(SaltType.X6,"t0qEgfub6cvueAPgR5m9aQWWVciEer7v"),
                KeyValuePair.Create(SaltType.PROD,"JwYDpKvLj6MrMqqYU6jTKF17KNO2PXoS")
                ]);
        }
    }
}