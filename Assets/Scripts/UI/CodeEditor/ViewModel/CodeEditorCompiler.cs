using System.Collections.Generic;
using CodeLearn.CodeEditor;
using CodeLearn.UI.CodeEditor.View;
using UnityEngine;

namespace CodeLearn.UI.CodeEditor.ViewModel
{
    public class CodeEditorCompiler : MonoBehaviour
    {
        [SerializeField] private RectTransform codeContainer;

        public void Compile()
        {
            List<ICodeNode> code = new();
            
            for (int i = 0; i < codeContainer.childCount; i++)
            {
                Transform child = codeContainer.GetChild(i);
                
                if (child.TryGetComponent(out IBaseNodeView nodeView))
                {
                    GetNodeResult nodeResult = nodeView.TryGetNode();

                    if (!nodeResult.wasSuccessful)
                    {
                        ThrowCompileError(i, nodeResult);
                        return;
                    }
                    
                    code.Add(nodeResult.resultNode);
                }
            }

            CodeInterpreter interpreter = new(code);
            interpreter.Run();
        }

        private void ThrowCompileError(int line, GetNodeResult nodeResult)
        {
            Debug.Log($"Compile error: {nodeResult.errorType} at line {line}");
        }
    }
}