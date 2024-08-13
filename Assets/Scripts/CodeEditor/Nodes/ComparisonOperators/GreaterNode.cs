namespace CodeEditor.Nodes
{
    public class GreaterNode : BinaryOperatorNode<float, bool>
    {
        public GreaterNode(IValueNode<float> firstOperand, IValueNode<float> secondOperand) : base(firstOperand, secondOperand)
        {
        }
        
        protected override bool CalculateResult(float firstOperand, float secondOperand)
        {
            return firstOperand > secondOperand;
        }
    }
}