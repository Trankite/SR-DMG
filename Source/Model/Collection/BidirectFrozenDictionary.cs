using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Model.Collection
{
    public static class BidirectFrozenDictionary
    {
        public static BidirectFrozenDictionary<TKey, TValue> Create<TKey, TValue>(params KeyValuePair<TKey, TValue>[] source) where TKey : notnull where TValue : notnull
        {
            return new BidirectFrozenDictionary<TKey, TValue>(source.ToFrozenDictionary(), source.Select(Kvp => KeyValuePair.Create(Kvp.Value, Kvp.Key)).ToFrozenDictionary());
        }
    }

    public class BidirectFrozenDictionary<TKey, TValue> where TKey : notnull where TValue : notnull
    {
        private readonly FrozenDictionary<TKey, TValue> _FrozenDictionary;

        private readonly FrozenDictionary<TValue, TKey> _BidirectFrozenDictionary;

        public BidirectFrozenDictionary(FrozenDictionary<TKey, TValue> frozenDictionary, FrozenDictionary<TValue, TKey> bidirectFrozenDictionary)
        {
            _FrozenDictionary = frozenDictionary;
            _BidirectFrozenDictionary = bidirectFrozenDictionary;
        }

        public TValue this[TKey key]
        {
            get => _FrozenDictionary[key];
        }

        public TKey this[TValue key]
        {
            get => _BidirectFrozenDictionary[key];
        }

        public bool TryGetValue(TKey key, [NotNullWhen(true)] out TValue? value)
        {
            return _FrozenDictionary.TryGetValue(key, out value);
        }

        public bool TryGetValue(TValue key, [NotNullWhen(true)] out TKey? value)
        {
            return _BidirectFrozenDictionary.TryGetValue(key, out value);
        }

        public TValue? GetValueOrDefault(TKey key)
        {
            return _FrozenDictionary.GetValueOrDefault(key);
        }

        public TKey? GetValueOrDefault(TValue key)
        {
            return _BidirectFrozenDictionary.GetValueOrDefault(key);
        }

        public bool ContainsKey(TKey key)
        {
            return _FrozenDictionary.ContainsKey(key);
        }

        public bool ContainsKey(TValue key)
        {
            return _BidirectFrozenDictionary.ContainsKey(key);
        }

        public bool ContainsValue(TKey key) => ContainsKey(key);

        public bool ContainsValue(TValue key) => ContainsKey(key);
    }
}