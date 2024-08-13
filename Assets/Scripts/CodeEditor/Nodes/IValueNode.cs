using CodeLearn.CodeEditor;

namespace CodeEditor.Nodes
{
    public interface IValueNode<out T> : ICodeNode
    {
        T GetResultValue();
    }
}