using System;
using CodeEditor.Nodes;
using CodeLearn.CodeEditor;
using UnityEngine;

namespace CodeLearn.UI.CodeEditor.View
{
    public class OperatorNodeView : MonoBehaviour, INodeView
    {
        [SerializeField] private OperatorType operatorType;

        private enum OperatorType
        {
            Sum,
            Subtraction,
            Multiplication,
            Division,
            AND,
            OR,
            Not,
            Greater,
            GreaterEqual,
            Smaller,
            SmallerEqual,
            Equal,
            NotEqual
        }
        
        public bool TryApplyNodeView()
        {
            return transform.parent.childCount == 2;
        }

        public ICodeNode CreateNumericOperationNode(IValueNode<float> leftOperand, IValueNode<float> rightOperand)
        {
            return operatorType switch
            {
                OperatorType.Sum => new SumNode(leftOperand, rightOperand),
                OperatorType.Subtraction => new SubtractionNode(leftOperand, rightOperand),
                OperatorType.Multiplication => new MultiplyNode(leftOperand, rightOperand),
                OperatorType.Division => new DivideNode(leftOperand, rightOperand),
                _ => null
            };
        }

        public ICodeNode CreateComparisonNode(IValueNode<float> leftOperand, IValueNode<float> rightOperand)
        {
            return operatorType switch
            {
                OperatorType.Greater => new GreaterNode(leftOperand, rightOperand),
                OperatorType.GreaterEqual => new GreaterOrEqualNode(leftOperand, rightOperand),
                OperatorType.Smaller => new SmallerNode(leftOperand, rightOperand),
                OperatorType.SmallerEqual => new SmallerOrEqualNode(leftOperand, rightOperand),
                OperatorType.Equal => new EqualNode(leftOperand, rightOperand),
                OperatorType.NotEqual => new NotEqualNode(leftOperand, rightOperand),
                _ => null
            };
        }

        public ICodeNode CreateLogicOperatorNode(IValueNode<bool> leftOperand, IValueNode<bool> rightOperand)
        {
            return operatorType switch
            {
                OperatorType.AND => new AndNode(leftOperand, rightOperand),
                OperatorType.OR => new OrNode(leftOperand, rightOperand),
                _ => null
            };
        }

        public ICodeNode CreateUnaryLogicOperatorNode(IValueNode<bool> operand)
        {
            return operatorType switch
            {
                OperatorType.Not => new NotNode(operand),
                _ => null
            };
        }
        
        public ICodeNode CreateUnaryNumericOperatorNode(IValueNode<float> operand)
        {
            return operatorType switch
            {
                OperatorType.Subtraction => new NegateNode(operand),
                _ => null
            };
        }
    }
}