using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.DataStruct.Formula.Abstraction;
using StarRailDamage.Source.Model.DataStruct.Formula.Evaluator;
using StarRailDamage.Source.Model.DataStruct.Formula.Factory;

namespace StarRailDamage.Source.Model.DataStruct.Formula
{
    public class MathFormulaManager : IFormulaManager
    {
        private static readonly MathFormulaFactory FormulaFactory = new();

        public Formula? Parse(string? formula) => FormulaParse(formula);

        public double GetValue(Formula? formula) => MathFormulaEvaluator.GetValue(formula);

        public static Formula? FormulaParse(string? formula) => FormulaFactory.Parse(formula);

        List<string> IFormulaManager.GetStep(Formula formula) => GetStep(formula);

        public static List<string> GetStep(Formula formula) => GetStep(formula, []);

        public static List<string> GetStep(Formula? formula, List<string> collection)
        {
            if (formula.IsNull()) return collection;
            if (!string.IsNullOrEmpty(formula.Symbol.Text))
            {
                GetStep(formula.Left, collection);
                GetStep(formula.Right, collection);
                collection.Add(formula.ToString());
            }
            return collection;
        }
    }
}