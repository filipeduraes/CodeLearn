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

        public bool TryGetValue<T>(string key, out T value)
        {
            if (_memoryValues.TryGetValue(key, out object storedValue))
            {
                if (storedValue is T convertedValue)
                {
                    value = convertedValue;
                    return true;
                }
            }

            value = default;
            return false;
        }

        public T GetValue<T>(string key)
        {
            return (T) _memoryValues[key];
        }
    }
}