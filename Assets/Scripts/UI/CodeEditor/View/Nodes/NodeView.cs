using UnityEngine;

namespace CodeLearn.UI.CodeEditor.View
{
    public abstract class NodeView : MonoBehaviour
    {
        [Header("Base Node View")]
        [SerializeField] private NodeType nodeType;
        [SerializeField] private Color mainColor;
        [SerializeField] private string groupName;
        [SerializeField] private string nodeName;

        public string NodeName => nodeName;
        public string GroupName => groupName;
        public Color MainColor => mainColor;
        public NodeType NodeType => nodeType;

        protected int InitialIndex
        {
            get
            {
                if (transform == null)
                    return -1;
                
                if (transform.GetSiblingIndex() == 0 && _parentNode)
                    return _parentNode.InitialIndex + 1;

                if (transform.GetSiblingIndex() - 1 >= 0)
                {
                    Transform lastChild = _owningContainer.GetChild(transform.GetSiblingIndex() - 1);

                    if (lastChild.TryGetComponent(out NodeView lastNode))
                        return lastNode.FinalIndex + 1;
                }

                return transform.GetSiblingIndex();
            }
        }

        private int FinalIndex
        {
            get
            {
                if (ChildContainer && ChildContainer.transform.childCount > 0)
                {
                    Transform lastChild = ChildContainer.transform.GetChild(ChildContainer.transform.childCount - 1);

                    if (lastChild.TryGetComponent(out NodeView lastNode))
                        return lastNode.FinalIndex;
                }
                
                return InitialIndex;
            }
        }

        protected virtual NodeContainer ChildContainer => null;
        
        private NodeView _parentNode;
        private Transform _owningContainer;

        public virtual bool TryApplyNodeView() => true;

        public void SetParentNode(NodeView parentNode)
        {
            _parentNode = parentNode;
        }

        public void SetOwningContainer(Transform container)
        {
            transform.SetParent(container);
            _owningContainer = container;
        }
    }
}