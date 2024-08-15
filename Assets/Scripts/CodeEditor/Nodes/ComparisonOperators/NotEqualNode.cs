using UnityEngine;

namespace CodeEditor.Nodes
{
    public class NotEqualNode : BinaryOperatorNode<float, bool>
    {
        public NotEqualNode(IValueNode<float> firstOperand, IValueNode<float> secondOperand) : base(firstOperand, secondOperand)
        {
        }
        
        protected override bool CalculateResult(float firstOperand, float secondOperand)
        {
            return Mathf.Abs(firstOperand - secondOperand) > 0.001f;
        }
    }
}