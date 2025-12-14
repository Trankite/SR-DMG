using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.DataStruct.Formula.Abstraction;
using StarRailDamage.Source.Model.DataStruct.Formula.Symbol;

namespace StarRailDamage.Source.Model.DataStruct.Formula
{
    public abstract class Formula
    {
        public Formula? Left { get; set; }

        public Formula? Right { get; set; }

        public IFormulaSymbol Symbol { get; set; } = FormulaSymbol.DefaultSymbol;

        public string? Value { get; set; }

        public override string ToString()
        {
            return $"{(Left.IsNotNull() ? (Left.Symbol.Rank < Symbol.Rank ? $"( {Left} )" : Left) : Value)}{(Right.IsNotNull() ? $" {Symbol.Text} {(Symbol.Rank > Right.Symbol.Rank ? $"({Right})" : Right)}" : string.Empty)}";
        }
    }
}