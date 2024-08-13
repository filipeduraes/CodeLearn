namespace CodeEditor.Nodes
{
    public class GreaterOrEqualNode : BinaryOperatorNode<float, bool>
    {
        public GreaterOrEqualNode(IValueNode<float> firstOperand, IValueNode<float> secondOperand) : base(firstOperand, secondOperand)
        {
        }
        
        protected override bool CalculateResult(float firstOperand, float secondOperand)
        {
            return firstOperand >= secondOperand;
        }
    }
}