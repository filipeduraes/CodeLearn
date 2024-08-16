using TMPro;
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

        public virtual bool TryApplyNodeView() => true;
    }
}