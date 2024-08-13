using CodeLearn.CodeEditor;

namespace CodeEditor.Nodes.Literals
{
    public class LogicalLiteralNode : IValueNode<bool>
    {
        private readonly bool _value;

        public LogicalLiteralNode(bool value)
        {
            _value = value;
        }

        public bool GetResultValue()
        {
            return _value;
        }

        public void Execute(MemoryModule memoryModule)
        {
            //TODO: Interface Segregation Principle
        }
    }
}