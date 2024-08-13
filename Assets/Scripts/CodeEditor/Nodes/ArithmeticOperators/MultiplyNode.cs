namespace CodeEditor.Nodes
{
    public class MultiplyNode : BinaryOperatorNode<float, float>
    {
        public MultiplyNode(IValueNode<float> firstOperand, IValueNode<float> secondOperand) : base(firstOperand, secondOperand)
        {
        }
        
        protected override float CalculateResult(float firstOperand, float secondOperand)
        {
            return firstOperand * secondOperand;
        }
    }
}