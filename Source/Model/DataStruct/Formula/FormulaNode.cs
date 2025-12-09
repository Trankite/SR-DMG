using StarRailDamage.Source.Service.Formula.Symbol;

namespace StarRailDamage.Source.Model.DataStruct.Formula
{
    public class FormulaNode
    {
        public FormulaNode? Left { get; set; }

        public FormulaNode? Right { get; set; }

        public FormulaSymbol Symbol { get; set; }

        public string? Text { get; set; }

        public FormulaNode() { }

        public FormulaNode(string? text)
        {
            Text = text;
        }

        public FormulaNode(FormulaNode? right, FormulaSymbol symbol, FormulaNode? left)
        {
            Right = right;
            Symbol = symbol;
            Left = left;
        }

        public override string ToString()
        {
            return (Symbol & FormulaSymbol.Function) == FormulaSymbol.Function ? $"{Symbol.Symbol()} ( {Left} ) {Right}" : $"{(Left is null ? Text : $"{(Left.Symbol != FormulaSymbol.None && Left.Symbol.Precedence() < Symbol.Precedence() ? $"( {Left} )" : Left)} ")}{Symbol.Symbol()}{(Right is null ? string.Empty : $" {(Right.Symbol != FormulaSymbol.None && (Symbol.Precedence() > Right.Symbol.Precedence() || (Symbol == FormulaSymbol.Subtraction && (Right.Symbol & (FormulaSymbol.Addition | FormulaSymbol.Subtraction)) != FormulaSymbol.None) || (Symbol & Right.Symbol & FormulaSymbol.Binding) == FormulaSymbol.Binding) ? $"( {Right} )" : Right)}")}";
        }
    }
}