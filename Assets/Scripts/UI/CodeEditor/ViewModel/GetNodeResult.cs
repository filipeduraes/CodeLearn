using CodeLearn.CodeEditor;

namespace CodeLearn.UI.CodeEditor.View
{
    public struct GetNodeResult
    {
        public bool wasSuccessful;
        public ErrorType errorType;
        public ICodeNode resultNode;

        public GetNodeResult(ErrorType errorType)
        {
            wasSuccessful = false;
            resultNode = null;
            this.errorType = errorType;
        }

        public GetNodeResult(ICodeNode node)
        {
            wasSuccessful = true;
            errorType = ErrorType.None;
            resultNode = node;
        }
        
        public enum ErrorType
        {
            None,
            InvalidSyntax,
            TypeMismatch,
            EmptyAssignment,
            VariableNotDeclared
        }
    }
}