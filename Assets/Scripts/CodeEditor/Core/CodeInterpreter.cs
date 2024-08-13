using System.Collections.Generic;

namespace CodeLearn.CodeEditor
{
    public class CodeInterpreter
    {
        public MemoryModule MemoryModule { get; } = new();
        private readonly List<ICodeNode> _nodes;

        public CodeInterpreter(List<ICodeNode> nodes)
        {
            _nodes = nodes;
        }
        
        public void Run()
        {
            foreach (ICodeNode node in _nodes)
            {
                node.Execute(MemoryModule);
            }
        }
    }
}