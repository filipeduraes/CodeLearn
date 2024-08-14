using System;
using System.Collections.Generic;

namespace CodeLearn.UI.CodeEditor.ViewModel
{
    public static class CodeEditorMemoryHolder
    {
        private static readonly Dictionary<string, Type> Variables = new();

        public static event Action OnVariableListChanged = delegate { };

        public static void SetVariable(string lastKey, string newKey, Type variableType)
        {
            if (Variables.ContainsKey(lastKey))
                Variables.Remove(lastKey);

            Variables[newKey] = variableType;
            OnVariableListChanged();
        }

        public static Type GetVariableType(string key)
        {
            return Variables[key];
        }

        public static int GetVariableCount()
        {
            return Variables.Count;
        }

        public static IEnumerable<string> GetVariableKeys()
        {
            return Variables.Keys;
        }

        public static bool HasVariable(string key)
        {
            return Variables.ContainsKey(key);
        }
    }
}