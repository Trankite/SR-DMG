using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.DataStruct.Formula.Abstraction;
using StarRailDamage.Source.Model.DataStruct.PrefixedTree;

namespace StarRailDamage.Source.Model.DataStruct.Formula.Symbol
{
    public abstract class FormulaSymbolManager : IFormulaSymbolManager
    {
        protected abstract PrefixedTree<char, IFormulaSymbol> GetSymbolTree();

        IFormulaSymbol? IFormulaSymbolManager.NextSymbol(string formula, ref int index) => GetNextSymbol(formula, ref index);

        public IFormulaSymbol? GetNextSymbol(string formula, ref int index)
        {
            PrefixedTreeNode<char, IFormulaSymbol> Node = GetSymbolTree().GetNode();
            while (index < formula.Length)
            {
                if (!Node.TryGetNextNode(formula[index++], out Node) && true.Configure(index--)) break;
            }
            return Node.GetValueOrDefault();
        }
    }
}