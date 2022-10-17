using System.Collections;
using System.Collections.Generic;

namespace VTTRPG.Values
{    
    public abstract class Value<T>
    {
        public string id;
        public T value;
    }    
}
