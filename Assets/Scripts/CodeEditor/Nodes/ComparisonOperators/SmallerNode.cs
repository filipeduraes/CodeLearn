namespace CodeEditor.Nodes
{
    public class SmallerNode : BinaryOperatorNode<float, bool>
    {
        public SmallerNode(IValueNode<float> firstOperand, IValueNode<float> secondOperand) : base(firstOperand, secondOperand)
        {
        }
        
        protected override bool CalculateResult(float firstOperand, float secondOperand)
        {
            return firstOperand < secondOperand;
        }
    }
}