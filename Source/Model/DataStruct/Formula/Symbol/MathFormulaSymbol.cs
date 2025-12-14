using StarRailDamage.Source.Model.DataStruct.Formula.Method;

namespace StarRailDamage.Source.Model.DataStruct.Formula.Symbol
{
    public class MathFormulaSymbol : FormulaSymbol
    {
        public static readonly IMathFormulaMethod DefaultMethod = new MathFormulaMethod.DefaultMethod();

        public Func<double, double, double> Method { get; init; }

        public MathFormulaSymbol(int symbolRank, string symbol) : base(symbolRank, symbol)
        {
            Method = DefaultMethod.Method;
        }

        public MathFormulaSymbol(int symbolRank, string symbol, Func<double, double, double> method) : base(symbolRank, symbol)
        {
            Method = method;
        }
    }
}