using System.Collections;
using System.Collections.Generic;

namespace ObjectValues.CollectionValues
{
    public abstract class NumberCollection<T, Q>
    {
        public List<T> value;

        public abstract Q Sum { get; }
    }
}
