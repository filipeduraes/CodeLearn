namespace CodeEditor.Nodes
{
    public class SmallerOrEqualNode : BinaryOperatorNode<float, bool>
    {
        public SmallerOrEqualNode(IValueNode<float> firstOperand, IValueNode<float> secondOperand) : base(firstOperand, secondOperand)
        {
        }
        
        protected override bool CalculateResult(float firstOperand, float secondOperand)
        {
            return firstOperand <= secondOperand;
        }
    }
}