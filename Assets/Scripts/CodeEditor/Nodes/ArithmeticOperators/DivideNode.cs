using CodeLearn.CodeEditor;

namespace CodeEditor.Nodes
{
    public class DivideNode : OperatorNode<float>
    {
        public DivideNode(IValueNode<float> firstOperand, IValueNode<float> secondOperand) : base(firstOperand, secondOperand)
        {
        }
        
        public override void Execute(MemoryModule memoryModule)
        {
            firstOperand.Execute(memoryModule);
            secondOperand.Execute(memoryModule);
            result = firstOperand.GetResultValue() / secondOperand.GetResultValue();
        }
    }
}