namespace StarRailDamage.Source.Service.Formula.Symbol
{
    [Flags]
    public enum FormulaSymbol
    {
        None = 0,

        Start = 1 << 0,
        End = 1 << 1,
        Separator = 1 << 2,

        If = 1 << 3,
        Else = 1 << 4,

        Not = 1 << 5,
        Or = 1 << 6,
        OrNot = Or | Not,
        And = 1 << 7,
        AndNot = And | Not,
        Equal = 1 << 8,
        NotEqual = Not | Equal,
        More = 1 << 9,
        MoreOrEqual = More | Equal,
        Less = 1 << 10,
        LessOrEqual = Less | Equal,

        Addition = 1 << 11,
        Subtraction = 1 << 12,
        Multiplication = 1 << 13,
        Division = 1 << 14,
        Power = 1 << 15,

        Function = 1 << 16,
        ToNot = Not | Function,
        Modulo = 1 << 17 | Function,
        Maximum = 1 << 18 | Function,
        Minimum = 1 << 19 | Function
    }
}