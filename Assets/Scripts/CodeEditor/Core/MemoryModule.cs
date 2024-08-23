using System;
using System.Collections.Generic;

namespace CodeLearn.CodeEditor
{
    public class MemoryModule
    {
        public event Action<string, object> OnValueChanged = delegate { }; 
        private readonly Dictionary<string, object> _memoryValues = new();

        public void StoreValue(string key, object value)
        {
            _memoryValues[key] = value;
            OnValueChanged(key, value);
        }

        public T GetValue<T>(string key)
        {
            return (T) _memoryValues[key];
        }
    }
}