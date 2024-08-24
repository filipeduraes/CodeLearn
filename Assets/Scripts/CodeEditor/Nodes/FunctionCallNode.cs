using System;
using CodeLearn.CodeEditor;

namespace CodeEditor.Nodes
{
    public class FunctionCallNode : ICodeNode
    {
        private readonly string _key;

        public FunctionCallNode(string key)
        {
            _key = key;
        }
        
        public void Execute(MemoryModule memoryModule)
        {
            if(memoryModule.TryGetValue(_key, out Action function))
                function();
        }
    }
}