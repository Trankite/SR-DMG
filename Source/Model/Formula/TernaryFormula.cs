using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Factory.PropertyExpression;
using StarRailDamage.Source.Service.Formula;
using StarRailDamage.Source.Service.Formula.Symbol;
using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Model.Formula
{
    public class TernaryFormula<T>
    {
        private static readonly Dictionary<FormulaSymbol, Func<FormulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>?, T?, bool, double>> SymbolFunctionMap = [];

        private static readonly Dictionary<FormulaSymbol, Func<IList<FormulaNode>, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>?, T?, bool, double>> FunctionMap = [];

        public virtual T? Sender { get; set; }

        public FormulaNode? Formula { get; set; }

        public virtual IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? Source { get; set; }

        public virtual bool ReadOnly { get; set; }

        public TernaryFormula() { }

        public TernaryFormula(bool readOnly) => ReadOnly = readOnly;

        public TernaryFormula<T> SetFormula(string formula) => this.With(Formula = FormulaSeparator.Parse(formula));

        public TernaryFormula<T> SetFormula(FormulaNode? formulaNode) => this.With(Formula = formulaNode);

        public double GetValue() => GetValue(Formula, Source, Sender, ReadOnly);

        public double GetValue(T? sender) => GetValue(Formula, Source, sender, ReadOnly);

        public double GetValue(T? sender, bool readOnly) => GetValue(Formula, Source, sender, readOnly);

        public double GetValue(FormulaNode? formulaNode) => GetValue(formulaNode, Source, Sender, ReadOnly);

        public double GetValue(FormulaNode? formulaNode, bool readOnly) => GetValue(formulaNode, Source, Sender, readOnly);

        public static double GetValue(FormulaNode? formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            if (formulaNode == null) return double.NaN;
            if ((formulaNode.Symbol & FormulaSymbol.Function) == FormulaSymbol.Function)
            {
                if (FunctionMap.TryGetValue(formulaNode.Symbol, out Func<IList<FormulaNode>, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>?, T?, bool, double>? Function))
                {
                    return Function(GetArguments(formulaNode), source, sender, readOnly);
                }
            }
            else
            {
                if (formulaNode.Symbol == FormulaSymbol.Binding)
                {
                    return Binding(formulaNode, source, sender, readOnly);
                }
                if (SymbolFunctionMap.TryGetValue(formulaNode.Symbol & ~FormulaSymbol.Binding, out Func<FormulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>?, T?, bool, double>? SymbolFunction))
                {
                    if ((formulaNode.Symbol & FormulaSymbol.Binding) == FormulaSymbol.Binding)
                    {
                        return Binding(SymbolFunction, formulaNode, source, sender, readOnly);
                    }
                    else
                    {
                        return SymbolFunction(formulaNode, source, sender, readOnly);
                    }
                }
            }
            return GetDefaultValue(formulaNode, source, sender);
        }

        private static List<FormulaNode> GetArguments(FormulaNode formulaNode)
        {
            return GetArguments(formulaNode.Right, GetArguments(formulaNode.Left, []));
        }

        private static List<FormulaNode> GetArguments(FormulaNode? formulaNode, List<FormulaNode> collection)
        {
            if (formulaNode?.Symbol == FormulaSymbol.Separator)
            {
                AppendArgument(formulaNode.Right, AppendArgument(formulaNode.Left, collection));
            }
            return collection;
        }

        private static List<FormulaNode> AppendArgument(FormulaNode? formulaNode, List<FormulaNode> collection)
        {
            if (formulaNode == null) return collection;
            if (formulaNode.Symbol == FormulaSymbol.Separator)
            {
                GetArguments(formulaNode, collection);
            }
            else
            {
                collection.Add(formulaNode);
            }
            return collection;
        }

        private static double If(FormulaNode formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return Convert.ToBoolean(GetValue(formulaNode.Left, source, sender, readOnly)) ? GetValue(formulaNode.Right, source, sender, readOnly) : Convert.ToDouble(false);
        }

        private static double Else(FormulaNode formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return Convert.ToBoolean(GetValue(formulaNode.Left, source, sender, readOnly)) ? Convert.ToDouble(true) : GetValue(formulaNode.Right, source, sender, readOnly);
        }

        private static double Or(FormulaNode formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return Convert.ToDouble(Convert.ToBoolean(GetValue(formulaNode.Left, source, sender, readOnly)) || Convert.ToBoolean(GetValue(formulaNode.Right, source, sender, readOnly)));
        }

        private static double OrNot(FormulaNode formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return Convert.ToDouble(Convert.ToBoolean(GetValue(formulaNode.Left, source, sender, readOnly)) || !Convert.ToBoolean(GetValue(formulaNode.Right, source, sender, readOnly)));
        }

        private static double And(FormulaNode formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return Convert.ToDouble(Convert.ToBoolean(GetValue(formulaNode.Left, source, sender, readOnly)) && Convert.ToBoolean(GetValue(formulaNode.Right, source, sender, readOnly)));
        }

        private static double AndNot(FormulaNode formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return Convert.ToDouble(Convert.ToBoolean(GetValue(formulaNode.Left, source, sender, readOnly)) && !Convert.ToBoolean(GetValue(formulaNode.Right, source, sender, readOnly)));
        }

        private static double Equal(FormulaNode formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return Convert.ToDouble(GetValue(formulaNode.Left, source, sender, readOnly) == GetValue(formulaNode.Right, source, sender, readOnly));
        }

        private static double NotEqual(FormulaNode formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return Convert.ToDouble(GetValue(formulaNode.Left, source, sender, readOnly) != GetValue(formulaNode.Right, source, sender, readOnly));
        }

        private static double More(FormulaNode formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return Convert.ToDouble(GetValue(formulaNode.Left, source, sender, readOnly) > GetValue(formulaNode.Right, source, sender, readOnly));
        }

        private static double MoreOrEqual(FormulaNode formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return Convert.ToDouble(GetValue(formulaNode.Left, source, sender, readOnly) >= GetValue(formulaNode.Right, source, sender, readOnly));
        }

        private static double Less(FormulaNode formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return Convert.ToDouble(GetValue(formulaNode.Left, source, sender, readOnly) < GetValue(formulaNode.Right, source, sender, readOnly));
        }

        private static double LessOrEqual(FormulaNode formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return Convert.ToDouble(GetValue(formulaNode.Left, source, sender, readOnly) <= GetValue(formulaNode.Right, source, sender, readOnly));
        }

        private static double Addition(FormulaNode formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return GetValue(formulaNode.Left, source, sender, readOnly) + GetValue(formulaNode.Right, source, sender, readOnly);
        }

        private static double Subtraction(FormulaNode formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return GetValue(formulaNode.Left, source, sender, readOnly) - GetValue(formulaNode.Right, source, sender, readOnly);
        }

        private static double Multiplication(FormulaNode formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return GetValue(formulaNode.Left, source, sender, readOnly) * GetValue(formulaNode.Right, source, sender, readOnly);
        }

        private static double Division(FormulaNode formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return GetValue(formulaNode.Left, source, sender, readOnly) / GetValue(formulaNode.Right, source, sender, readOnly);
        }

        private static double Power(FormulaNode formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return Math.Pow(GetValue(formulaNode.Left, source, sender, readOnly), GetValue(formulaNode.Right, source, sender, readOnly));
        }

        private static double Binding(double value, FormulaNode formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return readOnly ? value : value.With(TrySetValue(formulaNode.Left?.Text, source, sender, value));
        }

        private static double Binding(FormulaNode formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return Binding(GetValue(formulaNode.Right, source, sender, readOnly), formulaNode, source, sender, readOnly);
        }

        private static double Binding(Func<FormulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>?, T?, bool, double> symbolFunction, FormulaNode formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return Binding(symbolFunction(formulaNode, source, sender, readOnly), formulaNode, source, sender, readOnly);
        }

        private static double ToNot(IList<FormulaNode> arguments, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return Convert.ToDouble(arguments.Count >= 1 && !Convert.ToBoolean(GetValue(arguments[0], source, sender, readOnly)));
        }

        private static double Modulo(IList<FormulaNode> arguments, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return arguments.Count >= 2 ? GetValue(arguments[0], source, sender, readOnly) % GetValue(arguments[1], source, sender, readOnly) : double.NaN;
        }

        private static double Maximum(IList<FormulaNode> arguments, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return arguments.Count >= 1 ? arguments.Max(x => GetValue(x, source, sender, readOnly)) : double.NaN;
        }

        private static double Minimum(IList<FormulaNode> arguments, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, bool readOnly)
        {
            return arguments.Count >= 1 ? arguments.Min(x => GetValue(x, source, sender, readOnly)) : double.NaN;
        }

        private static double GetDefaultValue(FormulaNode formulaNode, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender)
        {
            if (string.IsNullOrEmpty(formulaNode.Text)) return double.NaN;
            bool IsPercent = formulaNode.Text.EndsWith('%');
            string Target = IsPercent ? formulaNode.Text.TrimEnd('%') : formulaNode.Text;
            if (double.TryParse(Target, out double Result) || TryGetDefaultValue(Target, source, sender, out Result))
            {
                return IsPercent ? Result / 100 : Result;
            }
            return double.NaN;
        }

        private static bool TryGetDefaultValue(string text, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, [NotNullWhen(true)] out double result)
        {
            return sender != null && TryGetPropertyExpression(text, source, out IPropertyExpression<T, double>? PropertyExpression) && PropertyExpression.TryGetValue(sender, out result) || false.With(result = default);
        }

        private static bool TrySetValue(string? text, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, T? sender, double value)
        {
            return text != null && sender != null && TryGetPropertyExpression(text, source, out IPropertyExpression<T, double>? PropertyExpression) && PropertyExpression.TrySetValue(sender, value);
        }

        private static bool TryGetPropertyExpression(string text, IEnumerable<Dictionary<string, IPropertyExpression<T, double>>>? source, [NotNullWhen(true)] out IPropertyExpression<T, double>? propertyExpression)
        {
            if (source != null)
            {
                foreach (Dictionary<string, IPropertyExpression<T, double>> Arguments in source)
                {
                    if (Arguments.TryGetValue(text.Trim('[', ']'), out propertyExpression)) return true;
                }
            }
            return false.With(propertyExpression = default);
        }

        public override string ToString()
        {
            return Formula == null ? string.Empty : Formula.ToString();
        }

        static TernaryFormula()
        {
            SymbolFunctionMap[FormulaSymbol.If] = If;
            SymbolFunctionMap[FormulaSymbol.Else] = Else;
            SymbolFunctionMap[FormulaSymbol.Or] = Or;
            SymbolFunctionMap[FormulaSymbol.OrNot] = OrNot;
            SymbolFunctionMap[FormulaSymbol.And] = And;
            SymbolFunctionMap[FormulaSymbol.AndNot] = AndNot;
            SymbolFunctionMap[FormulaSymbol.Equal] = Equal;
            SymbolFunctionMap[FormulaSymbol.NotEqual] = NotEqual;
            SymbolFunctionMap[FormulaSymbol.More] = More;
            SymbolFunctionMap[FormulaSymbol.MoreOrEqual] = MoreOrEqual;
            SymbolFunctionMap[FormulaSymbol.Less] = Less;
            SymbolFunctionMap[FormulaSymbol.LessOrEqual] = LessOrEqual;
            SymbolFunctionMap[FormulaSymbol.Addition] = Addition;
            SymbolFunctionMap[FormulaSymbol.Subtraction] = Subtraction;
            SymbolFunctionMap[FormulaSymbol.Multiplication] = Multiplication;
            SymbolFunctionMap[FormulaSymbol.Division] = Division;
            SymbolFunctionMap[FormulaSymbol.Power] = Power;
            FunctionMap[FormulaSymbol.ToNot] = ToNot;
            FunctionMap[FormulaSymbol.Modulo] = Modulo;
            FunctionMap[FormulaSymbol.Maximum] = Maximum;
            FunctionMap[FormulaSymbol.Minimum] = Minimum;
        }
    }
}