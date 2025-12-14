namespace StarRailDamage.Source.Model.DataStruct.Formula.Abstraction
{
    public interface IFormulaEvaluator
    {
        double GetValue(Formula? formula);
    }
}