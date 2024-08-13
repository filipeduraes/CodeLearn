using System.Collections.Generic;

namespace CodeLearn.CodeEditor
{
    public class MemoryModule
    {
        private readonly Dictionary<string, object> _memoryValues = new();

        public void StoreValue(string key, object value)
        {
            _memoryValues[key] = value;
        }

        public T GetValue<T>(string key)
        {
            return (T) _memoryValues[key];
        }
    }
}