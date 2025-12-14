using StarRailDamage.Source.Model.DataStruct.Formula.Abstraction;
using StarRailDamage.Source.Model.DataStruct.Formula.Symbol;

namespace StarRailDamage.Source.Model.DataStruct.Formula.Factory
{
    public class MathFormulaFactory : FormulaFactory
    {
        private static readonly MathFormulaSymbolManager FormulaSymbolManager = new();

        protected override IFormulaSymbolManager SymbolManager => FormulaSymbolManager;

        protected override Formula GetFormula(string value) => new MathFormula(value);

        protected override Formula GetFormula(Formula? rightFormula, IFormulaSymbol formulaSymbol, Formula? leftFormula) => new MathFormula(leftFormula, formulaSymbol, rightFormula);

        protected override bool IsBeginSymbol(IFormulaSymbol formulaSymbol) => formulaSymbol.Text.EndsWith('(');

        protected override bool IsEndedSymbol(IFormulaSymbol formulaSymbol) => formulaSymbol.Text.EndsWith(')');
    }
}