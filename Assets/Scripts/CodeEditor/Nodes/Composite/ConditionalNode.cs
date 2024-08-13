using System.Collections.Generic;
using CodeLearn.CodeEditor;

namespace CodeEditor.Nodes
{
    public class ConditionalNode : ICodeNode
    {
        private readonly IValueNode<bool> _condition;
        private readonly List<ICodeNode> _codeBlock;

        public ConditionalNode(IValueNode<bool> condition, List<ICodeNode> codeBlock)
        {
            _condition = condition;
            _codeBlock = codeBlock;
        }
        
        public void Execute(MemoryModule memoryModule)
        {
            if (!_condition.GetResultValue()) 
                return;
            
            foreach (ICodeNode codeNode in _codeBlock)
                codeNode.Execute(memoryModule);
        }
    }
}