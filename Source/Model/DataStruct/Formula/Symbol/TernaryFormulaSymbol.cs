using StarRailDamage.Source.Model.DataStruct.Formula.Method;

namespace StarRailDamage.Source.Model.DataStruct.Formula.Symbol
{
    public class TernaryFormulaSymbol : FormulaSymbol
    {
        public static readonly ITernaryFormulaMethod DefaultMethod = new TernaryFormulaMethod.DefaultMethod();

        public ITernaryFormulaMethod SymbolMethod { get; }

        public override string Text => SymbolMethod.Symbol;

        public TernaryFormulaSymbol(int symbolRank) : this(symbolRank, DefaultMethod) { }

        public TernaryFormulaSymbol(int symbolRank, ITernaryFormulaMethod symbolMethod) : base(symbolRank)
        {
            Rank = symbolRank;
            SymbolMethod = symbolMethod;
        }
    }
}