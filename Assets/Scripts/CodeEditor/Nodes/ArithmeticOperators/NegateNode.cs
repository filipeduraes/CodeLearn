namespace CodeEditor.Nodes
{
    public class NegateNode : UnaryOperatorNode<float, float>
    {
        public NegateNode(IValueNode<float> operand) : base(operand)
        {
        }
        
        protected override float CalculateResult(float operand)
        {
            return -operand;
        }
    }
}