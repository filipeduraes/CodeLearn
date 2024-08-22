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
            bool canGet = CodeEditorMemoryHolder.GetVariableCountAtLine(InitialIndex) > 0;

            if (canGet)
            {
                dropdown.SetCodeLineGetter(() => InitialIndex);
                dropdown.Populate();
            }
            
            return canGet;
        }
    }
}