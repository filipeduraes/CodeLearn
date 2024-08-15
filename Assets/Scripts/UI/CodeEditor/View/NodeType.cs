using System;

namespace CodeLearn.UI.CodeEditor.View
{
    [Flags]
    public enum NodeType
    {
        BaseNode = 1,
        NumericValue = 2,
        LogicValue = 4,
        NumericOperator = 8,
        LogicOperator = 16
    }
}