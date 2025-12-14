namespace StarRailDamage.Source.Model.DataStruct.Formula.Abstraction
{
    public interface IFormulaManager : IFormulaFactory, IFormulaEvaluator
    {
        List<string> GetStep(Formula formula);
    }
}