using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.DataStruct.Formula.Abstraction;
using System.Text;

namespace StarRailDamage.Source.Model.DataStruct.Formula.Factory
{
    public abstract class FormulaFactory : IFormulaFactory
    {
        protected abstract IFormulaSymbolManager SymbolManager { get; }

        protected abstract bool IsBeginSymbol(IFormulaSymbol formulaSymbol);

        protected abstract bool IsEndedSymbol(IFormulaSymbol formulaSymbol);

        protected abstract Formula GetFormula(string value);

        protected abstract Formula GetFormula(Formula? rightFormula, IFormulaSymbol formulaSymbol, Formula? leftFormula);

        protected virtual void OnPopBeginSymbol(IFormulaSymbol formulaSymbol, Stack<Formula?> formulaStack, Stack<IFormulaSymbol> formulaSymbolStack) { }

        public Formula? Parse(string? formula)
        {
            if (string.IsNullOrEmpty(formula)) return null;
            int Index = 0, RepeatedSymbol = 1;
            Stack<Formula?> FormulaStack = new();
            Stack<IFormulaSymbol> FormulaSymbolStack = new();
            while (Index < formula.Length)
            {
                IFormulaSymbol? FormulaSymbol = SymbolManager.NextSymbol(formula, ref Index);
                if (FormulaSymbol.IsNotNull())
                {
                    if (!AppendSymbol(FormulaSymbol, FormulaStack, FormulaSymbolStack, ref RepeatedSymbol)) return null;
                    if (RepeatedSymbol >= 2)
                    {
                        FormulaSymbol = AppendFormula(FormulaStack, formula, ref Index, FormulaSymbol.Text);
                    }
                    else continue;
                }
                else
                {
                    FormulaSymbol = AppendFormula(FormulaStack, formula, ref Index);
                }
                if (FormulaSymbol.IsNotNull().Configure(RepeatedSymbol = 0))
                {
                    if (!AppendSymbol(FormulaSymbol!, FormulaStack, FormulaSymbolStack, ref RepeatedSymbol)) return null;
                }
            }
            while (FormulaSymbolStack.Count >= 1)
            {
                if (!FormulaCombine(FormulaStack, FormulaSymbolStack)) return null;
            }
            return FormulaStack.PopOrDefault();
        }

        private bool FormulaCombine(Stack<Formula?> formulaStack, Stack<IFormulaSymbol> formulaSymbolStack)
        {
            if (formulaStack.Count < 2 || formulaSymbolStack.Count < 1) return false;
            formulaStack.Push(GetFormula(formulaStack.Pop(), formulaSymbolStack.Pop(), formulaStack.Pop()));
            return true;
        }

        private IFormulaSymbol? AppendFormula(Stack<Formula?> formulaStack, string formula, ref int index, string? text = null)
        {
            IFormulaSymbol? FormulaSymbol = null;
            StringBuilder StringBuilder = new(text);
            while (index < formula.Length)
            {
                StringBuilder.Append(formula[index++]);
                IFormulaSymbol? NextSymbol = SymbolManager.NextSymbol(formula, ref index);
                if (NextSymbol.IsNotNull())
                {
                    FormulaSymbol = NextSymbol; break;
                }
            }
            formulaStack.Push(GetFormula(StringBuilder.ToString()));
            return FormulaSymbol;
        }

        private bool AppendSymbol(IFormulaSymbol formulaSymbol, Stack<Formula?> formulaStack, Stack<IFormulaSymbol> formulaSymbolStack, ref int frequency)
        {
            if (IsEndedSymbol(formulaSymbol))
            {
                while (formulaSymbolStack.TryPeek(out IFormulaSymbol? NextSymbol) && !IsBeginSymbol(NextSymbol))
                {
                    if (!FormulaCombine(formulaStack, formulaSymbolStack)) return false;
                }
                if (!formulaSymbolStack.TryPop(out IFormulaSymbol? BeginSymbol)) return false;
                OnPopBeginSymbol(BeginSymbol, formulaStack, formulaSymbolStack);
            }
            else
            {
                if (!IsBeginSymbol(formulaSymbol))
                {
                    if (++frequency >= 2) return true;
                    while (formulaSymbolStack.TryPeek(out IFormulaSymbol? NextSymbol) && !IsBeginSymbol(NextSymbol) && NextSymbol.Rank >= formulaSymbol.Rank)
                    {
                        if (!FormulaCombine(formulaStack, formulaSymbolStack)) return false;
                    }
                }
                formulaSymbolStack.Push(formulaSymbol);
            }
            return true;
        }
    }
}