using StarRailDamage.Source.Factory.PrefixedTree;

namespace StarRailDamage.Source.Service.Formula.Symbol
{
    public static class FormulaSymbolExtension
    {
        private static readonly Dictionary<FormulaSymbol, int> SymbolPrecedence = [];

        private static readonly Dictionary<FormulaSymbol, string> SymbolText = [];

        private static readonly PrefixedTree<char, FormulaSymbol> SymbolTree = new();

        public static PrefixedTreeNode<char, FormulaSymbol> GetSymbolTree() => SymbolTree.RootNode;

        public static string Symbol(this FormulaSymbol value) => SymbolText.GetValueOrDefault(value) ?? string.Empty;

        public static int Precedence(this FormulaSymbol value) => SymbolPrecedence.GetValueOrDefault(value);

        private static void AddSymbolInfo(int precedence, FormulaSymbol formulaSymbol, string symbol)
        {
            SymbolText[formulaSymbol] = symbol;
            SymbolPrecedence[formulaSymbol] = precedence;
            if ((formulaSymbol & FormulaSymbol.Function) != FormulaSymbol.None)
            {
                SymbolTree.Add(symbol.ToCharArray().Concat(['(']), formulaSymbol | FormulaSymbol.Start);
            }
            else
            {
                SymbolTree.Add(symbol.ToCharArray(), formulaSymbol);
            }
        }

        static FormulaSymbolExtension()
        {
            AddSymbolInfo(0, FormulaSymbol.Start, "(");
            AddSymbolInfo(0, FormulaSymbol.End, ")");
            AddSymbolInfo(0, FormulaSymbol.Separator, ",");
            AddSymbolInfo(1, FormulaSymbol.Binding, "=");
            AddSymbolInfo(1, FormulaSymbol.AdditionAndBinding, "+=");
            AddSymbolInfo(1, FormulaSymbol.SubtractionAndBinding, "-=");
            AddSymbolInfo(1, FormulaSymbol.MultiplicationAndBinding, "*=");
            AddSymbolInfo(1, FormulaSymbol.DivisionAndBinding, "/=");
            AddSymbolInfo(2, FormulaSymbol.If, "?");
            AddSymbolInfo(2, FormulaSymbol.Else, ":");
            AddSymbolInfo(3, FormulaSymbol.Or, "|");
            AddSymbolInfo(3, FormulaSymbol.OrNot, "|!");
            AddSymbolInfo(4, FormulaSymbol.And, "&");
            AddSymbolInfo(4, FormulaSymbol.AndNot, "&!");
            AddSymbolInfo(5, FormulaSymbol.More, ">");
            AddSymbolInfo(5, FormulaSymbol.Less, "<");
            AddSymbolInfo(5, FormulaSymbol.Equal, "==");
            AddSymbolInfo(5, FormulaSymbol.NotEqual, "!=");
            AddSymbolInfo(5, FormulaSymbol.MoreOrEqual, ">=");
            AddSymbolInfo(5, FormulaSymbol.LessOrEqual, "<=");
            AddSymbolInfo(6, FormulaSymbol.Addition, "+");
            AddSymbolInfo(6, FormulaSymbol.Subtraction, "-");
            AddSymbolInfo(7, FormulaSymbol.Multiplication, "*");
            AddSymbolInfo(7, FormulaSymbol.Division, "/");
            AddSymbolInfo(8, FormulaSymbol.Power, "^");
            AddSymbolInfo(9, FormulaSymbol.ToNot, "!");
            AddSymbolInfo(9, FormulaSymbol.Modulo, "Mod");
            AddSymbolInfo(9, FormulaSymbol.Maximum, "Max");
            AddSymbolInfo(9, FormulaSymbol.Minimum, "Min");
        }
    }
}