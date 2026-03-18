using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.Web.Hoyolab.DataSign
{
    public class DataSignOptions
    {
        public string Salt { get; }

        public string RandomString { get; }

        public DataSignAlgorithm Algorithm { get; }

        public string? Body { get; set; }

        public string? Query
        {
            get => field;
            set => field = value.IsNotNull() ? SortQuery(value) : value;
        }

        private DataSignOptions(string salt, DataSignAlgorithm algorithm, string randomString)
        {
            Salt = salt;
            Algorithm = algorithm;
            RandomString = randomString;
        }

        public static DataSignOptions Create(SaltType type, DataSignAlgorithm algorithm)
        {
            return new DataSignOptions(HoyolabOptions.Salts[type], algorithm, algorithm >= DataSignAlgorithm.Gen2 ? Random.Shared.GetLowerAndNumberString(6) : GetRandomNumberString());
        }

        private static string GetRandomNumberString()
        {
            return Random.Shared.Next(100001, 200000).ToString();
        }

        private static string SortQuery(string query)
        {
            string[] Queries = Uri.UnescapeDataString(query).Split('?', 2);
            return Queries.Length >= 2 ? string.Join('&', Queries[1].Split('&').Order()) : string.Empty;
        }
    }
}