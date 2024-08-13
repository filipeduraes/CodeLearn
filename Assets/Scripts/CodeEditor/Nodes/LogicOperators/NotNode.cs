namespace CodeEditor.Nodes
{
    public class NotNode : UnaryOperatorNode<bool, bool>
    {
        public NotNode(IValueNode<bool> operand) : base(operand)
        {
        }
        
        protected override bool CalculateResult(bool operand)
        {
            return !operand;
        }
    }
}