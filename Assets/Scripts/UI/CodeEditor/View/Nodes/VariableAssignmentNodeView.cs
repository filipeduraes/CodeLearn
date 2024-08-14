using System;
using CodeLearn.UI.CodeEditor.ViewModel;
using TMPro;
using UnityEngine;

namespace CodeLearn.UI.CodeEditor.View
{
    public class VariableAssignmentNodeView : MonoBehaviour, INodeView
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

        private void UpdateContainerFromDropdown(int index)
        {
            UpdateContainer();
        }

        private void UpdateContainer()
        {
            string key = dropdown.options[dropdown.value].text;
            
            if (CodeEditorMemoryHolder.HasVariable(key))
            {
                Type variableType = CodeEditorMemoryHolder.GetVariableType(key);

                if (variableType == typeof(float))
                    container.SetNodeType(NodeType.NumericValue);
                else if (variableType == typeof(bool))
                    container.SetNodeType(NodeType.LogicValue);
            }
        }

        public bool TryApplyNodeView()
        {
            bool hasDeclaredVariables = CodeEditorMemoryHolder.GetVariableCount() > 0;
            
            if (hasDeclaredVariables)
                UpdateContainer();
            
            return hasDeclaredVariables;
        }
    }
}