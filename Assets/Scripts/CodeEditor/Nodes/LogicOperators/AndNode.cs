namespace CodeEditor.Nodes
{
    public class AndNode : BinaryOperatorNode<bool, bool>
    {
        public AndNode(IValueNode<bool> firstOperand, IValueNode<bool> secondOperand) : base(firstOperand, secondOperand)
        {
        }
        
        protected override bool CalculateResult(bool firstOperand, bool secondOperand)
        {
            return firstOperand && secondOperand;
        }
    }
}