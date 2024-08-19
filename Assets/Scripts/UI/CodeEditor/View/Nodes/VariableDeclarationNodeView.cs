﻿using CodeEditor.Nodes;
using CodeLearn.UI.CodeEditor.ViewModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeLearn.UI.CodeEditor.View
{
    public class VariableDeclarationNodeView : NodeView, IBaseNodeView
    {
        [Header("Declaration")]
        [SerializeField] private Button numericTypeButton;
        [SerializeField] private Button logicTypeButton;
        [SerializeField] private TMP_InputField variableNameInput;
        
        [Header("Colors")]
        [SerializeField] private Color enabledColor;
        [SerializeField] private Color disabledColor;

        private bool _isNumeric = true;
        private string _currentKey;

        private void OnEnable()
        {
            variableNameInput.onValidateInput = ValidateInput;
            variableNameInput.onEndEdit.AddListener(UpdateVariableName);
            numericTypeButton.onClick.AddListener(UpdateNumericClick);
            logicTypeButton.onClick.AddListener(UpdateLogicClick);
        }

        private void OnDisable()
        {
            variableNameInput.onEndEdit.RemoveListener(UpdateVariableName);
            numericTypeButton.onClick.RemoveListener(UpdateNumericClick);
            logicTypeButton.onClick.RemoveListener(UpdateLogicClick);
        }

        private void OnDestroy()
        {
            if(!string.IsNullOrEmpty(_currentKey))
                CodeEditorMemoryHolder.ClearVariable(_currentKey);
        }

        public GetNodeResult TryGetNode()
        {
            return new GetNodeResult(new VariableDeclarationNode(_currentKey, _isNumeric ? 0f : false));
        }

        public override void CopyTo(NodeView newNode)
        {
            if (newNode is VariableDeclarationNodeView variableDeclarationNodeView)
            {
                variableDeclarationNodeView._currentKey = _currentKey;
                variableDeclarationNodeView._isNumeric = _isNumeric;
            }
        }

        public override bool TryApplyNodeView()
        {
            if (_currentKey != null)
                return true;
            
            _currentKey = variableNameInput.text;
            int number = 0;

            while(CodeEditorMemoryHolder.HasVariable($"{_currentKey}{number}"))
                number++;
            
            _currentKey = $"{_currentKey}{number}";
            variableNameInput.SetTextWithoutNotify(_currentKey);
            
            CodeEditorMemoryHolder.SetVariable(_currentKey, _currentKey, () => transform.GetSiblingIndex(), _isNumeric ? typeof(float) : typeof(bool));
            UpdateButtons();

            return true;
        }

        private void UpdateNumericClick()
        {
            if (!_isNumeric)
            {
                _isNumeric = true;
                CodeEditorMemoryHolder.SetVariable(_currentKey, _currentKey, () => transform.GetSiblingIndex(), typeof(float));
                UpdateButtons();
            }
        }
        
        private void UpdateLogicClick()
        {
            if (_isNumeric)
            {
                _isNumeric = false;
                CodeEditorMemoryHolder.SetVariable(_currentKey, _currentKey, () => transform.GetSiblingIndex(), typeof(bool));
                UpdateButtons();
            }
        }

        private void UpdateVariableName(string newName)
        {
            CodeEditorMemoryHolder.SetVariable(_currentKey, newName, () => transform.GetSiblingIndex(), _isNumeric ? typeof(float) : typeof(bool));
            _currentKey = newName;
        }

        private char ValidateInput(string text, int charindex, char addedchar)
        {
            if (!char.IsLetterOrDigit(addedchar))
                return char.MinValue;

            return addedchar;
        }
        
        private void UpdateButtons()
        {
            numericTypeButton.image.color = _isNumeric ? enabledColor : disabledColor;
            logicTypeButton.image.color = !_isNumeric ? enabledColor : disabledColor;
        }
    }
}