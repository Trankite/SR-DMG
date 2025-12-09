using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Model.DataStruct.PrefixedTree
{
    public class PrefixedTree<TKey, TValue> where TKey : notnull
    {
        public PrefixedTreeNode<TKey, TValue> RootNode { get; } = new();

        public void Add(IEnumerable<TKey> keys, TValue value)
        {
            PrefixedTreeNode<TKey, TValue> currentNode = RootNode;
            foreach (TKey key in keys)
            {
                currentNode = currentNode.GetOrAddNode(key);
            }
            currentNode.Value = value;
        }

        public bool TryGetValue(IEnumerable<TKey> keys, [NotNullWhen(true)] out TValue? value)
        {
            PrefixedTreeNode<TKey, TValue> currentNode = RootNode;
            foreach (TKey key in keys)
            {
                if (currentNode is null || !currentNode.TryGetNode(key, out PrefixedTreeNode<TKey, TValue>? childNode))
                {
                    value = default;
                    return false;
                }
                currentNode = childNode;
            }
            return currentNode.TryGetValue(out value);
        }
    }
}