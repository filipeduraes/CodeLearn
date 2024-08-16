using System;
using TMPro;
using UnityEngine;

namespace CodeLearn.UI.CodeEditor.ViewModel
{
    public class VariablesDropdown : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown;
        public event Action OnValueChanged = delegate { };
        
        public string Key { get; private set; }

        private void Awake()
        {
            Populate();
        }

        private void OnEnable()
        {
            UpdateKey(dropdown.value);
            dropdown.onValueChanged.AddListener(UpdateKey);
            CodeEditorMemoryHolder.OnVariableListChanged += Populate;
        }

        private void OnDisable()
        {
            dropdown.onValueChanged.RemoveListener(UpdateKey);
            CodeEditorMemoryHolder.OnVariableListChanged -= Populate;
        }

        private void Populate()
        {
            dropdown.options.Clear();
            
            foreach (string variableKey in CodeEditorMemoryHolder.GetVariableKeys())
                dropdown.options.Add(new TMP_Dropdown.OptionData(variableKey));
            
            dropdown.RefreshShownValue();
            UpdateKey(dropdown.value);
        }
        
        private void UpdateKey(int position)
        {
            Key = dropdown.options[position].text;
            OnValueChanged();
        }
    }
}