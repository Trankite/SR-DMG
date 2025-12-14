using StarRailDamage.Source.Model.DataStruct.Formula.Abstraction;
using StarRailDamage.Source.Model.DataStruct.Formula.Method;
using StarRailDamage.Source.Model.DataStruct.PrefixedTree;
using static StarRailDamage.Source.Model.DataStruct.Formula.Method.TernaryFormulaMethod;

namespace StarRailDamage.Source.Model.DataStruct.Formula.Symbol
{
    public class TernaryFormulaSymbolManager : FormulaSymbolManager
    {
        private static readonly PrefixedTree<char, IFormulaSymbol> SymbolTree = new();

        protected override PrefixedTree<char, IFormulaSymbol> GetSymbolTree() => SymbolTree;

        private static void AppendSymbol(int rank, ITernaryFormulaMethod method)
        {
            SymbolTree.Add(method.Symbol.ToCharArray(), new TernaryFormulaSymbol(rank, method.Symbol, method.Method));
        }

        static TernaryFormulaSymbolManager()
        {
            AppendSymbol(0, new BeginMethod());
            AppendSymbol(0, new EndedMethod());
            AppendSymbol(0, new SeparatorMethod());
            AppendSymbol(1, new BindingMethod());
            AppendSymbol(1, new AddWithBindingMethod());
            AppendSymbol(1, new SubtractWithBindingMethod());
            AppendSymbol(1, new MultiplyWithBindingMethod());
            AppendSymbol(1, new DivideWithBindingMethod());
            AppendSymbol(2, new IfMethod());
            AppendSymbol(2, new ElseMethod());
            AppendSymbol(3, new OrMethod());
            AppendSymbol(3, new OrNotMethod());
            AppendSymbol(4, new AndMethod());
            AppendSymbol(4, new AndNotMethod());
            AppendSymbol(5, new MoreMethod());
            AppendSymbol(5, new MoreOrEqualMethod());
            AppendSymbol(5, new EqualMethod());
            AppendSymbol(5, new NotEqualMethod());
            AppendSymbol(5, new LessMethod());
            AppendSymbol(5, new LessOrEqualMethod());
            AppendSymbol(6, new AddMethod());
            AppendSymbol(6, new SubtractMethod());
            AppendSymbol(7, new MultiplyMethod());
            AppendSymbol(7, new DivideMethod());
            AppendSymbol(8, new PowerMethod());
            AppendSymbol(9, new NotMethod());
            AppendSymbol(9, new ModuloMethod());
            AppendSymbol(9, new MaximumMethod());
            AppendSymbol(9, new MinimumMethod());
            AppendSymbol(9, new SwitchMethod());
        }
    }
}