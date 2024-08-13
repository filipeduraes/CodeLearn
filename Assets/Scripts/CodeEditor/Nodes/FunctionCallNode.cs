using System;
using CodeLearn.CodeEditor;

namespace CodeEditor.Nodes
{
    public class FunctionCallNode : ICodeNode
    {
        private readonly Action<MemoryModule> _functionCall;

        public FunctionCallNode(Action<MemoryModule> functionCall)
        {
            _functionCall = functionCall;
        }
        
        public void Execute(MemoryModule memoryModule)
        {
            _functionCall(memoryModule);
        }
    }
}