using StarRailDamage.Source.Model.DataStruct.Formula.Abstraction;

namespace StarRailDamage.Source.Model.DataStruct.Formula.Method
{
    public interface IMathFormulaMethod : IFormulaMethod
    {
        double Method(double left, double right);
    }
}