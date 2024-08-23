using System;
using CodeEditor.Nodes;
using CodeLearn.CodeEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CodeLearn.UI.CodeEditor.View
{
    public class NodeContainer : MonoBehaviour, IBaseNodeView
    {
        [SerializeField] private RectTransform containerParent;
        [SerializeField] private NodeType nodeType;
        [SerializeField] private bool checkHorizontal;

        private RectTransform ContainerParent => containerParent;
        public NodeType NodeType => nodeType;
        private NodeView ParentNode { get; set; }

        public void SetParentNode(NodeView parentNode)
        {
            ParentNode = parentNode;
        }

        public void SetNodeType(NodeType type)
        {
            if (type != nodeType)
            {
                for (int i = containerParent.childCount - 1; i >= 0; i--)
                    Destroy(containerParent.GetChild(i).gameObject);
                
                nodeType = type;
            }
        }

        public GetNodeResult TryGetNode()
        {
            return containerParent.childCount switch
            {
                0 => new GetNodeResult(GetNodeResult.ErrorType.EmptyAssignment),
                1 => HandleSingleValue(),
                2 => HandleUnaryExpression(),
                3 => HandleExpression(),
                _ => new GetNodeResult(GetNodeResult.ErrorType.InvalidSyntax)
            };
        }

        private GetNodeResult HandleUnaryExpression()
        {
            ICodeNode valueNode = GetValueNode(containerParent.GetChild(1));
            ICodeNode result = null;

            if (containerParent.GetChild(0).TryGetComponent(out OperatorNodeView operatorNodeView))
            {
                if (valueNode is IValueNode<bool> logicValueNode)
                    result = operatorNodeView.CreateUnaryLogicOperatorNode(logicValueNode);
                else if(valueNode is IValueNode<float> numericValueNode)
                    result = operatorNodeView.CreateUnaryNumericOperatorNode(numericValueNode);
            }

            return result != null ? new GetNodeResult(result) : new GetNodeResult(GetNodeResult.ErrorType.InvalidSyntax);
        }

        private GetNodeResult HandleExpression()
        {
            ICodeNode operandNode = null;
            ICodeNode leftValueNode = GetValueNode(containerParent.GetChild(0));
            ICodeNode rightValueNode = GetValueNode(containerParent.GetChild(2));

            if (containerParent.GetChild(1).TryGetComponent(out OperatorNodeView operatorNodeView))
            {
                if (leftValueNode is IValueNode<float> leftNumericValue && rightValueNode is IValueNode<float> rightNumericValue)
                {
                    if((nodeType & NodeType.NumericOperator) != 0)
                        operandNode = operatorNodeView.CreateNumericOperationNode(leftNumericValue, rightNumericValue);
                    else if ((nodeType & NodeType.LogicOperator) != 0)
                        operandNode = operatorNodeView.CreateComparisonNode(leftNumericValue, rightNumericValue);
                }
                else if (leftValueNode is IValueNode<bool> leftLogicValue && rightValueNode is IValueNode<bool> rightLogicValue)
                {
                    operandNode = operatorNodeView.CreateLogicOperatorNode(leftLogicValue, rightLogicValue);
                }
            }

            return operandNode != null ? new GetNodeResult(operandNode) : new GetNodeResult(GetNodeResult.ErrorType.InvalidSyntax);
        }
        
        private ICodeNode GetValueNode(Component leftValueChild)
        {
            if (leftValueChild.TryGetComponent(out GetVariableNodeView getVariableNodeView))
                return GetVariableGetNode(getVariableNodeView);
            
            if (leftValueChild.TryGetComponent(out LiteralValueNodeView literalValueNodeView))
                return literalValueNodeView.GetLiteralValue();

            return null;
        }

        private GetNodeResult HandleSingleValue()
        {
            ICodeNode valueNode = GetValueNode(containerParent.GetChild(0));
            return valueNode == null ? new GetNodeResult(GetNodeResult.ErrorType.TypeMismatch) : new GetNodeResult(valueNode);
        }

        private ICodeNode GetVariableGetNode(GetVariableNodeView getVariableNodeView)
        {
            if ((nodeType & NodeType.NumericValue) != 0)
                return new VariableGetNode<float>(getVariableNodeView.Key);
            if ((nodeType & NodeType.LogicValue) != 0)
                return new VariableGetNode<bool>(getVariableNodeView.Key);

            return null;
        }

        public void FitNodeView(NodeView nodeView, Vector2 mousePosition)
        {
            Func<float> getMousePosition = checkHorizontal ? () => mousePosition.x : () => mousePosition.y;
            Func<int, float> getChildPosition = checkHorizontal ? index => ContainerParent.GetChild(index).position.x : index => ContainerParent.GetChild(index).position.y;
            
            int index = FindBestIndexForNodeView(getMousePosition, getChildPosition);
            nodeView.SetOwningContainer(ContainerParent);

            if(index >= 0)
                nodeView.transform.SetSiblingIndex(index);

            if (ParentNode != null)
            {
                nodeView.SetParentNode(ParentNode);
                LayoutRebuilder.ForceRebuildLayoutImmediate(containerParent);
            }
            
        }

        public void FitNodeView(NodeView nodeView)
        {
            nodeView.SetOwningContainer(ContainerParent);
            
            if (ParentNode != null)
                nodeView.SetParentNode(ParentNode);
        }

        private int FindBestIndexForNodeView(Func<float> getMousePosition, Func<int, float> getChildPosition)
        {
            if (ContainerParent.childCount == 0)
                return 0;
            
            if (checkHorizontal ? getMousePosition() < getChildPosition(0) : getMousePosition() > getChildPosition(0))
                return 0;
            
            if (checkHorizontal ? getMousePosition() > getChildPosition(ContainerParent.childCount - 1) : getMousePosition() < getChildPosition(ContainerParent.childCount - 1))
                return ContainerParent.childCount;

            int leftIndex = 0;
            int rightIndex = containerParent.childCount - 1;
            int closestIndex = -1;
            float closestIndexDistance = float.MaxValue;

            while (leftIndex <= rightIndex)
            {
                int middleIndex = (leftIndex + rightIndex) / 2;
                float nodeDistance = Mathf.Abs(getChildPosition(middleIndex) - getMousePosition());

                if (nodeDistance < closestIndexDistance)
                {
                    closestIndex = middleIndex;
                    closestIndexDistance = nodeDistance;
                }

                if (checkHorizontal ? getChildPosition(middleIndex) > getMousePosition() : getChildPosition(middleIndex) < getMousePosition())
                {
                    rightIndex = middleIndex - 1;
                }
                else
                {
                    leftIndex = middleIndex + 1;
                }
            }

            return closestIndex;
        }
    }
}