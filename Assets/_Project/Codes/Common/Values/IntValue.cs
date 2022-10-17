using System.Collections;
using System.Collections.Generic;

namespace VTTRPG.Values
{
    public class IntValue : Value<int>
    {
        public IntValue(string id, int value)
        {
            this.id = id;
            this.value = value;
        }
    }
}
