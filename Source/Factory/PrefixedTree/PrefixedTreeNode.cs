using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Factory.PrefixedTree
{
    public class PrefixedTreeNode<TKey, TValue> where TKey : notnull
    {
        public TValue? Value;

        public Dictionary<TKey, PrefixedTreeNode<TKey, TValue>>? Node;

        public PrefixedTreeNode() { }

        public PrefixedTreeNode<TKey, TValue> GetOrAddNode(TKey key)
        {
            Node ??= [];
            if (!Node.TryGetValue(key, out PrefixedTreeNode<TKey, TValue>? childNode))
            {
                childNode = new PrefixedTreeNode<TKey, TValue>();
            }
            return Node[key] = childNode;
        }

        public bool TryGetNode(TKey key, [NotNullWhen(true)] out PrefixedTreeNode<TKey, TValue>? childNode)
        {
            if (Node != null)
            {
                return Node.TryGetValue(key, out childNode);
            }
            childNode = null;
            return false;
        }

        public bool TryGetValue([NotNullWhen(true)] out TValue? value)
        {
            return (value = Value) != null;
        }
    }
}