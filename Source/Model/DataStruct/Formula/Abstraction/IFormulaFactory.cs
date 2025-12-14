namespace StarRailDamage.Source.Model.DataStruct.Formula.Abstraction
{
    public interface IFormulaFactory
    {
        Formula? Parse(string? formula);
    }
}