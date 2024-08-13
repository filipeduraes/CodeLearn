using CodeLearn.CodeEditor;

namespace CodeEditor.Nodes
{
    public abstract class UnaryOperatorNode<TOperand, TResult> : IValueNode<TResult>
    {
        private readonly IValueNode<TOperand> _operand;
        private TResult _result;

        protected UnaryOperatorNode(IValueNode<TOperand> operand)
        {
            _operand = operand;
        }
        
        public void Execute(MemoryModule memoryModule)
        {
            _operand.Execute(memoryModule);
            _result = CalculateResult(_operand.GetResultValue());
        }

        public TResult GetResultValue()
        {
            return _result;
        }
        
        protected abstract TResult CalculateResult(TOperand operand);
    }
}