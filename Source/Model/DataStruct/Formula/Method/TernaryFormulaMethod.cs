using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Factory.PropertyExpression;
using StarRailDamage.Source.Model.DataStruct.Formula.Symbol;
using static StarRailDamage.Source.Model.DataStruct.Formula.Evaluator.TernaryFormulaEvaluator;

namespace StarRailDamage.Source.Model.DataStruct.Formula.Method
{
    public class TernaryFormulaMethod
    {
        public class DefaultMethod : ITernaryFormulaMethod
        {
            public string Symbol => string.Empty;

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return double.NaN;
            }
        }

        public class BeginMethod : ITernaryFormulaMethod
        {
            public string Symbol => "(";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return TernaryFormulaSymbol.DefaultMethod.Method(parameters, source, readOnly);
            }
        }

        public class EndedMethod : ITernaryFormulaMethod
        {
            public string Symbol => ")";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return TernaryFormulaSymbol.DefaultMethod.Method(parameters, source, readOnly);
            }
        }

        public class SeparatorMethod : ITernaryFormulaMethod
        {
            public string Symbol => ",";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return TernaryFormulaSymbol.DefaultMethod.Method(parameters, source, readOnly);
            }
        }

        public class BindingMethod : ITernaryFormulaMethod
        {
            public string Symbol => "=";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return parameters.TryGetFirst(out Formula? Formula) && GetValue(Formula, source, readOnly).OutTemp(out double TempValue) && TrySetValue(Formula.Left, TempValue, source, readOnly) ? TempValue : TernaryFormulaSymbol.DefaultMethod.Method(parameters, source, readOnly);
            }
        }

        public class AddWithBindingMethod : ITernaryFormulaMethod
        {
            public string Symbol => "+=";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return parameters.TryGetFirst(out Formula? Formula) && AddMethod.Method(parameters, source, readOnly).OutTemp(out double TempValue) && TrySetValue(Formula.Left, TempValue, source, readOnly) ? TempValue : TernaryFormulaSymbol.DefaultMethod.Method(parameters, source, readOnly);
            }
        }

        public class SubtractWithBindingMethod : ITernaryFormulaMethod
        {
            public string Symbol => "-=";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return parameters.TryGetFirst(out Formula? Formula) && SubtractMethod.Method(parameters, source, readOnly).OutTemp(out double TempValue) && TrySetValue(Formula.Left, TempValue, source, readOnly) ? TempValue : TernaryFormulaSymbol.DefaultMethod.Method(parameters, source, readOnly);
            }
        }

        public class MultiplyWithBindingMethod : ITernaryFormulaMethod
        {
            public string Symbol => "*=";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return parameters.TryGetFirst(out Formula? Formula) && MultiplyMethod.Method(parameters, source, readOnly).OutTemp(out double TempValue) && TrySetValue(Formula.Left, TempValue, source, readOnly) ? TempValue : TernaryFormulaSymbol.DefaultMethod.Method(parameters, source, readOnly);
            }
        }

        public class DivideWithBindingMethod : ITernaryFormulaMethod
        {
            public string Symbol => "/=";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return parameters.TryGetFirst(out Formula? Formula) && DivideMethod.Method(parameters, source, readOnly).OutTemp(out double TempValue) && TrySetValue(Formula.Left, TempValue, source, readOnly) ? TempValue : TernaryFormulaSymbol.DefaultMethod.Method(parameters, source, readOnly);
            }
        }

        public class IfMethod : ITernaryFormulaMethod
        {
            public string Symbol => "?";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return parameters.TryGetFirst(out Formula? Formula) && Convert.ToBoolean(GetValue(Formula.Left, source, readOnly)) ? GetValue(Formula.Right, source, readOnly) : Convert.ToDouble(false);
            }
        }

        public class ElseMethod : ITernaryFormulaMethod
        {
            public string Symbol => ":";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return parameters.TryGetFirst(out Formula? Formula) && !Convert.ToBoolean(GetValue(Formula.Left, source, readOnly)) ? GetValue(Formula.Right, source, readOnly) : Convert.ToDouble(true);
            }
        }

        public class OrMethod : ITernaryFormulaMethod
        {
            public string Symbol => "|";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return Convert.ToDouble(parameters.TryGetFirst(out Formula? Formula) && (Convert.ToBoolean(GetValue(Formula.Left, source, readOnly)) || Convert.ToBoolean(GetValue(Formula.Right, source, readOnly))));
            }
        }

        public class OrNotMethod : ITernaryFormulaMethod
        {
            public string Symbol => "|!";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return Convert.ToDouble(parameters.TryGetFirst(out Formula? Formula) && (Convert.ToBoolean(GetValue(Formula.Left, source, readOnly)) || !Convert.ToBoolean(GetValue(Formula.Right, source, readOnly))));
            }
        }

        public class AndMethod : ITernaryFormulaMethod
        {
            public string Symbol => "&";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return Convert.ToDouble(parameters.TryGetFirst(out Formula? Formula) && Convert.ToBoolean(GetValue(Formula.Left, source, readOnly)) && Convert.ToBoolean(GetValue(Formula.Right, source, readOnly)));
            }
        }

        public class AndNotMethod : ITernaryFormulaMethod
        {
            public string Symbol => "&!";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return Convert.ToDouble(parameters.TryGetFirst(out Formula? Formula) && Convert.ToBoolean(GetValue(Formula.Left, source, readOnly)) && !Convert.ToBoolean(GetValue(Formula.Right, source, readOnly)));
            }
        }

        public class MoreMethod : ITernaryFormulaMethod
        {
            public string Symbol => ">";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return Convert.ToDouble(parameters.TryGetFirst(out Formula? Formula) && GetValue(Formula.Left, source, readOnly) > GetValue(Formula.Right, source, readOnly));
            }
        }

        public class MoreOrEqualMethod : ITernaryFormulaMethod
        {
            public string Symbol => ">=";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return Convert.ToDouble(parameters.TryGetFirst(out Formula? Formula) && GetValue(Formula.Left, source, readOnly) >= GetValue(Formula.Right, source, readOnly));
            }
        }

        public class EqualMethod : ITernaryFormulaMethod
        {
            public string Symbol => "==";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return Convert.ToDouble(parameters.TryGetFirst(out Formula? Formula) && GetValue(Formula.Left, source, readOnly) == GetValue(Formula.Right, source, readOnly));
            }
        }

        public class NotEqualMethod : ITernaryFormulaMethod
        {
            public string Symbol => "!=";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return Convert.ToDouble(parameters.TryGetFirst(out Formula? Formula) && GetValue(Formula.Left, source, readOnly) != GetValue(Formula.Right, source, readOnly));
            }
        }

        public class LessMethod : ITernaryFormulaMethod
        {
            public string Symbol => "<";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return Convert.ToDouble(parameters.TryGetFirst(out Formula? Formula) && GetValue(Formula.Left, source, readOnly) < GetValue(Formula.Right, source, readOnly));
            }
        }

        public class LessOrEqualMethod : ITernaryFormulaMethod
        {
            public string Symbol => "<=";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return Convert.ToDouble(parameters.TryGetFirst(out Formula? Formula) && GetValue(Formula.Left, source, readOnly) <= GetValue(Formula.Right, source, readOnly));
            }
        }

        public class AddMethod : ITernaryFormulaMethod
        {
            public string Symbol => "+";

            double ITernaryFormulaMethod.Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return Method(parameters, source, readOnly);
            }

            public static double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return parameters.TryGetFirst(out Formula? Formula) ? GetValue(Formula.Left, source, readOnly) + GetValue(Formula.Right, source, readOnly) : TernaryFormulaSymbol.DefaultMethod.Method(parameters, source, readOnly);
            }
        }

        public class SubtractMethod : ITernaryFormulaMethod
        {
            public string Symbol => "-";

            double ITernaryFormulaMethod.Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return Method(parameters, source, readOnly);
            }

            public static double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return parameters.TryGetFirst(out Formula? Formula) ? GetValue(Formula.Left, source, readOnly) - GetValue(Formula.Right, source, readOnly) : TernaryFormulaSymbol.DefaultMethod.Method(parameters, source, readOnly);
            }
        }

        public class MultiplyMethod : ITernaryFormulaMethod
        {
            public string Symbol => "*";

            double ITernaryFormulaMethod.Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return Method(parameters, source, readOnly);
            }

            public static double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return parameters.TryGetFirst(out Formula? Formula) ? GetValue(Formula.Left, source, readOnly) * GetValue(Formula.Right, source, readOnly) : TernaryFormulaSymbol.DefaultMethod.Method(parameters, source, readOnly);
            }
        }

        public class DivideMethod : ITernaryFormulaMethod
        {
            public string Symbol => "/";

            double ITernaryFormulaMethod.Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return Method(parameters, source, readOnly);
            }

            public static double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return parameters.TryGetFirst(out Formula? Formula) ? GetValue(Formula.Left, source, readOnly) / GetValue(Formula.Right, source, readOnly) : TernaryFormulaSymbol.DefaultMethod.Method(parameters, source, readOnly);
            }
        }

        public class PowerMethod : ITernaryFormulaMethod
        {
            public string Symbol => "^";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return parameters.TryGetFirst(out Formula? Formula) ? Math.Pow(GetValue(Formula.Left, source, readOnly), GetValue(Formula.Right, source, readOnly)) : TernaryFormulaSymbol.DefaultMethod.Method(parameters, source, readOnly);
            }
        }

        public class NotMethod : ITernaryFormulaMethod
        {
            public string Symbol => "!(";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return Convert.ToDouble(parameters.TryGetFirst(out Formula? Formula) && !Convert.ToBoolean(GetValue(Formula, source, readOnly)));
            }
        }

        public class ModuloMethod : ITernaryFormulaMethod
        {
            public string Symbol => "Mod(";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return parameters.Count == 2 ? GetValue(parameters[0], source, readOnly) % GetValue(parameters[1], source, readOnly) : TernaryFormulaSymbol.DefaultMethod.Method(parameters, source, readOnly);
            }
        }

        public class MaximumMethod : ITernaryFormulaMethod
        {
            public string Symbol => "Max(";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return parameters.Max(x => GetValue(x, source, readOnly));
            }
        }

        public class MinimumMethod : ITernaryFormulaMethod
        {
            public string Symbol => "Min(";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return parameters.Min(x => GetValue(x, source, readOnly));
            }
        }

        public class SwitchMethod : ITernaryFormulaMethod
        {
            public string Symbol => "The(";

            public double Method(IList<Formula> parameters, Dictionary<string, IPropertyExpression<double>>? source, bool readOnly)
            {
                return parameters.TryGetFirst(out Formula? Formula) && parameters.IndexTry((int)GetValue(Formula, source, readOnly), out Formula) ? GetValue(Formula, source, readOnly) : TernaryFormulaSymbol.DefaultMethod.Method(parameters, source, readOnly);
            }
        }
    }
}