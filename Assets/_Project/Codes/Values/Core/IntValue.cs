using System;
using System.Collections;
using System.Collections.Generic;

namespace ObjectValues.CoreValues
{
    [Serializable]
    public class IntValue : Value<int>
    {
        public IntValue(string id, int value)
        {
            this.id = id;
            this.value = value;
        }

        public void AddValue(int incremet)
        {
            ModifyValue(this.value + incremet);
        }
    }
}
