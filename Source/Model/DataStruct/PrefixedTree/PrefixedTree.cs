using StarRailDamage.Source.Extension;
using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Model.DataStruct.PrefixedTree
{
    public class PrefixedTree<TKey, TValue> where TKey : notnull
    {
        private PrefixedTreeNode<TKey, TValue> Node { get; } = new();

        public void Add(IEnumerable<TKey> keys, TValue value)
        {
            PrefixedTreeNode<TKey, TValue> currentNode = Node;
            foreach (TKey key in keys)
            {
                currentNode = currentNode.GetOrAddNode(key);
            }
            currentNode.Value = value;
        }

        public PrefixedTreeNode<TKey, TValue> GetNode() => Node;

        public bool TryGetValue(IEnumerable<TKey> keys, [NotNullWhen(true)] out TValue? value)
        {
            PrefixedTreeNode<TKey, TValue> CurrentNode = Node;
            foreach (TKey key in keys)
            {
                if (CurrentNode.IsNull() || !CurrentNode.TryGetNode(key, out PrefixedTreeNode<TKey, TValue>? ChildNode))
                {
                    return false.Configure(value = default);
                }
                CurrentNode = ChildNode;
            }
            return CurrentNode.TryGetValue(out value);
        }
    }
}