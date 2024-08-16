using CodeLearn.UI.CodeEditor.ViewModel;
using UnityEngine;

namespace CodeLearn.UI.CodeEditor.View
{
    public class GetVariableNodeView : NodeView
    {
        [SerializeField] private VariablesDropdown dropdown;
        public string Key => dropdown.Key;
        
        public override bool TryApplyNodeView()
        {
            return CodeEditorMemoryHolder.GetVariableCount() > 0;
        }
    }
}