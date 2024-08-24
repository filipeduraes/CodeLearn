using System;
using System.Collections;
using System.Collections.Generic;
using CodeLearn.CodeEditor;
using CodeLearn.UI.CodeEditor.View;
using UnityEngine;
using UnityEngine.Events;

namespace CodeLearn.UI.CodeEditor.ViewModel
{
    public class CodeEditorCompiler : MonoBehaviour
    {
        [SerializeField] private RectTransform codeContainer;
        [SerializeField] private UnityEvent onRunStarted;
        [SerializeField] private UnityEvent onRunStopped;

        public event Action<MemoryModule> OnCompile = delegate { };
        public event Action OnInterpreterLoop = delegate { };
        public event Action OnExecutionStopped = delegate { };
        
        private CodeInterpreter _interpreter;
        private Coroutine _runLoop;

        public void Compile()
        {
            List<ICodeNode> code = new();
            
            for (int i = 0; i < codeContainer.childCount; i++)
            {
                Transform child = codeContainer.GetChild(i);
                
                if (child.TryGetComponent(out IBaseNodeView nodeView) && !nodeView.IgnoreCompilation())
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

            _interpreter = new CodeInterpreter(code);
            OnCompile(_interpreter.MemoryModule);
            _runLoop = StartCoroutine(RunLoop());
        }

        public void Stop()
        {
            if (_runLoop != null)
                StopCoroutine(_runLoop);
            
            onRunStopped.Invoke();
            OnExecutionStopped();
        }

        private IEnumerator RunLoop()
        {
            onRunStarted.Invoke();
            
            while (true)
            {
                _interpreter.Run();
                OnInterpreterLoop();
                yield return new WaitForSeconds(0.5f);
            }
        }

        private void ThrowCompileError(int line, GetNodeResult nodeResult)
        {
            Debug.Log($"Compile error: {nodeResult.errorType} at line {line}");
        }
    }
}