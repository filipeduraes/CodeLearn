namespace CodeEditor.Nodes
{
    public class SubtractionNode : BinaryOperatorNode<float, float>
    {
        public SubtractionNode(IValueNode<float> firstOperand, IValueNode<float> secondOperand) : base(firstOperand, secondOperand)
        {
        }
        
        protected override float CalculateResult(float firstOperand, float secondOperand)
        {
            return firstOperand - secondOperand;
        }
    }
}