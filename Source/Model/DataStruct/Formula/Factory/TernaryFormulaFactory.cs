using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.DataStruct.Formula.Abstraction;
using StarRailDamage.Source.Model.DataStruct.Formula.Symbol;

namespace StarRailDamage.Source.Model.DataStruct.Formula.Factory
{
    public class TernaryFormulaFactory : FormulaFactory
    {
        private static readonly TernaryFormulaSymbolManager FormulaSymbolManager = new();

        protected override IFormulaSymbolManager SymbolManager => FormulaSymbolManager;

        protected override Formula GetFormula(string value) => new TernaryFormula(value);

        protected override Formula GetFormula(Formula? rightFromula, IFormulaSymbol formulaSymbol, Formula? leftFromula) => new TernaryFormula(leftFromula, formulaSymbol, rightFromula);

        protected override bool IsBeginSymbol(IFormulaSymbol formulaSymbol) => formulaSymbol.Text.EndsWith('(');

        protected override bool IsEndedSymbol(IFormulaSymbol formulaSymbol) => formulaSymbol.Text.EndsWith(')');

        protected override void OnPopBeginSymbol(IFormulaSymbol formulaSymbol, Stack<Formula?> formulaStack, Stack<IFormulaSymbol> formulaSymbolStack)
        {
            if (!formulaSymbol.Text.StartsWith('('))
            {
                formulaStack.Push(GetFormula(null, formulaSymbol, formulaStack.PopOrDefault()));
            }
        }
    }
}