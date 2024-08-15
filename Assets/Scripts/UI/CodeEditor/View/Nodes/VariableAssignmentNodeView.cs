using System;
using CodeEditor.Nodes;
using CodeLearn.UI.CodeEditor.ViewModel;
using TMPro;
using UnityEngine;

namespace CodeLearn.UI.CodeEditor.View
{
    public class VariableAssignmentNodeView : MonoBehaviour, INodeView, IBaseNodeView
    {
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private NodeContainer container;

        private void OnEnable()
        {
            CodeEditorMemoryHolder.OnVariableListChanged += UpdateContainer;
            dropdown.onValueChanged.AddListener(UpdateContainerFromDropdown);
        }

        private void OnDisable()
        {
            CodeEditorMemoryHolder.OnVariableListChanged -= UpdateContainer;
            dropdown.onValueChanged.RemoveListener(UpdateContainerFromDropdown);
        }
        
        public bool TryApplyNodeView()
        {
            bool hasDeclaredVariables = CodeEditorMemoryHolder.GetVariableCount() > 0;
            
            if (hasDeclaredVariables)
                UpdateContainer();
            
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
                    return new GetNodeResult(new VariableAssignmentNode<float>(GetVariableKey(), numericValueNode));
                if (variableType == typeof(bool) && nodeResult.resultNode is IValueNode<bool> logicValueNode)
                    return new GetNodeResult(new VariableAssignmentNode<bool>(GetVariableKey(), logicValueNode));

                return new GetNodeResult(GetNodeResult.ErrorType.TypeMismatch);
            }

            return new GetNodeResult(GetNodeResult.ErrorType.VariableNotDeclared);
        }

        private void UpdateContainerFromDropdown(int index)
        {
            UpdateContainer();
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
        }

        private bool TryGetVariableType(out Type type)
        {
            string key = GetVariableKey();
            bool hasVariable = CodeEditorMemoryHolder.HasVariable(key);
            type = null;

            if (hasVariable)
                type = CodeEditorMemoryHolder.GetVariableType(key);

            return hasVariable;
        }

        private string GetVariableKey()
        {
            return dropdown.options[dropdown.value].text;
        }
    }
}