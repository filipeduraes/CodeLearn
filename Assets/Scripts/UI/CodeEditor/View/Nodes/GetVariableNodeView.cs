using CodeLearn.UI.CodeEditor.ViewModel;
using UnityEngine;

namespace CodeLearn.UI.CodeEditor.View
{
    public class GetVariableNodeView : MonoBehaviour, INodeView
    {
        public bool TryApplyNodeView()
        {
            return CodeEditorMemoryHolder.GetVariableCount() > 0;
        }
    }
}