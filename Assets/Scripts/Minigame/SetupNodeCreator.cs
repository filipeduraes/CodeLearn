﻿using CodeLearn.UI.CodeEditor.View;
using UnityEngine;

namespace CodeLearn.Minigame
{
    public class SetupNodeCreator : MonoBehaviour
    {
        [SerializeField] private NodeContainer container;
        [SerializeField] private VariableDeclarationNodeView declarationNodeViewTemplate;
        [SerializeField] private MinigameSetup testSetup;

        private void Awake()
        {
            Setup(testSetup);
        }

        public void Setup(MinigameSetup setup)
        {
            foreach (VariableDeclaration variableDeclaration in setup.VariableDeclarations)
            {
                VariableDeclarationNodeView declarationNodeViewInstance = Instantiate(declarationNodeViewTemplate);
                declarationNodeViewInstance.Populate(variableDeclaration.Name, variableDeclaration.Type == VariableDeclaration.VariableType.Numeric);
                container.FitNodeView(declarationNodeViewInstance);
                declarationNodeViewInstance.TryApplyNodeView();
            }
        }
    }
}