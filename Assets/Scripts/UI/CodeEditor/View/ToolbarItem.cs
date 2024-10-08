using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeLearn.UI.CodeEditor.View
{
    public class ToolbarItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("Toolbar Items")]
        [SerializeField] private NodeView template;
        [SerializeField] private RectTransform parent;
        [SerializeField] private Image background;
        [SerializeField] private TMP_Text nodeName;
        
        [Header("Draggable Nodes")]
        [SerializeField] private bool destroyOnDrag;
        [SerializeField] private NodeView rootParent;
        
        private NodeView _templateInstance;
        private CanvasGroup _instanceCanvasGroup;
        private NodeContainer _currentContainer;

        public void Populate(string nodeName, NodeView template, RectTransform parent, Color baseColor)
        {
            this.nodeName.SetText(nodeName);
            this.template = template;
            this.parent = parent;
            background.color = baseColor;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (destroyOnDrag)
            {
                _templateInstance = rootParent;
                _templateInstance.transform.SetParent(parent);
            }
            else
            {
                _templateInstance = Instantiate(template, parent);
            }
            
            _templateInstance.transform.position = eventData.position;
            _instanceCanvasGroup = _templateInstance.gameObject.AddComponent<CanvasGroup>();
            _instanceCanvasGroup.blocksRaycasts = false;
            _instanceCanvasGroup.alpha = 0.5f;

            ToolbarItem toolbarItem = _templateInstance.GetComponentInChildren<ToolbarItem>();

            if (toolbarItem)
                toolbarItem.parent = parent;
        }

        public void OnDrag(PointerEventData eventData)
        {
            List<RaycastResult> results = new();
            PointerEventData pointerEventData = new(EventSystem.current) { position = eventData.position };
            EventSystem.current.RaycastAll(pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.TryGetComponent(out NodeContainer container) && (_templateInstance.NodeType & container.NodeType) != 0)
                {
                    _currentContainer = container;
                    _currentContainer.FitNodeView(_templateInstance, eventData.position);
                    return;
                }
            }

            _currentContainer = null;
            _templateInstance.transform.SetParent(parent);
            _templateInstance.transform.position = eventData.position;
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