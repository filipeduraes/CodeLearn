using CodeLearn.CodeEditor;

namespace CodeEditor.Nodes
{
    public class VariableDeclarationNode : ICodeNode
    {
        private readonly string _key;
        private readonly object _defaultValue;

        public VariableDeclarationNode(string key, object defaultValue)
        {
            _key = key;
            _defaultValue = defaultValue;
        }
        
        public void Execute(MemoryModule memoryModule)
        {
            memoryModule.StoreValue(_key, _defaultValue);
        }
    }
}