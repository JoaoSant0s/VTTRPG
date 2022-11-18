using System;
using System.Collections;
using System.Collections.Generic;

namespace VTTRPG.Values
{
    public abstract class BaseValue<T>
    {
        public event Action<T, T> OnChanged;

        public string id;
        public T value;

        public void ModifyValue(T newValue)
        {
            var previousValue = value;
            this.value = newValue;
            OnChanged?.Invoke( this.value, previousValue);
        }

        public override string ToString()
        {
            return $"{id} - {value}";
        }
    }
}
