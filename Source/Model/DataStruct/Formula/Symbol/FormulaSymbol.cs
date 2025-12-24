using StarRailDamage.Source.Model.DataStruct.Formula.Abstraction;

namespace StarRailDamage.Source.Model.DataStruct.Formula.Symbol
{
    public class FormulaSymbol : IFormulaSymbol
    {
        public int Rank { get; init; }

        public virtual string Text { get; } = string.Empty;

        public FormulaSymbol() { }

        public FormulaSymbol(int symbolRank)
        {
            Rank = symbolRank;
        }

        public static readonly IFormulaSymbol DefaultSymbol = new FormulaSymbol(int.MaxValue);

        public override string ToString() => Text;
    }
}