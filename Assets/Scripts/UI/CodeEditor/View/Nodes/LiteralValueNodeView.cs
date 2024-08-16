using CodeEditor.Nodes.Literals;
using CodeLearn.CodeEditor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeLearn.UI.CodeEditor.View
{
    public class LiteralValueNodeView : NodeView
    {
        [SerializeField] private TMP_InputField numericValueField;
        [SerializeField] private Toggle logicValueField;
        
        public ICodeNode GetLiteralValue()
        {
            if (numericValueField && float.TryParse(numericValueField.text, out float value))
                return new NumericLiteralNode(value);

            if (logicValueField)
                return new LogicalLiteralNode(logicValueField.isOn);

            return null;
        }

        public override bool TryApplyNodeView()
        {
            if (transform.parent.TryGetComponent(out NodeContainer container))
                return (container.NodeType & NodeType) != 0;

            return false;
        }
    }
}