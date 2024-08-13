using CodeLearn.CodeEditor;

namespace CodeEditor.Nodes
{
    public class VariableAssignmentNode<T> : ICodeNode
    {
        private readonly string _key;
        private readonly IValueNode<T> _valueNode;

        public VariableAssignmentNode(string key, IValueNode<T> valueNode)
        {
            _key = key;
            _valueNode = valueNode;
        }

        public void Execute(MemoryModule memoryModule)
        {
            _valueNode.Execute(memoryModule);
            memoryModule.StoreValue(_key, _valueNode.GetResultValue());
        }
    }
}