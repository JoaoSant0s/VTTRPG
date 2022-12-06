using System;
using System.Collections;
using System.Collections.Generic;

namespace ObjectValues.CoreValues
{
    public abstract class Value<T>
    {
        public event Action<T, T> OnChanged;

        public string id;
        public T value;

        public void ModifyValue(T newValue)
        {
            var previousValue = value;
            this.value = newValue;
            OnChanged?.Invoke(this.value, previousValue);
        }

        public override string ToString()
        {
            return $"{id} - {value}";
        }
    }
}
