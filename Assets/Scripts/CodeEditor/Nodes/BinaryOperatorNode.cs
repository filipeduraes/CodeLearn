using CodeLearn.CodeEditor;

namespace CodeEditor.Nodes
{
    public abstract class BinaryOperatorNode<TOperand, TResult> : IValueNode<TResult>
    {
        private readonly IValueNode<TOperand> _firstOperand;
        private readonly IValueNode<TOperand> _secondOperand;
        private TResult _result;

        protected BinaryOperatorNode(IValueNode<TOperand> firstOperand, IValueNode<TOperand> secondOperand)
        {
            _firstOperand = firstOperand;
            _secondOperand = secondOperand;
        }

        public void Execute(MemoryModule memoryModule)
        {
            _firstOperand.Execute(memoryModule);
            _secondOperand.Execute(memoryModule);
            _result = CalculateResult(_firstOperand.GetResultValue(), _secondOperand.GetResultValue());
        }
        
        public TResult GetResultValue()
        {
            return _result;
        }
        
        protected abstract TResult CalculateResult(TOperand firstOperand, TOperand secondOperand);
    }
}