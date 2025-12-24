using StarRailDamage.Source.Model.DataStruct.Formula.Method;

namespace StarRailDamage.Source.Model.DataStruct.Formula.Symbol
{
    public class MathFormulaSymbol : FormulaSymbol
    {
        public static readonly IMathFormulaMethod DefaultMethod = new MathFormulaMethod.DefaultMethod();

        public IMathFormulaMethod SymbolMethod { get; }

        public override string Text => SymbolMethod.Symbol;

        public MathFormulaSymbol(int symbolRank) : this(symbolRank, DefaultMethod) { }

        public MathFormulaSymbol(int symbolRank, IMathFormulaMethod symbolMethod) : base(symbolRank)
        {
            Rank = symbolRank;
            SymbolMethod = symbolMethod;
        }
    }
}