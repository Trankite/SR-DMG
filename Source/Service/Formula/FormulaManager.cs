using StarRailDamage.Source.Model.DataStruct.Formula;
using StarRailDamage.Source.Service.Formula.Symbol;

namespace StarRailDamage.Source.Service.Formula
{
    public class FormulaManager
    {
        public static List<string> GetStep(FormulaNode? formulaNode)
        {
            return GetStep(formulaNode, []);
        }

        public static List<string> GetStep(FormulaNode? formulaNode, List<string> collection)
        {
            if (formulaNode is null) return collection;
            if (formulaNode.Symbol != FormulaSymbol.None)
            {
                GetStep(formulaNode.Left, collection);
                GetStep(formulaNode.Right, collection);
                if (formulaNode.Symbol != FormulaSymbol.Separator)
                {
                    collection.Add(formulaNode.ToString());
                }
            }
            return collection;
        }

        public static List<FormulaNode> GetBinding(FormulaNode? formulaNode)
        {
            return GetBinding(formulaNode, []);
        }

        public static List<FormulaNode> GetBinding(FormulaNode? formulaNode, List<FormulaNode> collection)
        {
            if (formulaNode is null) return collection;
            GetBinding(formulaNode.Left, collection);
            GetBinding(formulaNode.Right, collection);
            if ((formulaNode.Symbol & FormulaSymbol.Binding) == FormulaSymbol.Binding)
            {
                collection.Add(formulaNode);
            }
            return collection;
        }
    }
}