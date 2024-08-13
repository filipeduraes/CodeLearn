using CodeLearn.CodeEditor;

namespace CodeEditor.Nodes
{
    public class VariableGetNode<T> : IValueNode<T>
    {
        private readonly string _key;
        private T _result;

        public VariableGetNode(string key)
        {
            _key = key;
        }

        public void Execute(MemoryModule memoryModule)
        {
            _result = memoryModule.GetValue<T>(_key);
        }
        
        public T GetResultValue()
        {
            return _result;
        }
    }
}