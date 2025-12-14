using StarRailDamage.Source.Factory.PropertyExpression;
using StarRailDamage.Source.Model.DataStruct.Formula.Abstraction;

namespace StarRailDamage.Source.Model.DataStruct.Formula.Method
{
    public interface ITernaryFormulaMethod : IFormulaMethod
    {
        double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly);
    }
}