using StarRailDamage.Source.Factory.PropertyExpression;

namespace StarRailDamage.Source.Model.Formula
{
    public class TernaryFormula<T>
    {
        public TernaryFormulaNode? RootNode;

        public Dictionary<string, IPropertyExpression<T, double>>? Source;

        public TernaryFormula() { }

        public TernaryFormula(TernaryFormulaNode? ternaryFormulaNode)
        {
            RootNode = ternaryFormulaNode;
        }

        public override string ToString()
        {
            return RootNode?.ToString() ?? string.Empty;
        }
    }
}