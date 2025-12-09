using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.DataStruct.Formula;
using StarRailDamage.Source.Model.DataStruct.PrefixedTree;
using StarRailDamage.Source.Service.Formula.Symbol;
using System.Text;

namespace StarRailDamage.Source.Service.Formula
{
    public static class FormulaSeparator
    {
        public static FormulaNode? Parse(string formula)
        {
            int Frequency = 1;
            Stack<FormulaSymbol> Symbols = new();
            Stack<FormulaNode?> Arguments = new();
            for (int i = 0; i < formula.Length; i++)
            {
                FormulaSymbol FormulaSymbol = NextFormulaSymbol(formula, ref i);
                if (FormulaSymbol != FormulaSymbol.None)
                {
                    if (!AppendSymbol(FormulaSymbol, Arguments, Symbols, ref Frequency)) return null;
                    if (Frequency >= 2)
                    {
                        FormulaSymbol = AppendArgument(Arguments, formula, ref i, FormulaSymbol);
                    }
                    else continue;
                }
                else
                {
                    FormulaSymbol = AppendArgument(Arguments, formula, ref i);
                }
                if (FormulaSymbol != FormulaSymbol.None.Configure(Frequency = 0))
                {
                    if (!AppendSymbol(FormulaSymbol, Arguments, Symbols, ref Frequency)) return null;
                }
            }
            while (Symbols.Count >= 1)
            {
                if (!FormulaSymbolPerform(Arguments, Symbols)) return null;
            }
            return Arguments.PopOrDefault();
        }

        private static bool FormulaSymbolPerform(Stack<FormulaNode?> arguments, Stack<FormulaSymbol> symbols)
        {
            if (arguments.Count >= 2 && symbols.Count >= 1)
            {
                arguments.Push(new FormulaNode(arguments.Pop(), symbols.Pop(), arguments.Pop()));
                return true;
            }
            return false;
        }

        private static FormulaSymbol NextFormulaSymbol(string fomula, ref int index, int moveIndex = 0, PrefixedTreeNode<char, FormulaSymbol>? symbolNode = null)
        {
            symbolNode ??= FormulaSymbolExtension.GetSymbolTree();
            while (index + moveIndex < fomula.Length)
            {
                if (symbolNode.TryGetNode(fomula[index + moveIndex], out PrefixedTreeNode<char, FormulaSymbol>? NextNode))
                {
                    symbolNode = NextNode.Configure(index++);
                }
                else
                {
                    index--; break;
                }
            }
            return symbolNode.Value;
        }

        private static FormulaSymbol AppendArgument(Stack<FormulaNode?> arguments, string formula, ref int index, FormulaSymbol? formulaSymbol = null)
        {
            StringBuilder StringBuilder = new();
            StringBuilder.Append(formulaSymbol?.Symbol());
            StringBuilder.Append(formula[++index]);
            FormulaSymbol FormulaSymbol = FormulaSymbol.None;
            PrefixedTreeNode<char, FormulaSymbol> SymbolNode = FormulaSymbolExtension.GetSymbolTree();
            while (++index < formula.Length)
            {
                if (SymbolNode.TryGetNode(formula[index], out PrefixedTreeNode<char, FormulaSymbol>? NextNode))
                {
                    FormulaSymbol = NextFormulaSymbol(formula, ref index, 1, NextNode).Configure(index++); break;
                }
                else StringBuilder.Append(formula[index]);
            }
            arguments.Push(new FormulaNode(StringBuilder.ToString()));
            return FormulaSymbol;
        }

        private static bool AppendSymbol(FormulaSymbol formulaSymbol, Stack<FormulaNode?> arguments, Stack<FormulaSymbol> symbols, ref int frequency)
        {
            if (formulaSymbol == FormulaSymbol.End)
            {
                while (symbols.TryPeek(out FormulaSymbol LastSymbol) && (LastSymbol & FormulaSymbol.Start) == FormulaSymbol.None)
                {
                    if (!FormulaSymbolPerform(arguments, symbols)) return false;
                }
                if (symbols.TryPop(out FormulaSymbol StartSymbol))
                {
                    if (StartSymbol != FormulaSymbol.Start)
                    {
                        symbols.Push(StartSymbol & ~FormulaSymbol.Start);
                        arguments.Push(null);
                    }
                }
                else return false;
            }
            else
            {
                if ((formulaSymbol & FormulaSymbol.Start) == FormulaSymbol.None)
                {
                    if (++frequency >= 2) return true;
                    while (symbols.TryPeek(out FormulaSymbol LastSymbol) && (LastSymbol & FormulaSymbol.Start) == FormulaSymbol.None && LastSymbol.Precedence() >= formulaSymbol.Precedence())
                    {
                        if (!FormulaSymbolPerform(arguments, symbols)) return false;
                    }
                }
                symbols.Push(formulaSymbol);
            }
            return true;
        }
    }
}