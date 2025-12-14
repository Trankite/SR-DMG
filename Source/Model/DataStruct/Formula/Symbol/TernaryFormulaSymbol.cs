using StarRailDamage.Source.Factory.PropertyExpression;
using StarRailDamage.Source.Model.DataStruct.Formula.Method;

namespace StarRailDamage.Source.Model.DataStruct.Formula.Symbol
{
    public class TernaryFormulaSymbol : FormulaSymbol
    {
        public static readonly ITernaryFormulaMethod DefaultMethod = new TernaryFormulaMethod.DefaultMethod();

        public Func<IList<Formula>, Dictionary<string, IPropertyExpression<double>>?, bool, double> Method { get; init; }

        public TernaryFormulaSymbol(int symbolRank, string symbol) : base(symbolRank, symbol)
        {
            Method = DefaultMethod.Method;
        }

        public TernaryFormulaSymbol(int symbolRank, string symbol, Func<IList<Formula>, Dictionary<string, IPropertyExpression<double>>?, bool, double> method) : base(symbolRank, symbol)
        {
            Method = method;
        }
    }
}