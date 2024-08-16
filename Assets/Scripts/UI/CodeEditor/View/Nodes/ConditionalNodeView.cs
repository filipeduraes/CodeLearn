using System.Collections.Generic;
using CodeEditor.Nodes;
using CodeLearn.CodeEditor;
using UnityEngine;

namespace CodeLearn.UI.CodeEditor.View
{
    public class ConditionalNodeView : NodeView, IBaseNodeView
    {
        [Header("Group Node")]
        [SerializeField] private NodeContainer conditionContainer;
        [SerializeField] private NodeContainer codeBlockContainer;

        public GetNodeResult TryGetNode()
        {
            GetNodeResult conditionResult = conditionContainer.TryGetNode();

            if (!conditionResult.wasSuccessful || conditionResult.resultNode is not IValueNode<bool> conditionNode)
                return conditionResult;
            
            List<ICodeNode> codeBlock = new();

            for (int i = 0; i < codeBlockContainer.transform.childCount; i++)
            {
                Transform child = codeBlockContainer.transform.GetChild(i);

                if (child.TryGetComponent(out IBaseNodeView baseNodeView))
                {
                    GetNodeResult childNodeResult = baseNodeView.TryGetNode();

                    if (!childNodeResult.wasSuccessful)
                        return childNodeResult;
                    
                    codeBlock.Add(childNodeResult.resultNode);
                }
            }

            return new GetNodeResult(new ConditionalNode(conditionNode, codeBlock));
        }
    }
}