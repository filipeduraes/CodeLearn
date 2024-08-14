using TMPro;
using UnityEngine;

namespace CodeLearn.UI.CodeEditor.ViewModel
{
    public class VariablesDropdown : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown;

        private void Awake()
        {
            Populate();
        }

        private void OnEnable()
        {
            CodeEditorMemoryHolder.OnVariableListChanged += Populate;
        }

        private void OnDisable()
        {
            CodeEditorMemoryHolder.OnVariableListChanged -= Populate;
        }

        private void Populate()
        {
            dropdown.options.Clear();
            
            foreach (string variableKey in CodeEditorMemoryHolder.GetVariableKeys())
                dropdown.options.Add(new TMP_Dropdown.OptionData(variableKey));
        }
    }
}