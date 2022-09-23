using System.Collections;
using System.Collections.Generic;

namespace VTTRPG.Values
{
    public class FloatValue : Value<float>
    {
        public FloatValue() { }
        
        public FloatValue(float value)
        {
            this.value = value;
        }
    }
}
