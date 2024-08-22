using System.Collections.Generic;
using CodeLearn.CodeEditor;

namespace CodeEditor.Nodes
{
    public class WhileNode : ICodeNode
    {
        private readonly IValueNode<bool> _condition;
        private readonly List<ICodeNode> _codeBlock;

        public WhileNode(IValueNode<bool> condition, List<ICodeNode> codeBlock)
        {
            _condition = condition;
            _codeBlock = codeBlock;
        }
        
        public void Execute(MemoryModule memoryModule)
        {
            _condition.Execute(memoryModule);
            
            while (_condition.GetResultValue())
            {
                foreach (ICodeNode codeNode in _codeBlock)
                    codeNode.Execute(memoryModule);
                
                _condition.Execute(memoryModule);
            } 
        }
    }
}