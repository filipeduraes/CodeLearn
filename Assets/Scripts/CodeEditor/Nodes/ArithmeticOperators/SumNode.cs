namespace CodeEditor.Nodes
{
    public class SumNode : BinaryOperatorNode<float, float>
    {
        public SumNode(IValueNode<float> firstOperand, IValueNode<float> secondOperand) : base(firstOperand, secondOperand)
        {
        }
        
        protected override float CalculateResult(float firstOperand, float secondOperand)
        {
            return firstOperand + secondOperand;
        }
    }
}