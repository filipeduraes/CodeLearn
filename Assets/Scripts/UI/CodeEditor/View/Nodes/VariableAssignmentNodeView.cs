using System;
using CodeEditor.Nodes;
using CodeLearn.UI.CodeEditor.ViewModel;
using UnityEngine;

namespace CodeLearn.UI.CodeEditor.View
{
    public class VariableAssignmentNodeView : NodeView, IBaseNodeView
    {
        [SerializeField] private VariablesDropdown dropdown;
        [SerializeField] private NodeContainer container;

        private void OnEnable()
        {
            CodeEditorMemoryHolder.OnVariableListChanged += UpdateContainer;
            dropdown.OnValueChanged += UpdateContainer;
        }

        private void OnDisable()
        {
            CodeEditorMemoryHolder.OnVariableListChanged -= UpdateContainer;
            dropdown.OnValueChanged -= UpdateContainer;
        }
        
        public override bool TryApplyNodeView()
        {
            container.SetParentNode(this);
            bool hasDeclaredVariables = CodeEditorMemoryHolder.GetVariableCountAtLine(InitialIndex) > 0;

            if (hasDeclaredVariables)
            {
                dropdown.SetCodeLineGetter(() => InitialIndex);
                dropdown.Populate();
                UpdateContainer();
            }
            
            return hasDeclaredVariables;
        }

        public GetNodeResult TryGetNode()
        {
            GetNodeResult nodeResult = container.TryGetNode();

            if (!nodeResult.wasSuccessful)
                return nodeResult;
            
            if (TryGetVariableType(out Type variableType))
            {
                if (variableType == typeof(float) && nodeResult.resultNode is IValueNode<float> numericValueNode)
                    return new GetNodeResult(new VariableAssignmentNode<float>(dropdown.Key, numericValueNode));
                if (variableType == typeof(bool) && nodeResult.resultNode is IValueNode<bool> logicValueNode)
                    return new GetNodeResult(new VariableAssignmentNode<bool>(dropdown.Key, logicValueNode));

                return new GetNodeResult(GetNodeResult.ErrorType.TypeMismatch);
            }

            return new GetNodeResult(GetNodeResult.ErrorType.VariableNotDeclared);
        }

        public bool IgnoreCompilation()
        {
            return false;
        }

        private void UpdateContainer()
        {
            if (TryGetVariableType(out Type variableType))
            {
                if (variableType == typeof(float))
                    container.SetNodeType(NodeType.NumericValue | NodeType.NumericOperator);
                else if (variableType == typeof(bool))
                    container.SetNodeType(NodeType.NumericValue | NodeType.LogicValue | NodeType.LogicOperator);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private bool TryGetVariableType(out Type type)
        {
            bool hasVariable = CodeEditorMemoryHolder.HasVariableAtLine(dropdown.Key, transform.GetSiblingIndex());
            type = null;

            if (hasVariable)
                type = CodeEditorMemoryHolder.GetVariableType(dropdown.Key);

            return hasVariable;
        }
    }
}