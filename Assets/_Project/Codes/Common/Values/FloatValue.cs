using System.Collections;
using System.Collections.Generic;

namespace VTTRPG.Values
{
    public class FloatValue : Value<float>
    {    
        public FloatValue(string id, float value)
        {
            this.id = id;
            this.value = value;
        }
    }
}
