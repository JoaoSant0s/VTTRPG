using System.Collections;
using System.Collections.Generic;

namespace VTTRPG.Values
{
    public class FloatValue : BaseValue<float>
    {
        public FloatValue(string id, float value)
        {
            this.id = id;
            this.value = value;
        }

        public void AddValue(float incremet)
        {
            ModifyValue(this.value + incremet);
        }
    }
}
