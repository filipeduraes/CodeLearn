using CodeEditor.Nodes;
using UnityEngine;

namespace CodeLearn.UI.CodeEditor.View
{
    public class FunctionNodeView : NodeView, IBaseNodeView
    {
        [SerializeField] private string functionKey;

        public GetNodeResult TryGetNode()
        {
            return new GetNodeResult(new FunctionCallNode(functionKey));
        }

        public bool IgnoreCompilation()
        {
            return false;
        }
    }
}