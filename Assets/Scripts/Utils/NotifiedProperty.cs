using System;

namespace CodeLearn.Utils
{
    public class NotifiedProperty<T>
    {
        public event Action<T> OnValueChanged = delegate { };
        private T _value;

        public void Set(T value)
        {
            _value = value;

            if (!value.Equals(_value))
                OnValueChanged(_value);
        }

        public T Get()
        {
            return _value;
        }

        public static implicit operator T(NotifiedProperty<T> notifiedProperty)
        {
            return notifiedProperty.Get();
        }
    }
}