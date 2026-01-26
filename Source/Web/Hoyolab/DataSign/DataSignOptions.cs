using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.Web.Hoyolab.DataSign
{
    public class DataSignOptions
    {
        public DataSignOptions(SaltType type, bool includeChars, DataSignAlgorithm algorithm)
        {
            Salt = HoyolabOptions.Salts[type];
            Factor = includeChars ? Random.Shared.GetLowerAndNumberString(6) : GetRandomNumberString();
            Algorithm = algorithm;
        }

        public string Salt { get; }

        public string Factor { get; }

        public DataSignAlgorithm Algorithm { get; }

        public string? Body { get; set; }

        public string? Query
        {
            get => field;
            set => field = value.IsNotNull() ? SortQuery(value) : value;
        }

        private static string GetRandomNumberString()
        {
            return Random.Shared.Next(100001, 200000).ToString();
        }

        private static string SortQuery(string query)
        {
            string[] queries = Uri.UnescapeDataString(query).Split('?', 2);
            return queries.Length >= 2 ? string.Join('&', queries[1].Split('&').OrderBy(x => x)) : string.Empty;
        }
    }
}