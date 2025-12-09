using StarRailDamage.Source.Factory.PropertyExpression;

namespace StarRailDamage.Source.Model.DataStruct.Formula
{
    public class MathFormula : TernaryFormula<object>
    {
        public override object? Sender { set => base.Sender = null; }

        public override IEnumerable<Dictionary<string, IPropertyExpression<object, double>>>? Source { set => base.Source = null; }

        public override bool ReadOnly { set => base.ReadOnly = true; }

        public MathFormula() => base.ReadOnly = true;

        public MathFormula(string formula) : this() => SetFormula(formula);

        public MathFormula(FormulaNode? formulaNode) : this() => SetFormula(formulaNode);
    }
}