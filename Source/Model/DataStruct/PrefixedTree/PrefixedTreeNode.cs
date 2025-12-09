using StarRailDamage.Source.Extension;
using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Model.DataStruct.PrefixedTree
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
            if (Node is not null)
            {
                return Node.TryGetValue(key, out childNode);
            }
            return false.Configure(childNode = null);
        }

        public bool TryGetValue([NotNullWhen(true)] out TValue? value)
        {
            return !Equals(value = Value, null);
        }
    }
}