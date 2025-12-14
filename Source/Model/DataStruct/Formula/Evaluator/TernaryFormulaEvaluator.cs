using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Factory.PropertyExpression;
using StarRailDamage.Source.Model.DataStruct.Formula.Abstraction;
using StarRailDamage.Source.Model.DataStruct.Formula.Symbol;
using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Model.DataStruct.Formula.Evaluator
{
    public class TernaryFormulaEvaluator : IFormulaEvaluator
    {
        public bool ReadOnly { get; set; }

        public Dictionary<string, IPropertyExpression<double>>? Source { get; set; }

        public TernaryFormulaEvaluator() { }

        public TernaryFormulaEvaluator(Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
        {
            Source = source;
            ReadOnly = readOnly;
        }

        public double GetValue(Formula? formula) => GetValue(formula, Source, ReadOnly);

        public double GetValue(Formula? formula, bool readOnly) => GetValue(formula, Source, readOnly);

        public static double GetValue(Formula? formula, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
        {
            if (formula.IsNull()) return double.NaN;
            if (string.IsNullOrEmpty(formula.Symbol.Text))
            {
                return GetDefaultValue(formula, source);
            }
            TernaryFormulaSymbol TernaryFormulaSymbol = (TernaryFormulaSymbol)formula.Symbol;
            if (formula.Symbol.Text.EndsWith('('))
            {
                return TernaryFormulaSymbol.Method(GetParameters(formula.Right, GetParameters(formula.Left, [])), source, readOnly);
            }
            else
            {
                return TernaryFormulaSymbol.Method([formula], source, readOnly);
            }
        }

        private static List<Formula> GetParameters(Formula? formula, List<Formula> collection)
        {
            return formula.IsNotNull() && formula.Symbol.Text == "," ? AppendParameter(formula.Right, AppendParameter(formula.Left, collection)) : collection;
        }

        private static List<Formula> AppendParameter(Formula? formula, List<Formula> collection)
        {
            if (formula.IsNull()) return collection;
            if (formula.Symbol.Text == ",")
            {
                return GetParameters(formula, collection);
            }
            else collection.Add(formula);
            return collection;
        }

        private static double GetDefaultValue(Formula formula, Dictionary<string, IPropertyExpression<double>>? source)
        {
            if (string.IsNullOrEmpty(formula.Value)) return double.NaN;
            if (double.TryParse(formula.Value, out double TempValue) || TryGetProperty(formula, source, out TempValue))
            {
                return formula.Value.EndsWith('%') ? TempValue / 100 : TempValue;
            }
            return double.NaN;
        }

        private static bool TryGetProperty(Formula? formula, Dictionary<string, IPropertyExpression<double>>? source, [NotNullWhen(true)] out double result)
        {
            return TryGetPropertyExpression(formula, source, out IPropertyExpression<double>? PropertyExpression) && PropertyExpression.TryGetValue(out result) || false.Configure(result = default);
        }

        public static bool TrySetValue(Formula? formula, double value, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
        {
            return readOnly || TryGetPropertyExpression(formula, source, out IPropertyExpression<double>? PropertyExpression) && PropertyExpression.TrySetValue(value);
        }

        private static bool TryGetPropertyExpression(Formula? formula, Dictionary<string, IPropertyExpression<double>>? source, [NotNullWhen(true)] out IPropertyExpression<double>? propertyExpression)
        {
            if (source.IsNotNull() && formula.IsNotNull() && !string.IsNullOrEmpty(formula.Value))
            {
                return source.TryGetValue(formula.Value.Trim('[', ']', '%'), out propertyExpression);
            }
            return false.Configure(propertyExpression = default);
        }
    }
}