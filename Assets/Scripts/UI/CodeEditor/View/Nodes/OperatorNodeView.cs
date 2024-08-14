using UnityEngine;

namespace CodeLearn.UI.CodeEditor.View
{
    public class OperatorNodeView : MonoBehaviour, INodeView
    {
        [SerializeField] private OperatorType operatorType;
        
        public enum OperatorType
        {
            Sum,
            Subtraction,
            Multiplication,
            Division
        }
        
        public bool TryApplyNodeView()
        {
            return transform.parent.childCount == 1;
        }
    }
}