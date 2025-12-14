using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Factory.PropertyExpression;
using StarRailDamage.Source.Model.DataStruct.Formula.Abstraction;
using StarRailDamage.Source.Model.DataStruct.Formula.Evaluator;
using StarRailDamage.Source.Model.DataStruct.Formula.Factory;

namespace StarRailDamage.Source.Model.DataStruct.Formula
{
    public class TernaryFormulaManager : IFormulaManager
    {
        private static readonly TernaryFormulaFactory FormulaFactory = new();

        public TernaryFormulaEvaluator FormulaEvaluator { get; set; }

        public TernaryFormulaManager()
        {
            FormulaEvaluator = new TernaryFormulaEvaluator();
        }

        public TernaryFormulaManager(Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
        {
            FormulaEvaluator = new TernaryFormulaEvaluator(source, readOnly);
        }

        public double GetValue(Formula? formula) => FormulaEvaluator.GetValue(formula);

        public Formula? Parse(string? formula) => FormulaParse(formula);

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
                if (!formula.Symbol.Text.EndsWith(','))
                {
                    collection.Add(formula.ToString());
                }
            }
            return collection;
        }

        public static List<Formula> GetBinding(Formula? formulaNode) => GetBinding(formulaNode, []);

        public static List<Formula> GetBinding(Formula? formulaNode, List<Formula> collection)
        {
            if (formulaNode is null) return collection;
            GetBinding(formulaNode.Left, collection);
            GetBinding(formulaNode.Right, collection);
            if (formulaNode.Symbol.Rank == 1)
            {
                collection.Add(formulaNode);
            }
            return collection;
        }
    }
}