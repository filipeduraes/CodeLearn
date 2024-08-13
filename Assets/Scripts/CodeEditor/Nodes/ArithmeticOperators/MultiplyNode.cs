using CodeLearn.CodeEditor;

namespace CodeEditor.Nodes
{
    public class MultiplyNode : OperatorNode<float>
    {
        public MultiplyNode(IValueNode<float> firstOperand, IValueNode<float> secondOperand) : base(firstOperand, secondOperand)
        {
        }
        
        public override void Execute(MemoryModule memoryModule)
        {
            firstOperand.Execute(memoryModule);
            secondOperand.Execute(memoryModule);
            result = firstOperand.GetResultValue() * secondOperand.GetResultValue();
        }
    }
}