using StarRailDamage.Source.Service.Formula.Symbol;

namespace StarRailDamage.Source.Model.Formula
{
    public class TernaryFormulaNode
    {
        public TernaryFormulaNode? Left { get; set; }

        public TernaryFormulaNode? Right { get; set; }

        public FormulaSymbol Symbol { get; set; }

        public string? Text { get; set; }

        public TernaryFormulaNode() { }

        public TernaryFormulaNode(string? text)
        {
            Text = text;
        }

        public TernaryFormulaNode(TernaryFormulaNode? right, FormulaSymbol symbol, TernaryFormulaNode? left)
        {
            Right = right;
            Symbol = symbol;
            Left = left;
        }

        public override string ToString()
        {
            return (Symbol & FormulaSymbol.Function) > FormulaSymbol.None ? $"{Symbol.Symbol()} ( {Left} ) {Right}" : $"{(Left == null ? Text : $"{(Left.Symbol != FormulaSymbol.None && Left.Symbol.Precedence() < Symbol.Precedence() ? $"( {Left} )" : Left)} ")}{Symbol.Symbol()}{(Right == null ? string.Empty : $" {(Right.Symbol != FormulaSymbol.None && (Symbol.Precedence() > Right.Symbol.Precedence() || (Symbol == FormulaSymbol.Subtraction && (Right.Symbol & (FormulaSymbol.Addition | FormulaSymbol.Subtraction)) != FormulaSymbol.None)) ? $"( {Right} )" : Right)}")}";
        }
    }
}