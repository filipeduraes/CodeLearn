using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeLearn.UI.CodeEditor.ViewModel
{
    public static class CodeEditorMemoryHolder
    {
        public static event Action OnVariableListChanged = delegate { };
        
        private static readonly Dictionary<string, (Type variableType, Func<int> lineGetter)> Variables = new();

        public static void SetVariable(string lastKey, string newKey, Func<int> lineGetter, Type variableType)
        {
            if (Variables.ContainsKey(lastKey))
                Variables.Remove(lastKey);

            Variables[newKey] = (variableType, lineGetter);
            OnVariableListChanged();
        }

        public static void ClearVariable(string key)
        {
            Variables.Remove(key);
            OnVariableListChanged();
        }

        public static Type GetVariableType(string key)
        {
            return Variables[key].variableType;
        }
        
        public static int GetVariableLine(string key)
        {
            return Variables[key].lineGetter();
        }

        public static int GetVariableCountAtLine(int line)
        {
            return GetVariableKeysDeclaredAtLine(line).Count();
        }

        public static IEnumerable<string> GetVariableKeysDeclaredAtLine(int line)
        {
            foreach ((string key, (Type _, Func<int> lineGetter)) in Variables)
            {
                if (lineGetter() < line)
                    yield return key;
            }
        }

        public static bool HasVariableAtLine(string key, int line)
        {
            if (!HasVariable(key)) 
                return false;
            
            int variableLine = Variables[key].lineGetter();
                
            if (variableLine >= 0) 
                return variableLine <= line;
                
            Variables.Remove(key);
            return false;
        }
        
        public static bool HasVariable(string key)
        {
            return Variables.ContainsKey(key);
        }
    }
}