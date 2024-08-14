using UnityEngine;

namespace CodeLearn.UI.CodeEditor.View
{
    public class NodeContainer : MonoBehaviour
    {
        [SerializeField] private RectTransform containerParent;
        [SerializeField] private NodeType nodeType;

        public RectTransform ContainerParent => containerParent;
        public NodeType NodeType => nodeType;

        public void SetNodeType(NodeType type)
        {
            if (type != nodeType)
            {
                for (int i = containerParent.childCount - 1; i >= 0; i--)
                    Destroy(containerParent.GetChild(i).gameObject);
                
                nodeType = type;
            }
        }
    }
}