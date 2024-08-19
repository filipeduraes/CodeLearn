using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CodeLearn.UI.CodeEditor.ViewModel
{
    public class VariablesDropdown : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown;
        
        private Func<int> _getCodeLine;
        
        public event Action OnValueChanged = delegate { };
        
        public string Key { get; private set; }

        private void OnEnable()
        {
            dropdown.onValueChanged.AddListener(UpdateKey);
            CodeEditorMemoryHolder.OnVariableListChanged += Populate;
        }

        private void OnDisable()
        {
            dropdown.onValueChanged.RemoveListener(UpdateKey);
            CodeEditorMemoryHolder.OnVariableListChanged -= Populate;
        }

        public void SetCodeLineGetter(Func<int> getCodeLine)
        {
            _getCodeLine = getCodeLine;
        }

        public void Populate()
        {
            dropdown.options.Clear();
            IEnumerable<string> variableKeysDeclared = CodeEditorMemoryHolder.GetVariableKeysDeclaredAtLine(_getCodeLine());
            
            foreach (string variableKey in variableKeysDeclared)
                dropdown.options.Add(new TMP_Dropdown.OptionData(variableKey));
            
            dropdown.RefreshShownValue();
            UpdateKey(dropdown.value);
        }
        
        private void UpdateKey(int position)
        {
            if(dropdown.options.Count == 0)
                return;
            
            Key = dropdown.options[position].text;
            OnValueChanged();
        }
    }
}