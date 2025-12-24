using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.DataStruct.Formula.Abstraction;
using StarRailDamage.Source.Model.DataStruct.Formula.Symbol;

namespace StarRailDamage.Source.Model.DataStruct.Formula.Evaluator
{
    public class MathFormulaEvaluator : IFormulaEvaluator
    {
        double IFormulaEvaluator.GetValue(Formula? formula) => GetValue(formula);

        public static double GetValue(Formula? formula)
        {
            if (formula.IsNull()) return double.NaN;
            if (string.IsNullOrEmpty(formula.Symbol.Text))
            {
                return GetDefaultValue(formula);
            }
            MathFormulaSymbol FormulaSymbol = (MathFormulaSymbol)formula.Symbol;
            return FormulaSymbol.SymbolMethod.Method(GetValue(formula.Left), GetValue(formula.Right));
        }

        private static double GetDefaultValue(Formula formula)
        {
            return double.TryParse(formula.Value, out double TempValue) ? TempValue : double.NaN;
        }
    }
}