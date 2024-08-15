using CodeLearn.UI.CodeEditor.ViewModel;
using UnityEngine;

namespace CodeLearn.UI.CodeEditor.View
{
    public class GetVariableNodeView : MonoBehaviour, INodeView
    {
        [SerializeField] private VariablesDropdown dropdown;
        public string Key => dropdown.Key;
        
        public bool TryApplyNodeView()
        {
            return CodeEditorMemoryHolder.GetVariableCount() > 0;
        }
    }
}