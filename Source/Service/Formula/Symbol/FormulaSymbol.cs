namespace StarRailDamage.Source.Service.Formula.Symbol
{
    [Flags]
    public enum FormulaSymbol
    {
        None = 0,

        Start = 1 << 0,
        End = 1 << 1,
        Separator = 1 << 2,
        Binding = 1 << 3,

        If = 1 << 4,
        Else = 1 << 5,

        Not = 1 << 6,
        Or = 1 << 7,
        OrNot = Or | Not,
        And = 1 << 8,
        AndNot = And | Not,
        Equal = 1 << 9,
        NotEqual = Not | Equal,
        More = 1 << 10,
        MoreOrEqual = More | Equal,
        Less = 1 << 11,
        LessOrEqual = Less | Equal,

        Addition = 1 << 12,
        AdditionAndBinding = Addition | Binding,
        Subtraction = 1 << 13,
        SubtractionAndBinding = Subtraction | Binding,
        Multiplication = 1 << 14,
        MultiplicationAndBinding = Multiplication | Binding,
        Division = 1 << 15,
        DivisionAndBinding = Division | Binding,
        Power = 1 << 16,

        Function = 1 << 17,
        ToNot = Not | Function,
        Modulo = 1 << 18 | Function,
        Maximum = 1 << 19 | Function,
        Minimum = 1 << 20 | Function
    }
}