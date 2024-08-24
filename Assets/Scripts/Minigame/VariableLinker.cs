using System;
using System.Collections.Generic;
using CodeLearn.CodeEditor;

namespace CodeLearn.Minigame
{
    public class VariableLinker
    {
        private readonly Dictionary<string, Action<object>> _subscribedValues = new();
        private readonly MemoryModule _memoryModule;

        public VariableLinker(MemoryModule memoryModule)
        {
            _memoryModule = memoryModule;
            _memoryModule.OnValueChanged += SendMemorySignal;
        }

        ~VariableLinker()
        {
            _memoryModule.OnValueChanged -= SendMemorySignal;
        }

        public void SubscribeSignalReceiver(string key, Action<object> updateValue)
        {
            _subscribedValues[key] = updateValue;
        }

        private void SendMemorySignal(string key, object value)
        {
            if(_subscribedValues.TryGetValue(key, out Action<object> subscribedValue))
                subscribedValue(value);
        }
    }
}