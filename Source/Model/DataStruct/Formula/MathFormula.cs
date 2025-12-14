using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.DataStruct.Formula.Abstraction;

namespace StarRailDamage.Source.Model.DataStruct.Formula
{
    public class MathFormula : Formula
    {
        public MathFormula() { }

        public MathFormula(string value)
        {
            Value = value;
        }

        public MathFormula(Formula? leftFormula, IFormulaSymbol formulaSymbol, Formula? rightFormula)
        {
            Left = leftFormula;
            Symbol = formulaSymbol;
            Right = rightFormula;
        }

        public override string ToString()
        {
            return $"{(Left.IsNotNull() ? (Left.Symbol.Rank < Symbol.Rank ? $"( {Left} )" : Left) : Value)}{(Right.IsNotNull() ? $" {Symbol.Text} {(Symbol.Rank > Right.Symbol.Rank || Symbol.Text is "-" && Right.Symbol.Text is "+" or "-" ? $"({Right})" : Right)}" : string.Empty)}";
        }
    }
}