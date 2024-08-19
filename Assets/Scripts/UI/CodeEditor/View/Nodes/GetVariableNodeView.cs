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
            NodeView parentNodeView = transform.parent.GetComponentInParent<NodeView>();
            bool canGet = CodeEditorMemoryHolder.GetVariableCountAtLine(parentNodeView.transform.GetSiblingIndex()) > 0;

            if (canGet)
            {
                dropdown.SetCodeLineGetter(() => parentNodeView.transform.GetSiblingIndex());
                dropdown.Populate();
            }
            
            return canGet;
        }
    }
}