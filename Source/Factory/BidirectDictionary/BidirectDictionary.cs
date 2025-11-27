using StarRailDamage.Source.Extension;
using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Factory.BidirectDictionary
{
    public class BidirectDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TKey : notnull where TValue : notnull
    {
        private readonly Dictionary<TValue, TKey> _BidirectDictionary = [];

        public new TValue this[TKey key]
        {
            get => base[key];
            set => base[key] = value.With(_BidirectDictionary[value] = key);
        }

        public TKey this[TValue key]
        {
            get => _BidirectDictionary[key];
            set => _BidirectDictionary[key] = value.With(this[value] = key);
        }

        public bool TryGetValue(TValue key, [NotNullWhen(true)] out TKey? value)
        {
            return _BidirectDictionary.TryGetValue(key, out value);
        }

        public TKey? GetValueOrDefault(TValue key)
        {
            return _BidirectDictionary.GetValueOrDefault(key);
        }

        public bool ContainsKey(TValue key)
        {
            return _BidirectDictionary.ContainsKey(key);
        }

        public new bool ContainsValue(TValue key) => ContainsKey(key);
    }
}