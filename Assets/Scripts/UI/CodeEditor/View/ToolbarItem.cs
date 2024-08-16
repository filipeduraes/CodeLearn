using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeLearn.UI.CodeEditor.View
{
    public class ToolbarItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private NodeType nodeType;
        [SerializeField] private RectTransform template;
        [SerializeField] private RectTransform parent;
        [SerializeField] private Image background;
        [SerializeField] private TMP_Text nodeName;
        
        private RectTransform _templateInstance;
        private CanvasGroup _instanceCanvasGroup;
        private NodeContainer _currentContainer;

        public void Populate(NodeType type, string nodeName, RectTransform template, RectTransform parent, Color baseColor)
        {
            nodeType = type;
            this.nodeName.SetText(nodeName);
            this.template = template;
            this.parent = parent;
            background.color = baseColor;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _templateInstance = Instantiate(template, parent);
            _templateInstance.position = eventData.position;
            
            _instanceCanvasGroup = _templateInstance.gameObject.AddComponent<CanvasGroup>();
            _instanceCanvasGroup.blocksRaycasts = false;
            _instanceCanvasGroup.alpha = 0.6f;
        }

        public void OnDrag(PointerEventData eventData)
        {
            List<RaycastResult> results = new();
            PointerEventData pointerEventData = new(EventSystem.current) { position = eventData.position };
            EventSystem.current.RaycastAll(pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.TryGetComponent(out NodeContainer container) && (nodeType & container.NodeType) != 0)
                {
                    _currentContainer = container;
                    _templateInstance.SetParent(container.ContainerParent);
                    return;
                }
            }

            _currentContainer = null;
            _templateInstance.SetParent(parent);
            _templateInstance.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Destroy(_instanceCanvasGroup);

            if (_currentContainer == null)
            {
                Destroy(_templateInstance.gameObject);
            }
            else if(_templateInstance.TryGetComponent(out NodeView nodeView))
            {
                if(!nodeView.TryApplyNodeView())
                    Destroy(_templateInstance.gameObject);
            }
        }
    }
}