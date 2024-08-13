using CodeLearn.CodeEditor;

namespace CodeEditor.Nodes
{
    public abstract class OperatorNode<T> : IValueNode<T>
    {
        protected readonly IValueNode<T> firstOperand;
        protected readonly IValueNode<T> secondOperand;
        protected T result;

        public OperatorNode(IValueNode<T> firstOperand, IValueNode<T> secondOperand)
        {
            this.firstOperand = firstOperand;
            this.secondOperand = secondOperand;
        }
        
        public T GetResultValue()
        {
            return result;
        }

        public abstract void Execute(MemoryModule memoryModule);
    }
}