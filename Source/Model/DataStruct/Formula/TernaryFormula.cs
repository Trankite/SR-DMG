using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.DataStruct.Formula.Abstraction;

namespace StarRailDamage.Source.Model.DataStruct.Formula
{
    public class TernaryFormula : Formula
    {
        public TernaryFormula() { }

        public TernaryFormula(string value)
        {
            Value = value;
        }

        public TernaryFormula(Formula? leftFormula, IFormulaSymbol formulaSymbol, Formula? rightFormula)
        {
            Left = leftFormula;
            Symbol = formulaSymbol;
            Right = rightFormula;
        }

        public override string ToString()
        {
            return Symbol.Text.EndsWith('(') ? $"{Symbol.Text.TrimEnd('(')} ( {Left} )" : $"{(Left.IsNotNull() ? (Left.Symbol.Rank < Symbol.Rank ? $"( {Left} )" : Left) : Value)}{(Right.IsNotNull() ? $" {Symbol.Text} {(Symbol.Rank > Right.Symbol.Rank || Symbol.Text is "-" && Right.Symbol.Text is "+" or "-" || Symbol.Text.EndsWith('=') && Right.Symbol.Text.EndsWith('=') ? $"({Right})" : Right)}" : string.Empty)}";
        }
    }
}