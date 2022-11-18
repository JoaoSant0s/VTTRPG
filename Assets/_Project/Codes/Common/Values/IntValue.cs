using System.Collections;
using System.Collections.Generic;

namespace VTTRPG.Values
{
    [System.Serializable]
    public class IntValue : BaseValue<int>
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
