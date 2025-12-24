using StarRailDamage.Source.Model.DataStruct.Formula.Abstraction;
using StarRailDamage.Source.Model.DataStruct.Formula.Method;
using StarRailDamage.Source.Model.DataStruct.PrefixedTree;
using static StarRailDamage.Source.Model.DataStruct.Formula.Method.MathFormulaMethod;

namespace StarRailDamage.Source.Model.DataStruct.Formula.Symbol
{
    public class MathFormulaSymbolManager : FormulaSymbolManager
    {
        private static readonly PrefixedTree<char, IFormulaSymbol> SymbolTree = new();

        protected override PrefixedTree<char, IFormulaSymbol> GetSymbolTree() => SymbolTree;

        private static void AppendSymbol(int rank, IMathFormulaMethod method)
        {
            SymbolTree.Add(method.Symbol.ToCharArray(), new MathFormulaSymbol(rank, method));
        }

        static MathFormulaSymbolManager()
        {
            AppendSymbol(0, new BeginMethod());
            AppendSymbol(0, new EndedMethod());
            AppendSymbol(1, new AddMethod());
            AppendSymbol(1, new SubtractMethod());
            AppendSymbol(2, new MultiplyMethod());
            AppendSymbol(2, new DivideMethod());
            AppendSymbol(2, new ModuloMethod());
            AppendSymbol(3, new PowerMethod());
        }
    }
}