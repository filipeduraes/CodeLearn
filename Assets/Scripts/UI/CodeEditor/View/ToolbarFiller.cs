using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CodeLearn.UI.CodeEditor.View
{
    public class ToolbarFiller : MonoBehaviour
    {
        [SerializeField] private NodeView[] nodeViews;
        [SerializeField] private TMP_Text groupTitle;
        [SerializeField] private ToolbarItem toolbarItemPrefab;
        [SerializeField] private RectTransform toolbarArea;
        [SerializeField] private RectTransform dragArea;

        private void Awake()
        {
            Dictionary<string, List<NodeView>> groupedNodeViews = new();

            foreach (NodeView view in nodeViews)
            {
                if (!groupedNodeViews.ContainsKey(view.GroupName))
                    groupedNodeViews[view.GroupName] = new List<NodeView>();
                
                groupedNodeViews[view.GroupName].Add(view);
            }

            CreateGroups(groupedNodeViews);
        }

        private void CreateGroups(Dictionary<string,List<NodeView>> groupedNodeViews)
        {
            foreach ((string title, List<NodeView> views) in groupedNodeViews)
            {
                TMP_Text titleInstance = Instantiate(groupTitle, toolbarArea);
                titleInstance.SetText(title);

                foreach (NodeView view in views)
                {
                    ToolbarItem toolbarItem = Instantiate(toolbarItemPrefab, toolbarArea);
                    toolbarItem.Populate(view.NodeType, view.NodeName, view, dragArea, view.MainColor);
                }
            }
        }
    }
}