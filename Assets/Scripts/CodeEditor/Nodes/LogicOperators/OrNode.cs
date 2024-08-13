namespace CodeEditor.Nodes
{
    public class OrNode : BinaryOperatorNode<bool, bool>
    {
        public OrNode(IValueNode<bool> firstOperand, IValueNode<bool> secondOperand) : base(firstOperand, secondOperand)
        {
        }
        
        protected override bool CalculateResult(bool firstOperand, bool secondOperand)
        {
            return firstOperand || secondOperand;
        }
    }
}