using System.Collections;
using System.Collections.Generic;

namespace VTTRPG.Values
{
    public class IntValue : Value<int>
    {
        public IntValue() { }
        public IntValue(int value)
        {
            this.value = value;
        }
    }
}
