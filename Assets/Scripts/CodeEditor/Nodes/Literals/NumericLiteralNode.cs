using CodeLearn.CodeEditor;

namespace CodeEditor.Nodes.Literals
{
    public class NumericLiteralNode : IValueNode<float>
    {
        private readonly float _literalValue;

        public NumericLiteralNode(float literalValue)
        {
            _literalValue = literalValue;
        }
        
        public float GetResultValue()
        {
            return _literalValue;
        }

        public void Execute(MemoryModule memoryModule)
        {
            //TODO: Interface Segregation Principle
        }
    }
}