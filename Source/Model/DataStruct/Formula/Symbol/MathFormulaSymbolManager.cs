using StarRailDamage.Source.Model.DataStruct.Formula.Abstraction;
using StarRailDamage.Source.Model.DataStruct.Formula.Method;
using StarRailDamage.Source.Model.DataStruct.PrefixedTree;

namespace StarRailDamage.Source.Model.DataStruct.Formula.Symbol
{
    public class MathFormulaSymbolManager : FormulaSymbolManager
    {
        private static readonly PrefixedTree<char, IFormulaSymbol> SymbolTree = new();

        protected override PrefixedTree<char, IFormulaSymbol> GetSymbolTree() => SymbolTree;

        private static void AppendSymbol(int rank, IMathFormulaMethod method)
        {
            SymbolTree.Add(method.Symbol.ToCharArray(), new MathFormulaSymbol(rank, method.Symbol, method.Method));
        }

        static MathFormulaSymbolManager()
        {
            AppendSymbol(0, new MathFormulaMethod.BeginMethod());
            AppendSymbol(0, new MathFormulaMethod.EndedMethod());
            AppendSymbol(1, new MathFormulaMethod.AddMethod());
            AppendSymbol(1, new MathFormulaMethod.SubtractMethod());
            AppendSymbol(2, new MathFormulaMethod.MultiplyMethod());
            AppendSymbol(2, new MathFormulaMethod.DivideMethod());
            AppendSymbol(2, new MathFormulaMethod.ModuloMethod());
            AppendSymbol(3, new MathFormulaMethod.PowerMethod());
        }
    }
}