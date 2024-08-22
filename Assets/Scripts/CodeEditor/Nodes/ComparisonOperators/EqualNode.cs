using UnityEngine;

namespace CodeEditor.Nodes
{
    public class EqualNode : BinaryOperatorNode<float, bool>
    {
        public EqualNode(IValueNode<float> firstOperand, IValueNode<float> secondOperand) : base(firstOperand, secondOperand)
        {
        }
        
        protected override bool CalculateResult(float firstOperand, float secondOperand)
        {
            return Mathf.Abs(firstOperand - secondOperand) <= 0.001f;
        }
    }
}