namespace StarRailDamage.Source.Model.DataStruct.Formula.Abstraction
{
    public interface IFormulaSymbolManager
    {
        IFormulaSymbol? NextSymbol(string formula, ref int index);
    }
}