using StarRailDamage.Source.Model.DataStruct.Formula.Symbol;

namespace StarRailDamage.Source.Model.DataStruct.Formula.Method
{
    public class MathFormulaMethod
    {
        public class DefaultMethod : IMathFormulaMethod
        {
            public string Symbol => string.Empty;

            double IMathFormulaMethod.Method(double left, double right)
            {
                return double.NaN;
            }
        }

        public class BeginMethod : IMathFormulaMethod
        {
            public string Symbol => "(";

            public double Method(double left, double right)
            {
                return MathFormulaSymbol.DefaultMethod.Method(left, right);
            }
        }

        public class EndedMethod : IMathFormulaMethod
        {
            public string Symbol => ")";

            public double Method(double left, double right)
            {
                return MathFormulaSymbol.DefaultMethod.Method(left, right);
            }
        }

        public class AddMethod : IMathFormulaMethod
        {
            public string Symbol => "+";

            public double Method(double left, double right)
            {
                return left + right;
            }
        }

        public class SubtractMethod : IMathFormulaMethod
        {
            public string Symbol => "-";

            public double Method(double left, double right)
            {
                return left - right;
            }
        }

        public class MultiplyMethod : IMathFormulaMethod
        {
            public string Symbol => "*";

            public double Method(double left, double right)
            {
                return left * right;
            }
        }

        public class DivideMethod : IMathFormulaMethod
        {
            public string Symbol => "/";

            public double Method(double left, double right)
            {
                return left / right;
            }
        }

        public class ModuloMethod : IMathFormulaMethod
        {
            public string Symbol => "%";

            public double Method(double left, double right)
            {
                return left % right;
            }
        }

        public class PowerMethod : IMathFormulaMethod
        {
            public string Symbol => "^";

            public double Method(double left, double right)
            {
                return Math.Pow(left, right);
            }
        }
    }
}