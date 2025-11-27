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
            AddSymbolInfo(1, FormulaSymbol.If, "?");
            AddSymbolInfo(1, FormulaSymbol.Else, ":");
            AddSymbolInfo(2, FormulaSymbol.Or, "|");
            AddSymbolInfo(2, FormulaSymbol.OrNot, "|!");
            AddSymbolInfo(3, FormulaSymbol.And, "&");
            AddSymbolInfo(3, FormulaSymbol.AndNot, "&!");
            AddSymbolInfo(4, FormulaSymbol.More, ">");
            AddSymbolInfo(4, FormulaSymbol.Less, "<");
            AddSymbolInfo(4, FormulaSymbol.Equal, "==");
            AddSymbolInfo(4, FormulaSymbol.NotEqual, "!=");
            AddSymbolInfo(4, FormulaSymbol.MoreOrEqual, ">=");
            AddSymbolInfo(4, FormulaSymbol.LessOrEqual, "<=");
            AddSymbolInfo(5, FormulaSymbol.Addition, "+");
            AddSymbolInfo(5, FormulaSymbol.Subtraction, "-");
            AddSymbolInfo(6, FormulaSymbol.Multiplication, "*");
            AddSymbolInfo(6, FormulaSymbol.Division, "/");
            AddSymbolInfo(7, FormulaSymbol.Power, "^");
            AddSymbolInfo(8, FormulaSymbol.ToNot, "!");
            AddSymbolInfo(8, FormulaSymbol.Modulo, "Mod");
            AddSymbolInfo(8, FormulaSymbol.Maximum, "Max");
            AddSymbolInfo(8, FormulaSymbol.Minimum, "Min");
        }
    }
}